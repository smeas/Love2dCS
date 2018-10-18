// Author : endlesstravel
// this part make basical ability to use love2d

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using size_t = System.UInt32;
using int64 = System.Int64;

namespace Love
{

    static partial class DllTool
    {
        /// <summary>
        /// <para>from C# string[] pass as char** to c language</para>
        /// </summary>
        public static void ExecuteStringArray(string[] arrayToPass, Action<IntPtr[]> func)
        {
            GCHandle[] hObjects = new GCHandle[arrayToPass.Length];
            var pointers = new IntPtr[arrayToPass.Length];
            var textBytes = new byte[arrayToPass.Length][];
            for (int i = 0; i < arrayToPass.Length; i++)
            {
                textBytes[i] = ToUTF8Bytes(arrayToPass[i]);
                hObjects[i] = GCHandle.Alloc(textBytes[i], GCHandleType.Pinned);
                pointers[i] = hObjects[i].AddrOfPinnedObject();
            }

            func(pointers);

            foreach (var h in hObjects)
            {
                if (h.IsAllocated)
                    h.Free();
            }
        }

        public static void ExecuteAsIntprt(object obj, Action<IntPtr> func)
        {
            GCHandle handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
            try
            {
                IntPtr pointer = handle.AddrOfPinnedObject();
                func(pointer);
            }
            finally
            {
                if (handle.IsAllocated)
                {
                    handle.Free();
                }
            }
        }

        public static IntPtr[] GenIntPtrArray<T>(T[] loveObjArray) where T : LoveObject
        {
            IntPtr[] t = new IntPtr[loveObjArray.Length];
            for (int i = 0; i < loveObjArray.Length; i++)
            {
                t[i] = loveObjArray[i].p;
            }
            return t;
        }

        static byte[] EmptyStringByteArray = new byte[1] { 0 };
        public static byte[] ToUTF8Bytes(this string str)
        {
            return str != null ? utf8.GetBytes(str) : EmptyStringByteArray;
        }


        public static string GetLoveLastError()
        {
            // 这里不能直接调用 WSToString(Love2dDll.wrap_love_dll_last_error());
            // 否则可能导致无限递归
            IntPtr out_errormsg = IntPtr.Zero;
            Love2dDll.wrap_love_dll_last_error(out out_errormsg);
            WrapString ws = (WrapString)Marshal.PtrToStructure(out_errormsg, typeof(WrapString));
            return PtrToStringUTF8(ws.data);
        }

        public static Float3 ToFloat3(IntPtr ip)
        {
            Float3 f3 = (Float3)Marshal.PtrToStructure(ip, typeof(Float3));
            return f3;
        }

        public static Float6 ToFloat6(IntPtr ip)
        {
            Float6 f = (Float6)Marshal.PtrToStructure(ip, typeof(Float6));
            return f;
        }

        static System.Text.Encoding utf8 = System.Text.Encoding.GetEncoding("utf-8");
        public static string PtrToStringUTF8(IntPtr ip)
        {
            List<byte> list = new List<byte>();
            int offset = 0;
            byte b;
            while ((b = Marshal.ReadByte(ip, offset)) != 0)
            {
                list.Add(b);
                offset++;
            }
            return utf8.GetString(list.ToArray());
        }
        //public static string WSToStringAndRelease(IntPtr ip)
        //{
        //    WrapString ws = (WrapString)Marshal.PtrToStructure(ip, typeof(WrapString));
        //    // return PtrToStringUTF8(ws.data);
        //    var str = Marshal.PtrToStringAnsi(ws.data);
        //    Love2dDll.wrap_love_dll_delete_WrapString(ip);
        //    return str;
        //}
        public static string WSToStringAndRelease(IntPtr ip)
        {
            if (ip == IntPtr.Zero)
            {
                return null;
            }

            WrapString ws = (WrapString)Marshal.PtrToStructure(ip, typeof(WrapString));
            var str = PtrToStringUTF8(ws.data);
            Love2dDll.wrap_love_dll_delete_WrapString(ip);
            return str;
        }

        public static string[] WSSToStringListAndRelease(IntPtr ip)
        {
            WrapSequenceString ws = (WrapSequenceString)Marshal.PtrToStructure(ip, typeof(WrapSequenceString));
            string[] buffer = new string[ws.len];

            for (int i = 0; i < ws.len; i++)
            {
                string item = PtrToStringUTF8(Marshal.ReadIntPtr(ws.data, IntPtr.Size * i));
                buffer[i] = item;
            }

            Love2dDll.wrap_love_dll_delete_WrapSequenceString(ip);
            return buffer;
        }

        public static byte[] readBytesAndRelease(IntPtr p, long size)
        {
            byte[] buffer = new byte[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = Marshal.ReadByte(p, i);
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static IntPtr[] readIntPtrsAndRelease(IntPtr p, long size)
        {
            IntPtr[] buffer = new IntPtr[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = Marshal.ReadIntPtr(p, i);
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static T[] readIntPtrsWithConvertAndRelease<T>(IntPtr p, long size) where T : LoveObject
        {
            IntPtr[] buffer = new IntPtr[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = Marshal.ReadIntPtr(p, i);
            }

            Love2dDll.wrap_love_dll_delete_array(p);

            T[] tarray = new T[buffer.Length];
            for (int i = 0; i < size; i++)
            {
                tarray[i] = LoveObject.NewObject<T>(buffer[i]);
            }

            return tarray;
        }

        public static int[] readInt32sAndRelease(IntPtr p, long size)
        {
            int[] buffer = new int[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = Marshal.ReadInt32(p, i);
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static uint[] readUInt32sAndRelease(IntPtr p, int size)
        {
            uint[] buffer = new uint[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(uint)) * size);
                buffer[i] = (uint)Marshal.PtrToStructure(offset, typeof(uint));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }


        public static long[] readLongsAndRelease(IntPtr p, int size)
        {
            long[] buffer = new long[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(long)) * size);
                buffer[i] = (long)Marshal.PtrToStructure(offset, typeof(long));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static double[] readDoublesAndRelease(IntPtr p, int size)
        {
            double[] buffer = new double[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(double)) * size);
                buffer[i] = (double)Marshal.PtrToStructure(offset, typeof(double));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }


        public static Int4[] readInt4sAndRelease(IntPtr p, int size)
        {
            Int4[] buffer = new Int4[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(Int4)) * size);
                buffer[i] = (Int4)Marshal.PtrToStructure(offset, typeof(Int4));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static Float4[] readFloat4sAndRelease(IntPtr p, int size)
        {
            Float4[] buffer = new Float4[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(Float4)) * size);
                buffer[i] = (Float4)Marshal.PtrToStructure(offset, typeof(Float4));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static Int2[] readInt2sAndRelease(IntPtr p, int size)
        {
            Int2[] buffer = new Int2[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(Int2)) * size);
                buffer[i] = (Int2)Marshal.PtrToStructure(offset, typeof(Int2));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }


        public static Float2[] readFloat2sAndRelease(IntPtr p, int size)
        {
            Float2[] buffer = new Float2[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(Float2)) * size);
                buffer[i] = (Float2)Marshal.PtrToStructure(offset, typeof(Float2));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }


        public static float[] readFloatsAndRelease(IntPtr p, int size)
        {
            float[] buffer = new float[size];
            Marshal.Copy(p, buffer, 0, size);
            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }
    }

    /// <summary>
    /// <para></para>
    /// <para></para>
    /// </summary>
    public partial class Common
    {
        /// <summary>
        /// Gets the current running version of LÖVE.
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_common_getVersion(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// Gets the current running version code of LÖVE.
        /// </summary>
        /// <returns></returns>
        public static string GetVersionCodeName()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_common_getVersionCodeName(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

    }

    /// <summary>
    /// <para>提供高精度计时功能。</para>
    /// <para>Provides high-resolution timing functionality.</para>
    /// </summary>
    public partial class Timer
    {
        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_timer_open_timer();
        }

        /// <summary>
        /// Measures the time between two frames.
        /// </summary>
        public static void Step()
        {
            Love2dDll.wrap_love_dll_timer_step();
        }

        /// <summary>
        /// Returns the time between the last two frames.
        /// </summary>
        /// <returns></returns>
        public static float GetDelta()
        {
            float out_delta = 0;
            Love2dDll.wrap_love_dll_timer_getDelta(out out_delta);
            return out_delta;
        }

        /// <summary>
        /// Returns the current frames per second.
        /// </summary>
        /// <returns></returns>
        public static float GetFPS()
        {
            float out_fps = 0;
            Love2dDll.wrap_love_dll_timer_getFPS(out out_fps);
            return out_fps;
        }

        /// <summary>
        /// Returns the average delta time over the last second.
        /// </summary>
        /// <returns></returns>
        public static float GetAverageDelta()
        {
            float out_averageDelta = 0;
            Love2dDll.wrap_love_dll_timer_getAverageDelta(out out_averageDelta);
            return out_averageDelta;
        }

        /// <summary>
        /// Pauses the current thread for the specified amount of time.	
        /// </summary>
        /// <param name="t"></param>
        public static void Sleep(float t)
        {
            Love2dDll.wrap_love_dll_timer_sleep(t);
        }

        /// <summary>
        /// Returns the amount of time since some time in the past.		
        /// </summary>
        /// <returns></returns>
        public static float GetTime()
        {
            float out_time = 0;
            Love2dDll.wrap_love_dll_timer_getTime(out out_time);
            return out_time;
        }
    }

    /// <summary>
    /// <para>用于修改窗口和获取窗口信息。</para>
    /// <para>Provides an interface for modifying and retrieving information about the program's window.</para>
    /// </summary>
    public partial class Window
    {
        

        public static bool Init()
        {
            var res = Love2dDll.wrap_love_dll_windows_open_love_window();
            return res;
        }

        /// <summary>
        /// <para>获取当前显示器的数量。</para>
        /// Gets the number of connected monitors.
        /// </summary>
        /// <returns></returns>
        public static int GetDisplayCount()
        {
            int out_count = 0;
            Love2dDll.wrap_love_dll_windows_getDisplayCount(out out_count);
            return out_count;
        }

        /// <summary>
        /// <para>获取显示器的名称。</para>
        /// <para>Gets the name of a display.</para>
        /// </summary>
        /// <param name="displayindex"></param>
        /// <returns></returns>
        public static string GetDisplayName(int displayindex)
        {
            IntPtr out_name = IntPtr.Zero;
            Love2dDll.wrap_love_dll_windows_getDisplayName(displayindex, out out_name);
            return DllTool.WSToStringAndRelease(out_name);
        }

        /// <summary>
        /// <para>设置窗口的显示模式和属性。</para>
        /// <para>如果width或height为0，则setMode将使用桌面的宽度和高度。</para>
        /// <para>更改显示模式可能会产生副作用：例如，将清除 Canvas 并使用Shader：send发送到着色器的值将被删除。 如果需要，请务必事先保存 Canvas 的内容或之后重新绘制。</para>
        /// <para>Sets the display mode and properties of the window.</para>
        /// <para>If width or height is 0, setMode will use the width and height of the desktop.</para>
        /// <para>Changing the display mode may have side effects: for example, canvases will be cleared and values sent to shaders with Shader:send will be erased. Make sure to save the contents of canvases beforehand or re-draw to them afterward if you need to.</para>
        /// </summary>
        /// <param name="width">Display width.</param>
        /// <param name="height">Display height.</param>
        /// <param name="flag"></param>
        public static bool SetMode(int width, int height, WindowSettings flag = null)
        {
            if (flag == null)
            {
                return Love2dDll.wrap_love_dll_windows_setMode_w_h(width, height);
            }
            return Love2dDll.wrap_love_dll_windows_setMode_w_h_setting(
                width, height,
                flag.fullscreen, (int)flag.fullscreenType, flag.vsync,
                flag.msaa, flag.depth, flag.stencil, 
                flag.resizable, flag.minWidth, flag.minHeight, 
                flag.borderless, flag.centered, flag.display, 
                flag.highDpi, flag.refreshrate, flag.useposition, flag.x, flag.y);
        }


        /// <summary>
        /// <para>设置窗口的显示模式和属性。</para>
        /// <para>如果width或height为0，则setMode将使用桌面的宽度和高度。</para>
        /// <para>更改显示模式可能会产生副作用：例如，将清除 Canvas 并使用Shader：send发送到着色器的值将被删除。 如果需要，请务必事先保存 Canvas 的内容或之后重新绘制。</para>
        /// <para>Sets the display mode and properties of the window.</para>
        /// <para>If width or height is 0, setMode will use the width and height of the desktop.</para>
        /// <para>Changing the display mode may have side effects: for example, canvases will be cleared and values sent to shaders with Shader:send will be erased. Make sure to save the contents of canvases beforehand or re-draw to them afterward if you need to.</para>
        /// </summary>
        /// <param name="flag"></param>
        public static bool SetMode(WindowSettings flag = null)
        {
            int w, h;
            GetMode(out w, out h);
            return SetMode(w, h, flag);
        }

        /// <summary>
        /// Gets the display mode and properties of the window.
        /// </summary>
        /// <returns></returns>
        public static WindowSettings GetMode()
        {
            int t;
            return GetMode(out t, out t);
        }

        /// <summary>
        /// Gets the display mode and properties of the window.
        /// </summary>
        /// <param name="out_width">Window width.</param>
        /// <param name="out_height">Window height.</param>
        /// <returns></returns>
        public static WindowSettings GetMode(out int out_width, out int out_height)
        {
            WindowSettings flag = new WindowSettings();

            int fullscreenType = 0;

            Love2dDll.wrap_love_dll_windows_getMode(
                out out_width, out out_height, 
                out flag.fullscreen, out fullscreenType, out flag.vsync, 
                out flag.msaa, out flag.depth, out flag.stencil,
                out flag.resizable, out flag.minWidth,  out flag.minHeight,
                out flag.borderless, out flag.centered, out flag.display, 
                out flag.highDpi, out flag.refreshrate, out flag.useposition, out flag.x, out flag.y);

            flag.fullscreenType = (FullscreenType)fullscreenType;


            return flag;
        }

        

        /// <summary>
        /// Gets a list of supported fullscreen modes.
        /// </summary>
        /// <param name="displayindex">The index of the display, if multiple monitors are available.</param>
        /// <returns></returns>
        public static Int2[] GetFullscreenModes(int displayindex = 0)
        {
            IntPtr out_modes;
            int out_modes_length;
            Love2dDll.wrap_love_dll_windows_getFullscreenModes(displayindex, out out_modes, out out_modes_length);
            return DllTool.readInt2sAndRelease(out_modes, out_modes_length);
        }

        /// <summary>
        /// Enters or exits fullscreen. The display to use when entering fullscreen is chosen based on which display the window is currently in, if multiple monitors are connected.
        /// </summary>
        /// <param name="fullscreen">Whether to enter or exit fullscreen mode.</param>
        /// <param name="fstype">The type of fullscreen mode to use.</param>
        /// <returns>True if an attempt to enter fullscreen was successful, false otherwise.</returns>
        public static bool SetFullscreen(bool fullscreen, FullscreenType fstype = FullscreenType.Exclusive)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_windows_setFullscreen(fullscreen, (int)fstype, out out_success);
            return out_success;
        }

        /// <summary>
        /// Gets whether the window is fullscreen.
        /// </summary>
        /// <param name="out_fullscreen">True if the window is fullscreen, false otherwise.</param>
        /// <param name="out_fstype">The type of fullscreen mode used.</param>
        public static void GetFullscreen(out bool out_fullscreen, out FullscreenType out_fstype)
        {
            int t_out_fstype;
            Love2dDll.wrap_love_dll_windows_getFullscreen(out out_fullscreen, out t_out_fstype);
            out_fstype = (FullscreenType)t_out_fstype;
        }

        /// <summary>
        /// True if the window is open, false otherwise.
        /// </summary>
        /// <returns></returns>
        public static bool IsOpen()
        {
            bool out_isopen = false;
            Love2dDll.wrap_love_dll_windows_isOpen(out out_isopen);
            return out_isopen;
        }

        /// <summary>
        /// Closes the window. It can be reopened with love.window.setMode.
        /// <para>love.graphics functions and objects will cause a hard crash of LÖVE if used while the window is closed.</para>
        /// </summary>
        public static void Close()
        {
            Love2dDll.wrap_love_dll_windows_close();
        }

        /// <summary>
        /// Gets the width and height of the desktop.
        /// </summary>
        /// <param name="displayIndex">The index of the display, if multiple monitors are available.</param>
        public static Int2 GetDesktopDimensions(int displayIndex = 0)
        {
            int out_width, out_height;
            Love2dDll.wrap_love_dll_windows_getDesktopDimensions(displayIndex, out out_width, out out_height);
            return new Int2(out_width, out_height);
        }

        /// <summary>
        /// Sets the position of the window on the screen.
        /// </summary>
        /// <param name="x">The x-coordinate of the window's position.</param>
        /// <param name="y">The y-coordinate of the window's position.</param>
        /// <param name="displayindex">The window position is in the coordinate space of the specified display.</param>
        public static void SetPosition(int x, int y, int displayindex = 0)
        {
            Love2dDll.wrap_love_dll_windows_setPosition(x, y, displayindex);
        }

        /// <summary>
        /// Gets the position of the window on the screen
        /// </summary>
        /// <returns></returns>
        public static Int2 GetPosition()
        {
            int out_x, out_y, out_displayindex;
            Love2dDll.wrap_love_dll_windows_getPosition(out out_x, out out_y, out out_displayindex);
            return new Int2(out_x, out_y);
        }

        /// <summary>
        /// Gets the position of the window on the screen.
        /// And the index of the display that the window is in.
        /// </summary>
        /// <param name="out_displayindex"></param>
        /// <returns></returns>
        public static Int2 GetPosition(out int out_displayindex)
        {
            int out_x, out_y;
            Love2dDll.wrap_love_dll_windows_getPosition(out out_x, out out_y, out out_displayindex);
            return new Int2(out_x, out_y);
        }

        /// <summary>
        /// Sets the window icon until the game is quit. Not all operating systems support very large icon images.
        /// </summary>
        /// <param name="imagedata">The window icon image.</param>
        /// <returns></returns>
        public static bool SetIcon(ImageData imagedata)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_windows_setIcon(imagedata.p, out out_success);
            return out_success;
        }

        /// <summary>
        /// The window icon imagedata, or nil if no icon has been set with love.window.setIcon.
        /// </summary>
        /// <returns></returns>
        public static ImageData GetIcon()
        {
            IntPtr out_imagedata = IntPtr.Zero;
            Love2dDll.wrap_love_dll_windows_getIcon(out out_imagedata);
            return LoveObject.NewObject<ImageData>(out_imagedata);
        }

        /// <summary>
        /// Sets whether the display is allowed to sleep while the program is running.
        /// <para>Display sleep is disabled by default. Some types of input(e.g.joystick button presses) might not prevent the display from sleeping, if display sleep is allowed.</para>        
        /// </summary>
        /// <param name="enable">True to enable system display sleep, false to disable it.</param>
        public static void SetDisplaySleepEnabled(bool enable)
        {
            Love2dDll.wrap_love_dll_windows_setDisplaySleepEnabled(enable);
        }

        /// <summary>
        /// Gets whether the display is allowed to sleep while the program is running.
        /// <para>Display sleep is disabled by default. Some types of input (e.g. joystick button presses) might not prevent the display from sleeping, if display sleep is allowed.</para>
        /// </summary>
        /// <returns></returns>
        public static bool IsDisplaySleepEnabled()
        {
            bool out_enable = false;
            Love2dDll.wrap_love_dll_windows_isDisplaySleepEnabled(out out_enable);
            return out_enable;
        }

        /// <summary>
        /// Sets the window title. (UTF-8 byte array version)
        /// <para>Constantly updating the window title can lead to issues on some systems and therefore is discouraged.</para>
        /// </summary>
        /// <param name="titleStr">The new window title.(UTF-8 byte array)</param>
        public static void SetTitle(byte[] titleStr)
        {
            Love2dDll.wrap_love_dll_windows_setTitle(titleStr);
        }

        /// <summary>
        /// Gets the window title.
        /// </summary>
        /// <returns></returns>
        public static string GetTitle()
        {
            IntPtr out_title = IntPtr.Zero;
            Love2dDll.wrap_love_dll_windows_getTitle(out out_title);
            return DllTool.WSToStringAndRelease(out_title);
        }

        /// <summary>
        /// Checks if the game window has keyboard focus.
        /// </summary>
        /// <returns>True if the window has the focus or false if not.</returns>
        public static bool HasFocus()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_hasFocus(out out_result);
            return out_result;
        }

        /// <summary>
        /// Checks if the game window has mouse focus.
        /// </summary>
        /// <returns>True if the window has mouse focus or false if not.</returns>
        public static bool HasMouseFocus()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_hasMouseFocus(out out_result);
            return out_result;
        }

        /// <summary>
        /// Checks if the game window is visible.
        /// <para>The window is considered visible if it's not minimized and the program isn't hidden.</para>
        /// </summary>
        /// <returns></returns>
        public static bool IsVisible()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_isVisible(out out_result);
            return out_result;
        }

        /// <summary>
        /// <para>Gets the DPI scale factor associated with the window.</para>
        /// <para>The pixel density inside the window might be greater (or smaller) than the "size" of the window. For example on a retina screen in Mac OS X with the highdpi window flag enabled, the window may take up the same physical size as an 800x600 window, but the area inside the window uses 1600x1200 pixels. love.window.getDPIScale() would return 2.0 in that case.</para>
        /// <para>The love.window.fromPixels and love.window.toPixels functions can also be used to convert between units.</para>
        /// <para>The highdpi window flag must be enabled to use the full pixel density of a Retina screen on Mac OS X and iOS. The flag currently does nothing on Windows and Linux, and on Android it is effectively always enabled.</para>
        /// </summary>
        /// <returns>The pixel scale factor associated with the window.</returns>
        public static double GetDPIScale()
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_windows_getDPIScale(out out_result);
            return out_result;
        }

        /// <summary>
        /// <para>Converts a number from density-independent units to pixels.</para>
        /// <para>The pixel density inside the window might be greater (or smaller) than the "size" of the window. For example on a retina screen in Mac OS X with the highdpi window flag enabled, the window may take up the same physical size as an 800x600 window, but the area inside the window uses 1600x1200 pixels. love.window.getDPIScale() would return 2.0 in that case.</para>
        /// <para>The love.window.fromPixels and love.window.toPixels functions can also be used to convert between units.</para>
        /// <para>The highdpi window flag must be enabled to use the full pixel density of a Retina screen on Mac OS X and iOS. The flag currently does nothing on Windows and Linux, and on Android it is effectively always enabled.</para>
        /// <para>Most LÖVE functions return values and expect arguments in terms of pixels rather than density-independent units.</para>
        /// </summary>
        /// <param name="value">A number in density-independent units to convert to pixels.</param>
        /// <returns>The converted number, in pixels.</returns>
        public static double ToPixels(double value)
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_windows_toPixels(value, out out_result);
            return out_result;
        }

        /// <summary>
        /// <para>Converts a number from pixels to density-independent units.</para>
        /// <para>The pixel density inside the window might be greater (or smaller) than the "size" of the window. For example on a retina screen in Mac OS X with the highdpi window flag enabled, the window may take up the same physical size as an 800x600 window, but the area inside the window uses 1600x1200 pixels. love.window.getDPIScale() would return 2.0 in that case.</para>
        /// <para>The love.window.fromPixels and love.window.toPixels functions can also be used to convert between units.</para>
        /// <para>The highdpi window flag must be enabled to use the full pixel density of a Retina screen on Mac OS X and iOS. The flag currently does nothing on Windows and Linux, and on Android it is effectively always enabled.</para>
        /// <para>Most LÖVE functions return values and expect arguments in terms of pixels rather than density-independent units.</para>
        /// </summary>
        /// <param name="pixelvalue">A number in pixels to convert to density-independent units.</param>
        /// <returns>The converted number, in density-independent units.</returns>
        public static double FromPixels(double pixelvalue)
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_windows_fromPixels(pixelvalue, out out_result);
            return out_result;
        }

        /// <summary>
        /// <para>Minimizes the window to the system's task bar / dock.</para>
        /// </summary>
        public static void Minimize()
        {
            Love2dDll.wrap_love_dll_windows_minimize();
        }

        /// <summary>
        /// <para>Makes the window as large as possible.</para>
        /// <para>This function has no effect if the window isn't resizable, since it essentially programmatically presses the window's "maximize" button.</para>
        /// </summary>
        public static void Maximize()
        {
            Love2dDll.wrap_love_dll_windows_maximize();
        }

        /// <summary>
        /// <para>Gets whether the Window is currently maximized.</para>
        /// <para>The window can be maximized if it is not fullscreen and is resizable, and either the user has pressed the window's Maximize button or love.window.maximize has been called.</para>
        /// </summary>
        /// <returns>True if the window is currently maximized in windowed mode, false otherwise.</returns>
        public static bool IsMaximized()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_isMaximized(out out_result);
            return out_result;
        }

        /// <summary>
        /// Displays a simple message box with a single 'OK' button. (UTF-8 version)
        /// <para>	This function will pause all execution of the main thread until the user has clicked a button to exit the message box. Calling the function from a different thread may cause love to crash.</para>
        /// </summary>
        /// <param name="title">The title of the message box. (UTF-8 byte array)</param>
        /// <param name="message">The text inside the message box. (UTF-8 byte array)</param>
        /// <param name="msgbox_type">The type of the message box.</param>
        /// <param name="attachToWindow">Whether the message box should be attached to the love window or free-floating.</param>
        /// <returns>Whether the message box was successfully displayed.</returns>
        public static bool ShowMessageBox(byte[] title, byte[] message, MessageBoxType msgbox_type, bool attachToWindow = true)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_showMessageBox(title, message, (int)msgbox_type, attachToWindow, out out_result);
            return out_result;
        }

        /// <summary>
        /// Displays a message box with a customized list of buttons.
        /// <para>	This function will pause all execution of the main thread until the user has clicked a button to exit the message box. Calling the function from a different thread may cause love to crash.</para>
        /// </summary>
        /// <param name="title">The title of the message box.</param>
        /// <param name="message">The text inside the message box.</param>
        /// <param name="buttonName"></param>
        /// <param name="enterButtonIndex"> when the user presses 'enter', which button index should be return </param>
        /// <param name="escapebuttonIndex"> when the user presses 'escapeb', which button index should be return </param>
        /// <param name="msgbox_type">The type of the message box.</param>
        /// <param name="attachToWindow">Whether the message box should be attached to the love window or free-floating.</param>
        /// <returns>The index of the button pressed by the user. May be 0 if the message box dialog was closed without pressing a button.</returns>
        public static int ShowMessageBox(string title, string message, string[] buttonName, int enterButtonIndex, int escapebuttonIndex, MessageBoxType msgbox_type = MessageBoxType.Info, bool attachToWindow = true)
        {
            int out_index_returned = 0;
            DllTool.ExecuteStringArray(buttonName, (temp) => {
                Love2dDll.wrap_love_dll_windows_showMessageBox_list(
                    DllTool.ToUTF8Bytes(title), DllTool.ToUTF8Bytes(message),
                    temp, temp.Length, enterButtonIndex, escapebuttonIndex,
                    (int)msgbox_type, attachToWindow, out out_index_returned);
            });
            return out_index_returned;
        }

        /// <summary>
        /// Causes the window to request the attention of the user if it is not in the foreground.
        /// <para>In Windows the taskbar icon will flash, and in OS X the dock icon will bounce.</para>
        /// </summary>
        /// <param name="continuous"></param>
        public static void RequestAttention(bool continuous = false)
        {
            Love2dDll.wrap_love_dll_windows_requestAttention(continuous);
        }

        //public static void WindowToPixelCoords(out double out_x, out double out_y)
        //{
        //    Love2dDll.wrap_love_dll_windows_windowToPixelCoords(out out_x, out out_y);
        //}
        //public static void PixelToWindowCoords(out double x, out double y)
        //{
        //    Love2dDll.wrap_love_dll_windows_pixelToWindowCoords(out x, out y);
        //}
    }

    /// <summary>
    /// Provides an interface to the user's mouse.
    /// </summary>
    public partial class Mouse
    {
        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_open_love_mouse();
        }

        /// <summary>
        /// <para>Creates a new hardware Cursor object from an image file or ImageData.</para>
        /// <para>Hardware cursors are framerate-independent and work the same way as normal operating system cursors. Unlike drawing an image at the mouse's current coordinates, hardware cursors never have visible lag between when the mouse is moved and when the cursor position updates, even at low framerates.</para>
        /// <para>The hot spot is the point the operating system uses to determine what was clicked and at what position the mouse cursor is. For example, the normal arrow pointer normally has its hot spot at the top left of the image, but a crosshair cursor might have it in the middle.</para>
        /// </summary>
        /// <param name="imageData">The ImageData to use for the new Cursor.</param>
        /// <param name="hotX">The x-coordinate in the image of the cursor's hot spot.</param>
        /// <param name="hotY">The y-coordinate in the image of the cursor's hot spot.</param>
        /// <returns></returns>
        public static Cursor NewCursor(ImageData imageData, int hotX, int hotY)
        {
            IntPtr out_cursor;
            Love2dDll.wrap_love_dll_mouse_newCursor(imageData.p, hotX, hotY, out out_cursor);
            return LoveObject.NewObject<Cursor>(out_cursor);
        }

        /// <summary>
        /// <para> Gets a Cursor object representing a system-native hardware cursor. </para>
        /// <para>Hardware cursors are framerate-independent and work the same way as normal operating system cursors. Unlike drawing an image at the mouse's current coordinates, hardware cursors never have visible lag between when the mouse is moved and when the cursor position updates, even at low framerates.</para>
        /// </summary>
        /// <param name="sysctype"></param>
        /// <returns></returns>
        public static Cursor GetSystemCursor(SystemCursor sysctype)
        {
            IntPtr out_cursor = IntPtr.Zero;
            Love2dDll.wrap_love_dll_mouse_getSystemCursor((int)sysctype, out out_cursor);
            return LoveObject.NewObject<Cursor>(out_cursor);
        }

        /// <summary>
        /// Sets the current mouse cursor.
        /// </summary>
        /// <param name="cursor"></param>
        public static void SetCursor(Cursor cursor = null)
        {
            Love2dDll.wrap_love_dll_mouse_setCursor(cursor != null ? cursor.p : IntPtr.Zero);
        }

        /// <summary>
        /// Gets the current Cursor.
        /// </summary>
        /// <returns></returns>
        public static Cursor GetCursor()
        {
            IntPtr out_cursor = IntPtr.Zero;
            Love2dDll.wrap_love_dll_mouse_getCursor(out out_cursor);
            return LoveObject.NewObject<Cursor>(out_cursor);
        }

        /// <summary>
        /// Gets whether cursor functionality is supported.
        /// </summary>
        /// <returns></returns>
        public static bool IsCursorSupported()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isCursorSupported(out out_result);
            return out_result;
        }

        /// <summary>
        /// Returns the current x-position of the mouse.
        /// </summary>
        /// <returns></returns>
        public static double GetXDouble()
        {
            double out_x = 0;
            Love2dDll.wrap_love_dll_mouse_getX(out out_x);
            return out_x;
        }

        /// <summary>
        /// Returns the current y-position of the mouse.
        /// </summary>
        /// <returns></returns>
        public static double GetYDouble()
        {
            double out_y = 0;
            Love2dDll.wrap_love_dll_mouse_getY(out out_y);
            return out_y;
        }

        /// <summary>
        /// Returns the current x-position of the mouse.
        /// </summary>
        /// <returns></returns>
        public static float GetX()
        {
            return (float)GetXDouble();
        }

        /// <summary>
        /// Returns the current y-position of the mouse.
        /// </summary>
        /// <returns></returns>
        public static float GetY()
        {
            return (float)GetYDouble();
        }


        /// <summary>
        /// Returns the current x/y-position of the mouse.
        /// </summary>
        /// <returns></returns>
        public static void GetPosition(out double out_x, out double out_y)
        {
            Love2dDll.wrap_love_dll_mouse_getPosition(out out_x, out out_y);
        }

        /// <summary>
        /// Sets the current X position of the mouse.
        /// </summary>
        /// <param name="x"></param>
        public static void SetX(double x)
        {
            Love2dDll.wrap_love_dll_mouse_setX(x);
        }

        /// <summary>
        /// Sets the current Y position of the mouse.
        /// </summary>
        /// <param name="y"></param>
        public static void SetY(double y)
        {
            Love2dDll.wrap_love_dll_mouse_setY(y);
        }


        /// <summary>
        /// Sets the current X/Y position of the mouse.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetPosition(double x, double y)
        {
            Love2dDll.wrap_love_dll_mouse_setPosition(x, y);
        }

        /// <summary>
        /// Checks whether a certain button is down.
        /// <para>This function does not detect mouse wheel scrolling; you must use the love.wheelmoved (or love.mousepressed in version 0.9.2 and older) callback for that.</para>
        /// </summary>
        /// <param name="buttonIndex">The index of a button to check. 1 is the primary mouse button, 2 is the secondary mouse button and 3 is the middle button. Further buttons are mouse dependant.</param>
        /// <returns>True if any specified button is down.</returns>
        public static bool IsDown(int buttonIndex)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isDown(buttonIndex, out out_result);
            return out_result;
        }

        /// <summary>
        /// Sets the current visibility of the cursor.
        /// </summary>
        /// <param name="visible"></param>
        public static void SetVisible(bool visible)
        {
            Love2dDll.wrap_love_dll_mouse_setVisible(visible);
        }

        /// <summary>
        /// Checks if the cursor is visible.
        /// </summary>
        /// <returns></returns>
        public static bool IsVisible()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isVisible(out out_result);
            return out_result;
        }

        /// <summary>
        /// Grabs the mouse and confines it to the window.
        /// </summary>
        /// <param name="grabbed"></param>
        public static void SetGrabbed(bool grabbed)
        {
            Love2dDll.wrap_love_dll_mouse_setGrabbed(grabbed);
        }

        /// <summary>
        /// Checks if the mouse is grabbed.
        /// </summary>
        /// <returns></returns>
        public static bool IsGrabbed()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isGrabbed(out out_result);
            return out_result;
        }

        /// <summary>
        /// <para>WARNNING: THIS API NOT STABLE, IT WILL CRASHED! </para>
        /// <para>Sets whether relative mode is enabled for the mouse. </para>
        /// <para>When relative mode is enabled, the cursor is hidden and doesn't move when the mouse does, but relative mouse motion events are still generated via love.mousemoved. This lets the mouse move in any direction indefinitely without the cursor getting stuck at the edges of the screen.</para>
        /// <para>The reported position of the mouse may not be updated while relative mode is enabled, even when relative mouse motion events are generated.</para>
        /// </summary>
        /// <param name="enable">True to enable relative mode, false to disable it.</param>
        /// <returns></returns>
        public static bool SetRelativeMode(bool enable)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_setRelativeMode(enable, out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets whether relative mode is enabled for the mouse.
        /// </summary>
        /// <returns>True if relative mode is enabled, false if it's disabled.</returns>
        public static bool GetRelativeMode()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_getRelativeMode(out out_result);
            return out_result;
        }
    }

    public partial class Keyboard
    {
        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_keyboard_open_love_keyboard();
        }

        /// <summary>
        /// Enables or disables key repeat for love.keypressed. It is disabled by default.
        /// <para>The interval between repeats depends on the user's system settings. This function doesn't affect whether <see cref="Scene.TextInput"/> is called multiple times while a key is held down</para>
        /// </summary>
        /// <param name="enable">Whether repeat keypress events should be enabled when a key is held down.</param>
        public static void SetKeyRepeat(bool enable)
        {
            Love2dDll.wrap_love_dll_keyboard_setKeyRepeat(enable);
        }

        /// <summary>
        /// Gets whether key repeat is enabled.
        /// </summary>
        /// <returns></returns>
        public static bool HasKeyRepeat()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_hasKeyRepeat(out out_result);
            return out_result;
        }

        /// <summary>
        /// Checks whether a certain <see cref="KeyConstant"/> is down. Not to be confused with <see cref="Scene.KeyPressed(KeyConstant, Scancode, bool)"/> or <see cref="Scene.KeyReleased(KeyConstant, Scancode)"/>.
        /// </summary>
        /// <param name="key_type">The key to check.</param>
        /// <returns>True if the key is down, false if not.</returns>
        public static bool IsDown(KeyConstant key_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_isDown((int)key_type, out out_result);
            return out_result;
        }

        /// <summary>
        /// Checks whether a certain <see cref="Scancode"/> is down. Not to be confused with <see cref="Scene.KeyPressed(KeyConstant, Scancode, bool)"/> or <see cref="Scene.KeyReleased(KeyConstant, Scancode)"/>.
        /// <para>Unlike regular KeyConstants, Scancodes are keyboard layout-independent. The scancode "w" is used if the key in the same place as the "w" key on an American keyboard is pressed, no matter what the key is labelled or what the user's operating system settings are.</para>
        /// </summary>
        /// <param name="scancode_type"></param>
        /// <returns></returns>
        public static bool IsScancodeDown(Scancode scancode_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_isScancodeDown((int)scancode_type, out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets the hardware scancode corresponding to the given key.
        /// <para>Unlike <see cref="KeyConstant"/>, <see cref="Scancode"/> are keyboard layout-independent. For example the scancode "w" will be generated if the key in the same place as the "w" key on an American keyboard is pressed, no matter what the key is labelled or what the user's operating system settings are.</para>
        /// <para><see cref="Scancode"/> are useful for creating default controls that have the same physical locations on on all systems.</para>
        /// </summary>
        /// <param name="key_type">The key to get the scancode from.</param>
        /// <returns>The scancode corresponding to the given key, or "unknown" if the given key has no known physical representation on the current system.</returns>
        public static Scancode GetScancodeFromKey(KeyConstant key_type)
        {
            int out_scancode_type = 0;
            Love2dDll.wrap_love_dll_keyboard_getScancodeFromKey((int)key_type, out out_scancode_type);
            return (Scancode)out_scancode_type;
        }

        /// <summary>
        /// <para>Gets the key corresponding to the given hardware scancode.</para>
        /// <para>Unlike <see cref="KeyConstant"/>, <see cref="Scancode"/> are keyboard layout-independent. For example the scancode "w" will be generated if the key in the same place as the "w" key on an American keyboard is pressed, no matter what the key is labelled or what the user's operating system settings are.</para>
        /// <para><see cref="Scancode"/> are useful for creating default controls that have the same physical locations on on all systems.</para>
        /// </summary>
        /// <param name="scancode_type">The scancode to get the key from.</param>
        /// <returns>The key corresponding to the given <see cref="Scancode"/> , or "unknown" if the <see cref="Scancode"/> doesn't map to a KeyConstant on the current system.</returns>
        public static KeyConstant GetKeyFromScancode(Scancode scancode_type)
        {
            int out_key_type = 0;
            Love2dDll.wrap_love_dll_keyboard_getKeyFromScancode((int)scancode_type, out out_key_type);
            return (KeyConstant)out_key_type;
        }

        /// <summary>
        /// <para>Enables or disables text input events. It is enabled by default on Windows, Mac, and Linux, and disabled by default on iOS and Android.</para>
        /// <para>On touch devices, this shows the system's native on-screen keyboard when it's enabled.</para>
        /// </summary>
        /// <param name="enable">Whether text input events should be enabled.</param>
        public static void SetTextInput(bool enable)
        {
            Love2dDll.wrap_love_dll_keyboard_setTextInput(enable);
        }

        /// <summary>
        /// <para>Enables or disables text input events. It is enabled by default on Windows, Mac, and Linux, and disabled by default on iOS and Android.</para>
        /// <para>On iOS and Android this variant tells the OS that the specified rectangle is where text will show up in the game, which prevents the system on-screen keyboard from covering the text.</para>
        /// <para>On touch devices, this shows the system's native on-screen keyboard when it's enabled.</para>
        /// </summary>
        /// <param name="enable">Whether text input events should be enabled.</param>
        /// <param name="x">Text rectangle x position.</param>
        /// <param name="y">Text rectangle y position.</param>
        /// <param name="w">Text rectangle width.</param>
        /// <param name="h">Text rectangle height.</param>
        public static void SetTextInput(bool enable, double x, double y, double w, double h)
        {
            Love2dDll.wrap_love_dll_keyboard_setTextInput_xywh(enable, x, y, w, h);
        }

        /// <summary>
        /// Gets whether key repeat is enabled.
        /// </summary>
        /// <returns>Whether key repeat is enabled.</returns>
        public static bool HasTextInput()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_hasTextInput(out out_result);
            return out_result;
        }


        public static bool HasScreenKeyboard()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_hasScreenKeyboard(out out_result);
            return out_result;
        }


    }

    public partial class Touch
    {
        public struct TouchInfo
        {
            public long index;
            public double x, y, dx, dy, pressure;
        }

        public static void Init()
        {
            Love2dDll.wrap_love_dll_touch_open_love_touch();
        }
        public static TouchInfo[] GetTouches()
        {
            IntPtr out_indexs;IntPtr out_x;IntPtr out_y;IntPtr out_dx;IntPtr out_dy;IntPtr out_pressure;int out_length;
            Love2dDll.wrap_love_dll_touch_open_love_getTouches(out out_indexs, out out_x, out out_y, out out_dx, out out_dy, out out_pressure, out out_length);

            var indexs = DllTool.readLongsAndRelease(out_indexs, out_length);
            var x = DllTool.readDoublesAndRelease(out_x, out_length);
            var y = DllTool.readDoublesAndRelease(out_y, out_length);
            var dx = DllTool.readDoublesAndRelease(out_dx, out_length);
            var dy = DllTool.readDoublesAndRelease(out_dy, out_length);
            var pressure = DllTool.readDoublesAndRelease(out_pressure, out_length);

            TouchInfo[] infos = new TouchInfo[out_length];
            for (int i = 0; i < out_length; i++)
            {
                infos[i].index = indexs[i];
                infos[i].x = x[i];
                infos[i].y = y[i];
                infos[i].dx = dx[i];
                infos[i].dy = dy[i];
                infos[i].pressure = pressure[i];
            }

            return infos;
        }
        public static void GetToucheInfo(long idx, out double out_x, out double out_y, out double out_dx, out double out_dy, out double out_pressure)
        {
            Love2dDll.wrap_love_dll_touch_open_love_getToucheInfo(idx, out out_x, out out_y, out out_dx, out out_dy, out out_pressure);
        }

    }

    public partial class Joystick
    {
        public static void Init()
        {
            Love2dDll.wrap_love_dll_joystick_open_love_joystick();
        }
        public Joystick[] GetJoysticks()
        {
            IntPtr out_sticks;
            int out_sticks_lenght;
            Love2dDll.wrap_love_dll_joystick_getJoysticks(out out_sticks, out out_sticks_lenght);

            return DllTool.readIntPtrsWithConvertAndRelease<Joystick>(out_sticks, out_sticks_lenght);
        }
        public static int GetIndex(Joystick j)
        {
            int out_index = 0;
            Love2dDll.wrap_love_dll_joystick_getIndex(j.p, out out_index);
            return out_index;
        }
        public static int GetJoystickCount()
        {
            int out_sticks_lenght = 0;
            Love2dDll.wrap_love_dll_joystick_getJoystickCount(out out_sticks_lenght);
            return out_sticks_lenght;
        }
        public static bool SetGamepadMapping(byte[] guid, Joystick.InputType gp_inputType_type, Joystick.InputType j_inputType_type, int inputIndex, Joystick.Hat hat_type)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_joystick_setGamepadMapping(guid, (int)gp_inputType_type, (int)j_inputType_type, inputIndex, (int)hat_type, out out_success);
            return out_success;
        }
        public static bool LoadGamepadMappings(byte[] str)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_joystick_loadGamepadMappings(str, out out_success);
            return out_success;
        }
        public static string SaveGamepadMappings()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_joystick_saveGamepadMappings(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

    }

    /// <summary>
    /// <para>Manages events, like keypresses.</para>
    /// </summary>
    public partial class Event
    {
        private enum WrapEventType
        {
            WRAP_EVENT_TYPE_UNKNOW,

            WRAP_EVENT_TYPE_KEY,
            WRAP_EVENT_TYPE_MOUSE_BUTTON,
            WRAP_EVENT_TYPE_MOUSE_MOTION,
            WRAP_EVENT_TYPE_MOUSE_WHEEL,

            WRAP_EVENT_TYPE_TOUCH_MOVED,
            WRAP_EVENT_TYPE_TOUCH_PRESSED,
            WRAP_EVENT_TYPE_TOUCH_RELEASED,

            WRAP_EVENT_TYPE_JOYSTICK_BUTTON,
            WRAP_EVENT_TYPE_JOYSTICK_AXIS_MOTION,
            WRAP_EVENT_TYPE_JOYSTICK_HAT_MOTION,
            WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_BUTTON,
            WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_AXIS_MOTION,
            WRAP_EVENT_TYPE_JOYSTICK_DEVICE_ADDED_OR_REMOVED,

            WRAP_EVENT_TYPE_TEXTINPUT,
            WRAP_EVENT_TYPE_TEXTEDITING,

            WRAP_EVENT_TYPE_WINDOW_VISIBLE,
            WRAP_EVENT_TYPE_WINDOW_ENTER_OR_LEAVE,
            WRAP_EVENT_TYPE_WINDOW_SHOWN_OR_HIDDEN,
            WRAP_EVENT_TYPE_WINDOW_RESIZED,

            WRAP_EVENT_TYPE_FILE_DROPPED,
            WRAP_EVENT_TYPE_DIRECTORY_DROPPED,

            WRAP_EVENT_TYPE_LOWMEMORY,
            WRAP_EVENT_TYPE_QUIT,
        };

        /// <summary>
        /// Exits the LÖVE program.
        /// <para>Adds the quit event to the queue.</para>
        /// <para>The quit event is a signal for the event handler to close LÖVE. It's possible to abort the exit process with the love.quit callback.</para>
        /// </summary>
        /// <param name="exitStatus">The program exit status to use when closing the application.</param>
        public static void Quit(int exitStatus = 0)
        {
            Environment.Exit(exitStatus);
        }

        public static void Init()
        {
            Love2dDll.wrap_love_dll_event_open_love_event();
        }

        public static void PollOrWait(bool isPoll, out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out string out_string, out Int4 out_int4, out Float4 out_float4, out float out_float_value, out Joystick out_joystick)
        {
            IntPtr out_str;
            IntPtr out_joystick_ptr;


            if (isPoll)
                Love2dDll.wrap_love_dll_event_poll(out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_str, out out_int4, out out_float4, out out_float_value, out out_joystick_ptr);
            else
                Love2dDll.wrap_love_dll_event_wait(out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_str, out out_int4, out out_float4, out out_float_value, out out_joystick_ptr);


            out_string = DllTool.WSToStringAndRelease(out_str);
            out_joystick = LoveObject.NewObject<Joystick>(out_joystick_ptr);
        }

        private static void DoHandleEvent(Scene scene, int out_event_type, bool out_down_or_up, bool out_bool, int out_idx, int out_enum1_type, int out_enum2_type, string out_string, Int4 out_int4, Float4 out_float4, float out_float_value, Joystick out_joystick)
        {
            switch ((WrapEventType)out_event_type)
            {
                case WrapEventType.WRAP_EVENT_TYPE_UNKNOW: { } break;
                case WrapEventType.WRAP_EVENT_TYPE_KEY:
                    {
                        if (out_down_or_up) scene.KeyPressed((KeyConstant)out_enum1_type, (Scancode)out_enum2_type, out_bool);
                        else scene.KeyReleased((KeyConstant)out_enum1_type, (Scancode)out_enum2_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_BUTTON:
                    {
                        if (out_down_or_up) scene.MousePressed(out_float4.x, out_float4.y, out_idx, out_bool);
                        else scene.MouseReleased(out_float4.x, out_float4.y, out_idx, out_bool);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_MOTION:
                    {
                        scene.MouseMoved(out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_bool);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_WHEEL:
                    {
                        scene.WheelMoved(out_int4.x, out_int4.y);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_MOVED:
                    {
                        scene.TouchMoved(out_idx, out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_PRESSED:
                    {
                        scene.TouchPressed(out_idx, out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_RELEASED:
                    {
                        scene.TouchReleased(out_idx, out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_float_value);
                    }
                    break;

                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_BUTTON:
                    {
                        if (out_down_or_up) scene.JoystickPressed(out_joystick, out_idx);
                        else scene.JoystickReleased(out_joystick, out_idx);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_AXIS_MOTION:
                    {
                        scene.JoystickAxis(out_joystick, out_idx, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_HAT_MOTION:
                    {
                        scene.JoystickHat(out_joystick, out_idx, (Joystick.Hat)out_enum1_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_BUTTON:
                    {
                        if (out_down_or_up) scene.JoystickGamepadPressed(out_joystick, (Joystick.GamepadButton)out_enum1_type);
                        else scene.JoystickGamepadReleased(out_joystick, (Joystick.GamepadButton)out_enum1_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_AXIS_MOTION:
                    {
                        scene.JoystickGamepadAxis(out_joystick, (Joystick.GamepadAxis)out_enum1_type, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_DEVICE_ADDED_OR_REMOVED:
                    {
                        if (out_bool) scene.JoystickAdded(out_joystick);
                        else scene.JoystickRemoved(out_joystick);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TEXTINPUT: {
                        scene.TextInput(out_string);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_TEXTEDITING: {
                        scene.TextEditing(out_string, out_int4.x, out_int4.y);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_VISIBLE: {
                        scene.WindowFocus(out_bool);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_ENTER_OR_LEAVE: {
                        scene.MouseFocus(out_bool);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_SHOWN_OR_HIDDEN: {
                        scene.WindowVisible(out_bool);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_RESIZED: {
                        scene.WindowResize(out_int4.x, out_int4.y);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_FILE_DROPPED: {
                        scene.FileDropped(FileSystem.NewFile(out_string));
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_DIRECTORY_DROPPED:
                    {
                        scene.DirectoryDropped(out_string);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_LOWMEMORY: {
                        scene.LowMemory();
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_QUIT: {
                        if (scene.Quit()) Quit();
                    } break;
                default: break;
            }
        }

        /// <summary>
        /// poll or wait a event
        /// </summary>
        /// <param name="poll_or_wait">True: poll; False: wait</param>
        /// <returns></returns>
        public static bool PollOrWait(Scene scene, bool poll_or_wait)
        {
            bool out_hasEvent;int out_event_type;bool out_down_or_up;bool out_bool;int out_idx;int out_enum1_type;int out_enum2_type;string out_string;Int4 out_int4;Float4 out_float4;float out_float_value;Joystick out_joystick;
            PollOrWait(true, out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_string, out out_int4, out out_float4, out out_float_value, out out_joystick);

            if (out_hasEvent == false)
                return false;

            DoHandleEvent(scene, out_event_type, out_down_or_up, out_bool, out_idx, out_enum1_type, out_enum2_type, out_string, out_int4, out_float4, out_float_value, out_joystick);
            return true;
        }
    }

    /// <summary>
    /// <para>Provides an interface to the user's filesystem.</para>
    /// <para>This module provides access to files in specific places:</para>
    /// <para>1. The root folder of the source directory archive / 2. The root folder of the game's save directory.</para>
    /// <para>Files that are opened for write or append will always be created in the save directory. The same goes for other operations that involve writing to the filesystem, like <see cref="FileSystem.CreateDirectory"/>.</para>
    /// <para>It is recommended to set your game's identity first.  You can set it with <see cref="FileSystem.SetIdentity"/> as well.</para>
    /// </summary>
    public partial class FileSystem
    {
        public class Info
        {
            /// <summary>
            /// Numbers will be -1 if they cannot be determined.
            /// </summary>
            public readonly int64 Size;

            /// <summary>
            /// The file's last modification time in seconds since the unix epoch, or nil if it can't be determined.
            /// </summary>
            public readonly int64 ModifyTime;

            /// <summary>
            /// The type of the object at the path (file, directory, symlink, etc.)
            /// </summary>
            public readonly FileType Type;

            public Info(int64 size, int64 modifyTime, FileType type)
            {
                Size = size;
                ModifyTime = modifyTime;
                Type = type;
            }

            public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch
                var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }

            public override string ToString()
            {
                var time = UnixTimeStampToDateTime(ModifyTime);
                return $"size: {string.Format("{0:0,00}", Size)}, modify-time: {time}, type: {Type}";
            }
        };

        /// <summary>
        /// Creates a new File object. It needs to be opened before it can be accessed. (UTF-8 version)
        /// </summary>
        /// <param name="filename">The filename of the file.(UTF-8 byte array needed)</param>
        /// <param name="fmode_type">The mode to open the file in.</param>
        /// <returns></returns>
        public static File NewFile(byte[] filename, FileMode fmode_type = FileMode.Read)
        {
            IntPtr out_file;
            Love2dDll.wrap_love_dll_filesystem_newFile(filename, (int)fmode_type, out out_file);
            return LoveObject.NewObject<File>(out_file);
        }

        /// <summary>
        /// Creates a new FileData object.
        /// </summary>
        /// <param name="contents">The contents of the file.</param>
        /// <param name="filename">The name of the file.</param>
        /// <returns></returns>
        public static FileData NewFileData(byte[] contents, byte[] filename)
        {
            IntPtr out_file;
            Love2dDll.wrap_love_dll_filesystem_newFileData_content(contents, contents.Length, filename, out out_file);
            return LoveObject.NewObject<FileData>(out_file);
        }

        /// <summary>
        /// Creates a new FileData object.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static FileData NewFileData(File file)
        {
            IntPtr out_file;
            Love2dDll.wrap_love_dll_filesystem_newFileData_file(file.p, out out_file);
            return LoveObject.NewObject<FileData>(out_file);
        }

        /// <summary>
        /// Initializes FileSystem, will be called internally, so should not be used explictly.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool Init(byte[] args)
        {
            Love2dDll.wrap_love_dll_filesystem_open_love_filesystem();
            return Love2dDll.wrap_love_dll_filesystem_init(args);
        }

        public static void _SetFused(bool flag)
        {
            Love2dDll.wrap_love_dll_filesystem_setFused(flag);
        }

        /// <summary>
        /// <para>Gets whether the game is in fused mode or not.</para>
        /// <para>If a game is in fused mode, its save directory will be directly in the Appdata directory instead of Appdata/LOVE/. The game will also be able to load C Lua dynamic libraries which are located in the save directory.</para>
        /// <para>A game is in fused mode if the source .love has been fused to the executable (see Game Distribution), or if "--fused" has been given as a command-line argument when starting the game.</para>
        /// </summary>
        /// <returns></returns>
        public static bool IsFused()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_isFused(out out_result);
            return out_result;
        }


        public static void SetAndroidSaveExternal(bool useExternal)
        {
            Love2dDll.wrap_love_dll_filesystem_setAndroidSaveExternal(useExternal);
        }


        /// <summary>
        /// Sets the write directory for your game. Note that you can only set the name of the folder to store your files in, not the location.
        /// </summary>
        /// <param name="path">The new identity that will be used as write directory.(UTF-8 byte array needed)</param>
        /// <param name="append">Whether the identity directory will be searched when reading a filepath before or after the game's source directory and any currently.
        /// TRUE: results in searching source before searching save directory; FALSE: results in searching game save directory before searching source directorymounted archives.</param>
        public static void SetIdentity(byte[] path, bool append = false)
        {
            Love2dDll.wrap_love_dll_filesystem_setIdentity(path, append);
        }

        /// <summary>
        /// Gets the write directory name for your game. Note that this only returns the name of the folder to store your files in, not the full path.
        /// </summary>
        /// <returns></returns>
        public static string GetIdentity()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getIdentity(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// Sets the source of the game, where the code is present. This function can only be called once, and is normally automatically done by LÖVE.
        /// </summary>
        /// <param name="path">Absolute path to the game's source folder.(UTF-8 byte array needed)</param>
        public static void SetSource(byte[] path)
        {
            Love2dDll.wrap_love_dll_filesystem_setSource(path);
        }

        /// <summary>
        /// <para>initially it was .exe folder </para>
        /// lua version: Returns the full path to the the .love file or directory. If the game is fused to the LÖVE executable, then the executable is returned.
        /// </summary>
        /// <returns>The full platform-dependent path of the .love file or directory.</returns>
        public static string GetSource()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getSource(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// Mounts a zip file or folder in the game's save directory for reading. It is also possible to mount love.filesystem.getSourceBaseDirectory if the game is in fused mode.
        /// </summary>
        /// <param name="archive">The folder or zip file in the game's save directory to mount.(UTF-8 byte array needed)</param>
        /// <param name="mountpoint">The new path the archive will be mounted to.(UTF-8 byte array needed)</param>
        /// <param name="appendToPath">Whether the archive will be searched when reading a filepath before or after already-mounted archives. This includes the game's source and save directories.</param>
        /// <returns></returns>
        public static bool Mount(byte[] archive, byte[] mountpoint, bool appendToPath = false)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_mount(archive, mountpoint, appendToPath, out out_result);
            return out_result;
        }

        /// <summary>
        /// Unmounts a zip file or folder previously mounted for reading with <see cref="Mount"/>.
        /// </summary>
        /// <param name="archive">The folder or zip file in the game's save directory which is currently mounted.</param>
        /// <returns>True if the archive was successfully unmounted, false otherwise.</returns>
        public static bool Unmount(byte[] archive)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_unmount(archive, out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets the current working directory.
        /// </summary>
        /// <returns></returns>
        public static string GetWorkingDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getWorkingDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// Returns the path of the user's directory
        /// </summary>
        /// <returns></returns>
        public static string GetUserDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getUserDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// Returns the application data directory (could be the same as getUserDirectory
        /// </summary>
        /// <returns></returns>
        public static string GetAppdataDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getAppdataDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// Gets the full path to the designated save directory. This can be useful if you want to use the standard io library (or something else) to read or write in the save directory.
        /// </summary>
        /// <returns></returns>
        public static string GetSaveDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getSaveDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// <para>Gets the platform-specific absolute path of the directory containing a filepath.</para>
        /// </summary>
        /// <param name="filename">The filepath to get the directory of.(UTF-8 byte array needed)</param>
        /// <returns>The platform-specific full path of the directory containing the filepath.</returns>
        public static string GetRealDirectory(byte[] filename)
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getRealDirectory(filename, out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// will be called internally, so should not be used explictly.
        /// </summary>
        /// <returns></returns>
        public static string GetExecutablePath()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getExecutablePath(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// Gets information about the specified file or directory.
        /// </summary>
        /// <param name="path">The file or directory path to check.</param>
        /// <returns></returns>
        public static Info GetInfo(byte[] path)
        {
            int fileType_int = 0;
            int64 size, modifyTime;
            bool success;
            Love2dDll.wrap_love_dll_filesystem_getInfo(path, out fileType_int, out size, out modifyTime, out success);
            return success ? new Info(size, modifyTime, (FileType)fileType_int) : null;
        }

        /// <summary>
        /// <para>Recursively creates a directory.</para>
        /// <para>When called with "a/b" it creates both "a" and "a/b", if they don't exist already.</para>
        /// </summary>
        /// <param name="name">The directory to create. (UTF-8 byte array needed)</param>
        /// <returns>True if the directory was created, false if not.</returns>
        public static bool CreateDirectory(byte[] name)
        {
            bool out_result;
            Love2dDll.wrap_love_dll_filesystem_createDirectory(name, out out_result);
            return out_result;
        }

        /// <summary>
        /// Removes a file or empty directory.
        /// </summary>
        /// <param name="path">The file or directory to remove.</param>
        public static bool Remove(byte[] path)
        {
            bool out_result;
            Love2dDll.wrap_love_dll_filesystem_remove(path, out out_result);
            return out_result;
        }

        /// <summary>
        /// Read the contents of a file.
        /// </summary>
        /// <param name="filename">The name (and path) of the file. (UTF-8 byte array needed)</param>
        /// <param name="len">How many bytes to read. (-1 means all)</param>
        /// <returns></returns>
        public static byte[] Read(byte[] filename, long len = -1)
        {
            IntPtr out_data;
            uint out_data_length;
            Love2dDll.wrap_love_dll_filesystem_read(filename, len, out out_data, out out_data_length);
            return DllTool.readBytesAndRelease(out_data, out_data_length);
        }

        /// <summary>
        /// Write data to a file in the save directory. If the file existed already, it will be completely replaced by the new contents.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.(UTF-8 byte need)</param>
        /// <param name="input">The data to write to the file.</param>
        public static void Write(byte[] filename, byte[] input)
        {
            Love2dDll.wrap_love_dll_filesystem_write(filename, input, (uint)input.Length);
        }

        /// <summary>
        /// Append data to an existing file. (UTF-8 version)
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="input">The data to append to the file.</param>
        public static void Append(byte[] filename, byte[] input)
        {
            Love2dDll.wrap_love_dll_filesystem_append(filename, input, (uint)input.Length);
        }

        /// <summary>
        /// <para>Returns a table with the names of files and subdirectories in the specified path. The table is not sorted in any way; the order is undefined.</para>
        /// <para>If the path passed to the function exists in the game and the save directory, it will list the files and directories from both places.</para>
        /// </summary>
        /// <param name="dir">The directory.</param>
        /// <returns></returns>
        public static string[] GetDirectoryItems(byte[] dir)
        {
            IntPtr out_wss = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getDirectoryItems(dir, out out_wss);
            return DllTool.WSSToStringListAndRelease(out_wss);
        }

        /// <summary>
        /// <para>Deprecated in LÖVE 11.0</para>
        /// <para>Gets the last modification time of a file.</para>
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static long _GetLastModified(byte[] filename)
        {
            long out_time;
            Love2dDll.wrap_love_dll_filesystem_getLastModified(filename, out out_time);
            return out_time;
        }

        /// <summary>
        /// Gets whether love.filesystem follows symbolic links.
        /// </summary>
        /// <returns></returns>
        public static bool AreSymlinksEnabled()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_areSymlinksEnabled(out out_result);
            return out_result;
        }

        /// <summary>
        /// <para>no need for C# version</para>
        /// Returns the full path to the directory containing the .love file.
        /// </summary>
        /// <returns></returns>
        public static string _GetSourceBaseDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getSourceBaseDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// <para>no need for C# version</para>
        /// Gets the filesystem paths that will be searched when require is called.
        /// </summary>
        /// <returns></returns>
        public static string _GetRequirePath()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getRequirePath(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// <para>no need for C# version</para>
        /// <para>Sets the filesystem paths that will be searched when require is called.</para>
        /// <para>The paths string given to this function is a sequence of path templates separated by semicolons. The argument passed to require will be inserted in place of any question mark ("?") character in each template (after the dot characters in the argument passed to require are replaced by directory separators.)</para>
        /// <para>The paths are relative to the game's source and save directories, as well as any paths mounted with love.filesystem.mount.</para>
        /// </summary>
        /// <param name="paths"></param>
        public static void _SetRequirePath(byte[] paths)
        {
            Love2dDll.wrap_love_dll_filesystem_setRequirePath(paths);
        }

    }

    public partial class Sound
    {
        /// <summary>
        /// Attempts to find a decoder for the encoded sound data in the specified file.
        /// </summary>
        /// <param name="fdata">The file data with encoded sound data.</param>
        /// <param name="buffersize">The size of each decoded chunk, in bytes.</param>
        /// <returns></returns>
        public static Decoder NewDecoder(FileData fdata, int buffersize = Decoder.DEFAULT_BUFFER_SIZE)
        {
            IntPtr out_ptr;
            Love2dDll.wrap_love_dll_sound_newDecoder_filedata(fdata.p, buffersize, out out_ptr);
            return LoveObject.NewObject<Decoder>(out_ptr);
        }

        /// <summary>
        /// Attempts to find a decoder for the encoded sound data in the specified file.
        /// </summary>
        /// <param name="file">The file with encoded sound data.</param>
        /// <param name="buffersize">The size of each decoded chunk, in bytes.</param>
        /// <returns></returns>
        public static Decoder NewDecoder(File file, int buffersize = Decoder.DEFAULT_BUFFER_SIZE)
        {
            IntPtr out_ptr;
            Love2dDll.wrap_love_dll_sound_newDecoder_file(file.p, buffersize, out out_ptr);
            return LoveObject.NewObject<Decoder>(out_ptr);
        }

        /// <summary>
        /// <para>Creates a new SoundData.</para>
        /// <para>It's also possible to create SoundData with a custom sample rate, channel and bit depth.</para>
        /// <para>The sound data will be decoded to the memory in a raw format. It is recommended to create only short sounds like effects, as a 3 minute song uses 30 MB of memory this way.</para>
        /// </summary>
        /// <param name="decoder">Decode data from this Decoder until EOF.</param>
        /// <returns>A new SoundData object.</returns>
        public static SoundData NewSoundData(Decoder decoder)
        {
            IntPtr out_soundData_ptr;
            Love2dDll.wrap_love_dll_sound_newSoundData_decoder(decoder.p, out out_soundData_ptr);
            return LoveObject.NewObject<SoundData>(out_soundData_ptr);
        }

        /// <summary>
        /// <para>Creates a new SoundData.</para>
        /// <para>It's also possible to create SoundData with a custom sample rate, channel and bit depth.</para>
        /// <para>The sound data will be decoded to the memory in a raw format. It is recommended to create only short sounds like effects, as a 3 minute song uses 30 MB of memory this way.</para>
        /// </summary>
        /// <param name="samples">Total number of samples.</param>
        /// <param name="sampleRate">Number of samples per second</param>
        /// <param name="bits">Bits per sample (8 or 16).</param>
        /// <param name="channels">Either 1 for mono or 2 for stereo.</param>
        /// <returns>A new SoundData object.</returns>
        public static SoundData NewSoundData(int samples, int sampleRate = Decoder.DEFAULT_SAMPLE_RATE, int bits = Decoder.DEFAULT_BIT_DEPTH, int channels = Decoder.DEFAULT_CHANNELS)
        {
            IntPtr out_soundData_ptr;
            Love2dDll.wrap_love_dll_sound_newSoundData(samples, sampleRate, bits, channels, out out_soundData_ptr);
            return LoveObject.NewObject<SoundData>(out_soundData_ptr);
        }

        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_sound_luaopen_love_sound();
        }
    }

    public partial class Audio
    {
        /// <summary>
        /// Creates a new Source from a Decoder. 
        /// </summary>
        /// <param name="decoder">The Decoder to create a Source from.</param>
        /// <param name="type">Streaming or static source.</param>
        /// <returns></returns>
        public static Source NewSource(Decoder decoder, SourceType type)
        {
            IntPtr out_decoder_ptr;
            Love2dDll.wrap_love_dll_audio_newSource_decoder(decoder.p, (int)type, out out_decoder_ptr);
            return LoveObject.NewObject<Source>(out_decoder_ptr);
        }

        /// <summary>
        /// Sources created from SoundData are always static.
        /// </summary>
        /// <param name="sd">The SoundData to create a Source from.</param>
        /// <returns></returns>
        public static Source NewSource(SoundData sd)
        {
            IntPtr out_decoder_ptr;
            Love2dDll.wrap_love_dll_audio_newSource_sounddata(sd.p, out out_decoder_ptr);
            return LoveObject.NewObject<Source>(out_decoder_ptr);
        }


        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_audio_open_love_audio();
        }

        /// <summary>
        /// Gets the current number of simultaneously playing sources.
        /// </summary>
        /// <returns></returns>
        public static int GetActiveSourceCount()
        {
            int out_reslut = 0;
            Love2dDll.wrap_love_dll_audio_getActiveSourceCount(out out_reslut);
            return out_reslut;
        }

        /// <summary>
        /// Stops all currently played sources.
        /// </summary>
        public static void Stop()
        {
            Love2dDll.wrap_love_dll_audio_stop();
        }

        /// <summary>
        /// Stops specified source.
        /// </summary>
        public static void Stop(params Source[] list)
        {
            foreach (var source in list)
            {
                source.Stop();
            }
        }

        /// <summary>
        /// Pauses all currently played Sources.
        /// </summary>
        public static void Pause()
        {
            Love2dDll.wrap_love_dll_audio_pause();
        }

        /// <summary>
        /// Pauses specific played Sources.
        /// </summary>
        public static void Pause(params Source[] list)
        {
            foreach( var source in list)
            {
                source.Pause();
            }
        }

        /// <summary>
        /// Plays the specified Source.
        /// </summary>
        /// <param name="sources"></param>
        public static void Play(params Source[] sources)
        {
            Check.ArgumentNull(sources);

            IntPtr[] ptrs = new IntPtr[sources.Length];
            for (int i = 0; i < sources.Length; i++)
            {
                ptrs[i] = sources[i].p;
            }

            Love2dDll.wrap_love_dll_audio_play(ptrs, ptrs.Length);
        }

        /// <summary>
        /// Sets the master volume.
        /// </summary>
        /// <param name="v">1.0 is max and 0.0 is off.</param>
        public static void SetVolume(float v)
        {
            Love2dDll.wrap_love_dll_audio_setVolume(v);
        }

        /// <summary>
        /// Returns the master volume.
        /// </summary>
        /// <returns></returns>
        public static float GetVolume()
        {
            float out_volume = 0;
            Love2dDll.wrap_love_dll_audio_getVolume(out out_volume);
            return out_volume;
        }

        /// <summary>
        /// Sets the position of the listener.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void SetPosition(float x, float y, float z)
        {
            Love2dDll.wrap_love_dll_audio_setPosition(x, y, z);
        }

        /// <summary>
        /// Returns the position of the listener.
        /// </summary>
        /// <returns></returns>
        public static Float3 GetPosition()
        {
            float out_x, out_y, out_z;
            Love2dDll.wrap_love_dll_audio_getPosition(out out_x, out out_y, out out_z);
            return new Float3(out_x, out_y, out_z);
        }

        /// <summary>
        /// Sets the orientation of the listener.
        /// </summary>
        /// <param name="forward">Forward vector of the listener orientation.</param>
        /// <param name="up">Up vector of the listener orientation.</param>
        public static void SetOrientation(Float3 forward, Float3 up)
        {
            Love2dDll.wrap_love_dll_audio_setOrientation(forward.x, forward.y, forward.z, up.x, up.y, up.z);
        }

        /// <summary>
        /// Returns the orientation of the listener.
        /// </summary>
        /// <param name="forward">Forward vector of the listener orientation.</param>
        /// <param name="up">Up vector of the listener orientation.</param>
        public static void GetOrientation(out Float3 forward, out Float3 up)
        {
            float out_x,out_y,out_z,out_dx,out_dy,out_dz;
            Love2dDll.wrap_love_dll_audio_getOrientation(out out_x, out out_y, out out_z, out out_dx, out out_dy, out out_dz);
            forward = new Float3(out_x, out_y, out_z);
            up = new Float3(out_dx, out_dy, out_dz);
        }

        /// <summary>
        /// Sets the velocity of the listener.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void SetVelocity(float x, float y, float z)
        {
            Love2dDll.wrap_love_dll_audio_setVelocity(x, y, z);
        }

        /// <summary>
        /// Returns the velocity of the listener.
        /// </summary>
        /// <returns></returns>
        public static Float3 GetVelocity()
        {
            float out_x, out_y, out_z;
            Love2dDll.wrap_love_dll_audio_getVelocity(out out_x, out out_y, out out_z);
            return new Float3(out_x, out_y, out_z);
        }

        /// <summary>
        /// Sets a global scale factor for doppler effects.
        /// </summary>
        /// <param name="scale">The new doppler scale factor. The scale must be greater than 0.</param>
        public static void SetDopplerScale(float scale)
        {
            Love2dDll.wrap_love_dll_audio_setDopplerScale(scale);
        }

        /// <summary>
        /// Gets the global scale factor for doppler effects.
        /// </summary>
        /// <returns></returns>
        public static float GetDopplerScale()
        {
            float out_scale = 0;
            Love2dDll.wrap_love_dll_audio_getDopplerScale(out out_scale);
            return out_scale;
        }

        /// <summary>
        /// Sets the distance attenuation model.
        /// </summary>
        /// <param name="distancemodel_type"></param>
        public static void SetDistanceModel(DistanceModel distancemodel_type)
        {
            Love2dDll.wrap_love_dll_audio_setDistanceModel((int)distancemodel_type);
        }

        /// <summary>
        /// Returns the distance attenuation model.
        /// </summary>
        /// <returns></returns>
        public static DistanceModel GetDistanceModel()
        {
            int out_distancemodel_type = 0;
            Love2dDll.wrap_love_dll_audio_getDistanceModel(out out_distancemodel_type);
            return (DistanceModel)out_distancemodel_type;
        }

    }

    /// <summary>
    /// Provides an interface to decode encoded image data.
    /// </summary>
    public partial class Image
    {

        /// <summary>
        /// Creates a new ImageData object.
        /// 
        /// </summary>
        /// <param name="w">The width of the ImageData.</param>
        /// <param name="h">The height of the ImageData.</param>
        /// <param name="format">The pixel format of the ImageData.</param>
        /// <param name="data">Optional raw byte data to load into the ImageData, in the format specified by format.</param>
        /// <returns></returns>
        public static ImageData NewImageData(int w, int h, ImageDataPixelFormat format = ImageDataPixelFormat.RGBA8, byte[] data = null)
        {
            if (w <= 0 || h <= 0)
            {
                throw new Exception("Invalid image size.");
            }

            if (data == null)
            {
                data = new byte[0];
            }

            IntPtr out_imagedata;
            Love2dDll.wrap_love_dll_image_newImageData_wh_format_data(w, h, data, data.Length, (int)format, out out_imagedata);
            return LoveObject.NewObject<ImageData>(out_imagedata);
        }
        public static ImageData NewImageData(FileData data)
        {
            IntPtr out_imagedata;
            Love2dDll.wrap_love_dll_image_newImageData_filedata(data.p, out out_imagedata);
            return LoveObject.NewObject<ImageData>(out_imagedata);
        }
        public static CompressedImageData NewCompressedData(FileData data)
        {
            IntPtr out_compressedimagedata;
            var res = Love2dDll.wrap_love_dll_image_newCompressedData(data.p, out out_compressedimagedata);
            return LoveObject.NewObject<CompressedImageData>(out_compressedimagedata);
        }


        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_image_open_love_image();
        }

        public static bool IsCompressed(FileData data)
        {
            bool out_result;
            Love2dDll.wrap_love_dll_image_isCompressed(data.p, out out_result);
            return out_result;
        }

    }

    public partial class Font
    {
        public static Rasterizer NewRasterizer(FileData fileData)
        {
            IntPtr out_reasterizer;
            Love2dDll.wrap_love_dll_font_newRasterizer(fileData.p, out out_reasterizer);
            return LoveObject.NewObject<Rasterizer>(out_reasterizer);
        }
        public static Rasterizer NewTrueTypeRasterizer(Data data, int size, TrueTypeRasterizer.Hinting hinting = TrueTypeRasterizer.Hinting.Normal)
        {
            IntPtr out_reasterizer;
            Love2dDll.wrap_love_dll_font_newTrueTypeRasterizer_data(data.p, size, (int)hinting, out out_reasterizer);
            return LoveObject.NewObject<Rasterizer>(out_reasterizer);
        }
        public static Rasterizer NewTrueTypeRasterizer(int size, TrueTypeRasterizer.Hinting hinting = TrueTypeRasterizer.Hinting.Normal)
        {
            IntPtr out_reasterizer;
            Love2dDll.wrap_love_dll_font_newTrueTypeRasterizer(size, (int)hinting, out out_reasterizer);
            return LoveObject.NewObject<Rasterizer>(out_reasterizer);
        }
        public static Rasterizer NewBMFontRasterizer(FileData fileData, params ImageData[] imageDatas)
        {
            IntPtr out_reasterizer;
            var datas = new IntPtr[imageDatas.Length];
            for (int i = 0; i < imageDatas.Length; i++) datas[i] = imageDatas[i].p;
            Love2dDll.wrap_love_dll_font_newBMFontRasterizer(fileData.p, datas, datas.Length, out out_reasterizer);
            return LoveObject.NewObject<Rasterizer>(out_reasterizer);
        }
        public static Rasterizer NewImageRasterizer(ImageData imageData, byte[] glyphs, int extraspacing)
        {
            IntPtr out_reasterizer;
            Love2dDll.wrap_love_dll_font_newImageRasterizer(imageData.p, glyphs, extraspacing, out out_reasterizer);
            return LoveObject.NewObject<Rasterizer>(out_reasterizer);
        }


        /// <summary>
        /// Creates a new GlyphData.
        /// </summary>
        /// <param name="rasterizer">The Rasterizer containing the font.</param>
        /// <param name="glyph">The character code of the glyph.</param>
        /// <returns></returns>
        public static GlyphData NewGlyphData(Rasterizer rasterizer, byte[] glyph)
        {
            IntPtr out_GlyphData;
            Love2dDll.wrap_love_dll_font_newGlyphData_rasterizer_str(rasterizer.p, glyph, out out_GlyphData);
            return LoveObject.NewObject<GlyphData>(out_GlyphData);
        }

        public static GlyphData NewGlyphData(Rasterizer rasterizer, int glyphCode)
        {
            IntPtr out_GlyphData;
            Love2dDll.wrap_love_dll_font_newGlyphData_rasterizer_num(rasterizer.p, glyphCode, out out_GlyphData);
            return LoveObject.NewObject<GlyphData>(out_GlyphData);
        }
        
        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_font_open_love_font();
        }
    }

    public partial class Video
    {
        /// <summary>
        /// Creates a new VideoStream. Currently only Ogg Theora video files are supported. VideoStreams can't draw videos, see love.graphics.newVideo for that.
        /// </summary>
        /// <param name="file">The File object containing the Ogg Theora video.</param>
        /// <returns>A new VideoStream.</returns>
        public static VideoStream NewVideoStream(File file)
        {
            IntPtr out_videostream;
            Love2dDll.wrap_love_dll_newVideoStream(file.p, out out_videostream);
            return LoveObject.NewObject<VideoStream>(out_videostream);
        }

        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_video_open_love_video();
        }

    }

    public partial class Mathf
    {
        static RandomGenerator rg = null;
        public static void Init()
        {
            Love2dDll.wrap_love_dll_open_love_math();
            rg = NewRandomGenerator();
        }
        public static float Random(int l, int u)
        {
            double random = rg.Random();
            return (float)(Math.Floor(random * (u - l + 1) + l));
        }
        public static RandomGenerator Ext_getRandomGenerator()
        {
            return rg;
        }
        public static RandomGenerator NewRandomGenerator()
        {
            IntPtr out_RandomGenerator = IntPtr.Zero;
            Love2dDll.wrap_love_dll_math_newRandomGenerator(out out_RandomGenerator);
            return LoveObject.NewObject<RandomGenerator>(out_RandomGenerator);
        }
        public static BezierCurve NewBezierCurve(Float2[] points)
        {
            IntPtr out_BezierCurve = IntPtr.Zero;
            Love2dDll.wrap_love_dll_math_newBezierCurve(points, points.Length, out out_BezierCurve);
            return LoveObject.NewObject<BezierCurve>(out_BezierCurve);
        }
        public static Triangle[] Triangulate(Float2[] points)
        {
            IntPtr out_triArray;
            int out_triCount;
            Love2dDll.wrap_love_dll_math_triangulate(points, points.Length, out out_triArray, out out_triCount);

            float[] buffer = DllTool.readFloatsAndRelease(out_triArray, out_triCount * 6);
            Triangle[] tri = new Triangle[out_triCount];

            for (int i = 0; i < out_triCount; i++)
            {
                tri[i].a = new Float2(buffer[i * 6 + 0], buffer[i * 6 + 1]);
                tri[i].b = new Float2(buffer[i * 6 + 2], buffer[i * 6 + 3]);
                tri[i].c = new Float2(buffer[i * 6 + 4], buffer[i * 6 + 5]);
            }

            return tri;
        }
        public static bool IsConvex(Float2[] points)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_math_isConvex(points, points.Length, out out_result);
            return out_result;
        }
        public static float GammaToLinear(float gama)
        {
            float out_liner = 0;
            Love2dDll.wrap_love_dll_math_gammaToLinear(gama, out out_liner);
            return out_liner;
        }
        public static float LinearToGamma(float liner)
        {
            float out_gama = 0;
            Love2dDll.wrap_love_dll_math_linearToGamma(liner, out out_gama);
            return out_gama;
        }
        public static float Noise(float x)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_1(x, out out_result);
            return out_result;
        }
        public static float Noise(float x, float y)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_2(x, y, out out_result);
            return out_result;
        }
        public static float Noise(float x, float y, float z)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_3(x, y, z, out out_result);
            return out_result;
        }
        public static float Noise(float x, float y, float z, float w)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_4(x, y, z, w, out out_result);
            return out_result;
        }

    }

    public partial class Graphics
    {
        public enum Renderer : int
        {
            OpenGL= 0,
            OpenGLES,
        };
        public enum SystemLimit : int
        {
            /// <summary>
            /// The maximum size of points.
            /// </summary>
            PointSize,

            /// <summary>
            /// The maximum width or height of Images and Canvases.
            /// </summary>
            TextureSize,

            /// <summary>
            /// The maximum number of simultaneously active canvases (via <see cref="Graphics.SetCanvas"/>.)
            /// </summary>
            MultiCanvas,

            /// <summary>
            /// The maximum number of antialiasing samples for a Canvas.
            /// </summary>
            CanvasMSAA,
        };

        #region graphics Object Creation

        public static bool Init()
        {
            Love2dDll.wrap_love_dll_graphics_open_love_graphics();
            Love.Love2dGraphicsShaderBoot.Init();
            return true;
        }

        public static Image NewImage(ImageDataBase[] imageData, bool flagMipmaps = false, bool flagLinear = false)
        {
            bool[] isCompressed = new bool[imageData.Length];
            for (int i = 0; i < imageData.Length; i++)
            {
                isCompressed[i] = imageData[i] is CompressedImageData;
            }

            var imageDataList = DllTool.GenIntPtrArray(imageData);

            IntPtr out_image = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newImage_data(imageDataList, isCompressed, imageDataList.Length, flagMipmaps, flagLinear, out out_image);
            return LoveObject.NewObject<Image>(out_image);
        }

        public static Quad NewQuad(double x, double y, double w, double h, double sw, double sh)
        {
            IntPtr out_quad = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newQuad(x, y, w, h, sw, sh, out out_quad);
            return LoveObject.NewObject<Quad>(out_quad);
        }
        public static Font NewFont(Rasterizer rasterizer)
        {
            IntPtr out_font = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newFont(rasterizer.p, out out_font);
            return LoveObject.NewObject<Font>(out_font);
        }
        public static SpriteBatch NewSpriteBatch(Texture texture, int maxSprites, SpriteBatchUsage usage_type)
        {
            IntPtr out_spriteBatch = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newSpriteBatch(texture.p, maxSprites, (int)usage_type, out out_spriteBatch);
            return LoveObject.NewObject<SpriteBatch>(out_spriteBatch);
        }
        public static ParticleSystem NewParticleSystem(Texture texture, int buffer = 1000)
        {
            IntPtr out_particleSystem = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newParticleSystem(texture.p, buffer, out out_particleSystem);
            return LoveObject.NewObject<ParticleSystem>(out_particleSystem);
        }

        public class Settings
        {
            public CanvasMipmapMode mipmaps = CanvasMipmapMode.None;
            public PixelFormat format = PixelFormat.NORMAL;
            public TextureType type = TextureType.TEXTURE_2D;
            public float? dpiScale = null;
            public int msaa = 0;
            public bool readable = true;
        }

        /// <summary>
        /// Creates a new Canvas.
        /// <para>This function can be slow if it is called repeatedly, such as from love.update or love.draw. If you need to use a specific resource often, create it once and store it somewhere it can be reused!</para>
        /// </summary>
        /// <param name="width">The desired width of the Canvas.</param>
        /// <param name="height">The desired height of the Canvas.</param>
        /// <param name="format_type"></param>
        /// <param name="msaa"></param>
        /// <returns>A new Canvas with dimensions equal to the window's size in pixels.</returns>
        public static Canvas NewCanvas(int width, int height, Settings settings = null)
        {
            if (settings == null)
            {
                settings = new Settings();
            }

            if (settings.dpiScale != null)
            {
                settings.dpiScale = GetDPIScale();
            }

            IntPtr out_canvas = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newCanvas(width, height,
                (int)settings.type,
                (int)settings.format,
                settings.readable,
                settings.msaa,
                (int)settings.dpiScale,
                (int)settings.mipmaps,
                out out_canvas);
            return LoveObject.NewObject<Canvas>(out_canvas);
        }
        public static Mesh NewMesh(Float2[] pos, Float2[] uv, Float4[] color, MeshDrawMode drawMode, SpriteBatchUsage usage)
        {
            IntPtr out_mesh = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newMesh_specifiedVertices(pos, uv, color, pos.Length, (int)drawMode, (int)usage, out out_mesh);
            return LoveObject.NewObject<Mesh>(out_mesh);
        }

        /// <summary>
        /// Creates a standard Mesh with the specified number of vertices.
        /// </summary>
        /// <param name="count">The total number of vertices the Mesh will use. Each vertex is initialized to position of (0,0), uv of (0,0), color of (1,1,1,1).</param>
        /// <param name="drawMode">How the vertices are used when drawing. The default mode "fan" is sufficient for simple convex polygons.</param>
        /// <param name="usage">The expected usage of the Mesh. The specified usage mode affects the Mesh's memory usage and performance.</param>
        /// <returns></returns>
        public static Mesh NewMesh(int count, MeshDrawMode drawMode, SpriteBatchUsage usage)
        {
            IntPtr out_mesh = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newMesh_count(count, (int)drawMode, (int)usage, out out_mesh);
            return LoveObject.NewObject<Mesh>(out_mesh);
        }

        public static Text NewText(Font font, ColoredStringArray coloredStr)
        {
            IntPtr out_text = IntPtr.Zero;
            coloredStr.ExecResource((tmp) => {
                Love2dDll.wrap_love_dll_graphics_newText(font.p, tmp.Item1, tmp.Item2, coloredStr.Length, out out_text);
            });
            return LoveObject.NewObject<Text>(out_text);
        }

        /// <summary>
        /// Creates a new drawable Video. Currently only Ogg Theora video files are supported.
        /// </summary>
        /// <param name="videoStream"> a VideoStream object </param>
        /// <param name="audio">Whether to try to load the video's audio into an audio Source. If not explicitly set to true or false, it will try without causing an error if the video has no audio.</param>
        /// <param name="dipScale">The DPI scale factor of the video. if it was null, value will be Graphics.GetDPIScale()</param>
        /// <returns></returns>
        public static Video NewVideo(VideoStream videoStream, bool audio = true, float? dipScale = null)
        {
            IntPtr out_video = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newVideo(videoStream.p, dipScale == null ? GetDPIScale() : dipScale.Value, out out_video);
            var video = LoveObject.NewObject<Video>(out_video);

            if (audio)
            {
                var source = Audio.NewSource(videoStream.GetFilename(), SourceType.Stream);
                video.SetSource(source);
            }

            return video;
        }

        #endregion

        #region graphics State

        /// <summary>
        /// Resets the current graphics settings.
        /// <para>Calling reset makes the current drawing color white, the current background color black, disables any active Canvas or Shader, and removes any scissor settings. It sets the BlendMode to alpha, enables all color component masks, disables wireframe mode and resets the current graphics transformation to the origin. It also sets both the point and line drawing modes to smooth and their sizes to 1.0.</para>
        /// </summary>
        void Reset()
        {
            Love2dDll.wrap_love_dll_graphics_reset();
        }

        /// <summary>
        /// Gets whether the graphics module is able to be used.
        /// </summary>
        /// <returns></returns>
        public static bool IsActive()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_isActive(out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets whether gamma-correct rendering is enabled.
        /// <para></para>
        /// <para>Not all devices support gamma-correct rendering, in which case it will be automatically disabled and this function will return false. It is supported on desktop systems which have graphics cards that are capable of using OpenGL 3 / DirectX 10, and iOS devices that can use OpenGL ES 3.</para>
        /// </summary>
        /// <returns></returns>
        public static bool IsGammaCorrect()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_isGammaCorrect(out out_result);
            return out_result;
        }

        /// <summary>
        /// Disables scissor.
        /// </summary>
        public static void SetScissor()
        {
            Love2dDll.wrap_love_dll_graphics_setScissor();
        }

        /// <summary>
        /// Sets scissor.
        /// <para>The scissor limits the drawing area to a specified rectangle. This affects all graphics calls, including love.graphics.clear.</para>
        /// <para>The dimensions of the scissor is unaffected by graphical transformations(translate, scale, ...).</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public static void SetScissor(int x, int y, int w, int h)
        {
            Love2dDll.wrap_love_dll_graphics_setScissor_xywh(x, y, w, h);
        }

        /// <summary>
        /// 设置 scissor 为所给矩形和现有 scissor 的交集（交集肯定还是个矩形）。如果之前没有设置 scissor，则相当于调用 <see cref="Graphics.SetScissor"/> 
        /// <para>Sets the scissor to the rectangle created by the intersection of the specified rectangle with the existing scissor.</para>
        /// <para>The scissor limits the drawing area to a specified rectangle. This affects all graphics calls, including love.graphics.clear.</para>
        /// <para>The dimensions of the scissor is unaffected by graphical transformations(translate, scale, ...).</para>
        /// </summary>
        /// <param name="x">x coordinate of upper left corner.</param>
        /// <param name="y">y coordinate of upper left corner.</param>
        /// <param name="w">width of clipping rectangle.</param>
        /// <param name="h">height of clipping rectangle.</param>
        public static void IntersectScissor(int x, int y, int w, int h)
        {
            Love2dDll.wrap_love_dll_graphics_intersectScissor(x, y, w, h);
        }

        /// <summary>
        /// Gets the current scissor box.
        /// <para> Int4:x The x-component of the top-left point of the box. </para>
        /// <para> Int4:y The y-component of the top-left point of the box. </para>
        /// <para> The width of the box. </para>
        /// <para> The height of the box. </para>
        /// </summary>
        /// <returns>
        /// </returns>
        public static Int4 GetScissor()
        {
            int out_x, out_y, out_w, out_h;
            Love2dDll.wrap_love_dll_graphics_getScissor(out out_x, out out_y, out out_w, out out_h);
            return new Int4(out_x, out_y, out_w, out_h);
        }

        /// <summary>
        /// Configures or disables stencil testing.
        /// <para>When stencil testing is enabled, the geometry of everything that is drawn afterward will be clipped / stencilled out based on a comparison between the arguments of this function and the stencil value of each pixel that the geometry touches. The stencil values of pixels are affected via Graphics.Stencil.</para>
        /// </summary>
        /// <param name="compare_type">The type of comparison to make for each pixel.</param>
        /// <param name="compareValue">The value to use when comparing with the stencil value of each pixel. </param>
        public static void SetStencilTest(CompareMode compare_type, int compareValue)
        {
            Love2dDll.wrap_love_dll_graphics_setStencilTest((int)compare_type, compareValue);
        }

        /// <summary>
        /// Gets the current stencil test configuration.
        /// <para>When stencil testing is enabled, the geometry of everything that is drawn afterward will be clipped / stencilled out based on a comparison between the arguments of this function and the stencil value of each pixel that the geometry touches. The stencil values of pixels are affected via Graphics.Stencil.</para>
        /// <para>Each Canvas has its own per-pixel stencil values.</para>
        /// </summary>
        /// <param name="out_compare_type">The type of comparison that is made for each pixel. Will be "always" if stencil testing is disabled.</param>
        /// <param name="out_compareValue">The value used when comparing with the stencil value of each pixel.</param>
        public static void GetStencilTest(out CompareMode out_compare_type, out int out_compareValue)
        {
            int compare_type = 0;
            Love2dDll.wrap_love_dll_graphics_getStencilTest(out compare_type, out out_compareValue);
            out_compare_type = (CompareMode)compare_type;
        }

        /// <summary>
        /// Sets the color used for drawing.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public static void SetColor(float r, float g, float b, float a = 1)
        {
            Love2dDll.wrap_love_dll_graphics_setColor(r, g, b, a);
        }

        /// <summary>
        /// Gets the current color.
        /// </summary>
        /// <returns></returns>
        public static Float4 GetColor()
        {
            float out_r, out_g, out_b, out_a;
            Love2dDll.wrap_love_dll_graphics_getColor(out out_r, out out_g, out out_b, out out_a);
            return new Float4(out_r, out_g, out_b, out_a);
        }

        /// <summary>
        /// Sets the background color.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public static void SetBackgroundColor(int r, int g, int b, int a = 255)
        {
            Love2dDll.wrap_love_dll_graphics_setBackgroundColor(r, g, b, a);
        }

        /// <summary>
        /// Gets the current background color.
        /// </summary>
        public static Int4 GetBackgroundColor()
        {
            int out_r, out_g, out_b, out_a;
            Love2dDll.wrap_love_dll_graphics_getBackgroundColor(out out_r, out out_g, out out_b, out out_a);
            return new Int4(out_r, out_g, out_b, out_a);
        }

        /// <summary>
        /// set font used. pass null to use default font
        /// </summary>
        /// <param name="font"></param>
        public static void SetFont(Font font = null)
        {
            if (font == null)
            {
                Love2dDll.wrap_love_dll_graphics_setFont(IntPtr.Zero);
            }
            else
            {
                Love2dDll.wrap_love_dll_graphics_setFont(font.p);
            }
        }

        /// <summary>
        /// Gets the current Font object.
        /// </summary>
        /// <returns></returns>
        public static Font GetFont()
        {
            IntPtr out_font = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_getFont(out out_font);
            return LoveObject.NewObject<Font>(out_font);
        }

        /// <summary>
        /// Sets the color mask. Enables or disables specific color components when rendering.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public static void SetColorMask(bool r, bool g, bool b, bool a)
        {
            Love2dDll.wrap_love_dll_graphics_setColorMask(r, g, b, a);
        }

        /// <summary>
        /// Gets the active color components used when drawing. Normally all 4 components are active unless <see cref="Graphics.SetColorMask"/> has been used.
        /// <para>The color mask determines whether individual components of the colors of drawn objects will affect the color of the screen. They affect <see cref="Graphics.Clear"/> as well.</para>
        /// </summary>
        /// <param name="out_r">Whether the red color component is active when rendering.</param>
        /// <param name="out_g">Whether the red green component is active when rendering.</param>
        /// <param name="out_b">Whether the red blue component is active when rendering.</param>
        /// <param name="out_a">Whether the red alpha component is active when rendering.</param>
        public static void GetColorMask(out bool out_r, out bool out_g, out bool out_b, out bool out_a)
        {
            Love2dDll.wrap_love_dll_graphics_getColorMask(out out_r, out out_g, out out_b, out out_a);
        }

        /// <summary>
        /// Sets the blending mode.
        /// </summary>
        /// <param name="blendMode_type">The blend mode to use.</param>
        /// <param name="blendAlphaMode_type">What to do with the alpha of drawn objects when blending.</param>
        public static void SetBlendMode(BlendMode blendMode_type, BlendAlphaMode blendAlphaMode_type = BlendAlphaMode.Multiply)
        {
            Love2dDll.wrap_love_dll_graphics_setBlendMode((int)blendMode_type, (int)blendAlphaMode_type);
        }

        /// <summary>
        /// Gets the blending mode.
        /// </summary>
        /// <param name="out_blendMode_type">The current blend mode.</param>
        /// <param name="out_blendAlphaMode_type">The current blend alpha mode – it determines how the alpha of drawn objects affects blending.</param>
        public static void GetBlendMode(out BlendMode out_blendMode_type, out BlendAlphaMode out_blendAlphaMode_type)
        {
            int blendMode_type = 0;
            int blendAlphaMode_type = 0;
            Love2dDll.wrap_love_dll_graphics_getBlendMode(out blendMode_type, out blendAlphaMode_type);
            out_blendMode_type = (BlendMode)blendMode_type;
            out_blendAlphaMode_type = (BlendAlphaMode)blendAlphaMode_type;
        }

        /// <summary>
        /// Sets the default scaling filters used with Images, Canvases, and Fonts.
        /// </summary>
        /// <param name="filterModeMagMin_type">Filter mode used when scaling the image down.</param>
        /// <param name="filterModeMag_type">Filter mode used when scaling the image up.</param>
        /// <param name="anisotropy">Maximum amount of Anisotropic Filtering used.</param>
        public static void SetDefaultFilter(Texture.FilterMode filterModeMagMin_type, Texture.FilterMode filterModeMag_type, int anisotropy = 1)
        {
            Love2dDll.wrap_love_dll_graphics_setDefaultFilter((int)filterModeMagMin_type, (int)filterModeMag_type, anisotropy);
        }

        /// <summary>
        /// Returns the default scaling filters used with Images, Canvases, and Fonts.
        /// </summary>
        /// <param name="out_filterModeMagMin_type">Filter mode used when scaling the image down.</param>
        /// <param name="out_filterModeMag_type">Filter mode used when scaling the image up.</param>
        /// <param name="out_anisotropy">Maximum amount of Anisotropic Filtering used.</param>
        public static void GetDefaultFilter(out Texture.FilterMode out_filterModeMagMin_type, out Texture.FilterMode out_filterModeMag_type, out int out_anisotropy)
        {
            int filterModeMagMin_type = 0;
            int filterModeMag_type = 0;
            Love2dDll.wrap_love_dll_graphics_getDefaultFilter(out filterModeMagMin_type, out filterModeMag_type, out out_anisotropy);
            out_filterModeMagMin_type = (Texture.FilterMode)filterModeMagMin_type;
            out_filterModeMag_type = (Texture.FilterMode)filterModeMag_type;
        }

        /// <summary>
        /// Sets the line width.
        /// </summary>
        /// <param name="width"></param>
        public static void SetLineWidth(float width)
        {
            Love2dDll.wrap_love_dll_graphics_setLineWidth(width);
        }

        /// <summary>
        /// Sets the line style.
        /// </summary>
        /// <param name="style_type"></param>
        public static void SetLineStyle(LineStyle style_type)
        {
            Love2dDll.wrap_love_dll_graphics_setLineStyle((int)style_type);
        }

        /// <summary>
        /// Sets the line join style.
        /// </summary>
        /// <param name="join_type"></param>
        public static void SetLineJoin(LineJoin join_type)
        {
            Love2dDll.wrap_love_dll_graphics_setLineJoin((int)join_type);
        }

        /// <summary>
        /// Gets the current line width.
        /// </summary>
        /// <returns></returns>
        public static float GetLineWidth()
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_graphics_getLineWidth(out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets the line style.
        /// </summary>
        /// <returns></returns>
        public static LineStyle GetLineStyle()
        {
            int out_lineStyle_type = 0;
            Love2dDll.wrap_love_dll_graphics_getLineStyle(out out_lineStyle_type);
            return (LineStyle)out_lineStyle_type;
        }

        /// <summary>
        /// Gets the line join style.
        /// </summary>
        /// <returns></returns>
        public static LineJoin GetLineJoin()
        {
            int out_lineJoinStyle_type = 0;
            Love2dDll.wrap_love_dll_graphics_getLineJoin(out out_lineJoinStyle_type);
            return (LineJoin)out_lineJoinStyle_type;
        }

        /// <summary>
        /// Sets the point size.
        /// </summary>
        /// <param name="size"></param>
        public static void SetPointSize(float size)
        {
            Love2dDll.wrap_love_dll_graphics_setPointSize(size);
        }

        /// <summary>
        /// Gets the point size.
        /// </summary>
        /// <returns></returns>
        public static float GetPointSize()
        {
            float out_size = 0;
            Love2dDll.wrap_love_dll_graphics_getPointSize(out out_size);
            return out_size;
        }
        public static void SetWireframe(bool enable)
        {
            Love2dDll.wrap_love_dll_graphics_setWireframe(enable);
        }

        /// <summary>
        /// 是否使用线框模式绘图
        /// Gets whether wireframe mode is used when drawing.
        /// </summary>
        /// <returns></returns>
        public static bool IsWireframe()
        {
            bool out_isWireFrame = false;
            Love2dDll.wrap_love_dll_graphics_isWireframe(out out_isWireFrame);
            return out_isWireFrame;
        }

        /// <summary>
        /// Captures drawing operations to a Canvas
        /// <para>Sets the render target to a specified Canvas. All drawing operations until the next love.graphics.setCanvas call will be redirected to the Canvas and not shown on the screen.</para>
        /// <para>if Length of canvas is zero, then resets the render target to the screen, i.e. re-enables drawing to the screen.</para>
        /// </summary>
        /// <param name="canvas"></param>
        public static void SetCanvas(params Canvas[] canvas)
        {
            if (canvas == null)
            {
                canvas = new Canvas[0];
            }

            IntPtr[] canvaList = new IntPtr[canvas.Length];
            for (int i = 0; i < canvas.Length; i++)
            {
                canvaList[i] = canvas[i].p;
            }

            Love2dDll.wrap_love_dll_graphics_setCanvas(canvaList, canvaList.Length);
        }

        /// <summary>
        /// Returns the current target Canvas. Returns zero length array if drawing to the real screen.
        /// </summary>
        public static Canvas[] getCanvas()
        {
            IntPtr out_canvaList = IntPtr.Zero;
            int out_canvaList_length = 0;
            Love2dDll.wrap_love_dll_graphics_getCanvas(out out_canvaList, out out_canvaList_length);
            var buffer = DllTool.readIntPtrsAndRelease(out_canvaList, out_canvaList_length);

            Canvas[] canvas = new Canvas[buffer.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                canvas[i] = LoveObject.NewObject<Canvas>(buffer[i]);
            }

            return canvas;
        }

        /// <summary>
        /// Routes drawing operations through a shader.
        /// </summary>
        /// <param name="shader"></param>
        public static void SetShader(Shader shader)
        {
            Love2dDll.wrap_love_dll_graphics_setShader(shader.p);
        }

        /// <summary>
        /// Disables shaders, allowing unfiltered drawing operations.
        /// </summary>
        public static void SetShader()
        {
            Love2dDll.wrap_love_dll_graphics_setShader(IntPtr.Zero);
        }

        /// <summary>
        /// Gets the current Shader. Returns null if none is set.
        /// </summary>
        /// <returns></returns>
        public static Shader GetShader()
        {
            IntPtr out_shader = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_getShader(out out_shader);
            return LoveObject.NewObject<Shader>(out_shader);
        }

        #endregion

        #region graphics Coordinate System

        /// <summary>
        /// Copies and pushes the current coordinate transformation to the transformation stack.
        /// </summary>
        /// <param name="stack"></param>
        public static void Push(StackType stack = StackType.Transform)
        {
            Love2dDll.wrap_love_dll_graphics_push((int)stack);
        }

        /// <summary>
        /// Pops the current coordinate transformation from the transformation stack.
        /// This function is always used to reverse a previous <see cref="Push"/> operation. It returns the current transformation state to what it was before the last preceding push.
        /// </summary>
        public static void Pop()
        {
            Love2dDll.wrap_love_dll_graphics_pop();
        }

        /// <summary>
        /// Rotates the coordinate system in two dimensions.
        /// <para>Calling this function affects all future drawing operations by rotating the coordinate system around the origin by the given amount of radians. This change lasts until Scene.Draw exits.</para>
        /// </summary>
        /// <param name="angle">The amount to rotate the coordinate system in radians.</param>
        public static void Rotate(float angle)
        {
            Love2dDll.wrap_love_dll_graphics_rotate(angle);
        }

        /// <summary>
        /// <para>以二维方式缩放坐标系。</para>
        /// <para>默认情况下，LÖVE中的坐标系在水平和垂直方向上一对一显示像素，x轴向右增加，y轴向下增加。 缩放坐标系会改变这种关系。</para>
        /// <para>在通过sx和sy进行缩放之后，所有坐标都被视为与sx和sy相乘。 绘图操作的每个结果也相应地缩放，例如按（2,2）缩放将意味着在x和y方向上使所有内容都变为两倍。 按负值缩放会使坐标系在相应的方向上翻转，所有内容都会被翻转或颠倒（或两者兼而有之）。 按零缩放没有意义。</para>
        /// <para>缩放(Scale)操作和平移(Translate)操作不是可交换操作，以不同的顺序调用它们会产生不同的结果。</para>
        /// <para>效果持续到 Scene.Draw 调用结束（每一帧画面绘制结束都会自动重置为1倍。）</para>
        /// 
        /// <para>Scales the coordinate system in two dimensions.</para> 
        /// <para>By default the coordinate system in LÖVE corresponds to the display pixels in horizontal and vertical directions one-to-one, and the x-axis increases towards the right while the y-axis increases downwards. Scaling the coordinate system changes this relation.</para>
        /// <para>After scaling by sx and sy, all coordinates are treated as if they were multiplied by sx and sy. Every result of a drawing operation is also correspondingly scaled, so scaling by (2, 2) for example would mean making everything twice as large in both x- and y-directions. Scaling by a negative value flips the coordinate system in the corresponding direction, which also means everything will be drawn flipped or upside down, or both. Scaling by zero is not a useful operation.</para>
        /// <para>Scale and translate are not commutative operations, therefore, calling them in different orders will change the outcome.</para>
        /// <para>Scaling lasts until Scene.Draw exits.</para>
        /// </summary>
        /// <param name="sx">The scaling in the direction of the x-axis.</param>
        /// <param name="sy">The scaling in the direction of the y-axis.</param>
        public static void Scale(float sx, float sy)
        {
            Love2dDll.wrap_love_dll_graphics_scale(sx, sy);
        }

        /// <summary>
        /// Translates the coordinate system in two dimensions.
        /// <para>When this function is called with two numbers, dx, and dy, all the following drawing operations take effect as if their x and y coordinates were x+dx and y+dy.</para>
        /// <para>Scale and translate are not commutative operations, therefore, calling them in different orders will change the outcome.</para>
        /// <para>This change lasts until Scene.Draw exits or else a <see cref="Graphics.Pop "/> reverts to a previous <see cref="Graphics.Push"/> .</para>
        /// <para>Translating using whole numbers will prevent tearing/blurring of images and fonts draw after translating.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void Translate(float x, float y)
        {
            Love2dDll.wrap_love_dll_graphics_translate(x, y);
        }

        /// <summary>
        /// Shears the coordinate system.
        /// </summary>
        /// <param name="kx"></param>
        /// <param name="ky"></param>
        public static void Shear(float kx, float ky)
        {
            Love2dDll.wrap_love_dll_graphics_shear(kx, ky);
        }

        /// <summary>
        /// Resets the current coordinate transformation.
        /// </summary>
        public static void Origin()
        {
            Love2dDll.wrap_love_dll_graphics_origin();
        }

        #endregion

        #region graphics Drwing

        /// <summary>
        /// Draws geometry as a stencil.
        /// <para> The geometry drawn by the supplied function sets invisible stencil values of pixels, instead of setting pixel colors. The stencil buffer (which contains those stencil values) can act like a mask / stencil - love.graphics.setStencilTest can be used afterward to determine how further rendering is affected by the stencil values in each pixel.</para>
        /// <para> Stencil values are integers within the range of [0, 255].</para>
        /// <para> Starting with version 11.0, a stencil buffer must be set or requested in love.graphics.setCanvas when using stencils with a Canvas. love.graphics.setCanvas{canvas, stencil=true} is an easy way to use an automatically provided temporary stencil buffer in that case.</para>
        /// https://love2d.org/wiki/love.graphics.stencil
        /// </summary>
        /// <param name="actionFunc">Function which draws geometry. The stencil values of pixels, rather than the color of each pixel, will be affected by the geometry.</param>
        /// <param name="stencilAction">How to modify any stencil values of pixels that are touched by what's drawn in the stencil function.</param>
        /// <param name="value">The new stencil value to use for pixels if the "replace" stencil action is used. Has no effect with other stencil actions. Must be between 0 and 255.</param>
        /// <param name="keepValue">True to preserve old stencil values of pixels, false to re-set every pixel's stencil value to 0 before executing the stencil function. love.graphics.clear will also re-set all stencil values.</param>
        public static void Stencil(Action actionFunc, StencilAction stencilAction = StencilAction.Replace, int value = 1, bool keepValue = false)
        {
            if (!keepValue)
            {
                Graphics.Clear(0, 0, 0, 0);
            }

            Love2dDll.wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer((int)stencilAction, value);

            actionFunc?.Invoke();

            Love2dDll.wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer();
        }

        public static void Clear(float r, float g, float b, float a = 1, float stencil = 0, bool enable_stencil = true, float depth = 1, bool enable_depth = true)
        {
            Love2dDll.wrap_love_dll_graphics_clear_rgba(r, g, b, a, stencil, enable_stencil, depth, enable_depth);
        }

        private static void Clear(Float4[] colorList, bool[] enableList, float stencil = 0, bool enable_stencil = true, float depth = 1, bool enable_depth = true)
        {
            if (colorList.Length != enableList.Length)
            {
                throw new Exception("length of colorList and enableList should same !");
            }
            Love2dDll.wrap_love_dll_graphics_clear_rgbalist(colorList, enableList, colorList.Length, stencil, enable_stencil, depth, enable_depth);
        }

        /// <summary>
        /// Discards (trashes) the contents of the screen or active Canvas. This is a performance optimization function with niche use cases.
        /// </summary>
        /// <param name="discardColors">An array containing boolean values indicating whether to discard the texture of each active Canvas, when multiple simultaneous Canvases are active.</param>
        /// <param name="discardStencil">Whether to discard the contents of the stencil buffer of the screen / active Canvas.</param>
        public static void Discard(bool[] discardColors, bool discardStencil)
        {
            Love2dDll.wrap_love_dll_graphics_discard(discardColors, discardColors.Length, discardStencil);
        }

        /// <summary>
        /// Displays the results of drawing operations on the screen.
        /// This function is used when writing your own Boot.Run function. It presents all the results of your drawing operations on the screen. See the example in Boot.Run for a typical use of this function.
        /// </summary>
        public static void Present()
        {
            Love2dDll.wrap_love_dll_graphics_present();
        }

        /// <summary>
        /// Draws a Drawable object (an Image, Canvas, SpriteBatch, ParticleSystem, Mesh, Text object, or Video) on the screen with optional rotation, scaling and shearing.
        /// </summary>
        /// <param name="drawable">A drawable object.</param>
        /// <param name="x">The position to draw the object (x-axis).</param>
        /// <param name="y">The position to draw the object (y-axis).</param>
        /// <param name="angle">Orientation (radians).</param>
        /// <param name="sx">Scale factor (x-axis).</param>
        /// <param name="sy">Scale factor (y-axis).</param>
        /// <param name="ox">Origin offset (x-axis).</param>
        /// <param name="oy">Origin offset (y-axis).</param>
        /// <param name="kx">Shearing factor (x-axis).</param>
        /// <param name="ky">Shearing factor (y-axis).</param>
        /// <returns></returns>
        public static void Draw(Drawable drawable, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            Love2dDll.wrap_love_dll_graphics_draw_drawable(drawable.p, x, y, angle, sx, sy, ox, oy, kx, ky);
        }
        public static void Draw(Quad quad, Texture texture, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            Love2dDll.wrap_love_dll_graphics_draw_texture_quad(quad.p, texture.p, x, y, angle, sx, sy, ox, oy, kx, ky);
        }

        /// <summary>
        /// Draws text on screen. If no Font is set, one will be created and set (once) if needed.
        /// As of LOVE 0.7.1, when using translation and scaling functions while drawing text, this function assumes the scale occurs first. If you don't script with this in mind, the text won't be in the right position, or possibly even on screen.
        /// Love.Graphics.Print and Love.Graphics.Printf both support UTF-8 encoding. You'll also need a proper Font for special characters.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="x">The position to draw the object (x-axis).</param>
        /// <param name="y">The position to draw the object (y-axis).</param>
        /// <param name="angle">Orientation (radians).</param>
        /// <param name="sx">Scale factor (x-axis).</param>
        /// <param name="sy">Scale factor (y-axis).</param>
        /// <param name="ox">Origin offset (x-axis).</param>
        /// <param name="oy">Origin offset (y-axis).</param>
        /// <param name="kx">Shearing factor (x-axis).</param>
        /// <param name="ky">Shearing factor (y-axis).</param>
        public static void Print(string text, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            var coloredStr = ColoredStringArray.Create(text);

            coloredStr.ExecResource((tmp) =>{
                Love2dDll.wrap_love_dll_graphics_print(tmp.Item1, tmp.Item2, coloredStr.Length, x, y, angle, sx, sy, ox, oy, kx, ky);
            });
        }

        /// <summary>
        /// Same as Love.Graphics.Print(string...), but coloredStr used to show text in different color.
        /// </summary>
        /// <param name="coloredStr">colors and strings </param>
        public static void Print(ColoredStringArray coloredStr, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            coloredStr.ExecResource((tmp) =>{
                Love2dDll.wrap_love_dll_graphics_print(tmp.Item1, tmp.Item2, coloredStr.Length, x, y, angle, sx, sy, ox, oy, kx, ky);
            });
        }

        public static void Printf(ColoredStringArray coloredStr, float x, float y, float wrap, Font.AlignMode align_type, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            coloredStr.ExecResource((tmp) =>{
                Love2dDll.wrap_love_dll_graphics_printf(tmp.Item1, tmp.Item2, coloredStr.Length, x, y, wrap, (int)align_type, angle, sx, sy, ox, oy, kx, ky);
            });
        }

        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="mode_type">How to draw the rectangle.</param>
        /// <param name="x">The position of top-left corner along the x-axis.</param>
        /// <param name="y">The position of top-left corner along the y-axis.</param>
        /// <param name="w">Width of the rectangle.</param>
        /// <param name="h">Height of the rectangle.</param>
        public static void Rectangle(DrawMode mode_type, float x, float y, float w, float h)
        {
            Love2dDll.wrap_love_dll_graphics_rectangle((int)mode_type, x, y, w, h);
        }

        /// <summary>
        /// Draws a rectangle with rounded corners.
        /// </summary>
        /// <param name="mode_type">How to draw the rectangle.</param>
        /// <param name="x">The position of top-left corner along the x-axis.</param>
        /// <param name="y">The position of top-left corner along the y-axis.</param>
        /// <param name="w">Width of the rectangle.</param>
        /// <param name="h">Height of the rectangle.</param>
        /// <param name="rx">The x-axis radius of each round corner. Cannot be greater than half the rectangle's width.</param>
        /// <param name="ry">The y-axis radius of each round corner. Cannot be greater than half the rectangle's height.</param>
        /// <param name="points">The number of segments used for drawing the round corners. A default amount will be chosen if no number is given.</param>
        public static void Rectangle(DrawMode mode_type, float x, float y, float w, float h, float rx, float ry, int points = 10)
        {
            Love2dDll.wrap_love_dll_graphics_rectangle_with_rounded_corners((int)mode_type, x, y, w, h, rx, ry, points = 10);
        }

        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="mode_type">How to draw the circle.</param>
        /// <param name="x">The position of the center along x-axis.</param>
        /// <param name="y">The position of the center along y-axis.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="points">The number of segments used for drawing the circle. Note: The default variable for the segments parameter varies between different versions of LÖVE.</param>
        /// <returns></returns>
        public static void Circle(DrawMode mode_type, float x, float y, float radius, int points = 10)
        {
            Love2dDll.wrap_love_dll_graphics_circle((int)mode_type, x, y, radius, points);
        }

        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="mode_type">How to draw the ellipse.</param>
        /// <param name="x">he position of the center along x-axis.</param>
        /// <param name="y">he position of the center along y-axis.</param>
        /// <param name="radiusX">The radius of the ellipse along the x-axis (half the ellipse's width).</param>
        /// <param name="radiusY">The radius of the ellipse along the y-axis (half the ellipse's height).</param>
        /// <param name="points">The number of segments used for drawing the ellipse.</param>
        /// <returns></returns>
        public static void Ellipse(DrawMode mode_type, float x, float y, float radiusX, float radiusY, int points = 10)
        {
            Love2dDll.wrap_love_dll_graphics_ellipse((int)mode_type, x, y, radiusX, radiusY, points);
        }
        public static void Arc(DrawMode mode_type, ArcType arcmode_type, float x, float y, float radius, float angle1, float angle2)
        {
            Love2dDll.wrap_love_dll_graphics_arc((int)mode_type, (int)arcmode_type, x, y, radius, angle1, angle2);
        }
        public static void Arc(DrawMode mode_type, ArcType arcmode_type, float x, float y, float radius, float angle1, float angle2, int points = 10)
        {
            Love2dDll.wrap_love_dll_graphics_arc_points((int)mode_type, (int)arcmode_type, x, y, radius, angle1, angle2, points);
        }

        /// <summary>
        /// Draws one or more points.
        /// </summary>
        /// <param name="coords">The position of the each point</param>
        public static void Points(params Float2[] coords)
        {
            Love2dDll.wrap_love_dll_graphics_points(coords, coords.Length);
        }

        public static void Points(Float2[] coords, Int4[] colors)
        {
            if (coords.Length != colors.Length)
            {
                throw new Exception("length of coords and colors should same");
            }
            Love2dDll.wrap_love_dll_graphics_points_colors(coords, colors, coords.Length);
        }

        /// <summary>
        /// Draws lines between points.
        /// </summary>
        /// <param name="coords">A array of point positions.</param>
        /// <returns></returns>
        public static void Line(params Float2[] coords)
        {
            Love2dDll.wrap_love_dll_graphics_line(coords, coords.Length);
        }

        /// <summary>
        /// Draw a polygon.
        /// When in fill mode, the polygon must be convex and simple or rendering artifacts may occur. Love.Mathf.Triangulate and Love.Mathf.IsConvex can be used in 0.9.0+.
        /// </summary>
        /// <param name="mode_type">How to draw the polygon.</param>
        /// <param name="coords">The vertices of the polygon.</param>
        public static void Polygon(DrawMode mode_type, params Float2[] coords)
        {
            Love2dDll.wrap_love_dll_graphics_polygon((int)mode_type, coords, coords.Length);
        }

        #endregion

        #region graphics Window

        public static bool IsCreated()
        {
            bool out_reslut = false;
            Love2dDll.wrap_love_dll_graphics_isCreated(out out_reslut);
            return out_reslut;
        }


        /// <summary>
        /// Gets the DPI scale factor of the window.
        /// <para>The DPI scale factor represents relative pixel density. The pixel density inside the window might be greater (or smaller) than the "size" of the window. For example on a retina screen in Mac OS X with the highdpi window flag enabled, the window may take up the same physical size as an 800x600 window, but the area inside the window uses 1600x1200 pixels. love.graphics.getDPIScale() would return 2 in that case.</para>
        /// <para>The love.window.fromPixels and love.window.toPixels functions can also be used to convert between units.</para>
        /// <para>The highdpi window flag must be enabled to use the full pixel density of a Retina screen on Mac OS X and iOS. The flag currently does nothing on Windows and Linux, and on Android it is effectively always enabled.</para>
        /// </summary>
        /// <returns></returns>
        public static float GetDPIScale()
        {
            double out_dpiscale = 0;
            Love2dDll.wrap_love_dll_graphics_getDPIScale(out out_dpiscale);
            return (float)out_dpiscale;
        }

        /// <summary>
        /// Gets the width in pixels of the window.
        /// </summary>
        /// <returns></returns>
        public static int GetWidth()
        {
            int out_width = 0;
            Love2dDll.wrap_love_dll_graphics_getWidth(out out_width);
            return out_width;
        }

        /// <summary>
        /// Gets the height in pixels of the window.
        /// </summary>
        /// <returns></returns>
        public static int GetHeight()
        {
            int out_height = 0;
            Love2dDll.wrap_love_dll_graphics_getHeight(out out_height);
            return out_height;
        }


        #endregion

        #region graphics System Information

        /// <summary>
        /// Gets the optional graphics features and whether they're supported.
        /// </summary>
        /// <param name="feature_type"></param>
        /// <returns></returns>
        public static bool GetSupported(Feature feature_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_getSupported((int)feature_type, out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets the available Canvas formats, and whether each is supported.
        /// </summary>
        /// <param name="format_type"></param>
        /// <returns></returns>
        public static bool GetCanvasFormats(PixelFormat format_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_getCanvasFormats((int)format_type, out out_result);
            return out_result;
        }
        public static bool GetImageFormats(PixelFormat format_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_getImageFormats((int)format_type, out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets information about the system's video card and drivers.
        /// 
        /// <para>	Almost everything returned by this function is highly dependent on the system running the code and should probably not be used to make run-time decisions</para>
        /// </summary>
        /// <param name="name">The name of the renderer, e.g. "OpenGL" or "OpenGL ES".</param>
        /// <param name="version">The version of the renderer with some extra driver-dependent version info, e.g. "2.1 INTEL-8.10.44".</param>
        /// <param name="vendor">The name of the graphics card vendor, e.g. "Intel Inc".</param>
        /// <param name="device">The name of the graphics card, e.g. "Intel HD Graphics 3000 OpenGL Engine".</param>
        public static void GetRendererInfo(out string name, out string version, out string vendor, out string device)
        {
            IntPtr out_wss;
            Love2dDll.wrap_love_dll_graphics_getRendererInfo(out out_wss);
            var infos = DllTool.WSSToStringListAndRelease(out_wss);

            name = infos[0];
            version = infos[1];
            vendor = infos[2];
            device = infos[3];
        }

        /// <summary>
        /// Gets the system-dependent maximum values for love.graphics features.
        /// </summary>
        /// <param name="limit_type"></param>
        /// <returns></returns>
        public static double GetSystemLimits(SystemLimit limit_type)
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_graphics_getSystemLimits((int)limit_type, out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets performance-related rendering statistics.
        /// <para>	The per-frame metrics (drawcalls, canvasswitches, shaderswitches) are reset by love.graphics.present, which for the default implementation of <see cref="Boot.Run"/> is called right after the execution of Scene.Draw. Therefore this function should probably be called at the end of Scene.Draw.</para>
        /// </summary>
        /// <param name="out_drawCalls">The number of draw calls made so far during the current frame.</param>
        /// <param name="out_canvasSwitches">The number of times the active Canvas has been switched so far during the current frame.</param>
        /// <param name="out_shaderSwitches">The number of times the active Shader has been changed so far during the current frame.</param>
        /// <param name="out_canvases">The number of Canvas objects currently loaded.</param>
        /// <param name="out_images">The number of Image objects currently loaded.</param>
        /// <param name="out_fonts">The number of Font objects currently loaded.</param>
        /// <param name="out_textureMemory">The estimated total size in bytes of video memory used by all loaded Images, Canvases, and Fonts.</param>
        public static void GetStats(out int out_drawCalls, out int out_canvasSwitches, out int out_shaderSwitches, out int out_canvases, out int out_images, out int out_fonts, out int out_textureMemory)
        {
            Love2dDll.wrap_love_dll_graphics_getStats(out out_drawCalls, out out_canvasSwitches, out out_shaderSwitches, out out_canvases, out out_images, out out_fonts, out out_textureMemory);
        }

        #endregion
    }
}
