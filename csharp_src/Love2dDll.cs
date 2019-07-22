// Author : endlesstravel
// this part provide C# interactive with C API

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


using size_t = System.UInt32;
using Int64 = System.Int64;
using UInt8 = System.Byte;
using BytePtr = System.IntPtr;

namespace Love
{
    
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal delegate float WrapShapeComputeMassCallbackDelegate(float x, float y, float mass, float inertia);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate float WrapShapeComputeAABBCallbackDelegate(float lx, float ly, float ux, float uy);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate float WrapShapeRayCastCallbackDelegate(float nx, float ny, float fraction);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate float WrapWorldRayCastCallbackDelegate(IntPtr pfixture, float x, float y, float nx, float ny, float fraction);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate bool WrapWorldQueryBoundingBoxCallbackDelegate(IntPtr pfixture);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void WrapWorldContactCallbackDelegate(IntPtr fixtureA, IntPtr fixtureB, IntPtr contact, IntPtr impluseArray, int impluseArrayLength);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate bool WrapWorldContactFilterCallbackDelegate(IntPtr fixtureA, IntPtr fixtureB);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int WrapCSharpCommunicationFuncDelegate(IntPtr strPtr);


    class Love2dDll
    {
        //[MethodImpl(MethodImplOptions.AggressiveInlining)] // .NET 4.5
        static bool CheckCAPIException(bool hasNoException)
        {
            if (hasNoException == false)
            {
                string errInfo = DllTool.GetLoveLastError();
                throw new Exception(errInfo);
            }

            return hasNoException;
        }

        const string DllPath = @"love";


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_c_size_info();

        #region platform
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_get_win32_handle(out IntPtr out_str);
        #endregion

        #region common
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_common_getVersion")]
        internal extern static void _wrap_love_dll_common_getVersion(out IntPtr out_str);
        internal static void wrap_love_dll_common_getVersion(out IntPtr out_str)
        {
            _wrap_love_dll_common_getVersion(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_common_getVersionCodeName")]
        internal extern static void _wrap_love_dll_common_getVersionCodeName(out IntPtr out_str);
        internal static void wrap_love_dll_common_getVersionCodeName(out IntPtr out_str)
        {
            _wrap_love_dll_common_getVersionCodeName(out out_str);
        }

        #endregion

        #region release delete region
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_last_error")]
        internal extern static void _wrap_love_dll_last_error(out IntPtr out_errormsg);
        internal static void wrap_love_dll_last_error(out IntPtr out_errormsg)
        {
            _wrap_love_dll_last_error(out out_errormsg);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_release_obj")]
        internal extern static void _wrap_love_dll_release_obj(IntPtr p);
        internal static void wrap_love_dll_release_obj(IntPtr p)
        {
            _wrap_love_dll_release_obj(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_retain_obj")]
        internal extern static void _wrap_love_dll_retain_obj(IntPtr p);
        internal static void wrap_love_dll_retain_obj(IntPtr p) // danger !!!!
        {
            _wrap_love_dll_retain_obj(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_delete")]
        internal extern static void _wrap_love_dll_delete(IntPtr p);
        internal static void wrap_love_dll_delete(IntPtr p)
        {
            _wrap_love_dll_delete(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_delete_array")]
        internal extern static void _wrap_love_dll_delete_array(IntPtr p);
        internal static void wrap_love_dll_delete_array(IntPtr p)
        {
            _wrap_love_dll_delete_array(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_delete_WrapString")]
        internal extern static void _wrap_love_dll_delete_WrapString(IntPtr ws);
        internal static void wrap_love_dll_delete_WrapString(IntPtr ws)
        {
            _wrap_love_dll_delete_WrapString(ws);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_delete_WrapSequenceString")]
        internal extern static void _wrap_love_dll_delete_WrapSequenceString(IntPtr wss);
        internal static void wrap_love_dll_delete_WrapSequenceString(IntPtr wss)
        {
            _wrap_love_dll_delete_WrapSequenceString(wss);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_delete_WrapSequenceInt2")]
        internal extern static void _wrap_love_dll_delete_WrapSequenceInt2(IntPtr wsi2);
        internal static void wrap_love_dll_delete_WrapSequenceInt2(IntPtr wsi2)
        {
            _wrap_love_dll_delete_WrapSequenceInt2(wsi2);
        }

        #endregion

        #region lua

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_init")]
        internal extern static bool _wrap_love_dll_luasupport_init(IntPtr L, WrapCSharpCommunicationFuncDelegate wrapCSharpCommunicationFunc);
        internal static bool wrap_love_dll_luasupport_init(IntPtr L, WrapCSharpCommunicationFuncDelegate func)
        {
            return CheckCAPIException(_wrap_love_dll_luasupport_init(L, func));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_doString")]
        internal extern static bool _wrap_love_dll_luasupport_doString(byte[] strCode);
        internal static bool wrap_love_dll_luasupport_doString(byte[] strCode)
        {
            return CheckCAPIException(_wrap_love_dll_luasupport_doString(strCode));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_doFile")]
        internal extern static bool _wrap_love_dll_luasupport_doFile(byte[] strCode);
        internal static bool wrap_love_dll_luasupport_doFile(byte[] strCode)
        {
            return CheckCAPIException(_wrap_love_dll_luasupport_doFile(strCode));
        }


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_debugStackDump")]
        internal extern static void _wrap_love_dll_luasupport_debugStackDump();
        internal static void wrap_love_dll_luasupport_debugStackDump()
        {
            _wrap_love_dll_luasupport_debugStackDump();
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_getTop")]
        internal extern static void _wrap_love_dll_luasupport_getTop(out int result);
        internal static void wrap_love_dll_luasupport_getTop(out int result)
        {
            _wrap_love_dll_luasupport_getTop(out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_setTop")]
        internal extern static void _wrap_love_dll_luasupport_setTop(int idx);
        internal static void wrap_love_dll_luasupport_setTop(int idx)
        {
            _wrap_love_dll_luasupport_setTop(idx);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_checkToString")]
        internal extern static void _wrap_love_dll_luasupport_checkToString(int index, out IntPtr result);
        internal static void wrap_love_dll_luasupport_checkToString(int index, out IntPtr result)
        {
            _wrap_love_dll_luasupport_checkToString(index, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_checkToNumber")]
        internal extern static void _wrap_love_dll_luasupport_checkToNumber(int index, out float result);
        internal static void wrap_love_dll_luasupport_checkToNumber(int index, out float result)
        {
            _wrap_love_dll_luasupport_checkToNumber(index, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_checkToInteger")]
        internal extern static void _wrap_love_dll_luasupport_checkToInteger(int index, out int result);
        internal static void wrap_love_dll_luasupport_checkToInteger(int index, out int result)
        {
            _wrap_love_dll_luasupport_checkToInteger(index, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_checkToBoolean")]
        internal extern static void _wrap_love_dll_luasupport_checkToBoolean(int index, out bool result);
        internal static void wrap_love_dll_luasupport_checkToBoolean(int index, out bool result)
        {
            _wrap_love_dll_luasupport_checkToBoolean(index, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_isTable")]
        internal extern static void _wrap_love_dll_luasupport_isTable(int index, out bool result);
        internal static void wrap_love_dll_luasupport_isTable(int index, out bool result)
        {
            _wrap_love_dll_luasupport_isTable(index, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_pushInteger")]
        internal extern static void _wrap_love_dll_luasupport_pushInteger(int num);
        internal static void wrap_love_dll_luasupport_pushInteger(int num)
        {
            _wrap_love_dll_luasupport_pushInteger(num);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_pushNumber")]
        internal extern static void _wrap_love_dll_luasupport_pushNumber(float num);
        internal static void wrap_love_dll_luasupport_pushNumber(float num)
        {
            _wrap_love_dll_luasupport_pushNumber(num);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_pushBoolean")]
        internal extern static void _wrap_love_dll_luasupport_pushBoolean(bool v);
        internal static void wrap_love_dll_luasupport_pushBoolean(bool v)
        {
            _wrap_love_dll_luasupport_pushBoolean(v);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_pushString")]
        internal extern static void _wrap_love_dll_luasupport_pushString(byte[] str);
        internal static void wrap_love_dll_luasupport_pushString(byte[] str)
        {
            _wrap_love_dll_luasupport_pushString(str);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_pushIntegerArray")]
        internal extern static void _wrap_love_dll_luasupport_pushIntegerArray(int[] num, int num_length);
        internal static void wrap_love_dll_luasupport_pushIntegerArray(int[] num, int num_length)
        {
            _wrap_love_dll_luasupport_pushIntegerArray(num, num_length);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_pushNumberArray")]
        internal extern static void _wrap_love_dll_luasupport_pushNumberArray(float[] num, int num_length);
        internal static void wrap_love_dll_luasupport_pushNumberArray(float[] num, int num_length)
        {
            _wrap_love_dll_luasupport_pushNumberArray(num, num_length);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_pushBooleanArray")]
        internal extern static void _wrap_love_dll_luasupport_pushBooleanArray(bool[] num, int num_length);
        internal static void wrap_love_dll_luasupport_pushBooleanArray(bool[] num, int num_length)
        {
            _wrap_love_dll_luasupport_pushBooleanArray(num, num_length);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_pushStringArray")]
        internal extern static void _wrap_love_dll_luasupport_pushStringArray(BytePtr[] textList, int num_length);
        internal static void wrap_love_dll_luasupport_pushStringArray(BytePtr[] textList, int num_length)
        {
            _wrap_love_dll_luasupport_pushStringArray(textList, num_length);
        }

       

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_checkToArrayBoolean")]
        internal extern static void _wrap_love_dll_luasupport_checkToArrayBoolean(int index, out IntPtr result, out int length);
        internal static void wrap_love_dll_luasupport_checkToArrayBoolean(int index, out IntPtr result, out int length)
        {
            _wrap_love_dll_luasupport_checkToArrayBoolean(index, out result, out length);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_checkToArrayInt")]
        internal extern static void _wrap_love_dll_luasupport_checkToArrayInt(int index, out IntPtr result, out int length);
        internal static void wrap_love_dll_luasupport_checkToArrayInt(int index, out IntPtr result, out int length)
        {
            _wrap_love_dll_luasupport_checkToArrayInt(index, out result, out length);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_checkToArrayNumber")]
        internal extern static void _wrap_love_dll_luasupport_checkToArrayNumber(int index, out IntPtr result, out int length);
        internal static void wrap_love_dll_luasupport_checkToArrayNumber(int index, out IntPtr result, out int length)
        {
            _wrap_love_dll_luasupport_checkToArrayNumber(index, out result, out length);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_luasupport_checkToArrayString")]
        internal extern static void _wrap_love_dll_luasupport_checkToArrayString(int index, out IntPtr result);
        internal static void wrap_love_dll_luasupport_checkToArrayString(int index, out IntPtr result)
        {
            _wrap_love_dll_luasupport_checkToArrayString(index, out result);
        }
        #endregion

        #region *new*
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_newFile")]
        internal extern static bool _wrap_love_dll_filesystem_newFile(byte[] filename, int fmode, out IntPtr out_file);
        internal static bool wrap_love_dll_filesystem_newFile(byte[] filename, int fmode, out IntPtr out_file)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_newFile(filename, fmode, out out_file));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_newFileData_content")]
        internal extern static bool _wrap_love_dll_filesystem_newFileData_content(byte[] contents, int contents_length, byte[] filename, out IntPtr out_filedata);
        internal static bool wrap_love_dll_filesystem_newFileData_content(byte[] contents, int contents_length, byte[] filename, out IntPtr out_filedata)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_newFileData_content(contents, contents_length, filename, out out_filedata));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_newFileData_file")]
        internal extern static bool _wrap_love_dll_filesystem_newFileData_file(IntPtr file, out IntPtr out_filedata);
        internal static bool wrap_love_dll_filesystem_newFileData_file(IntPtr file, out IntPtr out_filedata)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_newFileData_file(file, out out_filedata));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_sound_newDecoder_filedata")]
        internal extern static bool _wrap_love_dll_sound_newDecoder_filedata(IntPtr filedata, int bufferSize, out IntPtr out_Decoder);
        internal static bool wrap_love_dll_sound_newDecoder_filedata(IntPtr filedata, int bufferSize, out IntPtr out_Decoder)
        {
            return CheckCAPIException(_wrap_love_dll_sound_newDecoder_filedata(filedata, bufferSize, out out_Decoder));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_sound_newDecoder_file")]
        internal extern static bool _wrap_love_dll_sound_newDecoder_file(IntPtr filedata, int bufferSize, out IntPtr out_Decoder);
        internal static bool wrap_love_dll_sound_newDecoder_file(IntPtr filedata, int bufferSize, out IntPtr out_Decoder)
        {
            return CheckCAPIException(_wrap_love_dll_sound_newDecoder_file(filedata, bufferSize, out out_Decoder));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_sound_newSoundData_decoder")]
        internal extern static bool _wrap_love_dll_sound_newSoundData_decoder(IntPtr decoder, out IntPtr out_SoundData);
        internal static bool wrap_love_dll_sound_newSoundData_decoder(IntPtr decoder, out IntPtr out_SoundData)
        {
            return CheckCAPIException(_wrap_love_dll_sound_newSoundData_decoder(decoder, out out_SoundData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_sound_newSoundData")]
        internal extern static bool _wrap_love_dll_sound_newSoundData(int samples, int rate, int bits, int channels, out IntPtr out_SoundData);
        internal static bool wrap_love_dll_sound_newSoundData(int samples, int rate, int bits, int channels, out IntPtr out_SoundData)
        {
            return CheckCAPIException(_wrap_love_dll_sound_newSoundData(samples, rate, bits, channels, out out_SoundData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_newSource_decoder")]
        internal extern static bool _wrap_love_dll_audio_newSource_decoder(IntPtr decoder, int type, out IntPtr out_source);
        internal static bool wrap_love_dll_audio_newSource_decoder(IntPtr decoder, int type, out IntPtr out_source)
        {
            return CheckCAPIException(_wrap_love_dll_audio_newSource_decoder(decoder, type, out out_source));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_newSource_sounddata")]
        internal extern static bool _wrap_love_dll_audio_newSource_sounddata(IntPtr sd, out IntPtr out_source);
        internal static bool wrap_love_dll_audio_newSource_sounddata(IntPtr sd, out IntPtr out_source)
        {
            return CheckCAPIException(_wrap_love_dll_audio_newSource_sounddata(sd, out out_source));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_newCursor")]
        internal extern static bool _wrap_love_dll_mouse_newCursor(IntPtr imageData, int hotx, int hoty, out IntPtr out_cursor);
        internal static bool wrap_love_dll_mouse_newCursor(IntPtr imageData, int hotx, int hoty, out IntPtr out_cursor)
        {
            return CheckCAPIException(_wrap_love_dll_mouse_newCursor(imageData, hotx, hoty, out out_cursor));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_image_newImageData_wh_format_data")]
        internal extern static bool _wrap_love_dll_image_newImageData_wh_format_data(int w, int h, byte[] data, int dataLength, int format_type, out IntPtr out_imagedata);
        internal static bool wrap_love_dll_image_newImageData_wh_format_data(int w, int h, byte[] data, int dataLength, int format_type, out IntPtr out_imagedata)
        {
            return CheckCAPIException(_wrap_love_dll_image_newImageData_wh_format_data(w, h, data, dataLength, format_type, out out_imagedata));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_image_newImageData_filedata")]
        internal extern static bool _wrap_love_dll_image_newImageData_filedata(IntPtr fileData, out IntPtr out_imagedata);
        internal static bool wrap_love_dll_image_newImageData_filedata(IntPtr fileData, out IntPtr out_imagedata)
        {
            return CheckCAPIException(_wrap_love_dll_image_newImageData_filedata(fileData, out out_imagedata));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_image_newCompressedData")]
        internal extern static bool _wrap_love_dll_image_newCompressedData(IntPtr fileData, out IntPtr out_imagedata);
        internal static bool wrap_love_dll_image_newCompressedData(IntPtr fileData, out IntPtr out_imagedata)
        {
            return CheckCAPIException(_wrap_love_dll_image_newCompressedData(fileData, out out_imagedata));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newRasterizer")]
        internal extern static bool _wrap_love_dll_font_newRasterizer(IntPtr fileData, out IntPtr out_reasterizer);
        internal static bool wrap_love_dll_font_newRasterizer(IntPtr fileData, out IntPtr out_reasterizer)
        {
            return CheckCAPIException(_wrap_love_dll_font_newRasterizer(fileData, out out_reasterizer));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newTrueTypeRasterizer")]
        internal extern static bool _wrap_love_dll_font_newTrueTypeRasterizer(int size, int hinting_type, out IntPtr out_reasterizer);
        internal static bool wrap_love_dll_font_newTrueTypeRasterizer(int size, int hinting_type, out IntPtr out_reasterizer)
        {
            return CheckCAPIException(_wrap_love_dll_font_newTrueTypeRasterizer(size, hinting_type, out out_reasterizer));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newTrueTypeRasterizer_data")]
        internal extern static bool _wrap_love_dll_font_newTrueTypeRasterizer_data(IntPtr data, int size, int hinting_type, out IntPtr out_reasterizer);
        internal static bool wrap_love_dll_font_newTrueTypeRasterizer_data(IntPtr data, int size, int hinting_type, out IntPtr out_reasterizer)
        {
            return CheckCAPIException(_wrap_love_dll_font_newTrueTypeRasterizer_data(data, size, hinting_type, out out_reasterizer));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newBMFontRasterizer")]
        internal extern static bool _wrap_love_dll_font_newBMFontRasterizer(IntPtr fileData, IntPtr[] datas, int dataLength, out IntPtr out_reasterizer);
        internal static bool wrap_love_dll_font_newBMFontRasterizer(IntPtr fileData, IntPtr[] datas, int dataLength, out IntPtr out_reasterizer)
        {
            return CheckCAPIException(_wrap_love_dll_font_newBMFontRasterizer(fileData, datas, dataLength, out out_reasterizer));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newImageRasterizer")]
        internal extern static bool _wrap_love_dll_font_newImageRasterizer(IntPtr imageData, byte[] glyphsStr, int extraspacing, out IntPtr out_reasterizer);
        internal static bool wrap_love_dll_font_newImageRasterizer(IntPtr imageData, byte[] glyphsStr, int extraspacing, out IntPtr out_reasterizer)
        {
            return CheckCAPIException(_wrap_love_dll_font_newImageRasterizer(imageData, glyphsStr, extraspacing, out out_reasterizer));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newGlyphData_rasterizer_str")]
        internal extern static bool _wrap_love_dll_font_newGlyphData_rasterizer_str(IntPtr r, byte[] glyphStr, out IntPtr out_GlyphData);
        internal static bool wrap_love_dll_font_newGlyphData_rasterizer_str(IntPtr r, byte[] glyphStr, out IntPtr out_GlyphData)
        {
            return CheckCAPIException(_wrap_love_dll_font_newGlyphData_rasterizer_str(r, glyphStr, out out_GlyphData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newGlyphData_rasterizer_num")]
        internal extern static bool _wrap_love_dll_font_newGlyphData_rasterizer_num(IntPtr r, int glyphCode, out IntPtr out_GlyphData);
        internal static bool wrap_love_dll_font_newGlyphData_rasterizer_num(IntPtr r, int glyphCode, out IntPtr out_GlyphData)
        {
            return CheckCAPIException(_wrap_love_dll_font_newGlyphData_rasterizer_num(r, glyphCode, out out_GlyphData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_newVideoStream")]
        internal extern static bool _wrap_love_dll_newVideoStream(IntPtr file, out IntPtr out_videostream);
        internal static bool wrap_love_dll_newVideoStream(IntPtr file, out IntPtr out_videostream)
        {
            return CheckCAPIException(_wrap_love_dll_newVideoStream(file, out out_videostream));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_newRandomGenerator")]
        internal extern static bool _wrap_love_dll_math_newRandomGenerator(out IntPtr out_RandomGenerator);
        internal static bool wrap_love_dll_math_newRandomGenerator(out IntPtr out_RandomGenerator)
        {
            return CheckCAPIException(_wrap_love_dll_math_newRandomGenerator(out out_RandomGenerator));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_newBezierCurve")]
        internal extern static void _wrap_love_dll_math_newBezierCurve(Vector2[] pointsList, int pointsList_lenght, out IntPtr out_BezierCurve);
        internal static void wrap_love_dll_math_newBezierCurve(Vector2[] pointsList, int pointsList_lenght, out IntPtr out_BezierCurve)
        {
            _wrap_love_dll_math_newBezierCurve(pointsList, pointsList_lenght, out out_BezierCurve);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Canvas_newImageData_xywh")]
        internal extern static bool _wrap_love_dll_type_Canvas_newImageData_xywh(IntPtr canvas, int slice, int mipmap, int x, int y, int w, int h, out IntPtr out_imageData);
        internal static bool wrap_love_dll_type_Canvas_newImageData_xywh(IntPtr canvas, int slice, int mipmap, int x, int y, int w, int h, out IntPtr out_imageData)
        {
            return CheckCAPIException(_wrap_love_dll_type_Canvas_newImageData_xywh(canvas, slice, mipmap, x, y, w, h, out out_imageData));
        }

        #endregion

        #region timer
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_open_timer")]
        internal extern static bool _wrap_love_dll_timer_open_timer();
        internal static bool wrap_love_dll_timer_open_timer()
        {
            return CheckCAPIException(_wrap_love_dll_timer_open_timer());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_step")]
        internal extern static void _wrap_love_dll_timer_step();
        internal static void wrap_love_dll_timer_step()
        {
            _wrap_love_dll_timer_step();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_getDelta")]
        internal extern static void _wrap_love_dll_timer_getDelta(out float out_delta);
        internal static void wrap_love_dll_timer_getDelta(out float out_delta)
        {
            _wrap_love_dll_timer_getDelta(out out_delta);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_getFPS")]
        internal extern static void _wrap_love_dll_timer_getFPS(out float out_fps);
        internal static void wrap_love_dll_timer_getFPS(out float out_fps)
        {
            _wrap_love_dll_timer_getFPS(out out_fps);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_getAverageDelta")]
        internal extern static void _wrap_love_dll_timer_getAverageDelta(out float out_averageDelta);
        internal static void wrap_love_dll_timer_getAverageDelta(out float out_averageDelta)
        {
            _wrap_love_dll_timer_getAverageDelta(out out_averageDelta);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_sleep")]
        internal extern static void _wrap_love_dll_timer_sleep(float t);
        internal static void wrap_love_dll_timer_sleep(float t)
        {
            _wrap_love_dll_timer_sleep(t);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_getTime")]
        internal extern static void _wrap_love_dll_timer_getTime(out double out_time);
        internal static void wrap_love_dll_timer_getTime(out double out_time)
        {
            _wrap_love_dll_timer_getTime(out out_time);
        }

        #endregion

        #region window

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_open_love_window")]
        internal extern static bool _wrap_love_dll_windows_open_love_window();
        internal static bool wrap_love_dll_windows_open_love_window()
        {
            return CheckCAPIException(_wrap_love_dll_windows_open_love_window());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getDisplayCount")]
        internal extern static void _wrap_love_dll_windows_getDisplayCount(out int out_count);
        internal static void wrap_love_dll_windows_getDisplayCount(out int out_count)
        {
            _wrap_love_dll_windows_getDisplayCount(out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getDisplayName")]
        internal extern static bool _wrap_love_dll_windows_getDisplayName(int displayindex, out IntPtr out_name);
        internal static bool wrap_love_dll_windows_getDisplayName(int displayindex, out IntPtr out_name)
        {
            return CheckCAPIException(_wrap_love_dll_windows_getDisplayName(displayindex, out out_name));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setMode_w_h")]
        internal extern static bool _wrap_love_dll_windows_setMode_w_h(int width, int height);
        internal static bool wrap_love_dll_windows_setMode_w_h(int width, int height)
        {
            return CheckCAPIException(_wrap_love_dll_windows_setMode_w_h(width, height));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setMode_w_h_setting")]
        internal extern static bool _wrap_love_dll_windows_setMode_w_h_setting(int width, int height, bool fullscreen, int fstype, bool vsync, int msaa, int depth, bool stencil, bool resizable, int minwidth, int minheight, bool borderless, bool centered, int display, bool highdpi, double refreshrate, bool useposition, int x, int y);
        internal static bool wrap_love_dll_windows_setMode_w_h_setting(int width, int height, bool fullscreen, int fstype, bool vsync, int msaa, int depth, bool stencil, bool resizable, int minwidth, int minheight, bool borderless, bool centered, int display, bool highdpi, double refreshrate, bool useposition, int x, int y)
        {
            return CheckCAPIException(_wrap_love_dll_windows_setMode_w_h_setting(width, height, fullscreen, fstype, vsync, msaa, depth, stencil, resizable, minwidth, minheight, borderless, centered, display, highdpi, refreshrate, useposition, x, y));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getMode")]
        internal extern static void _wrap_love_dll_windows_getMode(out int out_width, out int out_height, out bool out_fullscreen, out int out_fstype, out bool out_vsync, out int out_msaa, out int out_depth, out bool out_stencil, out bool out_resizable, out int out_minwidth, out int out_minheight, out bool out_borderless, out bool out_centered, out int out_display, out bool out_highdpi, out double out_refreshrate, out bool out_useposition, out int out_x, out int out_y);
        internal static void wrap_love_dll_windows_getMode(out int out_width, out int out_height, out bool out_fullscreen, out int out_fstype, out bool out_vsync, out int out_msaa, out int out_depth, out bool out_stencil, out bool out_resizable, out int out_minwidth, out int out_minheight, out bool out_borderless, out bool out_centered, out int out_display, out bool out_highdpi, out double out_refreshrate, out bool out_useposition, out int out_x, out int out_y)
        {
            _wrap_love_dll_windows_getMode(out out_width, out out_height, out out_fullscreen, out out_fstype, out out_vsync, out out_msaa, out out_depth, out out_stencil, out out_resizable, out out_minwidth, out out_minheight, out out_borderless, out out_centered, out out_display, out out_highdpi, out out_refreshrate, out out_useposition, out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getFullscreenModes")]
        internal extern static void _wrap_love_dll_windows_getFullscreenModes(int displayindex, out IntPtr out_modes, out int out_modes_length);
        internal static void wrap_love_dll_windows_getFullscreenModes(int displayindex, out IntPtr out_modes, out int out_modes_length)
        {
            _wrap_love_dll_windows_getFullscreenModes(displayindex, out out_modes, out out_modes_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setFullscreen")]
        internal extern static void _wrap_love_dll_windows_setFullscreen(bool fullscreen, int fstype, out bool out_success);
        internal static void wrap_love_dll_windows_setFullscreen(bool fullscreen, int fstype, out bool out_success)
        {
            _wrap_love_dll_windows_setFullscreen(fullscreen, fstype, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getFullscreen")]
        internal extern static void _wrap_love_dll_windows_getFullscreen(out bool out_fullscreen, out int out_fstype);
        internal static void wrap_love_dll_windows_getFullscreen(out bool out_fullscreen, out int out_fstype)
        {
            _wrap_love_dll_windows_getFullscreen(out out_fullscreen, out out_fstype);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_isOpen")]
        internal extern static void _wrap_love_dll_windows_isOpen(out bool out_isopen);
        internal static void wrap_love_dll_windows_isOpen(out bool out_isopen)
        {
            _wrap_love_dll_windows_isOpen(out out_isopen);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_close")]
        internal extern static void _wrap_love_dll_windows_close();
        internal static void wrap_love_dll_windows_close()
        {
            _wrap_love_dll_windows_close();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getDesktopDimensions")]
        internal extern static void _wrap_love_dll_windows_getDesktopDimensions(int displayindex, out int out_width, out int out_height);
        internal static void wrap_love_dll_windows_getDesktopDimensions(int displayindex, out int out_width, out int out_height)
        {
            _wrap_love_dll_windows_getDesktopDimensions(displayindex, out out_width, out out_height);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setPosition")]
        internal extern static void _wrap_love_dll_windows_setPosition(int x, int y, int displayindex);
        internal static void wrap_love_dll_windows_setPosition(int x, int y, int displayindex)
        {
            _wrap_love_dll_windows_setPosition(x, y, displayindex);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getPosition")]
        internal extern static void _wrap_love_dll_windows_getPosition(out int out_x, out int out_y, out int out_displayindex);
        internal static void wrap_love_dll_windows_getPosition(out int out_x, out int out_y, out int out_displayindex)
        {
            _wrap_love_dll_windows_getPosition(out out_x, out out_y, out out_displayindex);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setIcon")]
        internal extern static void _wrap_love_dll_windows_setIcon(IntPtr imagedata, out bool out_success);
        internal static void wrap_love_dll_windows_setIcon(IntPtr imagedata, out bool out_success)
        {
            _wrap_love_dll_windows_setIcon(imagedata, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getIcon")]
        internal extern static void _wrap_love_dll_windows_getIcon(out IntPtr out_imagedata);
        internal static void wrap_love_dll_windows_getIcon(out IntPtr out_imagedata)
        {
            _wrap_love_dll_windows_getIcon(out out_imagedata);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setDisplaySleepEnabled")]
        internal extern static void _wrap_love_dll_windows_setDisplaySleepEnabled(bool enable);
        internal static void wrap_love_dll_windows_setDisplaySleepEnabled(bool enable)
        {
            _wrap_love_dll_windows_setDisplaySleepEnabled(enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_isDisplaySleepEnabled")]
        internal extern static void _wrap_love_dll_windows_isDisplaySleepEnabled(out bool out_enable);
        internal static void wrap_love_dll_windows_isDisplaySleepEnabled(out bool out_enable)
        {
            _wrap_love_dll_windows_isDisplaySleepEnabled(out out_enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setTitle")]
        internal extern static void _wrap_love_dll_windows_setTitle(byte[] titleStr);
        internal static void wrap_love_dll_windows_setTitle(byte[] titleStr)
        {
            _wrap_love_dll_windows_setTitle(titleStr);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getTitle")]
        internal extern static void _wrap_love_dll_windows_getTitle(out IntPtr out_title);
        internal static void wrap_love_dll_windows_getTitle(out IntPtr out_title)
        {
            _wrap_love_dll_windows_getTitle(out out_title);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_hasFocus")]
        internal extern static void _wrap_love_dll_windows_hasFocus(out bool out_result);
        internal static void wrap_love_dll_windows_hasFocus(out bool out_result)
        {
            _wrap_love_dll_windows_hasFocus(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_hasMouseFocus")]
        internal extern static void _wrap_love_dll_windows_hasMouseFocus(out bool out_result);
        internal static void wrap_love_dll_windows_hasMouseFocus(out bool out_result)
        {
            _wrap_love_dll_windows_hasMouseFocus(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_isVisible")]
        internal extern static void _wrap_love_dll_windows_isVisible(out bool out_result);
        internal static void wrap_love_dll_windows_isVisible(out bool out_result)
        {
            _wrap_love_dll_windows_isVisible(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getDPIScale")]
        internal extern static void _wrap_love_dll_windows_getDPIScale(out double out_result);
        internal static void wrap_love_dll_windows_getDPIScale(out double out_result)
        {
            _wrap_love_dll_windows_getDPIScale(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_toPixels")]
        internal extern static void _wrap_love_dll_windows_toPixels(double value, out double out_result);
        internal static void wrap_love_dll_windows_toPixels(double value, out double out_result)
        {
            _wrap_love_dll_windows_toPixels(value, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_fromPixels")]
        internal extern static void _wrap_love_dll_windows_fromPixels(double pixelvalue, out double out_result);
        internal static void wrap_love_dll_windows_fromPixels(double pixelvalue, out double out_result)
        {
            _wrap_love_dll_windows_fromPixels(pixelvalue, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_minimize")]
        internal extern static void _wrap_love_dll_windows_minimize();
        internal static void wrap_love_dll_windows_minimize()
        {
            _wrap_love_dll_windows_minimize();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_maximize")]
        internal extern static void _wrap_love_dll_windows_maximize();
        internal static void wrap_love_dll_windows_maximize()
        {
            _wrap_love_dll_windows_maximize();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_isMaximized")]
        internal extern static void _wrap_love_dll_windows_isMaximized(out bool out_result);
        internal static void wrap_love_dll_windows_isMaximized(out bool out_result)
        {
            _wrap_love_dll_windows_isMaximized(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_showMessageBox")]
        internal extern static void _wrap_love_dll_windows_showMessageBox(byte[] title, byte[] message, int type, bool attachToWindow, out bool out_result);
        internal static void wrap_love_dll_windows_showMessageBox(byte[] title, byte[] message, int type, bool attachToWindow, out bool out_result)
        {
            _wrap_love_dll_windows_showMessageBox(title, message, type, attachToWindow, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_showMessageBox_list")]
        internal extern static void _wrap_love_dll_windows_showMessageBox_list(byte[] title, byte[] message, IntPtr[] buttonName, int buttonsLength, int enterButtonIndex, int escapebuttonIndex, int type, bool attachToWindow, out int out_index_returned);
        internal static void wrap_love_dll_windows_showMessageBox_list(byte[] title, byte[] message, IntPtr[] buttonName, int buttonsLength, int enterButtonIndex, int escapebuttonIndex, int type, bool attachToWindow, out int out_index_returned)
        {
            _wrap_love_dll_windows_showMessageBox_list(title, message, buttonName, buttonsLength, enterButtonIndex, escapebuttonIndex, type, attachToWindow, out out_index_returned);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_requestAttention")]
        internal extern static void _wrap_love_dll_windows_requestAttention(bool continuous);
        internal static void wrap_love_dll_windows_requestAttention(bool continuous)
        {
            _wrap_love_dll_windows_requestAttention(continuous);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_windowToPixelCoords")]
        internal extern static void _wrap_love_dll_windows_windowToPixelCoords(out double out_x, out double out_y);
        internal static void wrap_love_dll_windows_windowToPixelCoords(out double out_x, out double out_y)
        {
            _wrap_love_dll_windows_windowToPixelCoords(out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_pixelToWindowCoords")]
        internal extern static void _wrap_love_dll_windows_pixelToWindowCoords(out double x, out double y);
        internal static void wrap_love_dll_windows_pixelToWindowCoords(out double x, out double y)
        {
            _wrap_love_dll_windows_pixelToWindowCoords(out x, out y);
        }

        #endregion

        #region System

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_system_open_love_system_module")]
        internal extern static bool _wrap_love_dll_system_open_love_system_module();
        internal static bool wrap_love_dll_system_open_love_system_module()
        {
            return CheckCAPIException(_wrap_love_dll_system_open_love_system_module());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_system_getOS")]
        internal extern static void _wrap_love_dll_system_getOS(out IntPtr out_str);
        internal static void wrap_love_dll_system_getOS(out IntPtr out_str)
        {
            _wrap_love_dll_system_getOS(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_system_getProcessorCount")]
        internal extern static void _wrap_love_dll_system_getProcessorCount(out int count);
        internal static void wrap_love_dll_system_getProcessorCount(out int count)
        {
            _wrap_love_dll_system_getProcessorCount(out count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_system_setClipboardText")]
        internal extern static bool _wrap_love_dll_system_setClipboardText(byte[] text);
        internal static bool wrap_love_dll_system_setClipboardText(byte[] text)
        {
            return CheckCAPIException(_wrap_love_dll_system_setClipboardText(text));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_system_getClipboardText")]
        internal extern static bool _wrap_love_dll_system_getClipboardText(out IntPtr out_str);
        internal static bool wrap_love_dll_system_getClipboardText(out IntPtr out_str)
        {
            return CheckCAPIException(_wrap_love_dll_system_getClipboardText(out out_str));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_system_getPowerInfo")]
        internal extern static bool _wrap_love_dll_system_getPowerInfo(out int stateType, out int percent, out int seconds);
        internal static bool wrap_love_dll_system_getPowerInfo(out int stateType, out int percent, out int seconds)
        {
            return CheckCAPIException(_wrap_love_dll_system_getPowerInfo(out stateType, out percent, out seconds));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_system_openURL")]
        internal extern static bool _wrap_love_dll_system_openURL(byte[] strUrl, out bool result);
        internal static bool wrap_love_dll_system_openURL(byte[] strUrl, out bool result)
        {
            return CheckCAPIException(_wrap_love_dll_system_openURL(strUrl, out result));
        }

        #endregion

        #region mouse
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_open_love_mouse")]
        internal extern static bool _wrap_love_dll_open_love_mouse();
        internal static bool wrap_love_dll_open_love_mouse()
        {
            return CheckCAPIException(_wrap_love_dll_open_love_mouse());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getSystemCursor")]
        internal extern static bool _wrap_love_dll_mouse_getSystemCursor(int sysctype, out IntPtr out_cursor);
        internal static bool wrap_love_dll_mouse_getSystemCursor(int sysctype, out IntPtr out_cursor)
        {
            return CheckCAPIException(_wrap_love_dll_mouse_getSystemCursor(sysctype, out out_cursor));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setCursor")]
        internal extern static void _wrap_love_dll_mouse_setCursor(IntPtr cursor);
        internal static void wrap_love_dll_mouse_setCursor(IntPtr cursor)
        {
            _wrap_love_dll_mouse_setCursor(cursor);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getCursor")]
        internal extern static void _wrap_love_dll_mouse_getCursor(out IntPtr out_cursor);
        internal static void wrap_love_dll_mouse_getCursor(out IntPtr out_cursor)
        {
            _wrap_love_dll_mouse_getCursor(out out_cursor);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_isCursorSupported")]
        internal extern static void _wrap_love_dll_mouse_isCursorSupported(out bool out_result);
        internal static void wrap_love_dll_mouse_isCursorSupported(out bool out_result)
        {
            _wrap_love_dll_mouse_isCursorSupported(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getX")]
        internal extern static void _wrap_love_dll_mouse_getX(out double out_x);
        internal static void wrap_love_dll_mouse_getX(out double out_x)
        {
            _wrap_love_dll_mouse_getX(out out_x);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getY")]
        internal extern static void _wrap_love_dll_mouse_getY(out double out_y);
        internal static void wrap_love_dll_mouse_getY(out double out_y)
        {
            _wrap_love_dll_mouse_getY(out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getPosition")]
        internal extern static void _wrap_love_dll_mouse_getPosition(out double out_x, out double out_y);
        internal static void wrap_love_dll_mouse_getPosition(out double out_x, out double out_y)
        {
            _wrap_love_dll_mouse_getPosition(out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setX")]
        internal extern static void _wrap_love_dll_mouse_setX(double x);
        internal static void wrap_love_dll_mouse_setX(double x)
        {
            _wrap_love_dll_mouse_setX(x);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setY")]
        internal extern static void _wrap_love_dll_mouse_setY(double y);
        internal static void wrap_love_dll_mouse_setY(double y)
        {
            _wrap_love_dll_mouse_setY(y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setPosition")]
        internal extern static void _wrap_love_dll_mouse_setPosition(double x, double y);
        internal static void wrap_love_dll_mouse_setPosition(double x, double y)
        {
            _wrap_love_dll_mouse_setPosition(x, y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_isDown")]
        internal extern static void _wrap_love_dll_mouse_isDown(int buttonIndex, out bool out_result);
        internal static void wrap_love_dll_mouse_isDown(int buttonIndex, out bool out_result)
        {
            _wrap_love_dll_mouse_isDown(buttonIndex, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setVisible")]
        internal extern static void _wrap_love_dll_mouse_setVisible(bool visible);
        internal static void wrap_love_dll_mouse_setVisible(bool visible)
        {
            _wrap_love_dll_mouse_setVisible(visible);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_isVisible")]
        internal extern static void _wrap_love_dll_mouse_isVisible(out bool out_result);
        internal static void wrap_love_dll_mouse_isVisible(out bool out_result)
        {
            _wrap_love_dll_mouse_isVisible(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setGrabbed")]
        internal extern static void _wrap_love_dll_mouse_setGrabbed(bool grabbed);
        internal static void wrap_love_dll_mouse_setGrabbed(bool grabbed)
        {
            _wrap_love_dll_mouse_setGrabbed(grabbed);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_isGrabbed")]
        internal extern static void _wrap_love_dll_mouse_isGrabbed(out bool out_result);
        internal static void wrap_love_dll_mouse_isGrabbed(out bool out_result)
        {
            _wrap_love_dll_mouse_isGrabbed(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setRelativeMode")]
        internal extern static void _wrap_love_dll_mouse_setRelativeMode(bool enable, out bool out_result);
        internal static void wrap_love_dll_mouse_setRelativeMode(bool enable, out bool out_result)
        {
            _wrap_love_dll_mouse_setRelativeMode(enable, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getRelativeMode")]
        internal extern static void _wrap_love_dll_mouse_getRelativeMode(out bool out_result);
        internal static void wrap_love_dll_mouse_getRelativeMode(out bool out_result)
        {
            _wrap_love_dll_mouse_getRelativeMode(out out_result);
        }

        #endregion

        #region keyboard
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_open_love_keyboard")]
        internal extern static bool _wrap_love_dll_keyboard_open_love_keyboard();
        internal static bool wrap_love_dll_keyboard_open_love_keyboard()
        {
            return CheckCAPIException(_wrap_love_dll_keyboard_open_love_keyboard());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_setKeyRepeat")]
        internal extern static void _wrap_love_dll_keyboard_setKeyRepeat(bool enable);
        internal static void wrap_love_dll_keyboard_setKeyRepeat(bool enable)
        {
            _wrap_love_dll_keyboard_setKeyRepeat(enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_hasKeyRepeat")]
        internal extern static void _wrap_love_dll_keyboard_hasKeyRepeat(out bool out_result);
        internal static void wrap_love_dll_keyboard_hasKeyRepeat(out bool out_result)
        {
            _wrap_love_dll_keyboard_hasKeyRepeat(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_isDown")]
        internal extern static void _wrap_love_dll_keyboard_isDown(int key_type, out bool out_result);
        internal static void wrap_love_dll_keyboard_isDown(int key_type, out bool out_result)
        {
            _wrap_love_dll_keyboard_isDown(key_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_isScancodeDown")]
        internal extern static void _wrap_love_dll_keyboard_isScancodeDown(int scancode_type, out bool out_result);
        internal static void wrap_love_dll_keyboard_isScancodeDown(int scancode_type, out bool out_result)
        {
            _wrap_love_dll_keyboard_isScancodeDown(scancode_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_getScancodeFromKey")]
        internal extern static void _wrap_love_dll_keyboard_getScancodeFromKey(int key_type, out int out_scancode_type);
        internal static void wrap_love_dll_keyboard_getScancodeFromKey(int key_type, out int out_scancode_type)
        {
            _wrap_love_dll_keyboard_getScancodeFromKey(key_type, out out_scancode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_getKeyFromScancode")]
        internal extern static void _wrap_love_dll_keyboard_getKeyFromScancode(int scancode_type, out int out_key_type);
        internal static void wrap_love_dll_keyboard_getKeyFromScancode(int scancode_type, out int out_key_type)
        {
            _wrap_love_dll_keyboard_getKeyFromScancode(scancode_type, out out_key_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_setTextInput")]
        internal extern static void _wrap_love_dll_keyboard_setTextInput(bool enable);
        internal static void wrap_love_dll_keyboard_setTextInput(bool enable)
        {
            _wrap_love_dll_keyboard_setTextInput(enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_setTextInput_xywh")]
        internal extern static void _wrap_love_dll_keyboard_setTextInput_xywh(bool enable, double x, double y, double w, double h);
        internal static void wrap_love_dll_keyboard_setTextInput_xywh(bool enable, double x, double y, double w, double h)
        {
            _wrap_love_dll_keyboard_setTextInput_xywh(enable, x, y, w, h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_hasTextInput")]
        internal extern static void _wrap_love_dll_keyboard_hasTextInput(out bool out_result);
        internal static void wrap_love_dll_keyboard_hasTextInput(out bool out_result)
        {
            _wrap_love_dll_keyboard_hasTextInput(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_hasScreenKeyboard")]
        internal extern static void _wrap_love_dll_keyboard_hasScreenKeyboard(out bool out_result);
        internal static void wrap_love_dll_keyboard_hasScreenKeyboard(out bool out_result)
        {
            _wrap_love_dll_keyboard_hasScreenKeyboard(out out_result);
        }

        #endregion

        #region touch
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_touch_open_love_touch")]
        internal extern static bool _wrap_love_dll_touch_open_love_touch();
        internal static bool wrap_love_dll_touch_open_love_touch()
        {
            return CheckCAPIException(_wrap_love_dll_touch_open_love_touch());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_touch_open_love_getTouches")]
        internal extern static void _wrap_love_dll_touch_open_love_getTouches(out IntPtr out_indexs, out IntPtr out_x, out IntPtr out_y, out IntPtr out_dx, out IntPtr out_dy, out IntPtr out_pressure, out int out_length);
        internal static void wrap_love_dll_touch_open_love_getTouches(out IntPtr out_indexs, out IntPtr out_x, out IntPtr out_y, out IntPtr out_dx, out IntPtr out_dy, out IntPtr out_pressure, out int out_length)
        {
            _wrap_love_dll_touch_open_love_getTouches(out out_indexs, out out_x, out out_y, out out_dx, out out_dy, out out_pressure, out out_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_touch_open_love_getToucheInfo")]
        internal extern static bool _wrap_love_dll_touch_open_love_getToucheInfo(long idx, out double out_x, out double out_y, out double out_dx, out double out_dy, out double out_pressure);
        internal static bool wrap_love_dll_touch_open_love_getToucheInfo(long idx, out double out_x, out double out_y, out double out_dx, out double out_dy, out double out_pressure)
        {
            return CheckCAPIException(_wrap_love_dll_touch_open_love_getToucheInfo(idx, out out_x, out out_y, out out_dx, out out_dy, out out_pressure));
        }


        #endregion

        #region joystick
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_open_love_joystick")]
        internal extern static bool _wrap_love_dll_joystick_open_love_joystick();
        internal static bool wrap_love_dll_joystick_open_love_joystick()
        {
            return CheckCAPIException(_wrap_love_dll_joystick_open_love_joystick());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_getJoysticks")]
        internal extern static void _wrap_love_dll_joystick_getJoysticks(out IntPtr out_sticks, out int out_sticks_lenght);
        internal static void wrap_love_dll_joystick_getJoysticks(out IntPtr out_sticks, out int out_sticks_lenght)
        {
            _wrap_love_dll_joystick_getJoysticks(out out_sticks, out out_sticks_lenght);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_getIndex")]
        internal extern static void _wrap_love_dll_joystick_getIndex(IntPtr j, out int out_index);
        internal static void wrap_love_dll_joystick_getIndex(IntPtr j, out int out_index)
        {
            _wrap_love_dll_joystick_getIndex(j, out out_index);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_getJoystickCount")]
        internal extern static void _wrap_love_dll_joystick_getJoystickCount(out int out_sticks_lenght);
        internal static void wrap_love_dll_joystick_getJoystickCount(out int out_sticks_lenght)
        {
            _wrap_love_dll_joystick_getJoystickCount(out out_sticks_lenght);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_setGamepadMapping")]
        internal extern static void _wrap_love_dll_joystick_setGamepadMapping(byte[] guid, int gp_inputType_type, int j_inputType_type, int inputIndex, int hat_type, out bool out_success);
        internal static void wrap_love_dll_joystick_setGamepadMapping(byte[] guid, int gp_inputType_type, int j_inputType_type, int inputIndex, int hat_type, out bool out_success)
        {
            _wrap_love_dll_joystick_setGamepadMapping(guid, gp_inputType_type, j_inputType_type, inputIndex, hat_type, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_loadGamepadMappings")]
        internal extern static void _wrap_love_dll_joystick_loadGamepadMappings(byte[] str, out bool out_success);
        internal static void wrap_love_dll_joystick_loadGamepadMappings(byte[] str, out bool out_success)
        {
            _wrap_love_dll_joystick_loadGamepadMappings(str, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_saveGamepadMappings")]
        internal extern static void _wrap_love_dll_joystick_saveGamepadMappings(out IntPtr out_str);
        internal static void wrap_love_dll_joystick_saveGamepadMappings(out IntPtr out_str)
        {
            _wrap_love_dll_joystick_saveGamepadMappings(out out_str);
        }

        #endregion

        #region Event
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_event_open_love_event")]
        internal extern static bool _wrap_love_dll_event_open_love_event();
        internal static bool wrap_love_dll_event_open_love_event()
        {
            return CheckCAPIException(_wrap_love_dll_event_open_love_event());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_event_push_nil")]
        internal extern static void _wrap_love_dll_event_push_nil();
        internal static void wrap_love_dll_event_push_nil()
        {
            _wrap_love_dll_event_push_nil();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_event_poll")]
        internal extern static void _wrap_love_dll_event_poll(out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out IntPtr out_str, out Int4 out_int4, out Vector4 out_float4, out float out_float_value, out IntPtr out_joystick);
        internal static void wrap_love_dll_event_poll(out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out IntPtr out_str, out Int4 out_int4, out Vector4 out_float4, out float out_float_value, out IntPtr out_joystick)
        {
            _wrap_love_dll_event_poll(out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_str, out out_int4, out out_float4, out out_float_value, out out_joystick);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_event_wait")]
        internal extern static void _wrap_love_dll_event_wait(out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out IntPtr out_str, out Int4 out_int4, out Vector4 out_float4, out float out_float_value, out IntPtr out_joystick);
        internal static void wrap_love_dll_event_wait(out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out IntPtr out_str, out Int4 out_int4, out Vector4 out_float4, out float out_float_value, out IntPtr out_joystick)
        {
            _wrap_love_dll_event_wait(out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_str, out out_int4, out out_float4, out out_float_value, out out_joystick);
        }

        #endregion

        #region filesystem
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_open_love_filesystem")]
        internal extern static bool _wrap_love_dll_filesystem_open_love_filesystem();
        internal static bool wrap_love_dll_filesystem_open_love_filesystem()
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_open_love_filesystem());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_init")]
        internal extern static bool _wrap_love_dll_filesystem_init(byte[] arg0);
        internal static bool wrap_love_dll_filesystem_init(byte[] arg0)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_init(arg0));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_setFused")]
        internal extern static void _wrap_love_dll_filesystem_setFused(bool flag);
        internal static void wrap_love_dll_filesystem_setFused(bool flag)
        {
            _wrap_love_dll_filesystem_setFused(flag);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_isFused")]
        internal extern static void _wrap_love_dll_filesystem_isFused(out bool out_result);
        internal static void wrap_love_dll_filesystem_isFused(out bool out_result)
        {
            _wrap_love_dll_filesystem_isFused(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_setAndroidSaveExternal")]
        internal extern static void _wrap_love_dll_filesystem_setAndroidSaveExternal(bool useExternal);
        internal static void wrap_love_dll_filesystem_setAndroidSaveExternal(bool useExternal)
        {
            _wrap_love_dll_filesystem_setAndroidSaveExternal(useExternal);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_setIdentity")]
        internal extern static bool _wrap_love_dll_filesystem_setIdentity(byte[] arg, bool append);
        internal static bool wrap_love_dll_filesystem_setIdentity(byte[] arg, bool append)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_setIdentity(arg, append));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getIdentity")]
        internal extern static void _wrap_love_dll_filesystem_getIdentity(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getIdentity(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getIdentity(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_setSource")]
        internal extern static bool _wrap_love_dll_filesystem_setSource(byte[] arg);
        internal static bool wrap_love_dll_filesystem_setSource(byte[] arg)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_setSource(arg));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getSource")]
        internal extern static void _wrap_love_dll_filesystem_getSource(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getSource(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getSource(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_mount")]
        internal extern static void _wrap_love_dll_filesystem_mount(byte[] archive, byte[] mountpoint, bool appendToPath, out bool out_result);
        internal static void wrap_love_dll_filesystem_mount(byte[] archive, byte[] mountpoint, bool appendToPath, out bool out_result)
        {
            _wrap_love_dll_filesystem_mount(archive, mountpoint, appendToPath, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_unmount")]
        internal extern static void _wrap_love_dll_filesystem_unmount(byte[] archive, out bool out_result);
        internal static void wrap_love_dll_filesystem_unmount(byte[] archive, out bool out_result)
        {
            _wrap_love_dll_filesystem_unmount(archive, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getWorkingDirectory")]
        internal extern static void _wrap_love_dll_filesystem_getWorkingDirectory(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getWorkingDirectory(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getWorkingDirectory(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getUserDirectory")]
        internal extern static void _wrap_love_dll_filesystem_getUserDirectory(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getUserDirectory(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getUserDirectory(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getAppdataDirectory")]
        internal extern static void _wrap_love_dll_filesystem_getAppdataDirectory(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getAppdataDirectory(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getAppdataDirectory(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getSaveDirectory")]
        internal extern static void _wrap_love_dll_filesystem_getSaveDirectory(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getSaveDirectory(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getSaveDirectory(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getSourceBaseDirectory")]
        internal extern static void _wrap_love_dll_filesystem_getSourceBaseDirectory(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getSourceBaseDirectory(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getSourceBaseDirectory(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getRealDirectory")]
        internal extern static bool _wrap_love_dll_filesystem_getRealDirectory(byte[] filename, out IntPtr out_str);
        internal static bool wrap_love_dll_filesystem_getRealDirectory(byte[] filename, out IntPtr out_str)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_getRealDirectory(filename, out out_str));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getExecutablePath")]
        internal extern static void _wrap_love_dll_filesystem_getExecutablePath(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getExecutablePath(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getExecutablePath(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getInfo")]
        internal extern static void _wrap_love_dll_filesystem_getInfo(byte[] arg, out int out_filetype, out long out_size, out long out_modtime, out bool out_result);
        internal static void wrap_love_dll_filesystem_getInfo(byte[] arg, out int out_filetype, out long out_size, out long out_modtime, out bool out_result)
        {
            _wrap_love_dll_filesystem_getInfo(arg, out out_filetype, out out_size, out out_modtime,  out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_createDirectory")]
        internal extern static void _wrap_love_dll_filesystem_createDirectory(byte[] arg, out bool out_result);
        internal static void wrap_love_dll_filesystem_createDirectory(byte[] arg, out bool out_result)
        {
            _wrap_love_dll_filesystem_createDirectory(arg, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_remove")]
        internal extern static void _wrap_love_dll_filesystem_remove(byte[] arg, out bool out_result);
        internal static void wrap_love_dll_filesystem_remove(byte[] arg, out bool out_result)
        {
            _wrap_love_dll_filesystem_remove(arg, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_read")]
        internal extern static bool _wrap_love_dll_filesystem_read(byte[] filename, long len, out IntPtr out_data, out uint out_data_length);
        internal static bool wrap_love_dll_filesystem_read(byte[] filename, long len, out IntPtr out_data, out uint out_data_length)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_read(filename, len, out out_data, out out_data_length));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_write")]
        internal extern static bool _wrap_love_dll_filesystem_write(byte[] filename, byte[] input, size_t len);
        internal static bool wrap_love_dll_filesystem_write(byte[] filename, byte[] input, size_t len)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_write(filename, input, len));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_append")]
        internal extern static bool _wrap_love_dll_filesystem_append(byte[] filename, byte[] input, size_t len);
        internal static bool wrap_love_dll_filesystem_append(byte[] filename, byte[] input, size_t len)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_append(filename, input, len));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getDirectoryItems")]
        internal extern static void _wrap_love_dll_filesystem_getDirectoryItems(byte[] dir, out IntPtr out_wss);
        internal static void wrap_love_dll_filesystem_getDirectoryItems(byte[] dir, out IntPtr out_wss)
        {
            _wrap_love_dll_filesystem_getDirectoryItems(dir, out out_wss);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getLastModified")]
        internal extern static bool _wrap_love_dll_filesystem_getLastModified(byte[] filename, out long out_time);
        internal static bool wrap_love_dll_filesystem_getLastModified(byte[] filename, out long out_time)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_getLastModified(filename, out out_time));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_areSymlinksEnabled")]
        internal extern static void _wrap_love_dll_filesystem_areSymlinksEnabled(out bool out_result);
        internal static void wrap_love_dll_filesystem_areSymlinksEnabled(out bool out_result)
        {
            _wrap_love_dll_filesystem_areSymlinksEnabled(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getRequirePath")]
        internal extern static void _wrap_love_dll_filesystem_getRequirePath(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getRequirePath(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getRequirePath(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_setRequirePath")]
        internal extern static void _wrap_love_dll_filesystem_setRequirePath(byte[] paths);
        internal static void wrap_love_dll_filesystem_setRequirePath(byte[] paths)
        {
            _wrap_love_dll_filesystem_setRequirePath(paths);
        }


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_ext_allowMountingForPath(IntPtr pathStr);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_ext_isRealDirectory(IntPtr pathStr, out bool out_result);

        #endregion

        #region sound
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_sound_luaopen_love_sound")]
        internal extern static bool _wrap_love_dll_sound_luaopen_love_sound();
        internal static bool wrap_love_dll_sound_luaopen_love_sound()
        {
            return CheckCAPIException(_wrap_love_dll_sound_luaopen_love_sound());
        }



        #endregion
        #region  audio

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_open_love_audio")]
        internal extern static bool _wrap_love_dll_audio_open_love_audio();
        internal static bool wrap_love_dll_audio_open_love_audio()
        {
            return CheckCAPIException(_wrap_love_dll_audio_open_love_audio());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getActiveSourceCount")]
        internal extern static void _wrap_love_dll_audio_getActiveSourceCount(out int out_reslut);
        internal static void wrap_love_dll_audio_getActiveSourceCount(out int out_reslut)
        {
            _wrap_love_dll_audio_getActiveSourceCount(out out_reslut);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_stop")]
        internal extern static void _wrap_love_dll_audio_stop();
        internal static void wrap_love_dll_audio_stop()
        {
            _wrap_love_dll_audio_stop();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_pause")]
        internal extern static void _wrap_love_dll_audio_pause();
        internal static void wrap_love_dll_audio_pause()
        {
            _wrap_love_dll_audio_pause();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_play")]
        internal extern static void _wrap_love_dll_audio_play(IntPtr[] sourceArray, int source_array_length);
        internal static void wrap_love_dll_audio_play(IntPtr[] sourceArray, int source_array_length)
        {
            _wrap_love_dll_audio_play(sourceArray, source_array_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setVolume")]
        internal extern static void _wrap_love_dll_audio_setVolume(float v);
        internal static void wrap_love_dll_audio_setVolume(float v)
        {
            _wrap_love_dll_audio_setVolume(v);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getVolume")]
        internal extern static void _wrap_love_dll_audio_getVolume(out float out_volume);
        internal static void wrap_love_dll_audio_getVolume(out float out_volume)
        {
            _wrap_love_dll_audio_getVolume(out out_volume);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setPosition")]
        internal extern static void _wrap_love_dll_audio_setPosition(float x, float y, float z);
        internal static void wrap_love_dll_audio_setPosition(float x, float y, float z)
        {
            _wrap_love_dll_audio_setPosition(x, y, z);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getPosition")]
        internal extern static void _wrap_love_dll_audio_getPosition(out float out_x, out float out_y, out float out_z);
        internal static void wrap_love_dll_audio_getPosition(out float out_x, out float out_y, out float out_z)
        {
            _wrap_love_dll_audio_getPosition(out out_x, out out_y, out out_z);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setOrientation")]
        internal extern static void _wrap_love_dll_audio_setOrientation(float x, float y, float z, float dx, float dy, float dz);
        internal static void wrap_love_dll_audio_setOrientation(float x, float y, float z, float dx, float dy, float dz)
        {
            _wrap_love_dll_audio_setOrientation(x, y, z, dx, dy, dz);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getOrientation")]
        internal extern static void _wrap_love_dll_audio_getOrientation(out float out_x, out float out_y, out float out_z, out float out_dx, out float out_dy, out float out_dz);
        internal static void wrap_love_dll_audio_getOrientation(out float out_x, out float out_y, out float out_z, out float out_dx, out float out_dy, out float out_dz)
        {
            _wrap_love_dll_audio_getOrientation(out out_x, out out_y, out out_z, out out_dx, out out_dy, out out_dz);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setVelocity")]
        internal extern static void _wrap_love_dll_audio_setVelocity(float x, float y, float z);
        internal static void wrap_love_dll_audio_setVelocity(float x, float y, float z)
        {
            _wrap_love_dll_audio_setVelocity(x, y, z);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getVelocity")]
        internal extern static void _wrap_love_dll_audio_getVelocity(out float out_x, out float out_y, out float out_z);
        internal static void wrap_love_dll_audio_getVelocity(out float out_x, out float out_y, out float out_z)
        {
            _wrap_love_dll_audio_getVelocity(out out_x, out out_y, out out_z);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setDopplerScale")]
        internal extern static void _wrap_love_dll_audio_setDopplerScale(float scale);
        internal static void wrap_love_dll_audio_setDopplerScale(float scale)
        {
            _wrap_love_dll_audio_setDopplerScale(scale);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getDopplerScale")]
        internal extern static void _wrap_love_dll_audio_getDopplerScale(out float out_scale);
        internal static void wrap_love_dll_audio_getDopplerScale(out float out_scale)
        {
            _wrap_love_dll_audio_getDopplerScale(out out_scale);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setDistanceModel")]
        internal extern static void _wrap_love_dll_audio_setDistanceModel(int distancemodel_type);
        internal static void wrap_love_dll_audio_setDistanceModel(int distancemodel_type)
        {
            _wrap_love_dll_audio_setDistanceModel(distancemodel_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getDistanceModel")]
        internal extern static void _wrap_love_dll_audio_getDistanceModel(out int out_distancemodel_type);
        internal static void wrap_love_dll_audio_getDistanceModel(out int out_distancemodel_type)
        {
            _wrap_love_dll_audio_getDistanceModel(out out_distancemodel_type);
        }



        #endregion
        #region  image

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_image_open_love_image")]
        internal extern static bool _wrap_love_dll_image_open_love_image();
        internal static bool wrap_love_dll_image_open_love_image()
        {
            return CheckCAPIException(_wrap_love_dll_image_open_love_image());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_image_isCompressed")]
        internal extern static void _wrap_love_dll_image_isCompressed(IntPtr fileData, out bool out_result);
        internal static void wrap_love_dll_image_isCompressed(IntPtr fileData, out bool out_result)
        {
            _wrap_love_dll_image_isCompressed(fileData, out out_result);
        }



        #endregion
        #region  font

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_open_love_font")]
        internal extern static bool _wrap_love_dll_font_open_love_font();
        internal static bool wrap_love_dll_font_open_love_font()
        {
            return CheckCAPIException(_wrap_love_dll_font_open_love_font());
        }



        #endregion
        #region  video

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_video_open_love_video")]
        internal extern static bool _wrap_love_dll_video_open_love_video();
        internal static bool wrap_love_dll_video_open_love_video()
        {
            return CheckCAPIException(_wrap_love_dll_video_open_love_video());
        }



        #endregion
        #region  math

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_open_love_math")]
        internal extern static void _wrap_love_dll_open_love_math();
        internal static void wrap_love_dll_open_love_math()
        {
            _wrap_love_dll_open_love_math();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_triangulate")]
        internal extern static bool _wrap_love_dll_math_triangulate(Vector2[] pointsList, int pointsList_lenght, out IntPtr out_triArray, out int out_triCount);
        internal static bool wrap_love_dll_math_triangulate(Vector2[] pointsList, int pointsList_lenght, out IntPtr out_triArray, out int out_triCount)
        {
            return CheckCAPIException(_wrap_love_dll_math_triangulate(pointsList, pointsList_lenght, out out_triArray, out out_triCount));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_isConvex")]
        internal extern static void _wrap_love_dll_math_isConvex(Vector2[] pointsList, int pointsList_lenght, out bool out_result);
        internal static void wrap_love_dll_math_isConvex(Vector2[] pointsList, int pointsList_lenght, out bool out_result)
        {
            _wrap_love_dll_math_isConvex(pointsList, pointsList_lenght, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_gammaToLinear")]
        internal extern static void _wrap_love_dll_math_gammaToLinear(float gama, out float out_liner);
        internal static void wrap_love_dll_math_gammaToLinear(float gama, out float out_liner)
        {
            _wrap_love_dll_math_gammaToLinear(gama, out out_liner);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_linearToGamma")]
        internal extern static void _wrap_love_dll_math_linearToGamma(float liner, out float out_gama);
        internal static void wrap_love_dll_math_linearToGamma(float liner, out float out_gama)
        {
            _wrap_love_dll_math_linearToGamma(liner, out out_gama);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_noise_1")]
        internal extern static void _wrap_love_dll_math_noise_1(float x, out float out_result);
        internal static void wrap_love_dll_math_noise_1(float x, out float out_result)
        {
            _wrap_love_dll_math_noise_1(x, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_noise_2")]
        internal extern static void _wrap_love_dll_math_noise_2(float x, float y, out float out_result);
        internal static void wrap_love_dll_math_noise_2(float x, float y, out float out_result)
        {
            _wrap_love_dll_math_noise_2(x, y, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_noise_3")]
        internal extern static void _wrap_love_dll_math_noise_3(float x, float y, float z, out float out_result);
        internal static void wrap_love_dll_math_noise_3(float x, float y, float z, out float out_result)
        {
            _wrap_love_dll_math_noise_3(x, y, z, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_noise_4")]
        internal extern static void _wrap_love_dll_math_noise_4(float x, float y, float z, float w, out float out_result);
        internal static void wrap_love_dll_math_noise_4(float x, float y, float z, float w, out float out_result)
        {
            _wrap_love_dll_math_noise_4(x, y, z, w, out out_result);
        }



        #endregion
        #region  graphics Object Creation

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_open_love_graphics")]
        internal extern static bool _wrap_love_dll_graphics_open_love_graphics();
        internal static bool wrap_love_dll_graphics_open_love_graphics()
        {
            return CheckCAPIException(_wrap_love_dll_graphics_open_love_graphics());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newImage_data")]
        internal extern static bool _wrap_love_dll_graphics_newImage_data(IntPtr[] imageDataList, bool[] compressedTypeList, int imageDataListLength, bool flagMipmaps, bool flagLinear, out IntPtr out_image);
        internal static bool wrap_love_dll_graphics_newImage_data(IntPtr[] imageDataList, bool[] compressedTypeList, int imageDataListLength, bool flagMipmaps, bool flagLinear, out IntPtr out_image)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newImage_data(imageDataList, compressedTypeList, imageDataListLength, flagMipmaps, flagLinear, out out_image));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newQuad")]
        internal extern static void _wrap_love_dll_graphics_newQuad(double x, double y, double w, double h, double sw, double sh, out IntPtr out_quad);
        internal static void wrap_love_dll_graphics_newQuad(double x, double y, double w, double h, double sw, double sh, out IntPtr out_quad)
        {
            _wrap_love_dll_graphics_newQuad(x, y, w, h, sw, sh, out out_quad);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newFont")]
        internal extern static bool _wrap_love_dll_graphics_newFont(IntPtr rasterizer, out IntPtr out_font);
        internal static bool wrap_love_dll_graphics_newFont(IntPtr rasterizer, out IntPtr out_font)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newFont(rasterizer, out out_font));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newSpriteBatch")]
        internal extern static bool _wrap_love_dll_graphics_newSpriteBatch(IntPtr texture, int maxSprites, int usage_type, out IntPtr out_spriteBatch);
        internal static bool wrap_love_dll_graphics_newSpriteBatch(IntPtr texture, int maxSprites, int usage_type, out IntPtr out_spriteBatch)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newSpriteBatch(texture, maxSprites, usage_type, out out_spriteBatch));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newParticleSystem")]
        internal extern static bool _wrap_love_dll_graphics_newParticleSystem(IntPtr texture, int buffer, out IntPtr out_particleSystem);
        internal static bool wrap_love_dll_graphics_newParticleSystem(IntPtr texture, int buffer, out IntPtr out_particleSystem)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newParticleSystem(texture, buffer, out out_particleSystem));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newCanvas")]
        internal extern static bool _wrap_love_dll_graphics_newCanvas(int width, int height, int texture_type, int format_type, bool readable, int msaa, float dpiscale, int mipmaps, out IntPtr out_canvas);
        internal static bool wrap_love_dll_graphics_newCanvas(int width, int height, int texture_type, int format_type, bool readable, int msaa, float dpiscale, int mipmaps, out IntPtr out_canvas)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newCanvas(width, height, texture_type, format_type, readable, msaa, dpiscale, mipmaps, out out_canvas));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newShader")]
        internal extern static bool _wrap_love_dll_graphics_newShader(byte[] vertexCodeStr, byte[] pixelCodeStr, out IntPtr out_shader);
        internal static bool wrap_love_dll_graphics_newShader(byte[] vertexCodeStr, byte[] pixelCodeStr, out IntPtr out_shader)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newShader(vertexCodeStr, pixelCodeStr, out out_shader));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newMesh_custom")]
        internal extern static bool _wrap_love_dll_graphics_newMesh_custom(IntPtr[] strAry, int[] vfType, int[] vfComponentCount, int vfLen, bool useData, byte[] data, int vertexCountOrSize, int drawMode_type, int usage_type, out IntPtr out_mesh);
        internal static bool wrap_love_dll_graphics_newMesh_custom(IntPtr[] strAry, int[] vfType, int[] vfComponentCount, int vfLen, bool useData, byte[] data, int vertexCountOrSize, int drawMode_type, int usage_type, out IntPtr out_mesh)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newMesh_custom(strAry,vfType, vfComponentCount, vfLen, useData, data, vertexCountOrSize, drawMode_type, usage_type, out out_mesh));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newMesh_count")]
        internal extern static bool _wrap_love_dll_graphics_newMesh_count(int count, int drawMode_type, int usage_type, out IntPtr out_mesh);
        internal static bool wrap_love_dll_graphics_newMesh_count(int count, int drawMode_type, int usage_type, out IntPtr out_mesh)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newMesh_count(count, drawMode_type, usage_type, out out_mesh));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newText")]
        internal extern static bool _wrap_love_dll_graphics_newText(IntPtr font, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength, out IntPtr out_text);
        internal static bool wrap_love_dll_graphics_newText(IntPtr font, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength, out IntPtr out_text)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newText(font, coloredStringText, coloredStringColor, coloredStringLength, out out_text));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newVideo")]
        internal extern static bool _wrap_love_dll_graphics_newVideo(IntPtr videoStream, float dpiScale, out IntPtr out_video);
        internal static bool wrap_love_dll_graphics_newVideo(IntPtr videoStream, float dpiScale, out IntPtr out_video)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newVideo(videoStream, dpiScale, out out_video));
        }



        #endregion
        #region  graphics State

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_reset")]
        internal extern static void _wrap_love_dll_graphics_reset();
        internal static void wrap_love_dll_graphics_reset()
        {
            _wrap_love_dll_graphics_reset();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_isActive")]
        internal extern static void _wrap_love_dll_graphics_isActive(out bool out_result);
        internal static void wrap_love_dll_graphics_isActive(out bool out_result)
        {
            _wrap_love_dll_graphics_isActive(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_isGammaCorrect")]
        internal extern static void _wrap_love_dll_graphics_isGammaCorrect(out bool out_result);
        internal static void wrap_love_dll_graphics_isGammaCorrect(out bool out_result)
        {
            _wrap_love_dll_graphics_isGammaCorrect(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setScissor")]
        internal extern static void _wrap_love_dll_graphics_setScissor();
        internal static void wrap_love_dll_graphics_setScissor()
        {
            _wrap_love_dll_graphics_setScissor();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setScissor_xywh")]
        internal extern static bool _wrap_love_dll_graphics_setScissor_xywh(int x, int y, int w, int h);
        internal static bool wrap_love_dll_graphics_setScissor_xywh(int x, int y, int w, int h)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_setScissor_xywh(x, y, w, h));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_intersectScissor")]
        internal extern static bool _wrap_love_dll_graphics_intersectScissor(int x, int y, int w, int h);
        internal static bool wrap_love_dll_graphics_intersectScissor(int x, int y, int w, int h)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_intersectScissor(x, y, w, h));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getScissor")]
        internal extern static void _wrap_love_dll_graphics_getScissor(out int out_x, out int out_y, out int out_w, out int out_h);
        internal static void wrap_love_dll_graphics_getScissor(out int out_x, out int out_y, out int out_w, out int out_h)
        {
            _wrap_love_dll_graphics_getScissor(out out_x, out out_y, out out_w, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setStencilTest")]
        internal extern static void _wrap_love_dll_graphics_setStencilTest(int compare_type, int compareValue);
        internal static void wrap_love_dll_graphics_setStencilTest(int compare_type, int compareValue)
        {
            _wrap_love_dll_graphics_setStencilTest(compare_type, compareValue);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getStencilTest")]
        internal extern static void _wrap_love_dll_graphics_getStencilTest(out int out_compare_type, out int out_compareValue);
        internal static void wrap_love_dll_graphics_getStencilTest(out int out_compare_type, out int out_compareValue)
        {
            _wrap_love_dll_graphics_getStencilTest(out out_compare_type, out out_compareValue);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setColor")]
        internal extern static void _wrap_love_dll_graphics_setColor(float r, float g, float b, float a);
        internal static void wrap_love_dll_graphics_setColor(float r, float g, float b, float a)
        {
            _wrap_love_dll_graphics_setColor(r, g, b, a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getColor")]
        internal extern static void _wrap_love_dll_graphics_getColor(out float out_r, out float out_g, out float out_b, out float out_a);
        internal static void wrap_love_dll_graphics_getColor(out float out_r, out float out_g, out float out_b, out float out_a)
        {
            _wrap_love_dll_graphics_getColor(out out_r, out out_g, out out_b, out out_a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setBackgroundColor")]
        internal extern static void _wrap_love_dll_graphics_setBackgroundColor(float r, float g, float b, float a);
        internal static void wrap_love_dll_graphics_setBackgroundColor(float r, float g, float b, float a)
        {
            _wrap_love_dll_graphics_setBackgroundColor(r, g, b, a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getBackgroundColor")]
        internal extern static void _wrap_love_dll_graphics_getBackgroundColor(out float out_r, out float out_g, out float out_b, out float out_a);
        internal static void wrap_love_dll_graphics_getBackgroundColor(out float out_r, out float out_g, out float out_b, out float out_a)
        {
            _wrap_love_dll_graphics_getBackgroundColor(out out_r, out out_g, out out_b, out out_a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setFont")]
        internal extern static void _wrap_love_dll_graphics_setFont(IntPtr font);
        internal static void wrap_love_dll_graphics_setFont(IntPtr font)
        {
            _wrap_love_dll_graphics_setFont(font);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getFont")]
        internal extern static bool _wrap_love_dll_graphics_getFont(out IntPtr out_font);
        internal static bool wrap_love_dll_graphics_getFont(out IntPtr out_font)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_getFont(out out_font));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setColorMask")]
        internal extern static void _wrap_love_dll_graphics_setColorMask(bool r, bool g, bool b, bool a);
        internal static void wrap_love_dll_graphics_setColorMask(bool r, bool g, bool b, bool a)
        {
            _wrap_love_dll_graphics_setColorMask(r, g, b, a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getColorMask")]
        internal extern static void _wrap_love_dll_graphics_getColorMask(out bool out_r, out bool out_g, out bool out_b, out bool out_a);
        internal static void wrap_love_dll_graphics_getColorMask(out bool out_r, out bool out_g, out bool out_b, out bool out_a)
        {
            _wrap_love_dll_graphics_getColorMask(out out_r, out out_g, out out_b, out out_a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setBlendMode")]
        internal extern static bool _wrap_love_dll_graphics_setBlendMode(int blendMode_type, int blendAlphaMode_type);
        internal static bool wrap_love_dll_graphics_setBlendMode(int blendMode_type, int blendAlphaMode_type)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_setBlendMode(blendMode_type, blendAlphaMode_type));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getBlendMode")]
        internal extern static void _wrap_love_dll_graphics_getBlendMode(out int out_blendMode_type, out int out_blendAlphaMode_type);
        internal static void wrap_love_dll_graphics_getBlendMode(out int out_blendMode_type, out int out_blendAlphaMode_type)
        {
            _wrap_love_dll_graphics_getBlendMode(out out_blendMode_type, out out_blendAlphaMode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setDefaultFilter")]
        internal extern static void _wrap_love_dll_graphics_setDefaultFilter(int filterModeMagMin_type, int filterModeMag_type, int anisotropy);
        internal static void wrap_love_dll_graphics_setDefaultFilter(int filterModeMagMin_type, int filterModeMag_type, int anisotropy)
        {
            _wrap_love_dll_graphics_setDefaultFilter(filterModeMagMin_type, filterModeMag_type, anisotropy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getDefaultFilter")]
        internal extern static void _wrap_love_dll_graphics_getDefaultFilter(out int out_filterModeMagMin_type, out int out_filterModeMag_type, out int out_anisotropy);
        internal static void wrap_love_dll_graphics_getDefaultFilter(out int out_filterModeMagMin_type, out int out_filterModeMag_type, out int out_anisotropy)
        {
            _wrap_love_dll_graphics_getDefaultFilter(out out_filterModeMagMin_type, out out_filterModeMag_type, out out_anisotropy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setLineWidth")]
        internal extern static void _wrap_love_dll_graphics_setLineWidth(float width);
        internal static void wrap_love_dll_graphics_setLineWidth(float width)
        {
            _wrap_love_dll_graphics_setLineWidth(width);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setLineStyle")]
        internal extern static void _wrap_love_dll_graphics_setLineStyle(int style_type);
        internal static void wrap_love_dll_graphics_setLineStyle(int style_type)
        {
            _wrap_love_dll_graphics_setLineStyle(style_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setLineJoin")]
        internal extern static void _wrap_love_dll_graphics_setLineJoin(int join_type);
        internal static void wrap_love_dll_graphics_setLineJoin(int join_type)
        {
            _wrap_love_dll_graphics_setLineJoin(join_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getLineWidth")]
        internal extern static void _wrap_love_dll_graphics_getLineWidth(out float out_result);
        internal static void wrap_love_dll_graphics_getLineWidth(out float out_result)
        {
            _wrap_love_dll_graphics_getLineWidth(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getLineStyle")]
        internal extern static void _wrap_love_dll_graphics_getLineStyle(out int out_lineStyle_type);
        internal static void wrap_love_dll_graphics_getLineStyle(out int out_lineStyle_type)
        {
            _wrap_love_dll_graphics_getLineStyle(out out_lineStyle_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getLineJoin")]
        internal extern static void _wrap_love_dll_graphics_getLineJoin(out int out_lineJoinStyle_type);
        internal static void wrap_love_dll_graphics_getLineJoin(out int out_lineJoinStyle_type)
        {
            _wrap_love_dll_graphics_getLineJoin(out out_lineJoinStyle_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setPointSize")]
        internal extern static void _wrap_love_dll_graphics_setPointSize(float size);
        internal static void wrap_love_dll_graphics_setPointSize(float size)
        {
            _wrap_love_dll_graphics_setPointSize(size);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getPointSize")]
        internal extern static void _wrap_love_dll_graphics_getPointSize(out float out_size);
        internal static void wrap_love_dll_graphics_getPointSize(out float out_size)
        {
            _wrap_love_dll_graphics_getPointSize(out out_size);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setWireframe")]
        internal extern static void _wrap_love_dll_graphics_setWireframe(bool enable);
        internal static void wrap_love_dll_graphics_setWireframe(bool enable)
        {
            _wrap_love_dll_graphics_setWireframe(enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_isWireframe")]
        internal extern static void _wrap_love_dll_graphics_isWireframe(out bool out_isWireFrame);
        internal static void wrap_love_dll_graphics_isWireframe(out bool out_isWireFrame)
        {
            _wrap_love_dll_graphics_isWireframe(out out_isWireFrame);
        }
        //[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setCanvas")]
        //internal extern static bool _wrap_love_dll_graphics_setCanvas(IntPtr[] canvaList, int canvaListLength);
        //internal static bool wrap_love_dll_graphics_setCanvas(IntPtr[] canvaList, int canvaListLength)
        //{
        //    return CheckCAPIException(_wrap_love_dll_graphics_setCanvas(canvaList, canvaListLength));
        //}

        //[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getCanvas")]
        //internal extern static void _wrap_love_dll_graphics_getCanvas(out IntPtr out_canvas, out int out_canvas_lenght);
        //internal static void wrap_love_dll_graphics_getCanvas(out IntPtr out_canvas, out int out_canvas_lenght)
        //{
        //    _wrap_love_dll_graphics_getCanvas(out out_canvas, out out_canvas_lenght);
        //}

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setCanvasEmpty")]
        internal extern static bool _wrap_love_dll_graphics_setCanvasEmpty();
        internal static bool wrap_love_dll_graphics_setCanvasEmpty()
        {
            return CheckCAPIException(_wrap_love_dll_graphics_setCanvasEmpty());
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setCanvasRenderTagets")]
        internal extern static bool _wrap_love_dll_graphics_setCanvasRenderTagets(IntPtr[] canvaList, int[] sliceList, int[] mipmapList, int canvaListLength, bool depth, bool stencil);
        internal static bool wrap_love_dll_graphics_setCanvasRenderTagets(IntPtr[] canvaList, int[] sliceList, int[] mipmapList, int canvaListLength, bool depth, bool stencil)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_setCanvasRenderTagets(canvaList, sliceList, mipmapList, canvaListLength, depth, stencil));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getCanvasTagets")]
        internal extern static bool _wrap_love_dll_graphics_getCanvasTagets(out IntPtr canvas, out IntPtr sliceList, out IntPtr mipmapList, out int targetCount);
        internal static bool wrap_love_dll_graphics_getCanvasTagets(out IntPtr canvas, out IntPtr sliceList, out IntPtr mipmapList, out int targetCount)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_getCanvasTagets(out canvas, out sliceList, out mipmapList, out targetCount));
        }


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setShader")]
        internal extern static void _wrap_love_dll_graphics_setShader(IntPtr shader);
        internal static void wrap_love_dll_graphics_setShader(IntPtr shader)
        {
            _wrap_love_dll_graphics_setShader(shader);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getShader")]
        internal extern static void _wrap_love_dll_graphics_getShader(out IntPtr out_shader);
        internal static void wrap_love_dll_graphics_getShader(out IntPtr out_shader)
        {
            _wrap_love_dll_graphics_getShader(out out_shader);
        }


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setDefaultShaderCode")]
        internal extern static void _wrap_love_dll_graphics_setDefaultShaderCode(IntPtr[] strPtr);
        internal static void wrap_love_dll_graphics_setDefaultShaderCode(IntPtr[] strPtr)
        {
            _wrap_love_dll_graphics_setDefaultShaderCode(strPtr);
        }

        #endregion


        #region  graphics 11.0
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_drawInstanced")]
        internal extern static bool _wrap_love_dll_graphics_drawInstanced(IntPtr tmesh, int instanceCount, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_graphics_drawInstanced(IntPtr tmesh, int instanceCount, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_drawInstanced(tmesh, instanceCount, x, y, a, sx, sy, ox, oy, kx, ky));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_drawLayer")]
        internal extern static bool _wrap_love_dll_graphics_drawLayer(IntPtr texture, IntPtr quad, int layer, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_graphics_drawLayer(IntPtr texture, IntPtr quad, int layer, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_drawLayer(texture, quad, layer, x, y, a, sx, sy, ox, oy, kx, ky));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_flushBatch")]
        internal extern static void _wrap_love_dll_graphics_flushBatch();
        internal static void wrap_love_dll_graphics_flushBatch()
        {
            _wrap_love_dll_graphics_flushBatch();
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_validateShader")]
        internal extern static void _wrap_love_dll_graphics_validateShader(bool gles, byte[] vertexCodeStr, byte[] pixelCodeStr, out bool success, out IntPtr str);
        internal static void wrap_love_dll_graphics_validateShader(bool gles, byte[] vertexCodeStr, byte[] pixelCodeStr, out bool success, out IntPtr str)
        {
            _wrap_love_dll_graphics_validateShader(gles, vertexCodeStr, pixelCodeStr, out success, out str);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getDepthMode")]
        internal extern static void _wrap_love_dll_graphics_getDepthMode(out int depthMode, out bool write);
        internal static void wrap_love_dll_graphics_getDepthMode(out int depthMode, out bool write)
        {
            _wrap_love_dll_graphics_getDepthMode(out depthMode, out write);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getFrontFaceWinding")]
        internal extern static void _wrap_love_dll_graphics_getFrontFaceWinding(out int winding);
        internal static void wrap_love_dll_graphics_getFrontFaceWinding(out int winding)
        {
            _wrap_love_dll_graphics_getFrontFaceWinding(out winding);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getMeshCullMode")]
        internal extern static void _wrap_love_dll_graphics_getMeshCullMode(out int mode);
        internal static void wrap_love_dll_graphics_getMeshCullMode(out int mode)
        {
            _wrap_love_dll_graphics_getMeshCullMode(out mode);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getStackDepth")]
        internal extern static void _wrap_love_dll_graphics_getStackDepth(out int depth);
        internal static void wrap_love_dll_graphics_getStackDepth(out int depth)
        {
            _wrap_love_dll_graphics_getStackDepth(out depth);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setDepthMode")]
        internal extern static bool _wrap_love_dll_graphics_setDepthMode(int odepthMode, bool write);
        internal static bool wrap_love_dll_graphics_setDepthMode(int odepthMode, bool write)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_setDepthMode(odepthMode, write));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setMeshCullMode")]
        internal extern static bool _wrap_love_dll_graphics_setMeshCullMode(int mode);
        internal static bool wrap_love_dll_graphics_setMeshCullMode(int mode)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_setMeshCullMode(mode));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getTextureTypes")]
        internal extern static void _wrap_love_dll_graphics_getTextureTypes(out IntPtr capList, out int len);
        internal static void wrap_love_dll_graphics_getTextureTypes(out IntPtr capList, out int len)
        {
            _wrap_love_dll_graphics_getTextureTypes(out capList, out len);
        }
        #endregion


        #region  graphics Coordinate System

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_push")]
        internal extern static bool _wrap_love_dll_graphics_push(int stack_type);
        internal static bool wrap_love_dll_graphics_push(int stack_type)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_push(stack_type));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_pop")]
        internal extern static bool _wrap_love_dll_graphics_pop();
        internal static bool wrap_love_dll_graphics_pop()
        {
            return CheckCAPIException(_wrap_love_dll_graphics_pop());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_rotate")]
        internal extern static void _wrap_love_dll_graphics_rotate(float angle);
        internal static void wrap_love_dll_graphics_rotate(float angle)
        {
            _wrap_love_dll_graphics_rotate(angle);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_scale")]
        internal extern static void _wrap_love_dll_graphics_scale(float sx, float sy);
        internal static void wrap_love_dll_graphics_scale(float sx, float sy)
        {
            _wrap_love_dll_graphics_scale(sx, sy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_translate")]
        internal extern static void _wrap_love_dll_graphics_translate(float x, float y);
        internal static void wrap_love_dll_graphics_translate(float x, float y)
        {
            _wrap_love_dll_graphics_translate(x, y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_shear")]
        internal extern static void _wrap_love_dll_graphics_shear(float kx, float ky);
        internal static void wrap_love_dll_graphics_shear(float kx, float ky)
        {
            _wrap_love_dll_graphics_shear(kx, ky);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_origin")]
        internal extern static void _wrap_love_dll_graphics_origin();
        internal static void wrap_love_dll_graphics_origin()
        {
            _wrap_love_dll_graphics_origin();
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_inverseTransformPoint")]
        internal extern static void _wrap_love_dll_graphics_inverseTransformPoint(float x, float y, out float out_x, out float out_y);
        internal static void wrap_love_dll_graphics_inverseTransformPoint(float x, float y, out float out_x, out float out_y)
        {
            _wrap_love_dll_graphics_inverseTransformPoint(x, y, out out_x, out out_y);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_transformPoint")]
        internal extern static void _wrap_love_dll_graphics_transformPoint(float x, float y, out float out_x, out float out_y);
        internal static void wrap_love_dll_graphics_transformPoint(float x, float y, out float out_x, out float out_y)
        {
            _wrap_love_dll_graphics_transformPoint(x, y, out out_x, out out_y);
        }


        #endregion
        #region  graphics drawing

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer")]
        internal extern static void _wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer(int action_type, int stencilValue);
        internal static void wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer(int action_type, int stencilValue)
        {
            _wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer(action_type, stencilValue);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer")]
        internal extern static void _wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer();
        internal static void wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer()
        {
            _wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_clear_rgba")]
        internal extern static bool _wrap_love_dll_graphics_clear_rgba(float r, float g, float b, float a, float stencil, bool enable_stencil, float depth, bool enable_depth);
        internal static bool wrap_love_dll_graphics_clear_rgba(float r, float g, float b, float a, float stencil, bool enable_stencil, float depth, bool enable_depth)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_clear_rgba(r, g, b, a, stencil, enable_stencil, depth, enable_depth));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_clear_rgbalist")]
        internal extern static bool _wrap_love_dll_graphics_clear_rgbalist(Vector4[] colorList, bool[] enableList, int listLength, float stencil, bool enable_stencil, float depth, bool enable_depth);
        internal static bool wrap_love_dll_graphics_clear_rgbalist(Vector4[] colorList, bool[] enableList, int listLength, float stencil, bool enable_stencil, float depth, bool enable_depth)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_clear_rgbalist(colorList, enableList, listLength, stencil, enable_stencil, depth, enable_depth));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_discard")]
        internal extern static void _wrap_love_dll_graphics_discard(bool[] discardColors, int discardColorsLength, bool discardStencil);
        internal static void wrap_love_dll_graphics_discard(bool[] discardColors, int discardColorsLength, bool discardStencil)
        {
            _wrap_love_dll_graphics_discard(discardColors, discardColorsLength, discardStencil);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_present")]
        internal extern static void _wrap_love_dll_graphics_present();
        internal static void wrap_love_dll_graphics_present()
        {
            _wrap_love_dll_graphics_present();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_draw_drawable")]
        internal extern static bool _wrap_love_dll_graphics_draw_drawable(IntPtr drawable, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_graphics_draw_drawable(IntPtr drawable, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_draw_drawable(drawable, x, y, a, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_draw_texture_quad")]
        internal extern static bool _wrap_love_dll_graphics_draw_texture_quad(IntPtr quad, IntPtr texture, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_graphics_draw_texture_quad(IntPtr quad, IntPtr texture, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_draw_texture_quad(quad, texture, x, y, a, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_print")]
        internal extern static bool _wrap_love_dll_graphics_print(IntPtr[] coloredStringListStr, Vector4[] coloredStringListColor, int coloredStringListLength, float x, float y, float angle, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_graphics_print(IntPtr[] coloredStringListStr, Vector4[] coloredStringListColor, int coloredStringListLength, float x, float y, float angle, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_print(coloredStringListStr, coloredStringListColor, coloredStringListLength, x, y, angle, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_printf")]
        internal extern static bool _wrap_love_dll_graphics_printf(BytePtr[] coloredStringListStr, Vector4[] coloredStringListColor, int coloredStringListLength, float x, float y, float wrap, int align_type, float angle, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_graphics_printf(BytePtr[] coloredStringListStr, Vector4[] coloredStringListColor, int coloredStringListLength, float x, float y, float wrap, int align_type, float angle, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_printf(coloredStringListStr, coloredStringListColor, coloredStringListLength, x, y, wrap, align_type, angle, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_rectangle")]
        internal extern static bool _wrap_love_dll_graphics_rectangle(int mode_type, float x, float y, float w, float h);
        internal static bool wrap_love_dll_graphics_rectangle(int mode_type, float x, float y, float w, float h)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_rectangle(mode_type, x, y, w, h));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_rectangle_batch")]
        internal extern static bool _wrap_love_dll_graphics_rectangle_batch(int mode_type, RectangleF[] rectArray, int rectArrayLenght);
        internal static bool wrap_love_dll_graphics_rectangle_batch(int mode_type, RectangleF[] rectArray, int rectArrayLenght)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_rectangle_batch(mode_type, rectArray, rectArrayLenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_rectangle_with_rounded_corners")]
        internal extern static bool _wrap_love_dll_graphics_rectangle_with_rounded_corners(int mode_type, float x, float y, float w, float h, float rx, float ry, int points);
        internal static bool wrap_love_dll_graphics_rectangle_with_rounded_corners(int mode_type, float x, float y, float w, float h, float rx, float ry, int points)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_rectangle_with_rounded_corners(mode_type, x, y, w, h, rx, ry, points));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_circle")]
        internal extern static bool _wrap_love_dll_graphics_circle(int mode_type, float x, float y, float radius, int points);
        internal static bool wrap_love_dll_graphics_circle(int mode_type, float x, float y, float radius, int points)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_circle(mode_type, x, y, radius, points));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_ellipse")]
        internal extern static bool _wrap_love_dll_graphics_ellipse(int mode_type, float x, float y, float a, float b, int points);
        internal static bool wrap_love_dll_graphics_ellipse(int mode_type, float x, float y, float a, float b, int points)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_ellipse(mode_type, x, y, a, b, points));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_arc")]
        internal extern static bool _wrap_love_dll_graphics_arc(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2);
        internal static bool wrap_love_dll_graphics_arc(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_arc(mode_type, arcmode_type, x, y, radius, angle1, angle2));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_arc_points")]
        internal extern static bool _wrap_love_dll_graphics_arc_points(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2, int points);
        internal static bool wrap_love_dll_graphics_arc_points(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2, int points)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_arc_points(mode_type, arcmode_type, x, y, radius, angle1, angle2, points));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_points")]
        internal extern static bool _wrap_love_dll_graphics_points(Vector2[] coords, int coordsLength);
        internal static bool wrap_love_dll_graphics_points(Vector2[] coords, int coordsLength)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_points(coords, coordsLength));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_points_colors")]
        internal extern static bool _wrap_love_dll_graphics_points_colors(Vector2[] coords, Vector4[] colors, int coordsLength);
        internal static bool wrap_love_dll_graphics_points_colors(Vector2[] coords, Vector4[] colors, int coordsLength)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_points_colors(coords, colors, coordsLength));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_line")]
        internal extern static bool _wrap_love_dll_graphics_line(Vector2[] coords, int coordsLength);
        internal static bool wrap_love_dll_graphics_line(Vector2[] coords, int coordsLength)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_line(coords, coordsLength));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_polygon")]
        internal extern static bool _wrap_love_dll_graphics_polygon(int mode_type, Vector2[] coords, int coordsLength);
        internal static bool wrap_love_dll_graphics_polygon(int mode_type, Vector2[] coords, int coordsLength)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_polygon(mode_type, coords, coordsLength));
        }



        #endregion
        #region  graphics Window

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_isCreated")]
        internal extern static void _wrap_love_dll_graphics_isCreated(out bool out_reslut);
        internal static void wrap_love_dll_graphics_isCreated(out bool out_reslut)
        {
            _wrap_love_dll_graphics_isCreated(out out_reslut);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getDPIScale")]
        internal extern static void _wrap_love_dll_graphics_getDPIScale(out double out_dpiscale);
        internal static void wrap_love_dll_graphics_getDPIScale(out double out_dpiscale)
        {
            _wrap_love_dll_graphics_getDPIScale(out out_dpiscale);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getWidth")]
        internal extern static void _wrap_love_dll_graphics_getWidth(out int out_width);
        internal static void wrap_love_dll_graphics_getWidth(out int out_width)
        {
            _wrap_love_dll_graphics_getWidth(out out_width);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getHeight")]
        internal extern static void _wrap_love_dll_graphics_getHeight(out int out_height);
        internal static void wrap_love_dll_graphics_getHeight(out int out_height)
        {
            _wrap_love_dll_graphics_getHeight(out out_height);
        }



        #endregion
        #region  graphics System Information

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getSupported")]
        internal extern static void _wrap_love_dll_graphics_getSupported(int feature_type, out bool out_result);
        internal static void wrap_love_dll_graphics_getSupported(int feature_type, out bool out_result)
        {
            _wrap_love_dll_graphics_getSupported(feature_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getCanvasFormats")]
        internal extern static void _wrap_love_dll_graphics_getCanvasFormats(int format_type, out bool out_result);
        internal static void wrap_love_dll_graphics_getCanvasFormats(int format_type, out bool out_result)
        {
            _wrap_love_dll_graphics_getCanvasFormats(format_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getImageFormats")]
        internal extern static void _wrap_love_dll_graphics_getImageFormats(int format_type, out bool out_result);
        internal static void wrap_love_dll_graphics_getImageFormats(int format_type, out bool out_result)
        {
            _wrap_love_dll_graphics_getImageFormats(format_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getRendererInfo")]
        internal extern static void _wrap_love_dll_graphics_getRendererInfo(out IntPtr out_wss);
        internal static void wrap_love_dll_graphics_getRendererInfo(out IntPtr out_wss)
        {
            _wrap_love_dll_graphics_getRendererInfo(out out_wss);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getSystemLimits")]
        internal extern static void _wrap_love_dll_graphics_getSystemLimits(int limit_type, out double out_result);
        internal static void wrap_love_dll_graphics_getSystemLimits(int limit_type, out double out_result)
        {
            _wrap_love_dll_graphics_getSystemLimits(limit_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getStats")]
        internal extern static void _wrap_love_dll_graphics_getStats(out int out_drawCalls, out int out_canvasSwitches, out int out_shaderSwitches, out int out_canvases, out int out_images, out int out_fonts, out int out_textureMemory);
        internal static void wrap_love_dll_graphics_getStats(out int out_drawCalls, out int out_canvasSwitches, out int out_shaderSwitches, out int out_canvases, out int out_images, out int out_fonts, out int out_textureMemory)
        {
            _wrap_love_dll_graphics_getStats(out out_drawCalls, out out_canvasSwitches, out out_shaderSwitches, out out_canvases, out out_images, out out_fonts, out out_textureMemory);
        }



        #endregion
        #region  type - Source

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_clone")]
        internal extern static bool _wrap_love_dll_type_Source_clone(IntPtr t, out IntPtr out_clone);
        internal static bool wrap_love_dll_type_Source_clone(IntPtr t, out IntPtr out_clone)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_clone(t, out out_clone));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_play")]
        internal extern static void _wrap_love_dll_type_Source_play(IntPtr t, out bool out_success);
        internal static void wrap_love_dll_type_Source_play(IntPtr t, out bool out_success)
        {
            _wrap_love_dll_type_Source_play(t, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_stop")]
        internal extern static void _wrap_love_dll_type_Source_stop(IntPtr t);
        internal static void wrap_love_dll_type_Source_stop(IntPtr t)
        {
            _wrap_love_dll_type_Source_stop(t);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_pause")]
        internal extern static void _wrap_love_dll_type_Source_pause(IntPtr t);
        internal static void wrap_love_dll_type_Source_pause(IntPtr t)
        {
            _wrap_love_dll_type_Source_pause(t);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setPitch")]
        internal extern static bool _wrap_love_dll_type_Source_setPitch(IntPtr t, float pitch);
        internal static bool wrap_love_dll_type_Source_setPitch(IntPtr t, float pitch)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setPitch(t, pitch));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getPitch")]
        internal extern static void _wrap_love_dll_type_Source_getPitch(IntPtr t, out float out_pitch);
        internal static void wrap_love_dll_type_Source_getPitch(IntPtr t, out float out_pitch)
        {
            _wrap_love_dll_type_Source_getPitch(t, out out_pitch);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setVolume")]
        internal extern static void _wrap_love_dll_type_Source_setVolume(IntPtr t, float p);
        internal static void wrap_love_dll_type_Source_setVolume(IntPtr t, float p)
        {
            _wrap_love_dll_type_Source_setVolume(t, p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getVolume")]
        internal extern static void _wrap_love_dll_type_Source_getVolume(IntPtr t, out float out_volume);
        internal static void wrap_love_dll_type_Source_getVolume(IntPtr t, out float out_volume)
        {
            _wrap_love_dll_type_Source_getVolume(t, out out_volume);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_seek")]
        internal extern static void _wrap_love_dll_type_Source_seek(IntPtr t, float offset, int unit_type);
        internal static void wrap_love_dll_type_Source_seek(IntPtr t, float offset, int unit_type)
        {
            _wrap_love_dll_type_Source_seek(t, offset, unit_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_tell")]
        internal extern static void _wrap_love_dll_type_Source_tell(IntPtr t, int unit_type, out float out_position);
        internal static void wrap_love_dll_type_Source_tell(IntPtr t, int unit_type, out float out_position)
        {
            _wrap_love_dll_type_Source_tell(t, unit_type, out out_position);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getDuration")]
        internal extern static void _wrap_love_dll_type_Source_getDuration(IntPtr t, int unit_type, out float out_duration);
        internal static void wrap_love_dll_type_Source_getDuration(IntPtr t, int unit_type, out float out_duration)
        {
            _wrap_love_dll_type_Source_getDuration(t, unit_type, out out_duration);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setPosition")]
        internal extern static bool _wrap_love_dll_type_Source_setPosition(IntPtr t, float x, float y, float z);
        internal static bool wrap_love_dll_type_Source_setPosition(IntPtr t, float x, float y, float z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setPosition(t, x, y, z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getPosition")]
        internal extern static bool _wrap_love_dll_type_Source_getPosition(IntPtr t, out float out_x, out float out_y, out float out_z);
        internal static bool wrap_love_dll_type_Source_getPosition(IntPtr t, out float out_x, out float out_y, out float out_z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getPosition(t, out out_x, out out_y, out out_z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setVelocity")]
        internal extern static bool _wrap_love_dll_type_Source_setVelocity(IntPtr t, float x, float y, float z);
        internal static bool wrap_love_dll_type_Source_setVelocity(IntPtr t, float x, float y, float z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setVelocity(t, x, y, z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getVelocity")]
        internal extern static bool _wrap_love_dll_type_Source_getVelocity(IntPtr t, out float out_x, out float out_y, out float out_z);
        internal static bool wrap_love_dll_type_Source_getVelocity(IntPtr t, out float out_x, out float out_y, out float out_z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getVelocity(t, out out_x, out out_y, out out_z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setDirection")]
        internal extern static bool _wrap_love_dll_type_Source_setDirection(IntPtr t, float x, float y, float z);
        internal static bool wrap_love_dll_type_Source_setDirection(IntPtr t, float x, float y, float z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setDirection(t, x, y, z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getDirection")]
        internal extern static bool _wrap_love_dll_type_Source_getDirection(IntPtr t, out float out_x, out float out_y, out float out_z);
        internal static bool wrap_love_dll_type_Source_getDirection(IntPtr t, out float out_x, out float out_y, out float out_z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getDirection(t, out out_x, out out_y, out out_z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setCone")]
        internal extern static bool _wrap_love_dll_type_Source_setCone(IntPtr t, float innerAngle, float outerAngle, float outerVolume);
        internal static bool wrap_love_dll_type_Source_setCone(IntPtr t, float innerAngle, float outerAngle, float outerVolume)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setCone(t, innerAngle, outerAngle, outerVolume));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getCone")]
        internal extern static bool _wrap_love_dll_type_Source_getCone(IntPtr t, out float out_innerAngle, out float out_outerAngle, out float out_outerVolume);
        internal static bool wrap_love_dll_type_Source_getCone(IntPtr t, out float out_innerAngle, out float out_outerAngle, out float out_outerVolume)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getCone(t, out out_innerAngle, out out_outerAngle, out out_outerVolume));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setRelative")]
        internal extern static bool _wrap_love_dll_type_Source_setRelative(IntPtr t, bool relative);
        internal static bool wrap_love_dll_type_Source_setRelative(IntPtr t, bool relative)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setRelative(t, relative));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_isRelative")]
        internal extern static bool _wrap_love_dll_type_Source_isRelative(IntPtr t, out bool out_relative);
        internal static bool wrap_love_dll_type_Source_isRelative(IntPtr t, out bool out_relative)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_isRelative(t, out out_relative));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setLooping")]
        internal extern static void _wrap_love_dll_type_Source_setLooping(IntPtr t, bool looping);
        internal static void wrap_love_dll_type_Source_setLooping(IntPtr t, bool looping)
        {
            _wrap_love_dll_type_Source_setLooping(t, looping);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_isLooping")]
        internal extern static void _wrap_love_dll_type_Source_isLooping(IntPtr t, out bool out_result);
        internal static void wrap_love_dll_type_Source_isLooping(IntPtr t, out bool out_result)
        {
            _wrap_love_dll_type_Source_isLooping(t, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_isPlaying")]
        internal extern static void _wrap_love_dll_type_Source_isPlaying(IntPtr t, out bool out_result);
        internal static void wrap_love_dll_type_Source_isPlaying(IntPtr t, out bool out_result)
        {
            _wrap_love_dll_type_Source_isPlaying(t, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setVolumeLimits")]
        internal extern static bool _wrap_love_dll_type_Source_setVolumeLimits(IntPtr t, float vmin, float vmax);
        internal static bool wrap_love_dll_type_Source_setVolumeLimits(IntPtr t, float vmin, float vmax)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setVolumeLimits(t, vmin, vmax));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getVolumeLimits")]
        internal extern static void _wrap_love_dll_type_Source_getVolumeLimits(IntPtr t, out float out_vmin, out float out_vmax);
        internal static void wrap_love_dll_type_Source_getVolumeLimits(IntPtr t, out float out_vmin, out float out_vmax)
        {
            _wrap_love_dll_type_Source_getVolumeLimits(t, out out_vmin, out out_vmax);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setAttenuationDistances")]
        internal extern static bool _wrap_love_dll_type_Source_setAttenuationDistances(IntPtr t, float dref, float dmax);
        internal static bool wrap_love_dll_type_Source_setAttenuationDistances(IntPtr t, float dref, float dmax)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setAttenuationDistances(t, dref, dmax));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getAttenuationDistances")]
        internal extern static bool _wrap_love_dll_type_Source_getAttenuationDistances(IntPtr t, out float out_dref, out float out_dmax);
        internal static bool wrap_love_dll_type_Source_getAttenuationDistances(IntPtr t, out float out_dref, out float out_dmax)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getAttenuationDistances(t, out out_dref, out out_dmax));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setRolloff")]
        internal extern static bool _wrap_love_dll_type_Source_setRolloff(IntPtr t, float rolloff);
        internal static bool wrap_love_dll_type_Source_setRolloff(IntPtr t, float rolloff)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setRolloff(t, rolloff));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getRolloff")]
        internal extern static bool _wrap_love_dll_type_Source_getRolloff(IntPtr t, out float out_rolloff);
        internal static bool wrap_love_dll_type_Source_getRolloff(IntPtr t, out float out_rolloff)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getRolloff(t, out out_rolloff));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getChannelCount")]
        internal extern static void _wrap_love_dll_type_Source_getChannelCount(IntPtr t, out int out_chanels);
        internal static void wrap_love_dll_type_Source_getChannelCount(IntPtr t, out int out_chanels)
        {
            _wrap_love_dll_type_Source_getChannelCount(t, out out_chanels);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getType")]
        internal extern static void _wrap_love_dll_type_Source_getType(IntPtr t, out int out_type);
        internal static void wrap_love_dll_type_Source_getType(IntPtr t, out int out_type)
        {
            _wrap_love_dll_type_Source_getType(t, out out_type);
        }



        #endregion
        #region  type - File

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_getSize")]
        internal extern static bool _wrap_love_dll_type_File_getSize(IntPtr file, out double out_size);
        internal static bool wrap_love_dll_type_File_getSize(IntPtr file, out double out_size)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_getSize(file, out out_size));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_open")]
        internal extern static bool _wrap_love_dll_type_File_open(IntPtr file, int mode_type);
        internal static bool wrap_love_dll_type_File_open(IntPtr file, int mode_type)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_open(file, mode_type));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_close")]
        internal extern static void _wrap_love_dll_type_File_close(IntPtr file, out bool out_result);
        internal static void wrap_love_dll_type_File_close(IntPtr file, out bool out_result)
        {
            _wrap_love_dll_type_File_close(file, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_isOpen")]
        internal extern static void _wrap_love_dll_type_File_isOpen(IntPtr file, out bool out_result);
        internal static void wrap_love_dll_type_File_isOpen(IntPtr file, out bool out_result)
        {
            _wrap_love_dll_type_File_isOpen(file, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_read")]
        internal extern static bool _wrap_love_dll_type_File_read(IntPtr file, Int64 size, out IntPtr out_data, out Int64 out_readedSize);
        internal static bool wrap_love_dll_type_File_read(IntPtr file, Int64 size, out IntPtr out_data, out Int64 out_readedSize)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_read(file, size, out out_data, out out_readedSize));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_write_String")]
        internal extern static bool _wrap_love_dll_type_File_write_String(IntPtr file, byte[] data, Int64 datasize);
        internal static bool wrap_love_dll_type_File_write_String(IntPtr file, byte[] data, Int64 datasize)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_write_String(file, data, datasize));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_write_Data_datasize")]
        internal extern static bool _wrap_love_dll_type_File_write_Data_datasize(IntPtr file, IntPtr data, Int64 datasize);
        internal static bool wrap_love_dll_type_File_write_Data_datasize(IntPtr file, IntPtr data, Int64 datasize)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_write_Data_datasize(file, data, datasize));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_flush")]
        internal extern static bool _wrap_love_dll_type_File_flush(IntPtr file);
        internal static bool wrap_love_dll_type_File_flush(IntPtr file)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_flush(file));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_isEOF")]
        internal extern static void _wrap_love_dll_type_File_isEOF(IntPtr file, out bool out_result);
        internal static void wrap_love_dll_type_File_isEOF(IntPtr file, out bool out_result)
        {
            _wrap_love_dll_type_File_isEOF(file, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_tell")]
        internal extern static bool _wrap_love_dll_type_File_tell(IntPtr file, out Int64 out_pos);
        internal static bool wrap_love_dll_type_File_tell(IntPtr file, out Int64 out_pos)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_tell(file, out out_pos));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_seek")]
        internal extern static bool _wrap_love_dll_type_File_seek(IntPtr file, Int64 pos);
        internal static bool wrap_love_dll_type_File_seek(IntPtr file, Int64 pos)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_seek(file, pos));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_setBuffer")]
        internal extern static bool _wrap_love_dll_type_File_setBuffer(IntPtr file, int bufmode_type, Int64 size);
        internal static bool wrap_love_dll_type_File_setBuffer(IntPtr file, int bufmode_type, Int64 size)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_setBuffer(file, bufmode_type, size));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_getBuffer")]
        internal extern static void _wrap_love_dll_type_File_getBuffer(IntPtr file, out int out_bufmode_type, out Int64 out_size);
        internal static void wrap_love_dll_type_File_getBuffer(IntPtr file, out int out_bufmode_type, out Int64 out_size)
        {
            _wrap_love_dll_type_File_getBuffer(file, out out_bufmode_type, out out_size);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_getMode")]
        internal extern static void _wrap_love_dll_type_File_getMode(IntPtr file, out int out_mode_type);
        internal static void wrap_love_dll_type_File_getMode(IntPtr file, out int out_mode_type)
        {
            _wrap_love_dll_type_File_getMode(file, out out_mode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_getFilename")]
        internal extern static void _wrap_love_dll_type_File_getFilename(IntPtr file, out IntPtr out_filename);
        internal static void wrap_love_dll_type_File_getFilename(IntPtr file, out IntPtr out_filename)
        {
            _wrap_love_dll_type_File_getFilename(file, out out_filename);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_getExtension")]
        internal extern static void _wrap_love_dll_type_File_getExtension(IntPtr file, out IntPtr out_extension);
        internal static void wrap_love_dll_type_File_getExtension(IntPtr file, out IntPtr out_extension)
        {
            _wrap_love_dll_type_File_getExtension(file, out out_extension);
        }



        #endregion
        #region  type - FileData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_FileData_getFilename")]
        internal extern static void _wrap_love_dll_type_FileData_getFilename(IntPtr t, out IntPtr out_filename);
        internal static void wrap_love_dll_type_FileData_getFilename(IntPtr t, out IntPtr out_filename)
        {
            _wrap_love_dll_type_FileData_getFilename(t, out out_filename);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_FileData_getExtension")]
        internal extern static void _wrap_love_dll_type_FileData_getExtension(IntPtr t, out IntPtr out_extension);
        internal static void wrap_love_dll_type_FileData_getExtension(IntPtr t, out IntPtr out_extension)
        {
            _wrap_love_dll_type_FileData_getExtension(t, out out_extension);
        }



        #endregion
        #region  type - GlyphData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getWidth")]
        internal extern static void _wrap_love_dll_type_GlyphData_getWidth(IntPtr t, out int out_width);
        internal static void wrap_love_dll_type_GlyphData_getWidth(IntPtr t, out int out_width)
        {
            _wrap_love_dll_type_GlyphData_getWidth(t, out out_width);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getHeight")]
        internal extern static void _wrap_love_dll_type_GlyphData_getHeight(IntPtr t, out int out_height);
        internal static void wrap_love_dll_type_GlyphData_getHeight(IntPtr t, out int out_height)
        {
            _wrap_love_dll_type_GlyphData_getHeight(t, out out_height);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getGlyph")]
        internal extern static void _wrap_love_dll_type_GlyphData_getGlyph(IntPtr t, out uint out_glyph);
        internal static void wrap_love_dll_type_GlyphData_getGlyph(IntPtr t, out uint out_glyph)
        {
            _wrap_love_dll_type_GlyphData_getGlyph(t, out out_glyph);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getGlyphString")]
        internal extern static bool _wrap_love_dll_type_GlyphData_getGlyphString(IntPtr t, out IntPtr out_str);
        internal static bool wrap_love_dll_type_GlyphData_getGlyphString(IntPtr t, out IntPtr out_str)
        {
            return CheckCAPIException(_wrap_love_dll_type_GlyphData_getGlyphString(t, out out_str));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getAdvance")]
        internal extern static void _wrap_love_dll_type_GlyphData_getAdvance(IntPtr t, out int out_advance);
        internal static void wrap_love_dll_type_GlyphData_getAdvance(IntPtr t, out int out_advance)
        {
            _wrap_love_dll_type_GlyphData_getAdvance(t, out out_advance);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getBearing")]
        internal extern static void _wrap_love_dll_type_GlyphData_getBearing(IntPtr t, out int out_bearingX, out int out_bearingY);
        internal static void wrap_love_dll_type_GlyphData_getBearing(IntPtr t, out int out_bearingX, out int out_bearingY)
        {
            _wrap_love_dll_type_GlyphData_getBearing(t, out out_bearingX, out out_bearingY);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getBoundingBox")]
        internal extern static void _wrap_love_dll_type_GlyphData_getBoundingBox(IntPtr t, out int out_minX, out int out_minY, out int out_width, out int out_height);
        internal static void wrap_love_dll_type_GlyphData_getBoundingBox(IntPtr t, out int out_minX, out int out_minY, out int out_width, out int out_height)
        {
            _wrap_love_dll_type_GlyphData_getBoundingBox(t, out out_minX, out out_minY, out out_width, out out_height);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getFormat")]
        internal extern static void _wrap_love_dll_type_GlyphData_getFormat(IntPtr t, out int out_format_type);
        internal static void wrap_love_dll_type_GlyphData_getFormat(IntPtr t, out int out_format_type)
        {
            _wrap_love_dll_type_GlyphData_getFormat(t, out out_format_type);
        }



        #endregion
        #region  type - Rasterizer

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getHeight")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getHeight(IntPtr t, out int out_heigth);
        internal static void wrap_love_dll_type_Rasterizer_getHeight(IntPtr t, out int out_heigth)
        {
            _wrap_love_dll_type_Rasterizer_getHeight(t, out out_heigth);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getAdvance")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getAdvance(IntPtr t, out int out_advance);
        internal static void wrap_love_dll_type_Rasterizer_getAdvance(IntPtr t, out int out_advance)
        {
            _wrap_love_dll_type_Rasterizer_getAdvance(t, out out_advance);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getAscent")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getAscent(IntPtr t, out int out_ascent);
        internal static void wrap_love_dll_type_Rasterizer_getAscent(IntPtr t, out int out_ascent)
        {
            _wrap_love_dll_type_Rasterizer_getAscent(t, out out_ascent);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getDescent")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getDescent(IntPtr t, out int out_descent);
        internal static void wrap_love_dll_type_Rasterizer_getDescent(IntPtr t, out int out_descent)
        {
            _wrap_love_dll_type_Rasterizer_getDescent(t, out out_descent);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getLineHeight")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getLineHeight(IntPtr t, out int out_lineHeight);
        internal static void wrap_love_dll_type_Rasterizer_getLineHeight(IntPtr t, out int out_lineHeight)
        {
            _wrap_love_dll_type_Rasterizer_getLineHeight(t, out out_lineHeight);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getGlyphData_uint32")]
        internal extern static bool _wrap_love_dll_type_Rasterizer_getGlyphData_uint32(IntPtr t, uint glyph, out IntPtr out_glyphData);
        internal static bool wrap_love_dll_type_Rasterizer_getGlyphData_uint32(IntPtr t, uint glyph, out IntPtr out_glyphData)
        {
            return CheckCAPIException(_wrap_love_dll_type_Rasterizer_getGlyphData_uint32(t, glyph, out out_glyphData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getGlyphData_string")]
        internal extern static bool _wrap_love_dll_type_Rasterizer_getGlyphData_string(IntPtr t, byte[] str, out IntPtr out_glyphData);
        internal static bool wrap_love_dll_type_Rasterizer_getGlyphData_string(IntPtr t, byte[] str, out IntPtr out_glyphData)
        {
            return CheckCAPIException(_wrap_love_dll_type_Rasterizer_getGlyphData_string(t, str, out out_glyphData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getGlyphCount")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getGlyphCount(IntPtr t, out int out_count);
        internal static void wrap_love_dll_type_Rasterizer_getGlyphCount(IntPtr t, out int out_count)
        {
            _wrap_love_dll_type_Rasterizer_getGlyphCount(t, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_hasGlyphs_uint32")]
        internal extern static bool _wrap_love_dll_type_Rasterizer_hasGlyphs_uint32(IntPtr t, uint glyph, out bool out_result);
        internal static bool wrap_love_dll_type_Rasterizer_hasGlyphs_uint32(IntPtr t, uint glyph, out bool out_result)
        {
            return CheckCAPIException(_wrap_love_dll_type_Rasterizer_hasGlyphs_uint32(t, glyph, out out_result));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_hasGlyphs_string")]
        internal extern static bool _wrap_love_dll_type_Rasterizer_hasGlyphs_string(IntPtr t, byte[] str, out bool out_result);
        internal static bool wrap_love_dll_type_Rasterizer_hasGlyphs_string(IntPtr t, byte[] str, out bool out_result)
        {
            return CheckCAPIException(_wrap_love_dll_type_Rasterizer_hasGlyphs_string(t, str, out out_result));
        }



        #endregion
        #region  type - Canvas

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Canvas_getFormat")]
        internal extern static void _wrap_love_dll_type_Canvas_getFormat(IntPtr canvas, out int out_format_type);
        internal static void wrap_love_dll_type_Canvas_getFormat(IntPtr canvas, out int out_format_type)
        {
            _wrap_love_dll_type_Canvas_getFormat(canvas, out out_format_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Canvas_getMSAA")]
        internal extern static void _wrap_love_dll_type_Canvas_getMSAA(IntPtr canvas, out int out_msaa);
        internal static void wrap_love_dll_type_Canvas_getMSAA(IntPtr canvas, out int out_msaa)
        {
            _wrap_love_dll_type_Canvas_getMSAA(canvas, out out_msaa);
        }



        #endregion
        #region  type - Font

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getHeight")]
        internal extern static void _wrap_love_dll_type_Font_getHeight(IntPtr t, out int out_height);
        internal static void wrap_love_dll_type_Font_getHeight(IntPtr t, out int out_height)
        {
            _wrap_love_dll_type_Font_getHeight(t, out out_height);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getWidth")]
        internal extern static bool _wrap_love_dll_type_Font_getWidth(IntPtr t, byte[] str, out int out_width);
        internal static bool wrap_love_dll_type_Font_getWidth(IntPtr t, byte[] str, out int out_width)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_getWidth(t, str, out out_width));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getWrap")]
        internal extern static bool _wrap_love_dll_type_Font_getWrap(IntPtr t, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength, float wrap, out int out_maxWidth, out IntPtr out_pws);
        internal static bool wrap_love_dll_type_Font_getWrap(IntPtr t, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength, float wrap, out int out_maxWidth, out IntPtr out_pws)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_getWrap(t, coloredStringText, coloredStringColor, coloredStringLength, wrap, out out_maxWidth, out out_pws));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_setLineHeight")]
        internal extern static void _wrap_love_dll_type_Font_setLineHeight(IntPtr t, float h);
        internal static void wrap_love_dll_type_Font_setLineHeight(IntPtr t, float h)
        {
            _wrap_love_dll_type_Font_setLineHeight(t, h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getLineHeight")]
        internal extern static void _wrap_love_dll_type_Font_getLineHeight(IntPtr t, out float out_h);
        internal static void wrap_love_dll_type_Font_getLineHeight(IntPtr t, out float out_h)
        {
            _wrap_love_dll_type_Font_getLineHeight(t, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_setFilter")]
        internal extern static bool _wrap_love_dll_type_Font_setFilter(IntPtr t, int min_type, int mag_type, float anisotropy);
        internal static bool wrap_love_dll_type_Font_setFilter(IntPtr t, int min_type, int mag_type, float anisotropy)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_setFilter(t, min_type, mag_type, anisotropy));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getFilter")]
        internal extern static void _wrap_love_dll_type_Font_getFilter(IntPtr t, out int out_min_type, out int out_mag_type, out float out_anisotropy);
        internal static void wrap_love_dll_type_Font_getFilter(IntPtr t, out int out_min_type, out int out_mag_type, out float out_anisotropy)
        {
            _wrap_love_dll_type_Font_getFilter(t, out out_min_type, out out_mag_type, out out_anisotropy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getAscent")]
        internal extern static void _wrap_love_dll_type_Font_getAscent(IntPtr t, out int out_ascent);
        internal static void wrap_love_dll_type_Font_getAscent(IntPtr t, out int out_ascent)
        {
            _wrap_love_dll_type_Font_getAscent(t, out out_ascent);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getDescent")]
        internal extern static void _wrap_love_dll_type_Font_getDescent(IntPtr t, out int out_descent);
        internal static void wrap_love_dll_type_Font_getDescent(IntPtr t, out int out_descent)
        {
            _wrap_love_dll_type_Font_getDescent(t, out out_descent);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getBaseline")]
        internal extern static void _wrap_love_dll_type_Font_getBaseline(IntPtr t, out float out_baseline);
        internal static void wrap_love_dll_type_Font_getBaseline(IntPtr t, out float out_baseline)
        {
            _wrap_love_dll_type_Font_getBaseline(t, out out_baseline);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_hasGlyphs_uint32")]
        internal extern static bool _wrap_love_dll_type_Font_hasGlyphs_uint32(IntPtr t, uint chr, out bool out_result);
        internal static bool wrap_love_dll_type_Font_hasGlyphs_uint32(IntPtr t, uint chr, out bool out_result)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_hasGlyphs_uint32(t, chr, out out_result));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_hasGlyphs_string")]
        internal extern static bool _wrap_love_dll_type_Font_hasGlyphs_string(IntPtr t, byte[] str, out bool out_result);
        internal static bool wrap_love_dll_type_Font_hasGlyphs_string(IntPtr t, byte[] str, out bool out_result)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_hasGlyphs_string(t, str, out out_result));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_setFallbacks")]
        internal extern static bool _wrap_love_dll_type_Font_setFallbacks(IntPtr t, IntPtr[] fallback, int length);
        internal static bool wrap_love_dll_type_Font_setFallbacks(IntPtr t, IntPtr[] fallback, int length)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_setFallbacks(t, fallback, length));
        }



        #endregion
        #region  type - Image

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Image_isCompressed")]
        internal extern static void _wrap_love_dll_type_Image_isCompressed(IntPtr image, out bool out_result);
        internal static void wrap_love_dll_type_Image_isCompressed(IntPtr image, out bool out_result)
        {
            _wrap_love_dll_type_Image_isCompressed(image, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Image_replacePixels")]
        internal extern static bool _wrap_love_dll_type_Image_replacePixels(IntPtr image, IntPtr imageData, int slice, int mipmap, int x, int y, bool reloadmipmaps);
        internal static bool wrap_love_dll_type_Image_replacePixels(IntPtr image, IntPtr imageData, int slice, int mipmap, int x, int y, bool reloadmipmaps)
        {
            return CheckCAPIException(_wrap_love_dll_type_Image_replacePixels(image, imageData, slice, mipmap, x, y, reloadmipmaps));
        }



        #endregion
        #region  type - Mesh

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setVertexAttribute")]
        internal extern static bool _wrap_love_dll_type_Mesh_setVertexAttribute(IntPtr mesh, int vertIndex, int attrIndex, byte[] dataPtr, int dataLen);
        internal static bool wrap_love_dll_type_Mesh_setVertexAttribute(IntPtr mesh, int vertIndex, int attrIndex, byte[] dataPtr, int dataLen)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setVertexAttribute(mesh, vertIndex, attrIndex, dataPtr, dataLen));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getVertexAttribute")]
        internal extern static bool _wrap_love_dll_type_Mesh_getVertexAttribute(IntPtr mesh, int vertIndex, int attrIndex, out IntPtr dataPtr, out int dataLen);
        internal static bool wrap_love_dll_type_Mesh_getVertexAttribute(IntPtr mesh, int vertIndex, int attrIndex, out IntPtr dataPtr, out int dataLen)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_getVertexAttribute(mesh, vertIndex, attrIndex, out dataPtr, out dataLen));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setVertices")]
        internal extern static bool _wrap_love_dll_type_Mesh_setVertices(IntPtr t, int vertOffset, byte[] inputData, int dataSize);
        internal static bool wrap_love_dll_type_Mesh_setVertices(IntPtr t, int vertOffset, byte[] inputData, int dataSize)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setVertices(t, vertOffset, inputData, dataSize));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getVertex")]
        internal extern static bool _wrap_love_dll_type_Mesh_getVertex(IntPtr t, int index, out IntPtr data, out int dataSize);
        internal static bool wrap_love_dll_type_Mesh_getVertex(IntPtr t, int index, out IntPtr data, out int dataSize)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_getVertex(t, index, out data, out dataSize));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setVertex")]
        internal extern static bool _wrap_love_dll_type_Mesh_setVertex(IntPtr t, int index, byte[] data, int dataSize);
        internal static bool wrap_love_dll_type_Mesh_setVertex(IntPtr t, int index, byte[] data, int dataSize)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setVertex(t, index, data, dataSize));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getVertexFormat")]
        internal extern static bool _wrap_love_dll_type_Mesh_getVertexFormat(IntPtr t, out IntPtr wss, out IntPtr typeList, out IntPtr comCountList, out int len);
        internal static bool wrap_love_dll_type_Mesh_getVertexFormat(IntPtr t, out IntPtr wss, out IntPtr typeList, out IntPtr comCountList, out int len)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_getVertexFormat(t, out wss, out typeList, out comCountList, out len));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_isAttributeEnabled")]
        internal extern static bool _wrap_love_dll_type_Mesh_isAttributeEnabled(IntPtr t, byte[] name, out bool res);
        internal static bool wrap_love_dll_type_Mesh_isAttributeEnabled(IntPtr t, byte[] name, out bool res)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_isAttributeEnabled(t, name, out res));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setAttributeEnabled")]
        internal extern static bool _wrap_love_dll_type_Mesh_setAttributeEnabled(IntPtr t, byte[] name, bool flag);
        internal static bool wrap_love_dll_type_Mesh_setAttributeEnabled(IntPtr t, byte[] name, bool flag)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setAttributeEnabled(t, name, flag));
        }




        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getVertexCount")]
        internal extern static void _wrap_love_dll_type_Mesh_getVertexCount(IntPtr p, out int out_result);
        internal static void wrap_love_dll_type_Mesh_getVertexCount(IntPtr p, out int out_result)
        {
            _wrap_love_dll_type_Mesh_getVertexCount(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_flush")]
        internal extern static void _wrap_love_dll_type_Mesh_flush(IntPtr p);
        internal static void wrap_love_dll_type_Mesh_flush(IntPtr p)
        {
            _wrap_love_dll_type_Mesh_flush(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setVertexMap_nil")]
        internal extern static bool _wrap_love_dll_type_Mesh_setVertexMap_nil(IntPtr p);
        internal static bool wrap_love_dll_type_Mesh_setVertexMap_nil(IntPtr p)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setVertexMap_nil(p));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setVertexMap")]
        internal extern static bool _wrap_love_dll_type_Mesh_setVertexMap(IntPtr p, uint[] vertexmaps, int vertexmaps_length);
        internal static bool wrap_love_dll_type_Mesh_setVertexMap(IntPtr p, uint[] vertexmaps, int vertexmaps_length)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setVertexMap(p, vertexmaps, vertexmaps_length));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getVertexMap")]
        internal extern static bool _wrap_love_dll_type_Mesh_getVertexMap(IntPtr p, out bool out_has_vertex_map, out IntPtr out_vertexmaps, out int out_vertexmaps_length);
        internal static bool wrap_love_dll_type_Mesh_getVertexMap(IntPtr p, out bool out_has_vertex_map, out IntPtr out_vertexmaps, out int out_vertexmaps_length)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_getVertexMap(p, out out_has_vertex_map, out out_vertexmaps, out out_vertexmaps_length));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setTexture_nil")]
        internal extern static void _wrap_love_dll_type_Mesh_setTexture_nil(IntPtr p);
        internal static void wrap_love_dll_type_Mesh_setTexture_nil(IntPtr p)
        {
            _wrap_love_dll_type_Mesh_setTexture_nil(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setTexture_Texture")]
        internal extern static void _wrap_love_dll_type_Mesh_setTexture_Texture(IntPtr p, IntPtr tex);
        internal static void wrap_love_dll_type_Mesh_setTexture_Texture(IntPtr p, IntPtr tex)
        {
            _wrap_love_dll_type_Mesh_setTexture_Texture(p, tex);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getTexture")]
        internal extern static bool _wrap_love_dll_type_Mesh_getTexture(IntPtr p, out IntPtr out_result);
        internal static bool wrap_love_dll_type_Mesh_getTexture(IntPtr p, out IntPtr out_result)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_getTexture(p, out out_result));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setDrawMode")]
        internal extern static void _wrap_love_dll_type_Mesh_setDrawMode(IntPtr p, int mode_type);
        internal static void wrap_love_dll_type_Mesh_setDrawMode(IntPtr p, int mode_type)
        {
            _wrap_love_dll_type_Mesh_setDrawMode(p, mode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getDrawMode")]
        internal extern static void _wrap_love_dll_type_Mesh_getDrawMode(IntPtr p, out int out_mode_type);
        internal static void wrap_love_dll_type_Mesh_getDrawMode(IntPtr p, out int out_mode_type)
        {
            _wrap_love_dll_type_Mesh_getDrawMode(p, out out_mode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setDrawRange")]
        internal extern static void _wrap_love_dll_type_Mesh_setDrawRange(IntPtr p);
        internal static void wrap_love_dll_type_Mesh_setDrawRange(IntPtr p)
        {
            _wrap_love_dll_type_Mesh_setDrawRange(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setDrawRange_minmax")]
        internal extern static bool _wrap_love_dll_type_Mesh_setDrawRange_minmax(IntPtr p, int rangemin, int rangemax);
        internal static bool wrap_love_dll_type_Mesh_setDrawRange_minmax(IntPtr p, int rangemin, int rangemax)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setDrawRange_minmax(p, rangemin, rangemax));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getDrawRange")]
        internal extern static bool _wrap_love_dll_type_Mesh_getDrawRange(IntPtr p, out int out_rangemin, out int out_rangemax);
        internal static bool wrap_love_dll_type_Mesh_getDrawRange(IntPtr p, out int out_rangemin, out int out_rangemax)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_getDrawRange(p, out out_rangemin, out out_rangemax));
        }



        #endregion
        #region  type - ParticleSystem

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_clone")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_clone(IntPtr p, out IntPtr out_clone);
        internal static bool wrap_love_dll_type_ParticleSystem_clone(IntPtr p, out IntPtr out_clone)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_clone(p, out out_clone));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setTexture")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setTexture(IntPtr p, IntPtr tex);
        internal static void wrap_love_dll_type_ParticleSystem_setTexture(IntPtr p, IntPtr tex)
        {
            _wrap_love_dll_type_ParticleSystem_setTexture(p, tex);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getTexture")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_getTexture(IntPtr p, out IntPtr out_texture);
        internal static bool wrap_love_dll_type_ParticleSystem_getTexture(IntPtr p, out IntPtr out_texture)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_getTexture(p, out out_texture));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setBufferSize")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setBufferSize(IntPtr p, uint buffersize);
        internal static bool wrap_love_dll_type_ParticleSystem_setBufferSize(IntPtr p, uint buffersize)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setBufferSize(p, buffersize));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getBufferSize")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getBufferSize(IntPtr p, out uint out_buffersize);
        internal static void wrap_love_dll_type_ParticleSystem_getBufferSize(IntPtr p, out uint out_buffersize)
        {
            _wrap_love_dll_type_ParticleSystem_getBufferSize(p, out out_buffersize);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setInsertMode")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setInsertMode(IntPtr p, int mode_type);
        internal static void wrap_love_dll_type_ParticleSystem_setInsertMode(IntPtr p, int mode_type)
        {
            _wrap_love_dll_type_ParticleSystem_setInsertMode(p, mode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getInsertMode")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getInsertMode(IntPtr p, out int out_mode_type);
        internal static void wrap_love_dll_type_ParticleSystem_getInsertMode(IntPtr p, out int out_mode_type)
        {
            _wrap_love_dll_type_ParticleSystem_getInsertMode(p, out out_mode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setEmissionRate")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setEmissionRate(IntPtr p, float rate);
        internal static bool wrap_love_dll_type_ParticleSystem_setEmissionRate(IntPtr p, float rate)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setEmissionRate(p, rate));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getEmissionRate")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getEmissionRate(IntPtr p, out float out_rate);
        internal static void wrap_love_dll_type_ParticleSystem_getEmissionRate(IntPtr p, out float out_rate)
        {
            _wrap_love_dll_type_ParticleSystem_getEmissionRate(p, out out_rate);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setEmitterLifetime")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setEmitterLifetime(IntPtr p, float lifetime);
        internal static void wrap_love_dll_type_ParticleSystem_setEmitterLifetime(IntPtr p, float lifetime)
        {
            _wrap_love_dll_type_ParticleSystem_setEmitterLifetime(p, lifetime);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getEmitterLifetime")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getEmitterLifetime(IntPtr p, out float out_lifetime);
        internal static void wrap_love_dll_type_ParticleSystem_getEmitterLifetime(IntPtr p, out float out_lifetime)
        {
            _wrap_love_dll_type_ParticleSystem_getEmitterLifetime(p, out out_lifetime);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setParticleLifetime")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setParticleLifetime(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setParticleLifetime(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setParticleLifetime(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getParticleLifetime")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getParticleLifetime(IntPtr p, out int out_min, out int out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getParticleLifetime(IntPtr p, out int out_min, out int out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getParticleLifetime(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setPosition")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setPosition(IntPtr p, float x, float y);
        internal static void wrap_love_dll_type_ParticleSystem_setPosition(IntPtr p, float x, float y)
        {
            _wrap_love_dll_type_ParticleSystem_setPosition(p, x, y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getPosition")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getPosition(IntPtr p, out float out_x, out float out_y);
        internal static void wrap_love_dll_type_ParticleSystem_getPosition(IntPtr p, out float out_x, out float out_y)
        {
            _wrap_love_dll_type_ParticleSystem_getPosition(p, out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_moveTo")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_moveTo(IntPtr p, float x, float y);
        internal static void wrap_love_dll_type_ParticleSystem_moveTo(IntPtr p, float x, float y)
        {
            _wrap_love_dll_type_ParticleSystem_moveTo(p, x, y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setEmissionArea")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setEmissionArea(IntPtr p, int distribution_type, float x, float y, float angle, bool directionRelativeToCenter);
        internal static bool wrap_love_dll_type_ParticleSystem_setEmissionArea(IntPtr p, int distribution_type, float x, float y, float angle, bool directionRelativeToCenter)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setEmissionArea(p, distribution_type, x, y, angle, directionRelativeToCenter));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getAreaSpread")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getAreaSpread(IntPtr p, out int out_distribution_type, out float out_x, out float out_y);
        internal static void wrap_love_dll_type_ParticleSystem_getAreaSpread(IntPtr p, out int out_distribution_type, out float out_x, out float out_y)
        {
            _wrap_love_dll_type_ParticleSystem_getAreaSpread(p, out out_distribution_type, out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setDirection")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setDirection(IntPtr p, float direction);
        internal static void wrap_love_dll_type_ParticleSystem_setDirection(IntPtr p, float direction)
        {
            _wrap_love_dll_type_ParticleSystem_setDirection(p, direction);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getDirection")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getDirection(IntPtr p, out float out_direction);
        internal static void wrap_love_dll_type_ParticleSystem_getDirection(IntPtr p, out float out_direction)
        {
            _wrap_love_dll_type_ParticleSystem_getDirection(p, out out_direction);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSpread")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setSpread(IntPtr p, float spread);
        internal static void wrap_love_dll_type_ParticleSystem_setSpread(IntPtr p, float spread)
        {
            _wrap_love_dll_type_ParticleSystem_setSpread(p, spread);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSpread")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSpread(IntPtr p, out float out_spread);
        internal static void wrap_love_dll_type_ParticleSystem_getSpread(IntPtr p, out float out_spread)
        {
            _wrap_love_dll_type_ParticleSystem_getSpread(p, out out_spread);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSpeed")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setSpeed(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setSpeed(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setSpeed(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSpeed")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSpeed(IntPtr p, out float out_min, out float out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getSpeed(IntPtr p, out float out_min, out float out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getSpeed(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setLinearAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setLinearAcceleration(IntPtr p, float xmin, float ymin, float xmax, float ymax);
        internal static void wrap_love_dll_type_ParticleSystem_setLinearAcceleration(IntPtr p, float xmin, float ymin, float xmax, float ymax)
        {
            _wrap_love_dll_type_ParticleSystem_setLinearAcceleration(p, xmin, ymin, xmax, ymax);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getLinearAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getLinearAcceleration(IntPtr p, out float out_xmin, out float out_ymin, out float out_xmax, out float out_ymax);
        internal static void wrap_love_dll_type_ParticleSystem_getLinearAcceleration(IntPtr p, out float out_xmin, out float out_ymin, out float out_xmax, out float out_ymax)
        {
            _wrap_love_dll_type_ParticleSystem_getLinearAcceleration(p, out out_xmin, out out_ymin, out out_xmax, out out_ymax);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setRadialAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setRadialAcceleration(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setRadialAcceleration(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setRadialAcceleration(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getRadialAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getRadialAcceleration(IntPtr p, out int out_min, out int out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getRadialAcceleration(IntPtr p, out int out_min, out int out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getRadialAcceleration(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setTangentialAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setTangentialAcceleration(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setTangentialAcceleration(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setTangentialAcceleration(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getTangentialAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getTangentialAcceleration(IntPtr p, out int out_min, out int out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getTangentialAcceleration(IntPtr p, out int out_min, out int out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getTangentialAcceleration(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setLinearDamping")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setLinearDamping(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setLinearDamping(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setLinearDamping(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getLinearDamping")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getLinearDamping(IntPtr p, out int out_min, out int out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getLinearDamping(IntPtr p, out int out_min, out int out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getLinearDamping(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSizes")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setSizes(IntPtr p, float[] sizearray, int sizearray_length);
        internal static bool wrap_love_dll_type_ParticleSystem_setSizes(IntPtr p, float[] sizearray, int sizearray_length)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setSizes(p, sizearray, sizearray_length));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSizes")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSizes(IntPtr p, out IntPtr out_sizearray, out int out_sizearray_length);
        internal static void wrap_love_dll_type_ParticleSystem_getSizes(IntPtr p, out IntPtr out_sizearray, out int out_sizearray_length)
        {
            _wrap_love_dll_type_ParticleSystem_getSizes(p, out out_sizearray, out out_sizearray_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSizeVariation")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setSizeVariation(IntPtr p, float variation);
        internal static bool wrap_love_dll_type_ParticleSystem_setSizeVariation(IntPtr p, float variation)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setSizeVariation(p, variation));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSizeVariation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSizeVariation(IntPtr p, out float out_variation);
        internal static void wrap_love_dll_type_ParticleSystem_getSizeVariation(IntPtr p, out float out_variation)
        {
            _wrap_love_dll_type_ParticleSystem_getSizeVariation(p, out out_variation);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setRotation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setRotation(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setRotation(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setRotation(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getRotation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getRotation(IntPtr p, out int out_min, out int out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getRotation(IntPtr p, out int out_min, out int out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getRotation(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSpin")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setSpin(IntPtr p, float start, float end);
        internal static void wrap_love_dll_type_ParticleSystem_setSpin(IntPtr p, float start, float end)
        {
            _wrap_love_dll_type_ParticleSystem_setSpin(p, start, end);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSpin")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSpin(IntPtr p, out float out_start, out float out_end);
        internal static void wrap_love_dll_type_ParticleSystem_getSpin(IntPtr p, out float out_start, out float out_end)
        {
            _wrap_love_dll_type_ParticleSystem_getSpin(p, out out_start, out out_end);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSpinVariation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setSpinVariation(IntPtr p, float variation);
        internal static void wrap_love_dll_type_ParticleSystem_setSpinVariation(IntPtr p, float variation)
        {
            _wrap_love_dll_type_ParticleSystem_setSpinVariation(p, variation);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSpinVariation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSpinVariation(IntPtr p, out float out_variation);
        internal static void wrap_love_dll_type_ParticleSystem_getSpinVariation(IntPtr p, out float out_variation)
        {
            _wrap_love_dll_type_ParticleSystem_getSpinVariation(p, out out_variation);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setOffset")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setOffset(IntPtr p, float x, float y);
        internal static void wrap_love_dll_type_ParticleSystem_setOffset(IntPtr p, float x, float y)
        {
            _wrap_love_dll_type_ParticleSystem_setOffset(p, x, y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getOffset")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getOffset(IntPtr p, out float out_x, out float out_y);
        internal static void wrap_love_dll_type_ParticleSystem_getOffset(IntPtr p, out float out_x, out float out_y)
        {
            _wrap_love_dll_type_ParticleSystem_getOffset(p, out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setColors")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setColors(IntPtr p, Vector4[] colorarray, int colorarray_length);
        internal static bool wrap_love_dll_type_ParticleSystem_setColors(IntPtr p, Vector4[] colorarray, int colorarray_length)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setColors(p, colorarray, colorarray_length));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getColors")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getColors(IntPtr p, out IntPtr out_colorarray, out int out_colorarray_length);
        internal static void wrap_love_dll_type_ParticleSystem_getColors(IntPtr p, out IntPtr out_colorarray, out int out_colorarray_length)
        {
            _wrap_love_dll_type_ParticleSystem_getColors(p, out out_colorarray, out out_colorarray_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setQuads")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setQuads(IntPtr p, IntPtr[] quadsarray, int quadsarray_length);
        internal static void wrap_love_dll_type_ParticleSystem_setQuads(IntPtr p, IntPtr[] quadsarray, int quadsarray_length)
        {
            _wrap_love_dll_type_ParticleSystem_setQuads(p, quadsarray, quadsarray_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getQuads")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getQuads(IntPtr p, out IntPtr out_quadsarray, out int out_quadsarray_length);
        internal static void wrap_love_dll_type_ParticleSystem_getQuads(IntPtr p, out IntPtr out_quadsarray, out int out_quadsarray_length)
        {
            _wrap_love_dll_type_ParticleSystem_getQuads(p, out out_quadsarray, out out_quadsarray_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setRelativeRotation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setRelativeRotation(IntPtr p, bool enable);
        internal static void wrap_love_dll_type_ParticleSystem_setRelativeRotation(IntPtr p, bool enable)
        {
            _wrap_love_dll_type_ParticleSystem_setRelativeRotation(p, enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_hasRelativeRotation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_hasRelativeRotation(IntPtr p, out bool out_enable);
        internal static void wrap_love_dll_type_ParticleSystem_hasRelativeRotation(IntPtr p, out bool out_enable)
        {
            _wrap_love_dll_type_ParticleSystem_hasRelativeRotation(p, out out_enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getCount")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getCount(IntPtr p, out uint out_count);
        internal static void wrap_love_dll_type_ParticleSystem_getCount(IntPtr p, out uint out_count)
        {
            _wrap_love_dll_type_ParticleSystem_getCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_start")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_start(IntPtr p);
        internal static void wrap_love_dll_type_ParticleSystem_start(IntPtr p)
        {
            _wrap_love_dll_type_ParticleSystem_start(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_stop")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_stop(IntPtr p);
        internal static void wrap_love_dll_type_ParticleSystem_stop(IntPtr p)
        {
            _wrap_love_dll_type_ParticleSystem_stop(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_pause")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_pause(IntPtr p);
        internal static void wrap_love_dll_type_ParticleSystem_pause(IntPtr p)
        {
            _wrap_love_dll_type_ParticleSystem_pause(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_reset")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_reset(IntPtr p);
        internal static void wrap_love_dll_type_ParticleSystem_reset(IntPtr p)
        {
            _wrap_love_dll_type_ParticleSystem_reset(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_emit")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_emit(IntPtr p, int num);
        internal static void wrap_love_dll_type_ParticleSystem_emit(IntPtr p, int num)
        {
            _wrap_love_dll_type_ParticleSystem_emit(p, num);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_isActive")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_isActive(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_ParticleSystem_isActive(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_ParticleSystem_isActive(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_isPaused")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_isPaused(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_ParticleSystem_isPaused(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_ParticleSystem_isPaused(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_isStopped")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_isStopped(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_ParticleSystem_isStopped(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_ParticleSystem_isStopped(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_update")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_update(IntPtr p, float dt);
        internal static void wrap_love_dll_type_ParticleSystem_update(IntPtr p, float dt)
        {
            _wrap_love_dll_type_ParticleSystem_update(p, dt);
        }



        #endregion
        #region  type - Quad

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Quad_setViewport")]
        internal extern static void _wrap_love_dll_type_Quad_setViewport(IntPtr p, float x, float y, float w, float h);
        internal static void wrap_love_dll_type_Quad_setViewport(IntPtr p, float x, float y, float w, float h)
        {
            _wrap_love_dll_type_Quad_setViewport(p, x, y, w, h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Quad_getViewport")]
        internal extern static void _wrap_love_dll_type_Quad_getViewport(IntPtr p, out float out_x, out float out_y, out float out_w, out float out_h);
        internal static void wrap_love_dll_type_Quad_getViewport(IntPtr p, out float out_x, out float out_y, out float out_w, out float out_h)
        {
            _wrap_love_dll_type_Quad_getViewport(p, out out_x, out out_y, out out_w, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Quad_getTextureDimensions")]
        internal extern static void _wrap_love_dll_type_Quad_getTextureDimensions(IntPtr p, out float out_sw, out float out_sh);
        internal static void wrap_love_dll_type_Quad_getTextureDimensions(IntPtr p, out float out_sw, out float out_sh)
        {
            _wrap_love_dll_type_Quad_getTextureDimensions(p, out out_sw, out out_sh);
        }



        #endregion
        #region  type - Shader

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_getWarnings")]
        internal extern static void _wrap_love_dll_type_Shader_getWarnings(IntPtr p, out IntPtr out_str);
        internal static void wrap_love_dll_type_Shader_getWarnings(IntPtr p, out IntPtr out_str)
        {
            _wrap_love_dll_type_Shader_getWarnings(p, out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendColors")]
        internal extern static bool _wrap_love_dll_type_Shader_sendColors(IntPtr p, byte[] name, Vector4[] valuearray, int value_lenght);
        internal static bool wrap_love_dll_type_Shader_sendColors(IntPtr p, byte[] name, Vector4[] valuearray, int value_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendColors(p, name, valuearray, value_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendFloats")]
        internal extern static bool _wrap_love_dll_type_Shader_sendFloats(IntPtr p, byte[] name, float[] valuearray, int valuearray_lenght);
        internal static bool wrap_love_dll_type_Shader_sendFloats(IntPtr p, byte[] name, float[] valuearray, int valuearray_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendFloats(p, name, valuearray, valuearray_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendUints")]
        internal extern static bool _wrap_love_dll_type_Shader_sendUints(IntPtr p, byte[] name, uint[] valuearray, int valuearray_lenght);
        internal static bool wrap_love_dll_type_Shader_sendUints(IntPtr p, byte[] name, uint[] valuearray, int valuearray_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendUints(p, name, valuearray, valuearray_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendInts")]
        internal extern static bool _wrap_love_dll_type_Shader_sendInts(IntPtr p, byte[] name, int[] valuearray, int valuearray_lenght);
        internal static bool wrap_love_dll_type_Shader_sendInts(IntPtr p, byte[] name, int[] valuearray, int valuearray_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendInts(p, name, valuearray, valuearray_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendBooleans")]
        internal extern static bool _wrap_love_dll_type_Shader_sendBooleans(IntPtr p, byte[] name, bool[] valuearray, int valuearray_lenght);
        internal static bool wrap_love_dll_type_Shader_sendBooleans(IntPtr p, byte[] name, bool[] valuearray, int valuearray_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendBooleans(p, name, valuearray, valuearray_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendMatrices")]
        internal extern static bool _wrap_love_dll_type_Shader_sendMatrices(IntPtr p, byte[] name, float[] valueArray, int columns_lenght, int rows_length, int matrix_count);
        internal static bool wrap_love_dll_type_Shader_sendMatrices(IntPtr p, byte[] name, float[] valueArray, int columns_lenght, int rows_length, int matrix_count)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendMatrices(p, name, valueArray, columns_lenght, rows_length, matrix_count));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendTexture")]
        internal extern static bool _wrap_love_dll_type_Shader_sendTexture(IntPtr p, byte[] name, IntPtr[] texture, int texture_lenght);
        internal static bool wrap_love_dll_type_Shader_sendTexture(IntPtr p, byte[] name, IntPtr[] texture, int texture_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendTexture(p, name, texture, texture_lenght));
        }



        #endregion
        #region  type - SpriteBatch

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_add")]
        internal extern static bool _wrap_love_dll_type_SpriteBatch_add(IntPtr p, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index);
        internal static bool wrap_love_dll_type_SpriteBatch_add(IntPtr p, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index)
        {
            return CheckCAPIException(_wrap_love_dll_type_SpriteBatch_add(p, x, y, a, sx, sy, ox, oy, kx, ky, out out_index));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_add_Quad")]
        internal extern static bool _wrap_love_dll_type_SpriteBatch_add_Quad(IntPtr p, IntPtr quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index);
        internal static bool wrap_love_dll_type_SpriteBatch_add_Quad(IntPtr p, IntPtr quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index)
        {
            return CheckCAPIException(_wrap_love_dll_type_SpriteBatch_add_Quad(p, quad, x, y, a, sx, sy, ox, oy, kx, ky, out out_index));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_set")]
        internal extern static bool _wrap_love_dll_type_SpriteBatch_set(IntPtr p, int index, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_type_SpriteBatch_set(IntPtr p, int index, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_type_SpriteBatch_set(p, index, x, y, a, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_set_Quad")]
        internal extern static bool _wrap_love_dll_type_SpriteBatch_set_Quad(IntPtr p, int index, IntPtr quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_type_SpriteBatch_set_Quad(IntPtr p, int index, IntPtr quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_type_SpriteBatch_set_Quad(p, index, quad, x, y, a, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_clear")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_clear(IntPtr p);
        internal static void wrap_love_dll_type_SpriteBatch_clear(IntPtr p)
        {
            _wrap_love_dll_type_SpriteBatch_clear(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_flush")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_flush(IntPtr p);
        internal static void wrap_love_dll_type_SpriteBatch_flush(IntPtr p)
        {
            _wrap_love_dll_type_SpriteBatch_flush(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_setTexture")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_setTexture(IntPtr p, IntPtr tex);
        internal static void wrap_love_dll_type_SpriteBatch_setTexture(IntPtr p, IntPtr tex)
        {
            _wrap_love_dll_type_SpriteBatch_setTexture(p, tex);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_getTexture")]
        internal extern static bool _wrap_love_dll_type_SpriteBatch_getTexture(IntPtr p, out IntPtr out_texture);
        internal static bool wrap_love_dll_type_SpriteBatch_getTexture(IntPtr p, out IntPtr out_texture)
        {
            return CheckCAPIException(_wrap_love_dll_type_SpriteBatch_getTexture(p, out out_texture));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_setColor_nil")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_setColor_nil(IntPtr p);
        internal static void wrap_love_dll_type_SpriteBatch_setColor_nil(IntPtr p)
        {
            _wrap_love_dll_type_SpriteBatch_setColor_nil(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_setColor")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_setColor(IntPtr p, float r, float g, float b, float a);
        internal static void wrap_love_dll_type_SpriteBatch_setColor(IntPtr p, float r, float g, float b, float a)
        {
            _wrap_love_dll_type_SpriteBatch_setColor(p, r, g, b, a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_getColor")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_getColor(IntPtr p, out bool out_exist, out float out_r, out float out_g, out float out_b, out float out_a);
        internal static void wrap_love_dll_type_SpriteBatch_getColor(IntPtr p, out bool out_exist, out float out_r, out float out_g, out float out_b, out float out_a)
        {
            _wrap_love_dll_type_SpriteBatch_getColor(p, out out_exist, out out_r, out out_g, out out_b, out out_a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_getCount")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_getCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_SpriteBatch_getCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_SpriteBatch_getCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_getBufferSize")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_getBufferSize(IntPtr p, out int out_buffersize);
        internal static void wrap_love_dll_type_SpriteBatch_getBufferSize(IntPtr p, out int out_buffersize)
        {
            _wrap_love_dll_type_SpriteBatch_getBufferSize(p, out out_buffersize);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_attachAttribute")]
        internal extern static bool _wrap_love_dll_type_SpriteBatch_attachAttribute(IntPtr p, byte[] name, IntPtr mesh);
        internal static bool wrap_love_dll_type_SpriteBatch_attachAttribute(IntPtr p, byte[] name, IntPtr mesh)
        {
            return CheckCAPIException(_wrap_love_dll_type_SpriteBatch_attachAttribute(p, name, mesh));
        }



        #endregion
        #region  type - Text

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_set_coloredstring")]
        internal extern static bool _wrap_love_dll_type_Text_set_coloredstring(IntPtr p, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength);
        internal static bool wrap_love_dll_type_Text_set_coloredstring(IntPtr p, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength)
        {
            return CheckCAPIException(_wrap_love_dll_type_Text_set_coloredstring(p, coloredStringText, coloredStringColor, coloredStringLength));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_setf")]
        internal extern static bool _wrap_love_dll_type_Text_setf(IntPtr p, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength, float wraplimit, int align_type);
        internal static bool wrap_love_dll_type_Text_setf(IntPtr p, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength, float wraplimit, int align_type)
        {
            return CheckCAPIException(_wrap_love_dll_type_Text_setf(p, coloredStringText, coloredStringColor, coloredStringLength, wraplimit, align_type));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_add")]
        internal extern static bool _wrap_love_dll_type_Text_add(IntPtr p, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index);
        internal static bool wrap_love_dll_type_Text_add(IntPtr p, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index)
        {
            return CheckCAPIException(_wrap_love_dll_type_Text_add(p, coloredStringText, coloredStringColor, coloredStringLength, x, y, a, sx, sy, ox, oy, kx, ky, out out_index));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_addf")]
        internal extern static bool _wrap_love_dll_type_Text_addf(IntPtr p, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, float wraplimit, int align_type, out int out_index);
        internal static bool wrap_love_dll_type_Text_addf(IntPtr p, BytePtr[] coloredStringText, Vector4[] coloredStringColor, int coloredStringLength, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, float wraplimit, int align_type, out int out_index)
        {
            return CheckCAPIException(_wrap_love_dll_type_Text_addf(p, coloredStringText, coloredStringColor, coloredStringLength, x, y, a, sx, sy, ox, oy, kx, ky, wraplimit, align_type, out out_index));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_clear")]
        internal extern static bool _wrap_love_dll_type_Text_clear(IntPtr p);
        internal static bool wrap_love_dll_type_Text_clear(IntPtr p)
        {
            return CheckCAPIException(_wrap_love_dll_type_Text_clear(p));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_setFont")]
        internal extern static bool _wrap_love_dll_type_Text_setFont(IntPtr p, IntPtr f);
        internal static bool wrap_love_dll_type_Text_setFont(IntPtr p, IntPtr f)
        {
            return CheckCAPIException(_wrap_love_dll_type_Text_setFont(p, f));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_getFont")]
        internal extern static void _wrap_love_dll_type_Text_getFont(IntPtr p, out IntPtr font);
        internal static void wrap_love_dll_type_Text_getFont(IntPtr p, out IntPtr font)
        {
            _wrap_love_dll_type_Text_getFont(p, out font);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_getWidth")]
        internal extern static void _wrap_love_dll_type_Text_getWidth(IntPtr p, int index, out int out_w);
        internal static void wrap_love_dll_type_Text_getWidth(IntPtr p, int index, out int out_w)
        {
            _wrap_love_dll_type_Text_getWidth(p, index, out out_w);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_getHeight")]
        internal extern static void _wrap_love_dll_type_Text_getHeight(IntPtr p, int index, out int out_h);
        internal static void wrap_love_dll_type_Text_getHeight(IntPtr p, int index, out int out_h)
        {
            _wrap_love_dll_type_Text_getHeight(p, index, out out_h);
        }



        #endregion
        #region  type - Texture

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_setMipmapFilter")]
        internal extern static bool _wrap_love_dll_type_Texture_setMipmapFilter(IntPtr image, int mipmap_type, float sharpness);
        internal static bool wrap_love_dll_type_Texture_setMipmapFilter(IntPtr image, int mipmap_type, float sharpness)
        {
            return CheckCAPIException(_wrap_love_dll_type_Texture_setMipmapFilter(image, mipmap_type, sharpness));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_getMipmapFilter")]
        internal extern static void _wrap_love_dll_type_Texture_getMipmapFilter(IntPtr image, out int out_mipmap_type, out float out_sharpness);
        internal static void wrap_love_dll_type_Texture_getMipmapFilter(IntPtr image, out int out_mipmap_type, out float out_sharpness)
        {
            _wrap_love_dll_type_Texture_getMipmapFilter(image, out out_mipmap_type, out out_sharpness);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_getMipmapCount")]
        internal extern static void _wrap_love_dll_type_Texture_getMipmapCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_Texture_getMipmapCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_Texture_getMipmapCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_getWidth")]
        internal extern static void _wrap_love_dll_type_Texture_getWidth(IntPtr p, out int out_w);
        internal static void wrap_love_dll_type_Texture_getWidth(IntPtr p, out int out_w)
        {
            _wrap_love_dll_type_Texture_getWidth(p, out out_w);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_getHeight")]
        internal extern static void _wrap_love_dll_type_Texture_getHeight(IntPtr p, out int out_h);
        internal static void wrap_love_dll_type_Texture_getHeight(IntPtr p, out int out_h)
        {
            _wrap_love_dll_type_Texture_getHeight(p, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_setFilter")]
        internal extern static bool _wrap_love_dll_type_Texture_setFilter(IntPtr p, int filtermin_type, int filtermag_type, float anisotropy);
        internal static bool wrap_love_dll_type_Texture_setFilter(IntPtr p, int filtermin_type, int filtermag_type, float anisotropy)
        {
            return CheckCAPIException(_wrap_love_dll_type_Texture_setFilter(p, filtermin_type, filtermag_type, anisotropy));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_getFilter")]
        internal extern static void _wrap_love_dll_type_Texture_getFilter(IntPtr p, out int out_filtermin_type, out int out_filtermag_type, out float out_anisotropy);
        internal static void wrap_love_dll_type_Texture_getFilter(IntPtr p, out int out_filtermin_type, out int out_filtermag_type, out float out_anisotropy)
        {
            _wrap_love_dll_type_Texture_getFilter(p, out out_filtermin_type, out out_filtermag_type, out out_anisotropy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_setWrap")]
        internal extern static void _wrap_love_dll_type_Texture_setWrap(IntPtr p, int wraphoriz_type, int wrapvert_type);
        internal static void wrap_love_dll_type_Texture_setWrap(IntPtr p, int wraphoriz_type, int wrapvert_type)
        {
            _wrap_love_dll_type_Texture_setWrap(p, wraphoriz_type, wrapvert_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_getWrap")]
        internal extern static void _wrap_love_dll_type_Texture_getWrap(IntPtr p, out int out_wraphoriz_type, out int out_wrapvert_type);
        internal static void wrap_love_dll_type_Texture_getWrap(IntPtr p, out int out_wraphoriz_type, out int out_wrapvert_type)
        {
            _wrap_love_dll_type_Texture_getWrap(p, out out_wraphoriz_type, out out_wrapvert_type);
        }



        #endregion
        #region  type - Video

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_getStream")]
        internal extern static void _wrap_love_dll_type_Video_getStream(IntPtr p, out IntPtr out_videsStream);
        internal static void wrap_love_dll_type_Video_getStream(IntPtr p, out IntPtr out_videsStream)
        {
            _wrap_love_dll_type_Video_getStream(p, out out_videsStream);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_getSource")]
        internal extern static void _wrap_love_dll_type_Video_getSource(IntPtr p, out IntPtr out_source);
        internal static void wrap_love_dll_type_Video_getSource(IntPtr p, out IntPtr out_source)
        {
            _wrap_love_dll_type_Video_getSource(p, out out_source);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_setSource_nil")]
        internal extern static void _wrap_love_dll_type_Video_setSource_nil(IntPtr p);
        internal static void wrap_love_dll_type_Video_setSource_nil(IntPtr p)
        {
            _wrap_love_dll_type_Video_setSource_nil(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_setSource")]
        internal extern static void _wrap_love_dll_type_Video_setSource(IntPtr p, IntPtr source);
        internal static void wrap_love_dll_type_Video_setSource(IntPtr p, IntPtr source)
        {
            _wrap_love_dll_type_Video_setSource(p, source);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_getWidth")]
        internal extern static void _wrap_love_dll_type_Video_getWidth(IntPtr p, out int out_w);
        internal static void wrap_love_dll_type_Video_getWidth(IntPtr p, out int out_w)
        {
            _wrap_love_dll_type_Video_getWidth(p, out out_w);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_getHeight")]
        internal extern static void _wrap_love_dll_type_Video_getHeight(IntPtr p, out int out_h);
        internal static void wrap_love_dll_type_Video_getHeight(IntPtr p, out int out_h)
        {
            _wrap_love_dll_type_Video_getHeight(p, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_setFilter")]
        internal extern static bool _wrap_love_dll_type_Video_setFilter(IntPtr p, int filtermin_type, int filtermag_type, float anisotropy);
        internal static bool wrap_love_dll_type_Video_setFilter(IntPtr p, int filtermin_type, int filtermag_type, float anisotropy)
        {
            return CheckCAPIException(_wrap_love_dll_type_Video_setFilter(p, filtermin_type, filtermag_type, anisotropy));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_getFilter")]
        internal extern static void _wrap_love_dll_type_Video_getFilter(IntPtr p, out int out_filtermin_type, out int out_filtermag_type, out float out_anisotropy);
        internal static void wrap_love_dll_type_Video_getFilter(IntPtr p, out int out_filtermin_type, out int out_filtermag_type, out float out_anisotropy)
        {
            _wrap_love_dll_type_Video_getFilter(p, out out_filtermin_type, out out_filtermag_type, out out_anisotropy);
        }



        #endregion
        #region  type - CompressedImageData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CompressedImageData_getWidth")]
        internal extern static bool _wrap_love_dll_type_CompressedImageData_getWidth(IntPtr p, int miplevel, out int out_w);
        internal static bool wrap_love_dll_type_CompressedImageData_getWidth(IntPtr p, int miplevel, out int out_w)
        {
            return CheckCAPIException(_wrap_love_dll_type_CompressedImageData_getWidth(p, miplevel, out out_w));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CompressedImageData_getHeight")]
        internal extern static bool _wrap_love_dll_type_CompressedImageData_getHeight(IntPtr p, int miplevel, out int out_h);
        internal static bool wrap_love_dll_type_CompressedImageData_getHeight(IntPtr p, int miplevel, out int out_h)
        {
            return CheckCAPIException(_wrap_love_dll_type_CompressedImageData_getHeight(p, miplevel, out out_h));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CompressedImageData_getMipmapCount")]
        internal extern static void _wrap_love_dll_type_CompressedImageData_getMipmapCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_CompressedImageData_getMipmapCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_CompressedImageData_getMipmapCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CompressedImageData_getFormat")]
        internal extern static void _wrap_love_dll_type_CompressedImageData_getFormat(IntPtr p, out int out_format_type);
        internal static void wrap_love_dll_type_CompressedImageData_getFormat(IntPtr p, out int out_format_type)
        {
            _wrap_love_dll_type_CompressedImageData_getFormat(p, out out_format_type);
        }



        #endregion
        #region  type - ImageData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_getWidth")]
        internal extern static void _wrap_love_dll_type_ImageData_getWidth(IntPtr p, out int out_w);
        internal static void wrap_love_dll_type_ImageData_getWidth(IntPtr p, out int out_w)
        {
            _wrap_love_dll_type_ImageData_getWidth(p, out out_w);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_getHeight")]
        internal extern static void _wrap_love_dll_type_ImageData_getHeight(IntPtr p, out int out_h);
        internal static void wrap_love_dll_type_ImageData_getHeight(IntPtr p, out int out_h)
        {
            _wrap_love_dll_type_ImageData_getHeight(p, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_getPixel")]
        internal extern static bool _wrap_love_dll_type_ImageData_getPixel(IntPtr p, int x, int y, out Pixel out_pixel);
        internal static bool wrap_love_dll_type_ImageData_getPixel(IntPtr p, int x, int y, out Pixel out_pixel)
        {
            return CheckCAPIException(_wrap_love_dll_type_ImageData_getPixel(p, x, y, out out_pixel));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_setPixel")]
        internal extern static bool _wrap_love_dll_type_ImageData_setPixel(IntPtr p, int x, int y, Pixel pixel);
        internal static bool wrap_love_dll_type_ImageData_setPixel(IntPtr p, int x, int y, Pixel pixel)
        {
            return CheckCAPIException(_wrap_love_dll_type_ImageData_setPixel(p, x, y, pixel));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_GetFormat")]
        internal extern static void _wrap_love_dll_type_ImageData_GetFormat(IntPtr p, out int out_pixelFormat);
        internal static void wrap_love_dll_type_ImageData_GetFormat(IntPtr p, out int out_pixelFormat)
        {
            _wrap_love_dll_type_ImageData_GetFormat(p, out out_pixelFormat);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_ext_getPixelUnsafe")]
        internal extern static void _wrap_love_dll_type_ImageData_ext_getPixelUnsafe(IntPtr p, int x, int y, out byte out_r, out byte out_g, out byte out_b, out byte out_a);
        internal static void wrap_love_dll_type_ImageData_ext_getPixelUnsafe(IntPtr p, int x, int y, out byte out_r, out byte out_g, out byte out_b, out byte out_a)
        {
            _wrap_love_dll_type_ImageData_ext_getPixelUnsafe(p, x, y, out out_r, out out_g, out out_b, out out_a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_ext_setPixelUnsafe")]
        internal extern static void _wrap_love_dll_type_ImageData_ext_setPixelUnsafe(IntPtr p, int x, int y, byte r, byte g, byte b, byte a);
        internal static void wrap_love_dll_type_ImageData_ext_setPixelUnsafe(IntPtr p, int x, int y, byte r, byte g, byte b, byte a)
        {
            _wrap_love_dll_type_ImageData_ext_setPixelUnsafe(p, x, y, r, g, b, a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_paste")]
        internal extern static void _wrap_love_dll_type_ImageData_paste(IntPtr p, IntPtr src_imageData, int dx, int dy, int sx, int sy, int sw, int sh);
        internal static void wrap_love_dll_type_ImageData_paste(IntPtr p, IntPtr src_imageData, int dx, int dy, int sx, int sy, int sw, int sh)
        {
            _wrap_love_dll_type_ImageData_paste(p, src_imageData, dx, dy, sx, sy, sw, sh);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_encode")]
        internal extern static bool _wrap_love_dll_type_ImageData_encode(IntPtr p, int format_type, bool writeToFile, byte[] filename, out IntPtr out_fileData);
        internal static bool wrap_love_dll_type_ImageData_encode(IntPtr p, int format_type, bool writeToFile, byte[] filename, out IntPtr out_fileData)
        {
            return CheckCAPIException(_wrap_love_dll_type_ImageData_encode(p, format_type, writeToFile, filename, out out_fileData));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_getPixelSize(IntPtr p, out int out_pixelSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_lock(IntPtr p);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_unlock(IntPtr p);


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_setPixels(IntPtr p, IntPtr data, int byteLenght);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_setPixels_float4(IntPtr p, Vector4[] src);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_getPixels_float4(IntPtr p, IntPtr dest);

        #endregion
        #region  type - Cursor

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Cursor_getType")]
        internal extern static void _wrap_love_dll_type_Cursor_getType(IntPtr p, out int out_cursortype_type, out bool out_custom);
        internal static void wrap_love_dll_type_Cursor_getType(IntPtr p, out int out_cursortype_type, out bool out_custom)
        {
            _wrap_love_dll_type_Cursor_getType(p, out out_cursortype_type, out out_custom);
        }


        #endregion
        #region  type - Decoder

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Decoder_getChannelCount")]
        internal extern static void _wrap_love_dll_type_Decoder_getChannelCount(IntPtr p, out int out_channels);
        internal static void wrap_love_dll_type_Decoder_getChannelCount(IntPtr p, out int out_channels)
        {
            _wrap_love_dll_type_Decoder_getChannelCount(p, out out_channels);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Decoder_getBitDepth")]
        internal extern static void _wrap_love_dll_type_Decoder_getBitDepth(IntPtr p, out int out_bitDepth);
        internal static void wrap_love_dll_type_Decoder_getBitDepth(IntPtr p, out int out_bitDepth)
        {
            _wrap_love_dll_type_Decoder_getBitDepth(p, out out_bitDepth);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Decoder_getSampleRate")]
        internal extern static void _wrap_love_dll_type_Decoder_getSampleRate(IntPtr p, out int out_sampleRate);
        internal static void wrap_love_dll_type_Decoder_getSampleRate(IntPtr p, out int out_sampleRate)
        {
            _wrap_love_dll_type_Decoder_getSampleRate(p, out out_sampleRate);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Decoder_getDuration")]
        internal extern static void _wrap_love_dll_type_Decoder_getDuration(IntPtr p, out double out_duration);
        internal static void wrap_love_dll_type_Decoder_getDuration(IntPtr p, out double out_duration)
        {
            _wrap_love_dll_type_Decoder_getDuration(p, out out_duration);
        }



        #endregion
        #region  type - SoundData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getChannelCount")]
        internal extern static void _wrap_love_dll_SoundData_getChannelCount(IntPtr p, out int out_channels);
        internal static void wrap_love_dll_SoundData_getChannelCount(IntPtr p, out int out_channels)
        {
            _wrap_love_dll_SoundData_getChannelCount(p, out out_channels);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getBitDepth")]
        internal extern static void _wrap_love_dll_SoundData_getBitDepth(IntPtr p, out int out_bitDepth);
        internal static void wrap_love_dll_SoundData_getBitDepth(IntPtr p, out int out_bitDepth)
        {
            _wrap_love_dll_SoundData_getBitDepth(p, out out_bitDepth);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getSampleRate")]
        internal extern static void _wrap_love_dll_SoundData_getSampleRate(IntPtr p, out int out_sampleRate);
        internal static void wrap_love_dll_SoundData_getSampleRate(IntPtr p, out int out_sampleRate)
        {
            _wrap_love_dll_SoundData_getSampleRate(p, out out_sampleRate);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getSampleCount")]
        internal extern static void _wrap_love_dll_SoundData_getSampleCount(IntPtr p, out int out_sampleCount);
        internal static void wrap_love_dll_SoundData_getSampleCount(IntPtr p, out int out_sampleCount)
        {
            _wrap_love_dll_SoundData_getSampleCount(p, out out_sampleCount);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getDuration")]
        internal extern static void _wrap_love_dll_SoundData_getDuration(IntPtr p, out double out_duration);
        internal static void wrap_love_dll_SoundData_getDuration(IntPtr p, out double out_duration)
        {
            _wrap_love_dll_SoundData_getDuration(p, out out_duration);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_setSample")]
        internal extern static bool _wrap_love_dll_SoundData_setSample(IntPtr p, int i, float sample);
        internal static bool wrap_love_dll_SoundData_setSample(IntPtr p, int i, float sample)
        {
            return CheckCAPIException(_wrap_love_dll_SoundData_setSample(p, i, sample));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getSample")]
        internal extern static bool _wrap_love_dll_SoundData_getSample(IntPtr p, int i, out float out_sample);
        internal static bool wrap_love_dll_SoundData_getSample(IntPtr p, int i, out float out_sample)
        {
            return CheckCAPIException(_wrap_love_dll_SoundData_getSample(p, i, out out_sample));
        }



        #endregion
        #region  type - VideoStream

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_setSync")]
        internal extern static void _wrap_love_dll_type_VideoStream_setSync(IntPtr p, IntPtr source);
        internal static void wrap_love_dll_type_VideoStream_setSync(IntPtr p, IntPtr source)
        {
            _wrap_love_dll_type_VideoStream_setSync(p, source);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_getFilename")]
        internal extern static void _wrap_love_dll_type_VideoStream_getFilename(IntPtr p, out IntPtr out_filename);
        internal static void wrap_love_dll_type_VideoStream_getFilename(IntPtr p, out IntPtr out_filename)
        {
            _wrap_love_dll_type_VideoStream_getFilename(p, out out_filename);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_play")]
        internal extern static void _wrap_love_dll_type_VideoStream_play(IntPtr p);
        internal static void wrap_love_dll_type_VideoStream_play(IntPtr p)
        {
            _wrap_love_dll_type_VideoStream_play(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_pause")]
        internal extern static void _wrap_love_dll_type_VideoStream_pause(IntPtr p);
        internal static void wrap_love_dll_type_VideoStream_pause(IntPtr p)
        {
            _wrap_love_dll_type_VideoStream_pause(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_seek")]
        internal extern static void _wrap_love_dll_type_VideoStream_seek(IntPtr p, double offset);
        internal static void wrap_love_dll_type_VideoStream_seek(IntPtr p, double offset)
        {
            _wrap_love_dll_type_VideoStream_seek(p, offset);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_rewind")]
        internal extern static void _wrap_love_dll_type_VideoStream_rewind(IntPtr p);
        internal static void wrap_love_dll_type_VideoStream_rewind(IntPtr p)
        {
            _wrap_love_dll_type_VideoStream_rewind(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_tell")]
        internal extern static void _wrap_love_dll_type_VideoStream_tell(IntPtr p, out double out_position);
        internal static void wrap_love_dll_type_VideoStream_tell(IntPtr p, out double out_position)
        {
            _wrap_love_dll_type_VideoStream_tell(p, out out_position);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_isPlaying")]
        internal extern static void _wrap_love_dll_type_VideoStream_isPlaying(IntPtr p, out bool out_isplaying);
        internal static void wrap_love_dll_type_VideoStream_isPlaying(IntPtr p, out bool out_isplaying)
        {
            _wrap_love_dll_type_VideoStream_isPlaying(p, out out_isplaying);
        }



        #endregion
        #region  type - BezierCurve

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_getDegree")]
        internal extern static void _wrap_love_dll_type_BezierCurve_getDegree(IntPtr p, out int out_degree);
        internal static void wrap_love_dll_type_BezierCurve_getDegree(IntPtr p, out int out_degree)
        {
            _wrap_love_dll_type_BezierCurve_getDegree(p, out out_degree);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_getDerivative")]
        internal extern static void _wrap_love_dll_type_BezierCurve_getDerivative(IntPtr p, out IntPtr out_deriv);
        internal static void wrap_love_dll_type_BezierCurve_getDerivative(IntPtr p, out IntPtr out_deriv)
        {
            _wrap_love_dll_type_BezierCurve_getDerivative(p, out out_deriv);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_getControlPoint")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_getControlPoint(IntPtr p, int idx, out float out_x, out float out_y);
        internal static bool wrap_love_dll_type_BezierCurve_getControlPoint(IntPtr p, int idx, out float out_x, out float out_y)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_getControlPoint(p, idx, out out_x, out out_y));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_setControlPoint")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_setControlPoint(IntPtr p, int idx, float x, float y);
        internal static bool wrap_love_dll_type_BezierCurve_setControlPoint(IntPtr p, int idx, float x, float y)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_setControlPoint(p, idx, x, y));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_insertControlPoint")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_insertControlPoint(IntPtr p, int idx, float x, float y);
        internal static bool wrap_love_dll_type_BezierCurve_insertControlPoint(IntPtr p, int idx, float x, float y)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_insertControlPoint(p, idx, x, y));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_removeControlPoint")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_removeControlPoint(IntPtr p, int idx);
        internal static bool wrap_love_dll_type_BezierCurve_removeControlPoint(IntPtr p, int idx)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_removeControlPoint(p, idx));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_getControlPointCount")]
        internal extern static void _wrap_love_dll_type_BezierCurve_getControlPointCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_BezierCurve_getControlPointCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_BezierCurve_getControlPointCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_translate")]
        internal extern static void _wrap_love_dll_type_BezierCurve_translate(IntPtr p, float dx, float dy);
        internal static void wrap_love_dll_type_BezierCurve_translate(IntPtr p, float dx, float dy)
        {
            _wrap_love_dll_type_BezierCurve_translate(p, dx, dy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_rotate")]
        internal extern static void _wrap_love_dll_type_BezierCurve_rotate(IntPtr p, double phi, float ox, float oy);
        internal static void wrap_love_dll_type_BezierCurve_rotate(IntPtr p, double phi, float ox, float oy)
        {
            _wrap_love_dll_type_BezierCurve_rotate(p, phi, ox, oy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_scale")]
        internal extern static void _wrap_love_dll_type_BezierCurve_scale(IntPtr p, double s, float ox, float oy);
        internal static void wrap_love_dll_type_BezierCurve_scale(IntPtr p, double s, float ox, float oy)
        {
            _wrap_love_dll_type_BezierCurve_scale(p, s, ox, oy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_evaluate")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_evaluate(IntPtr p, double t, out float out_x, out float out_y);
        internal static bool wrap_love_dll_type_BezierCurve_evaluate(IntPtr p, double t, out float out_x, out float out_y)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_evaluate(p, t, out out_x, out out_y));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_getSegment")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_getSegment(IntPtr p, double t1, double t2, out IntPtr out_segment);
        internal static bool wrap_love_dll_type_BezierCurve_getSegment(IntPtr p, double t1, double t2, out IntPtr out_segment)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_getSegment(p, t1, t2, out out_segment));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_render")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_render(IntPtr p, int accuracy, out IntPtr out_points, out int out_points_lenght);
        internal static bool wrap_love_dll_type_BezierCurve_render(IntPtr p, int accuracy, out IntPtr out_points, out int out_points_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_render(p, accuracy, out out_points, out out_points_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_renderSegment")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_renderSegment(IntPtr p, double start, double end, int accuracy, out IntPtr out_points, out int out_points_lenght);
        internal static bool wrap_love_dll_type_BezierCurve_renderSegment(IntPtr p, double start, double end, int accuracy, out IntPtr out_points, out int out_points_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_renderSegment(p, start, end, accuracy, out out_points, out out_points_lenght));
        }



        #endregion
        #region  type - RandomGenerator

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_random")]
        internal extern static void _wrap_love_dll_type_RandomGenerator_random(IntPtr p, out double out_result);
        internal static void wrap_love_dll_type_RandomGenerator_random(IntPtr p, out double out_result)
        {
            _wrap_love_dll_type_RandomGenerator_random(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_randomNormal")]
        internal extern static void _wrap_love_dll_type_RandomGenerator_randomNormal(IntPtr p, double stddev, double mean, out double out_result);
        internal static void wrap_love_dll_type_RandomGenerator_randomNormal(IntPtr p, double stddev, double mean, out double out_result)
        {
            _wrap_love_dll_type_RandomGenerator_randomNormal(p, stddev, mean, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_setSeed")]
        internal extern static bool _wrap_love_dll_type_RandomGenerator_setSeed(IntPtr p, uint low, uint high);
        internal static bool wrap_love_dll_type_RandomGenerator_setSeed(IntPtr p, uint low, uint high)
        {
            return CheckCAPIException(_wrap_love_dll_type_RandomGenerator_setSeed(p, low, high));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_getSeed")]
        internal extern static void _wrap_love_dll_type_RandomGenerator_getSeed(IntPtr p, out uint out_low, out uint out_high);
        internal static void wrap_love_dll_type_RandomGenerator_getSeed(IntPtr p, out uint out_low, out uint out_high)
        {
            _wrap_love_dll_type_RandomGenerator_getSeed(p, out out_low, out out_high);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_setState")]
        internal extern static bool _wrap_love_dll_type_RandomGenerator_setState(IntPtr p, byte[] state);
        internal static bool wrap_love_dll_type_RandomGenerator_setState(IntPtr p, byte[] state)
        {
            return CheckCAPIException(_wrap_love_dll_type_RandomGenerator_setState(p, state));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_getState")]
        internal extern static void _wrap_love_dll_type_RandomGenerator_getState(IntPtr p, out IntPtr out_str);
        internal static void wrap_love_dll_type_RandomGenerator_getState(IntPtr p, out IntPtr out_str)
        {
            _wrap_love_dll_type_RandomGenerator_getState(p, out out_str);
        }



        #endregion
        #region  type - Joystick

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_isConnected")]
        internal extern static void _wrap_love_dll_type_Joystick_isConnected(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_Joystick_isConnected(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_Joystick_isConnected(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getName")]
        internal extern static void _wrap_love_dll_type_Joystick_getName(IntPtr p, out IntPtr out_str);
        internal static void wrap_love_dll_type_Joystick_getName(IntPtr p, out IntPtr out_str)
        {
            _wrap_love_dll_type_Joystick_getName(p, out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getID")]
        internal extern static void _wrap_love_dll_type_Joystick_getID(IntPtr p, out int out_id);
        internal static void wrap_love_dll_type_Joystick_getID(IntPtr p, out int out_id)
        {
            _wrap_love_dll_type_Joystick_getID(p, out out_id);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getInstanceID")]
        internal extern static void _wrap_love_dll_type_Joystick_getInstanceID(IntPtr p, out int out_instanceid);
        internal static void wrap_love_dll_type_Joystick_getInstanceID(IntPtr p, out int out_instanceid)
        {
            _wrap_love_dll_type_Joystick_getInstanceID(p, out out_instanceid);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getGUID")]
        internal extern static void _wrap_love_dll_type_Joystick_getGUID(IntPtr p, out IntPtr out_str);
        internal static void wrap_love_dll_type_Joystick_getGUID(IntPtr p, out IntPtr out_str)
        {
            _wrap_love_dll_type_Joystick_getGUID(p, out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getAxisCount")]
        internal extern static void _wrap_love_dll_type_Joystick_getAxisCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_Joystick_getAxisCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_Joystick_getAxisCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getButtonCount")]
        internal extern static void _wrap_love_dll_type_Joystick_getButtonCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_Joystick_getButtonCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_Joystick_getButtonCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getHatCount")]
        internal extern static void _wrap_love_dll_type_Joystick_getHatCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_Joystick_getHatCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_Joystick_getHatCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getAxis")]
        internal extern static void _wrap_love_dll_type_Joystick_getAxis(IntPtr p, int axisindex, out float out_axis);
        internal static void wrap_love_dll_type_Joystick_getAxis(IntPtr p, int axisindex, out float out_axis)
        {
            _wrap_love_dll_type_Joystick_getAxis(p, axisindex, out out_axis);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getAxes")]
        internal extern static void _wrap_love_dll_type_Joystick_getAxes(IntPtr p, out IntPtr out_axes, out int out_axes_length);
        internal static void wrap_love_dll_type_Joystick_getAxes(IntPtr p, out IntPtr out_axes, out int out_axes_length)
        {
            _wrap_love_dll_type_Joystick_getAxes(p, out out_axes, out out_axes_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getHat")]
        internal extern static void _wrap_love_dll_type_Joystick_getHat(IntPtr p, int hatindex, out int out_hat_type);
        internal static void wrap_love_dll_type_Joystick_getHat(IntPtr p, int hatindex, out int out_hat_type)
        {
            _wrap_love_dll_type_Joystick_getHat(p, hatindex, out out_hat_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_isDown")]
        internal extern static void _wrap_love_dll_type_Joystick_isDown(IntPtr p, int button, out bool out_result);
        internal static void wrap_love_dll_type_Joystick_isDown(IntPtr p, int button, out bool out_result)
        {
            _wrap_love_dll_type_Joystick_isDown(p, button, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_isGamepad")]
        internal extern static void _wrap_love_dll_type_Joystick_isGamepad(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_Joystick_isGamepad(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_Joystick_isGamepad(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getGamepadAxis")]
        internal extern static void _wrap_love_dll_type_Joystick_getGamepadAxis(IntPtr p, int axis_type, out float out_gamepadaxis);
        internal static void wrap_love_dll_type_Joystick_getGamepadAxis(IntPtr p, int axis_type, out float out_gamepadaxis)
        {
            _wrap_love_dll_type_Joystick_getGamepadAxis(p, axis_type, out out_gamepadaxis);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_isGamepadDown")]
        internal extern static void _wrap_love_dll_type_Joystick_isGamepadDown(IntPtr p, int gamepadButton_type, out bool out_result);
        internal static void wrap_love_dll_type_Joystick_isGamepadDown(IntPtr p, int gamepadButton_type, out bool out_result)
        {
            _wrap_love_dll_type_Joystick_isGamepadDown(p, gamepadButton_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_isVibrationSupported")]
        internal extern static void _wrap_love_dll_type_Joystick_isVibrationSupported(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_Joystick_isVibrationSupported(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_Joystick_isVibrationSupported(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_setVibration_nil")]
        internal extern static void _wrap_love_dll_type_Joystick_setVibration_nil(IntPtr p, out bool out_success);
        internal static void wrap_love_dll_type_Joystick_setVibration_nil(IntPtr p, out bool out_success)
        {
            _wrap_love_dll_type_Joystick_setVibration_nil(p, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_setVibration")]
        internal extern static void _wrap_love_dll_type_Joystick_setVibration(IntPtr p, float left, float right, float duration, out bool out_success);
        internal static void wrap_love_dll_type_Joystick_setVibration(IntPtr p, float left, float right, float duration, out bool out_success)
        {
            _wrap_love_dll_type_Joystick_setVibration(p, left, right, duration, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getVibration")]
        internal extern static void _wrap_love_dll_type_Joystick_getVibration(IntPtr p, out float out_left, out float out_right);
        internal static void wrap_love_dll_type_Joystick_getVibration(IntPtr p, out float out_left, out float out_right)
        {
            _wrap_love_dll_type_Joystick_getVibration(p, out out_left, out out_right);
        }



        #endregion
        #region  type - Other

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Data_getSize")]
        internal extern static void _wrap_love_dll_type_Data_getSize(IntPtr data, out uint out_datasize);
        internal static void wrap_love_dll_type_Data_getSize(IntPtr data, out uint out_datasize)
        {
            _wrap_love_dll_type_Data_getSize(data, out out_datasize);
        }


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Data_getPointer")]
        internal extern static void _wrap_love_dll_type_Data_getPointer(IntPtr data, out IntPtr out_pointer);
        internal static void wrap_love_dll_type_Data_getPointer(IntPtr data, out IntPtr out_pointer)
        {
            _wrap_love_dll_type_Data_getPointer(data, out out_pointer);
        }

        #endregion
























































        #region Physic

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getTransform")]
        internal extern static void _wrap_love_dll_type_Body_getTransform(IntPtr pBody, out Vector3 pos);
        internal static void wrap_love_dll_type_Body_getTransform(IntPtr pBody, out Vector3 pos)
        {
            _wrap_love_dll_type_Body_getTransform(pBody, out pos);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getLinearVelocity")]
        internal extern static void _wrap_love_dll_type_Body_getLinearVelocity(IntPtr pBody, out Vector2 result);
        internal static void wrap_love_dll_type_Body_getLinearVelocity(IntPtr pBody, out Vector2 result)
        {
            _wrap_love_dll_type_Body_getLinearVelocity(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getWorldCenter")]
        internal extern static void _wrap_love_dll_type_Body_getWorldCenter(IntPtr pBody, out Vector2 result);
        internal static void wrap_love_dll_type_Body_getWorldCenter(IntPtr pBody, out Vector2 result)
        {
            _wrap_love_dll_type_Body_getWorldCenter(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getLocalCenter")]
        internal extern static void _wrap_love_dll_type_Body_getLocalCenter(IntPtr pBody, out Vector2 result);
        internal static void wrap_love_dll_type_Body_getLocalCenter(IntPtr pBody, out Vector2 result)
        {
            _wrap_love_dll_type_Body_getLocalCenter(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getAngularVelocity")]
        internal extern static void _wrap_love_dll_type_Body_getAngularVelocity(IntPtr pBody, out float result);
        internal static void wrap_love_dll_type_Body_getAngularVelocity(IntPtr pBody, out float result)
        {
            _wrap_love_dll_type_Body_getAngularVelocity(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getMass")]
        internal extern static void _wrap_love_dll_type_Body_getMass(IntPtr pBody, out float result);
        internal static void wrap_love_dll_type_Body_getMass(IntPtr pBody, out float result)
        {
            _wrap_love_dll_type_Body_getMass(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getInertia")]
        internal extern static void _wrap_love_dll_type_Body_getInertia(IntPtr pBody, out float result);
        internal static void wrap_love_dll_type_Body_getInertia(IntPtr pBody, out float result)
        {
            _wrap_love_dll_type_Body_getInertia(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getAngularDamping")]
        internal extern static void _wrap_love_dll_type_Body_getAngularDamping(IntPtr pBody, out float result);
        internal static void wrap_love_dll_type_Body_getAngularDamping(IntPtr pBody, out float result)
        {
            _wrap_love_dll_type_Body_getAngularDamping(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getLinearDamping")]
        internal extern static void _wrap_love_dll_type_Body_getLinearDamping(IntPtr pBody, out float result);
        internal static void wrap_love_dll_type_Body_getLinearDamping(IntPtr pBody, out float result)
        {
            _wrap_love_dll_type_Body_getLinearDamping(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getGravityScale")]
        internal extern static void _wrap_love_dll_type_Body_getGravityScale(IntPtr pBody, out float result);
        internal static void wrap_love_dll_type_Body_getGravityScale(IntPtr pBody, out float result)
        {
            _wrap_love_dll_type_Body_getGravityScale(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getType")]
        internal extern static void _wrap_love_dll_type_Body_getType(IntPtr pBody, out int body_type);
        internal static void wrap_love_dll_type_Body_getType(IntPtr pBody, out int body_type)
        {
            _wrap_love_dll_type_Body_getType(pBody, out body_type);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_applyLinearImpulse_xy")]
        internal extern static void _wrap_love_dll_type_Body_applyLinearImpulse_xy(IntPtr pBody, float jx, float jy);
        internal static void wrap_love_dll_type_Body_applyLinearImpulse_xy(IntPtr pBody, float jx, float jy)
        {
            _wrap_love_dll_type_Body_applyLinearImpulse_xy(pBody, jx, jy);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_applyLinearImpulse_xy_offset")]
        internal extern static void _wrap_love_dll_type_Body_applyLinearImpulse_xy_offset(IntPtr pBody, float jx, float jy, float ox, float oy);
        internal static void wrap_love_dll_type_Body_applyLinearImpulse_xy_offset(IntPtr pBody, float jx, float jy, float ox, float oy)
        {
            _wrap_love_dll_type_Body_applyLinearImpulse_xy_offset(pBody, jx, jy, ox, oy);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_applyAngularImpulse")]
        internal extern static void _wrap_love_dll_type_Body_applyAngularImpulse(IntPtr pBody, float i);
        internal static void wrap_love_dll_type_Body_applyAngularImpulse(IntPtr pBody, float i)
        {
            _wrap_love_dll_type_Body_applyAngularImpulse(pBody, i);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_applyTorque")]
        internal extern static void _wrap_love_dll_type_Body_applyTorque(IntPtr pBody, float torque);
        internal static void wrap_love_dll_type_Body_applyTorque(IntPtr pBody, float torque)
        {
            _wrap_love_dll_type_Body_applyTorque(pBody, torque);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_applyForce_xy")]
        internal extern static void _wrap_love_dll_type_Body_applyForce_xy(IntPtr pBody, float fx, float fy);
        internal static void wrap_love_dll_type_Body_applyForce_xy(IntPtr pBody, float fx, float fy)
        {
            _wrap_love_dll_type_Body_applyForce_xy(pBody, fx, fy);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_applyForce_xy_offset")]
        internal extern static void _wrap_love_dll_type_Body_applyForce_xy_offset(IntPtr pBody, float fx, float fy, float ox, float oy);
        internal static void wrap_love_dll_type_Body_applyForce_xy_offset(IntPtr pBody, float fx, float fy, float ox, float oy)
        {
            _wrap_love_dll_type_Body_applyForce_xy_offset(pBody, fx, fy, ox, oy);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setX")]
        internal extern static bool _wrap_love_dll_type_Body_setX(IntPtr pBody, float x);
        internal static bool wrap_love_dll_type_Body_setX(IntPtr pBody, float x)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_setX(pBody, x));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setY")]
        internal extern static bool _wrap_love_dll_type_Body_setY(IntPtr pBody, float y);
        internal static bool wrap_love_dll_type_Body_setY(IntPtr pBody, float y)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_setY(pBody, y));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setLinearVelocity")]
        internal extern static void _wrap_love_dll_type_Body_setLinearVelocity(IntPtr pBody, float x, float y);
        internal static void wrap_love_dll_type_Body_setLinearVelocity(IntPtr pBody, float x, float y)
        {
            _wrap_love_dll_type_Body_setLinearVelocity(pBody, x, y);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setAngle")]
        internal extern static bool _wrap_love_dll_type_Body_setAngle(IntPtr pBody, float angle);
        internal static bool wrap_love_dll_type_Body_setAngle(IntPtr pBody, float angle)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_setAngle(pBody, angle));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setAngularVelocity")]
        internal extern static void _wrap_love_dll_type_Body_setAngularVelocity(IntPtr pBody, float angleVelocity);
        internal static void wrap_love_dll_type_Body_setAngularVelocity(IntPtr pBody, float angleVelocity)
        {
            _wrap_love_dll_type_Body_setAngularVelocity(pBody, angleVelocity);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setPosition")]
        internal extern static bool _wrap_love_dll_type_Body_setPosition(IntPtr pBody, float x, float y);
        internal static bool wrap_love_dll_type_Body_setPosition(IntPtr pBody, float x, float y)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_setPosition(pBody, x, y));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_resetMassData")]
        internal extern static bool _wrap_love_dll_type_Body_resetMassData(IntPtr pBody);
        internal static bool wrap_love_dll_type_Body_resetMassData(IntPtr pBody)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_resetMassData(pBody));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setMass")]
        internal extern static bool _wrap_love_dll_type_Body_setMass(IntPtr pBody, float m);
        internal static bool wrap_love_dll_type_Body_setMass(IntPtr pBody, float m)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_setMass(pBody, m));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setInertia")]
        internal extern static bool _wrap_love_dll_type_Body_setInertia(IntPtr pBody, float inertia);
        internal static bool wrap_love_dll_type_Body_setInertia(IntPtr pBody, float inertia)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_setInertia(pBody, inertia));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setAngularDamping")]
        internal extern static void _wrap_love_dll_type_Body_setAngularDamping(IntPtr pBody, float angularDamping);
        internal static void wrap_love_dll_type_Body_setAngularDamping(IntPtr pBody, float angularDamping)
        {
            _wrap_love_dll_type_Body_setAngularDamping(pBody, angularDamping);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setLinearDamping")]
        internal extern static void _wrap_love_dll_type_Body_setLinearDamping(IntPtr pBody, float linerDamping);
        internal static void wrap_love_dll_type_Body_setLinearDamping(IntPtr pBody, float linerDamping)
        {
            _wrap_love_dll_type_Body_setLinearDamping(pBody, linerDamping);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setGravityScale")]
        internal extern static void _wrap_love_dll_type_Body_setGravityScale(IntPtr pBody, float scale);
        internal static void wrap_love_dll_type_Body_setGravityScale(IntPtr pBody, float scale)
        {
            _wrap_love_dll_type_Body_setGravityScale(pBody, scale);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setType")]
        internal extern static bool _wrap_love_dll_type_Body_setType(IntPtr pBody, int body_type);
        internal static bool wrap_love_dll_type_Body_setType(IntPtr pBody, int body_type)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_setType(pBody, body_type));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getWorldPoint")]
        internal extern static void _wrap_love_dll_type_Body_getWorldPoint(IntPtr pBody, float x, float y, out Vector2 result);
        internal static void wrap_love_dll_type_Body_getWorldPoint(IntPtr pBody, float x, float y, out Vector2 result)
        {
            _wrap_love_dll_type_Body_getWorldPoint(pBody, x, y, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getWorldVector")]
        internal extern static void _wrap_love_dll_type_Body_getWorldVector(IntPtr pBody, float x, float y, out Vector2 result);
        internal static void wrap_love_dll_type_Body_getWorldVector(IntPtr pBody, float x, float y, out Vector2 result)
        {
            _wrap_love_dll_type_Body_getWorldVector(pBody, x, y, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getLocalPoint")]
        internal extern static void _wrap_love_dll_type_Body_getLocalPoint(IntPtr pBody, float x, float y, out Vector2 result);
        internal static void wrap_love_dll_type_Body_getLocalPoint(IntPtr pBody, float x, float y, out Vector2 result)
        {
            _wrap_love_dll_type_Body_getLocalPoint(pBody, x, y, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getLocalVector")]
        internal extern static void _wrap_love_dll_type_Body_getLocalVector(IntPtr pBody, float x, float y, out Vector2 result);
        internal static void wrap_love_dll_type_Body_getLocalVector(IntPtr pBody, float x, float y, out Vector2 result)
        {
            _wrap_love_dll_type_Body_getLocalVector(pBody, x, y, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getLinearVelocityFromWorldPoint")]
        internal extern static void _wrap_love_dll_type_Body_getLinearVelocityFromWorldPoint(IntPtr pBody, float x, float y, out Vector2 result);
        internal static void wrap_love_dll_type_Body_getLinearVelocityFromWorldPoint(IntPtr pBody, float x, float y, out Vector2 result)
        {
            _wrap_love_dll_type_Body_getLinearVelocityFromWorldPoint(pBody, x, y, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getLinearVelocityFromLocalPoint")]
        internal extern static void _wrap_love_dll_type_Body_getLinearVelocityFromLocalPoint(IntPtr pBody, float x, float y, out Vector2 result);
        internal static void wrap_love_dll_type_Body_getLinearVelocityFromLocalPoint(IntPtr pBody, float x, float y, out Vector2 result)
        {
            _wrap_love_dll_type_Body_getLinearVelocityFromLocalPoint(pBody, x, y, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_isBullet")]
        internal extern static void _wrap_love_dll_type_Body_isBullet(IntPtr pBody, out bool result);
        internal static void wrap_love_dll_type_Body_isBullet(IntPtr pBody, out bool result)
        {
            _wrap_love_dll_type_Body_isBullet(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setBullet")]
        internal extern static void _wrap_love_dll_type_Body_setBullet(IntPtr pBody, bool b);
        internal static void wrap_love_dll_type_Body_setBullet(IntPtr pBody, bool b)
        {
            _wrap_love_dll_type_Body_setBullet(pBody, b);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_isActive")]
        internal extern static void _wrap_love_dll_type_Body_isActive(IntPtr pBody, out bool result);
        internal static void wrap_love_dll_type_Body_isActive(IntPtr pBody, out bool result)
        {
            _wrap_love_dll_type_Body_isActive(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_isAwake")]
        internal extern static void _wrap_love_dll_type_Body_isAwake(IntPtr pBody, out bool result);
        internal static void wrap_love_dll_type_Body_isAwake(IntPtr pBody, out bool result)
        {
            _wrap_love_dll_type_Body_isAwake(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setSleepingAllowed")]
        internal extern static void _wrap_love_dll_type_Body_setSleepingAllowed(IntPtr pBody, bool b);
        internal static void wrap_love_dll_type_Body_setSleepingAllowed(IntPtr pBody, bool b)
        {
            _wrap_love_dll_type_Body_setSleepingAllowed(pBody, b);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_isSleepingAllowed")]
        internal extern static void _wrap_love_dll_type_Body_isSleepingAllowed(IntPtr pBody, out bool result);
        internal static void wrap_love_dll_type_Body_isSleepingAllowed(IntPtr pBody, out bool result)
        {
            _wrap_love_dll_type_Body_isSleepingAllowed(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setActive")]
        internal extern static bool _wrap_love_dll_type_Body_setActive(IntPtr pBody, bool b);
        internal static bool wrap_love_dll_type_Body_setActive(IntPtr pBody, bool b)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_setActive(pBody, b));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setAwake")]
        internal extern static void _wrap_love_dll_type_Body_setAwake(IntPtr pBody, bool b);
        internal static void wrap_love_dll_type_Body_setAwake(IntPtr pBody, bool b)
        {
            _wrap_love_dll_type_Body_setAwake(pBody, b);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_setFixedRotation")]
        internal extern static bool _wrap_love_dll_type_Body_setFixedRotation(IntPtr pBody, bool b);
        internal static bool wrap_love_dll_type_Body_setFixedRotation(IntPtr pBody, bool b)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_setFixedRotation(pBody, b));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_isFixedRotation")]
        internal extern static void _wrap_love_dll_type_Body_isFixedRotation(IntPtr pBody, out bool result);
        internal static void wrap_love_dll_type_Body_isFixedRotation(IntPtr pBody, out bool result)
        {
            _wrap_love_dll_type_Body_isFixedRotation(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getWorld")]
        internal extern static void _wrap_love_dll_type_Body_getWorld(IntPtr pBody, out IntPtr world);
        internal static void wrap_love_dll_type_Body_getWorld(IntPtr pBody, out IntPtr world)
        {
            _wrap_love_dll_type_Body_getWorld(pBody, out world);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_destroy")]
        internal extern static bool _wrap_love_dll_type_Body_destroy(IntPtr pBody);
        internal static bool wrap_love_dll_type_Body_destroy(IntPtr pBody)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_destroy(pBody));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_isDestroyed")]
        internal extern static void _wrap_love_dll_type_Body_isDestroyed(IntPtr pBody, out bool result);
        internal static void wrap_love_dll_type_Body_isDestroyed(IntPtr pBody, out bool result)
        {
            _wrap_love_dll_type_Body_isDestroyed(pBody, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getFixtureList")]
        internal extern static bool _wrap_love_dll_type_Body_getFixtureList(IntPtr pBody, out IntPtr fixtures, out int fixtures_length);
        internal static bool wrap_love_dll_type_Body_getFixtureList(IntPtr pBody, out IntPtr fixtures, out int fixtures_length)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_getFixtureList(pBody, out fixtures, out fixtures_length));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getJointList")]
        internal extern static bool _wrap_love_dll_type_Body_getJointList(IntPtr pBody, out IntPtr joints, out int joints_length);
        internal static bool wrap_love_dll_type_Body_getJointList(IntPtr pBody, out IntPtr joints, out int joints_length)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_getJointList(pBody, out joints, out joints_length));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Body_getContactList")]
        internal extern static bool _wrap_love_dll_type_Body_getContactList(IntPtr pBody, out IntPtr contacts, out int contacts_length);
        internal static bool wrap_love_dll_type_Body_getContactList(IntPtr pBody, out IntPtr contacts, out int contacts_length)
        {
            return CheckCAPIException(_wrap_love_dll_type_Body_getContactList(pBody, out contacts, out contacts_length));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shape_getType")]
        internal extern static void _wrap_love_dll_type_Shape_getType(IntPtr pShape, out int shapeType);
        internal static void wrap_love_dll_type_Shape_getType(IntPtr pShape, out int shapeType)
        {
            _wrap_love_dll_type_Shape_getType(pShape, out shapeType);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shape_getRadius")]
        internal extern static void _wrap_love_dll_type_Shape_getRadius(IntPtr pShape, out float radius);
        internal static void wrap_love_dll_type_Shape_getRadius(IntPtr pShape, out float radius)
        {
            _wrap_love_dll_type_Shape_getRadius(pShape, out radius);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shape_getChildCount")]
        internal extern static void _wrap_love_dll_type_Shape_getChildCount(IntPtr pShape, out float childCount);
        internal static void wrap_love_dll_type_Shape_getChildCount(IntPtr pShape, out float childCount)
        {
            _wrap_love_dll_type_Shape_getChildCount(pShape, out childCount);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shape_testPoint")]
        internal extern static void _wrap_love_dll_type_Shape_testPoint(IntPtr pShape, float tx, float ty, float tr, float px, float py, out bool result);
        internal static void wrap_love_dll_type_Shape_testPoint(IntPtr pShape, float tx, float ty, float tr, float px, float py, out bool result)
        {
            _wrap_love_dll_type_Shape_testPoint(pShape, tx, ty, tr, px, py, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shape_rayCast")]
        internal extern static bool _wrap_love_dll_type_Shape_rayCast(IntPtr pShape, Vector2 p1, Vector2 p2, float maxFraction, Vector2 trans, float tr, int childIndex, WrapShapeRayCastCallbackDelegate callback);
        internal static bool wrap_love_dll_type_Shape_rayCast(IntPtr pShape, Vector2 p1, Vector2 p2, float maxFraction, Vector2 trans, float tr, int childIndex, WrapShapeRayCastCallbackDelegate callback)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shape_rayCast(pShape, p1, p2, maxFraction, trans, tr, childIndex, callback));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shape_computeAABB")]
        internal extern static bool _wrap_love_dll_type_Shape_computeAABB(IntPtr pShape, float x, float y, float r, int childIndex, WrapShapeComputeAABBCallbackDelegate callback);
        internal static bool wrap_love_dll_type_Shape_computeAABB(IntPtr pShape, float x, float y, float r, int childIndex, WrapShapeComputeAABBCallbackDelegate callback)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shape_computeAABB(pShape, x, y, r, childIndex, callback));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shape_computeMass")]
        internal extern static void _wrap_love_dll_type_Shape_computeMass(IntPtr pShape, float density, WrapShapeComputeMassCallbackDelegate callback);
        internal static void wrap_love_dll_type_Shape_computeMass(IntPtr pShape, float density, WrapShapeComputeMassCallbackDelegate callback)
        {
            _wrap_love_dll_type_Shape_computeMass(pShape, density, callback);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ChainShape_setNextVertex_nil")]
        internal extern static void _wrap_love_dll_type_ChainShape_setNextVertex_nil(IntPtr pChainShape);
        internal static void wrap_love_dll_type_ChainShape_setNextVertex_nil(IntPtr pChainShape)
        {
            _wrap_love_dll_type_ChainShape_setNextVertex_nil(pChainShape);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ChainShape_setNextVertex")]
        internal extern static bool _wrap_love_dll_type_ChainShape_setNextVertex(IntPtr pChainShape, float x, float y);
        internal static bool wrap_love_dll_type_ChainShape_setNextVertex(IntPtr pChainShape, float x, float y)
        {
            return CheckCAPIException(_wrap_love_dll_type_ChainShape_setNextVertex(pChainShape, x, y));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ChainShape_setPreviousVertex_nil")]
        internal extern static void _wrap_love_dll_type_ChainShape_setPreviousVertex_nil(IntPtr pChainShape);
        internal static void wrap_love_dll_type_ChainShape_setPreviousVertex_nil(IntPtr pChainShape)
        {
            _wrap_love_dll_type_ChainShape_setPreviousVertex_nil(pChainShape);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ChainShape_setPreviousVertex")]
        internal extern static bool _wrap_love_dll_type_ChainShape_setPreviousVertex(IntPtr pChainShape, float x, float y);
        internal static bool wrap_love_dll_type_ChainShape_setPreviousVertex(IntPtr pChainShape, float x, float y)
        {
            return CheckCAPIException(_wrap_love_dll_type_ChainShape_setPreviousVertex(pChainShape, x, y));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ChainShape_getChildEdge")]
        internal extern static bool _wrap_love_dll_type_ChainShape_getChildEdge(IntPtr pChainShape, int index, out IntPtr edgeShape);
        internal static bool wrap_love_dll_type_ChainShape_getChildEdge(IntPtr pChainShape, int index, out IntPtr edgeShape)
        {
            return CheckCAPIException(_wrap_love_dll_type_ChainShape_getChildEdge(pChainShape, index, out edgeShape));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ChainShape_getVertexCount")]
        internal extern static void _wrap_love_dll_type_ChainShape_getVertexCount(IntPtr pChainShape, out int count);
        internal static void wrap_love_dll_type_ChainShape_getVertexCount(IntPtr pChainShape, out int count)
        {
            _wrap_love_dll_type_ChainShape_getVertexCount(pChainShape, out count);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ChainShape_getPoint")]
        internal extern static bool _wrap_love_dll_type_ChainShape_getPoint(IntPtr pChainShape, int index, out Vector2 point);
        internal static bool wrap_love_dll_type_ChainShape_getPoint(IntPtr pChainShape, int index, out Vector2 point)
        {
            return CheckCAPIException(_wrap_love_dll_type_ChainShape_getPoint(pChainShape, index, out point));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ChainShape_getNextVertex")]
        internal extern static void _wrap_love_dll_type_ChainShape_getNextVertex(IntPtr pChainShape, out bool hasNextVertex, out Vector2 result);
        internal static void wrap_love_dll_type_ChainShape_getNextVertex(IntPtr pChainShape, out bool hasNextVertex, out Vector2 result)
        {
            _wrap_love_dll_type_ChainShape_getNextVertex(pChainShape, out hasNextVertex, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ChainShape_getPreviousVertex")]
        internal extern static void _wrap_love_dll_type_ChainShape_getPreviousVertex(IntPtr pChainShape, out bool hasPrevVertex, out Vector2 result);
        internal static void wrap_love_dll_type_ChainShape_getPreviousVertex(IntPtr pChainShape, out bool hasPrevVertex, out Vector2 result)
        {
            _wrap_love_dll_type_ChainShape_getPreviousVertex(pChainShape, out hasPrevVertex, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ChainShape_getPoints")]
        internal extern static void _wrap_love_dll_type_ChainShape_getPoints(IntPtr pChainShape, out IntPtr points, out int points_length);
        internal static void wrap_love_dll_type_ChainShape_getPoints(IntPtr pChainShape, out IntPtr points, out int points_length)
        {
            _wrap_love_dll_type_ChainShape_getPoints(pChainShape, out points, out points_length);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CircleShape_getRadius")]
        internal extern static void _wrap_love_dll_type_CircleShape_getRadius(IntPtr pCircleShape, out float radius);
        internal static void wrap_love_dll_type_CircleShape_getRadius(IntPtr pCircleShape, out float radius)
        {
            _wrap_love_dll_type_CircleShape_getRadius(pCircleShape, out radius);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CircleShape_setRadius")]
        internal extern static void _wrap_love_dll_type_CircleShape_setRadius(IntPtr pCircleShape, float r);
        internal static void wrap_love_dll_type_CircleShape_setRadius(IntPtr pCircleShape, float r)
        {
            _wrap_love_dll_type_CircleShape_setRadius(pCircleShape, r);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CircleShape_getPoint")]
        internal extern static void _wrap_love_dll_type_CircleShape_getPoint(IntPtr pCircleShape, out Vector2 point);
        internal static void wrap_love_dll_type_CircleShape_getPoint(IntPtr pCircleShape, out Vector2 point)
        {
            _wrap_love_dll_type_CircleShape_getPoint(pCircleShape, out point);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CircleShape_setPoint")]
        internal extern static void _wrap_love_dll_type_CircleShape_setPoint(IntPtr pCircleShape, float x, float y);
        internal static void wrap_love_dll_type_CircleShape_setPoint(IntPtr pCircleShape, float x, float y)
        {
            _wrap_love_dll_type_CircleShape_setPoint(pCircleShape, x, y);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_EdgeShape_setNextVertex_nil")]
        internal extern static void _wrap_love_dll_type_EdgeShape_setNextVertex_nil(IntPtr pEdgeShape);
        internal static void wrap_love_dll_type_EdgeShape_setNextVertex_nil(IntPtr pEdgeShape)
        {
            _wrap_love_dll_type_EdgeShape_setNextVertex_nil(pEdgeShape);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_EdgeShape_setNextVertex")]
        internal extern static void _wrap_love_dll_type_EdgeShape_setNextVertex(IntPtr pEdgeShape, float x, float y);
        internal static void wrap_love_dll_type_EdgeShape_setNextVertex(IntPtr pEdgeShape, float x, float y)
        {
            _wrap_love_dll_type_EdgeShape_setNextVertex(pEdgeShape, x, y);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_EdgeShape_setPreviousVertex_nil")]
        internal extern static void _wrap_love_dll_type_EdgeShape_setPreviousVertex_nil(IntPtr pEdgeShape);
        internal static void wrap_love_dll_type_EdgeShape_setPreviousVertex_nil(IntPtr pEdgeShape)
        {
            _wrap_love_dll_type_EdgeShape_setPreviousVertex_nil(pEdgeShape);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_EdgeShape_setPreviousVertex")]
        internal extern static void _wrap_love_dll_type_EdgeShape_setPreviousVertex(IntPtr pEdgeShape, float x, float y);
        internal static void wrap_love_dll_type_EdgeShape_setPreviousVertex(IntPtr pEdgeShape, float x, float y)
        {
            _wrap_love_dll_type_EdgeShape_setPreviousVertex(pEdgeShape, x, y);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_EdgeShape_getNextVertex")]
        internal extern static void _wrap_love_dll_type_EdgeShape_getNextVertex(IntPtr pEdgeShape, out bool hasNextVertex, out Vector2 result);
        internal static void wrap_love_dll_type_EdgeShape_getNextVertex(IntPtr pEdgeShape, out bool hasNextVertex, out Vector2 result)
        {
            _wrap_love_dll_type_EdgeShape_getNextVertex(pEdgeShape, out hasNextVertex, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_EdgeShape_getPreviousVertex")]
        internal extern static void _wrap_love_dll_type_EdgeShape_getPreviousVertex(IntPtr pEdgeShape, out bool hasPrevVertex, out Vector2 result);
        internal static void wrap_love_dll_type_EdgeShape_getPreviousVertex(IntPtr pEdgeShape, out bool hasPrevVertex, out Vector2 result)
        {
            _wrap_love_dll_type_EdgeShape_getPreviousVertex(pEdgeShape, out hasPrevVertex, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_EdgeShape_getPoints")]
        internal extern static void _wrap_love_dll_type_EdgeShape_getPoints(IntPtr pEdgeShape, out float x1, out float y1, out float x2, out float y2);
        internal static void wrap_love_dll_type_EdgeShape_getPoints(IntPtr pEdgeShape, out float x1, out float y1, out float x2, out float y2)
        {
            _wrap_love_dll_type_EdgeShape_getPoints(pEdgeShape, out x1, out y1, out x2, out y2);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PolygonShape_validate")]
        internal extern static void _wrap_love_dll_type_PolygonShape_validate(IntPtr pPolygonShape, out bool validate);
        internal static void wrap_love_dll_type_PolygonShape_validate(IntPtr pPolygonShape, out bool validate)
        {
            _wrap_love_dll_type_PolygonShape_validate(pPolygonShape, out validate);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PolygonShape_getPoints")]
        internal extern static void _wrap_love_dll_type_PolygonShape_getPoints(IntPtr pPolygonShape, out IntPtr pointList, out int pointListLength);
        internal static void wrap_love_dll_type_PolygonShape_getPoints(IntPtr pPolygonShape, out IntPtr pointList, out int pointListLength)
        {
            _wrap_love_dll_type_PolygonShape_getPoints(pPolygonShape, out pointList, out pointListLength);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_getFriction")]
        internal extern static void _wrap_love_dll_type_Contact_getFriction(IntPtr pContact, out float friction);
        internal static void wrap_love_dll_type_Contact_getFriction(IntPtr pContact, out float friction)
        {
            _wrap_love_dll_type_Contact_getFriction(pContact, out friction);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_getRestitution")]
        internal extern static void _wrap_love_dll_type_Contact_getRestitution(IntPtr pContact, out float restitution);
        internal static void wrap_love_dll_type_Contact_getRestitution(IntPtr pContact, out float restitution)
        {
            _wrap_love_dll_type_Contact_getRestitution(pContact, out restitution);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_isEnabled")]
        internal extern static void _wrap_love_dll_type_Contact_isEnabled(IntPtr pContact, out bool result);
        internal static void wrap_love_dll_type_Contact_isEnabled(IntPtr pContact, out bool result)
        {
            _wrap_love_dll_type_Contact_isEnabled(pContact, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_isTouching")]
        internal extern static void _wrap_love_dll_type_Contact_isTouching(IntPtr pContact, out bool result);
        internal static void wrap_love_dll_type_Contact_isTouching(IntPtr pContact, out bool result)
        {
            _wrap_love_dll_type_Contact_isTouching(pContact, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_setFriction")]
        internal extern static void _wrap_love_dll_type_Contact_setFriction(IntPtr pContact, float friction);
        internal static void wrap_love_dll_type_Contact_setFriction(IntPtr pContact, float friction)
        {
            _wrap_love_dll_type_Contact_setFriction(pContact, friction);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_setRestitution")]
        internal extern static void _wrap_love_dll_type_Contact_setRestitution(IntPtr pContact, float restitution);
        internal static void wrap_love_dll_type_Contact_setRestitution(IntPtr pContact, float restitution)
        {
            _wrap_love_dll_type_Contact_setRestitution(pContact, restitution);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_setEnabled")]
        internal extern static void _wrap_love_dll_type_Contact_setEnabled(IntPtr pContact, bool enabled);
        internal static void wrap_love_dll_type_Contact_setEnabled(IntPtr pContact, bool enabled)
        {
            _wrap_love_dll_type_Contact_setEnabled(pContact, enabled);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_resetFriction")]
        internal extern static void _wrap_love_dll_type_Contact_resetFriction(IntPtr pContact);
        internal static void wrap_love_dll_type_Contact_resetFriction(IntPtr pContact)
        {
            _wrap_love_dll_type_Contact_resetFriction(pContact);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_resetRestitution")]
        internal extern static void _wrap_love_dll_type_Contact_resetRestitution(IntPtr pContact);
        internal static void wrap_love_dll_type_Contact_resetRestitution(IntPtr pContact)
        {
            _wrap_love_dll_type_Contact_resetRestitution(pContact);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_setTangentSpeed")]
        internal extern static void _wrap_love_dll_type_Contact_setTangentSpeed(IntPtr pContact, float speed);
        internal static void wrap_love_dll_type_Contact_setTangentSpeed(IntPtr pContact, float speed)
        {
            _wrap_love_dll_type_Contact_setTangentSpeed(pContact, speed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_getTangentSpeed")]
        internal extern static void _wrap_love_dll_type_Contact_getTangentSpeed(IntPtr pContact, out float speed);
        internal static void wrap_love_dll_type_Contact_getTangentSpeed(IntPtr pContact, out float speed)
        {
            _wrap_love_dll_type_Contact_getTangentSpeed(pContact, out speed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_getChildren")]
        internal extern static void _wrap_love_dll_type_Contact_getChildren(IntPtr pContact, out int childA, out int childB);
        internal static void wrap_love_dll_type_Contact_getChildren(IntPtr pContact, out int childA, out int childB)
        {
            _wrap_love_dll_type_Contact_getChildren(pContact, out childA, out childB);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_getFixtures")]
        internal extern static bool _wrap_love_dll_type_Contact_getFixtures(IntPtr pContact, out IntPtr a, out IntPtr b);
        internal static bool wrap_love_dll_type_Contact_getFixtures(IntPtr pContact, out IntPtr a, out IntPtr b)
        {
            return CheckCAPIException(_wrap_love_dll_type_Contact_getFixtures(pContact, out a, out b));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_isDestroyed")]
        internal extern static void _wrap_love_dll_type_Contact_isDestroyed(IntPtr pContact, out bool result);
        internal static void wrap_love_dll_type_Contact_isDestroyed(IntPtr pContact, out bool result)
        {
            _wrap_love_dll_type_Contact_isDestroyed(pContact, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_getPositions")]
        internal extern static void _wrap_love_dll_type_Contact_getPositions(IntPtr pContact, out IntPtr pointList, out int pointListLength);
        internal static void wrap_love_dll_type_Contact_getPositions(IntPtr pContact, out IntPtr pointList, out int pointListLength)
        {
            _wrap_love_dll_type_Contact_getPositions(pContact, out pointList, out pointListLength);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Contact_getNormal")]
        internal extern static void _wrap_love_dll_type_Contact_getNormal(IntPtr pContact, out float nx, out float ny);
        internal static void wrap_love_dll_type_Contact_getNormal(IntPtr pContact, out float nx, out float ny)
        {
            _wrap_love_dll_type_Contact_getNormal(pContact, out nx, out ny);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_DistanceJoint_setLength")]
        internal extern static void _wrap_love_dll_type_DistanceJoint_setLength(IntPtr pDistanceJoint, float length);
        internal static void wrap_love_dll_type_DistanceJoint_setLength(IntPtr pDistanceJoint, float length)
        {
            _wrap_love_dll_type_DistanceJoint_setLength(pDistanceJoint, length);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_DistanceJoint_getLength")]
        internal extern static void _wrap_love_dll_type_DistanceJoint_getLength(IntPtr pDistanceJoint, out float length);
        internal static void wrap_love_dll_type_DistanceJoint_getLength(IntPtr pDistanceJoint, out float length)
        {
            _wrap_love_dll_type_DistanceJoint_getLength(pDistanceJoint, out length);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_DistanceJoint_setFrequency")]
        internal extern static void _wrap_love_dll_type_DistanceJoint_setFrequency(IntPtr pDistanceJoint, float frequency);
        internal static void wrap_love_dll_type_DistanceJoint_setFrequency(IntPtr pDistanceJoint, float frequency)
        {
            _wrap_love_dll_type_DistanceJoint_setFrequency(pDistanceJoint, frequency);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_DistanceJoint_getFrequency")]
        internal extern static void _wrap_love_dll_type_DistanceJoint_getFrequency(IntPtr pDistanceJoint, out float frequency);
        internal static void wrap_love_dll_type_DistanceJoint_getFrequency(IntPtr pDistanceJoint, out float frequency)
        {
            _wrap_love_dll_type_DistanceJoint_getFrequency(pDistanceJoint, out frequency);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_DistanceJoint_setDampingRatio")]
        internal extern static void _wrap_love_dll_type_DistanceJoint_setDampingRatio(IntPtr pDistanceJoint, float dampingRatio);
        internal static void wrap_love_dll_type_DistanceJoint_setDampingRatio(IntPtr pDistanceJoint, float dampingRatio)
        {
            _wrap_love_dll_type_DistanceJoint_setDampingRatio(pDistanceJoint, dampingRatio);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_DistanceJoint_getDampingRatio")]
        internal extern static void _wrap_love_dll_type_DistanceJoint_getDampingRatio(IntPtr pDistanceJoint, out float dampingRatio);
        internal static void wrap_love_dll_type_DistanceJoint_getDampingRatio(IntPtr pDistanceJoint, out float dampingRatio)
        {
            _wrap_love_dll_type_DistanceJoint_getDampingRatio(pDistanceJoint, out dampingRatio);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getType")]
        internal extern static void _wrap_love_dll_type_Fixture_getType(IntPtr pFixture, out int fixture_type);
        internal static void wrap_love_dll_type_Fixture_getType(IntPtr pFixture, out int fixture_type)
        {
            _wrap_love_dll_type_Fixture_getType(pFixture, out fixture_type);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_setFriction")]
        internal extern static void _wrap_love_dll_type_Fixture_setFriction(IntPtr pFixture, float friction);
        internal static void wrap_love_dll_type_Fixture_setFriction(IntPtr pFixture, float friction)
        {
            _wrap_love_dll_type_Fixture_setFriction(pFixture, friction);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_setRestitution")]
        internal extern static void _wrap_love_dll_type_Fixture_setRestitution(IntPtr pFixture, float restitution);
        internal static void wrap_love_dll_type_Fixture_setRestitution(IntPtr pFixture, float restitution)
        {
            _wrap_love_dll_type_Fixture_setRestitution(pFixture, restitution);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_setDensity")]
        internal extern static bool _wrap_love_dll_type_Fixture_setDensity(IntPtr pFixture, float density);
        internal static bool wrap_love_dll_type_Fixture_setDensity(IntPtr pFixture, float density)
        {
            return CheckCAPIException(_wrap_love_dll_type_Fixture_setDensity(pFixture, density));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_setSensor")]
        internal extern static void _wrap_love_dll_type_Fixture_setSensor(IntPtr pFixture, bool sensor);
        internal static void wrap_love_dll_type_Fixture_setSensor(IntPtr pFixture, bool sensor)
        {
            _wrap_love_dll_type_Fixture_setSensor(pFixture, sensor);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getFriction")]
        internal extern static void _wrap_love_dll_type_Fixture_getFriction(IntPtr pFixture, out float result);
        internal static void wrap_love_dll_type_Fixture_getFriction(IntPtr pFixture, out float result)
        {
            _wrap_love_dll_type_Fixture_getFriction(pFixture, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getRestitution")]
        internal extern static void _wrap_love_dll_type_Fixture_getRestitution(IntPtr pFixture, out float result);
        internal static void wrap_love_dll_type_Fixture_getRestitution(IntPtr pFixture, out float result)
        {
            _wrap_love_dll_type_Fixture_getRestitution(pFixture, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getDensity")]
        internal extern static void _wrap_love_dll_type_Fixture_getDensity(IntPtr pFixture, out float result);
        internal static void wrap_love_dll_type_Fixture_getDensity(IntPtr pFixture, out float result)
        {
            _wrap_love_dll_type_Fixture_getDensity(pFixture, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_isSensor")]
        internal extern static void _wrap_love_dll_type_Fixture_isSensor(IntPtr pFixture, out bool result);
        internal static void wrap_love_dll_type_Fixture_isSensor(IntPtr pFixture, out bool result)
        {
            _wrap_love_dll_type_Fixture_isSensor(pFixture, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getBody")]
        internal extern static void _wrap_love_dll_type_Fixture_getBody(IntPtr pFixture, out IntPtr body);
        internal static void wrap_love_dll_type_Fixture_getBody(IntPtr pFixture, out IntPtr body)
        {
            _wrap_love_dll_type_Fixture_getBody(pFixture, out body);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getShape")]
        internal extern static void _wrap_love_dll_type_Fixture_getShape(IntPtr pFixture, out IntPtr shape);
        internal static void wrap_love_dll_type_Fixture_getShape(IntPtr pFixture, out IntPtr shape)
        {
            _wrap_love_dll_type_Fixture_getShape(pFixture, out shape);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_testPoint")]
        internal extern static void _wrap_love_dll_type_Fixture_testPoint(IntPtr pFixture, float x, float y, out bool result);
        internal static void wrap_love_dll_type_Fixture_testPoint(IntPtr pFixture, float x, float y, out bool result)
        {
            _wrap_love_dll_type_Fixture_testPoint(pFixture, x, y, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_setFilterData")]
        internal extern static void _wrap_love_dll_type_Fixture_setFilterData(IntPtr pFixture, float categories, float mask, float group);
        internal static void wrap_love_dll_type_Fixture_setFilterData(IntPtr pFixture, float categories, float mask, float group)
        {
            _wrap_love_dll_type_Fixture_setFilterData(pFixture, categories, mask, group);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getFilterData")]
        internal extern static void _wrap_love_dll_type_Fixture_getFilterData(IntPtr pFixture, out float categories, out float mask, out float group);
        internal static void wrap_love_dll_type_Fixture_getFilterData(IntPtr pFixture, out float categories, out float mask, out float group)
        {
            _wrap_love_dll_type_Fixture_getFilterData(pFixture, out categories, out mask, out group);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getGroupIndex")]
        internal extern static void _wrap_love_dll_type_Fixture_getGroupIndex(IntPtr pFixture, out int index);
        internal static void wrap_love_dll_type_Fixture_getGroupIndex(IntPtr pFixture, out int index)
        {
            _wrap_love_dll_type_Fixture_getGroupIndex(pFixture, out index);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_setGroupIndex")]
        internal extern static void _wrap_love_dll_type_Fixture_setGroupIndex(IntPtr pFixture, int index);
        internal static void wrap_love_dll_type_Fixture_setGroupIndex(IntPtr pFixture, int index)
        {
            _wrap_love_dll_type_Fixture_setGroupIndex(pFixture, index);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_destroy")]
        internal extern static bool _wrap_love_dll_type_Fixture_destroy(IntPtr pFixture);
        internal static bool wrap_love_dll_type_Fixture_destroy(IntPtr pFixture)
        {
            return CheckCAPIException(_wrap_love_dll_type_Fixture_destroy(pFixture));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_isDestroyed")]
        internal extern static void _wrap_love_dll_type_Fixture_isDestroyed(IntPtr pFixture, out bool result);
        internal static void wrap_love_dll_type_Fixture_isDestroyed(IntPtr pFixture, out bool result)
        {
            _wrap_love_dll_type_Fixture_isDestroyed(pFixture, out result);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_rayCast")]
        internal extern static bool _wrap_love_dll_type_Fixture_rayCast(IntPtr pFixture, float x1, float y1, float x2, float y2, float maxFraction, int childIndex, out bool out_hasHit, out Vector2 pos, out float fraction);
        internal static bool wrap_love_dll_type_Fixture_rayCast(IntPtr pFixture, float x1, float y1, float x2, float y2, float maxFraction, int childIndex, out bool out_hasHit, out Vector2 pos, out float fraction)
        {
            return CheckCAPIException(_wrap_love_dll_type_Fixture_rayCast(pFixture, x1, y1, x2, y2, maxFraction, childIndex, out out_hasHit, out pos, out fraction));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_setCategory")]
        internal extern static bool _wrap_love_dll_type_Fixture_setCategory(IntPtr pFixture, UInt16 categories);
        internal static bool wrap_love_dll_type_Fixture_setCategory(IntPtr pFixture, UInt16 categories)
        {
            return CheckCAPIException(_wrap_love_dll_type_Fixture_setCategory(pFixture, categories));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getCategory")]
        internal extern static bool _wrap_love_dll_type_Fixture_getCategory(IntPtr pFixture, out UInt16 categories);
        internal static bool wrap_love_dll_type_Fixture_getCategory(IntPtr pFixture, out UInt16 categories)
        {
            return CheckCAPIException(_wrap_love_dll_type_Fixture_getCategory(pFixture, out categories));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_setMask")]
        internal extern static bool _wrap_love_dll_type_Fixture_setMask(IntPtr pFixture, UInt16 masks);
        internal static bool wrap_love_dll_type_Fixture_setMask(IntPtr pFixture, UInt16 masks)
        {
            return CheckCAPIException(_wrap_love_dll_type_Fixture_setMask(pFixture, masks));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getMask")]
        internal extern static bool _wrap_love_dll_type_Fixture_getMask(IntPtr pFixture, out UInt16 mask);
        internal static bool wrap_love_dll_type_Fixture_getMask(IntPtr pFixture, out UInt16 mask)
        {
            return CheckCAPIException(_wrap_love_dll_type_Fixture_getMask(pFixture, out mask));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getBoundingBox")]
        internal extern static bool _wrap_love_dll_type_Fixture_getBoundingBox(IntPtr pFixture, int childIndex, out float lx, out float ly, out float ux, out float uy);
        internal static bool wrap_love_dll_type_Fixture_getBoundingBox(IntPtr pFixture, int childIndex, out float lx, out float ly, out float ux, out float uy)
        {
            return CheckCAPIException(_wrap_love_dll_type_Fixture_getBoundingBox(pFixture, childIndex, out lx, out ly, out ux, out uy));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Fixture_getMassData")]
        internal extern static bool _wrap_love_dll_type_Fixture_getMassData(IntPtr pFixture, out Vector2 center, out float mass, out float rotationalInertia);
        internal static bool wrap_love_dll_type_Fixture_getMassData(IntPtr pFixture, out Vector2 center, out float mass, out float rotationalInertia)
        {
            return CheckCAPIException(_wrap_love_dll_type_Fixture_getMassData(pFixture, out center, out mass, out rotationalInertia));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_FrictionJoint_setMaxForce")]
        internal extern static bool _wrap_love_dll_type_FrictionJoint_setMaxForce(IntPtr pFrictionJoint, float maxForce);
        internal static bool wrap_love_dll_type_FrictionJoint_setMaxForce(IntPtr pFrictionJoint, float maxForce)
        {
            return CheckCAPIException(_wrap_love_dll_type_FrictionJoint_setMaxForce(pFrictionJoint, maxForce));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_FrictionJoint_getMaxForce")]
        internal extern static void _wrap_love_dll_type_FrictionJoint_getMaxForce(IntPtr pFrictionJoint, out float maxForce);
        internal static void wrap_love_dll_type_FrictionJoint_getMaxForce(IntPtr pFrictionJoint, out float maxForce)
        {
            _wrap_love_dll_type_FrictionJoint_getMaxForce(pFrictionJoint, out maxForce);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_FrictionJoint_setMaxTorque")]
        internal extern static bool _wrap_love_dll_type_FrictionJoint_setMaxTorque(IntPtr pFrictionJoint, float maxTorque);
        internal static bool wrap_love_dll_type_FrictionJoint_setMaxTorque(IntPtr pFrictionJoint, float maxTorque)
        {
            return CheckCAPIException(_wrap_love_dll_type_FrictionJoint_setMaxTorque(pFrictionJoint, maxTorque));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_FrictionJoint_getMaxTorque")]
        internal extern static void _wrap_love_dll_type_FrictionJoint_getMaxTorque(IntPtr pFrictionJoint, out float maxTorque);
        internal static void wrap_love_dll_type_FrictionJoint_getMaxTorque(IntPtr pFrictionJoint, out float maxTorque)
        {
            _wrap_love_dll_type_FrictionJoint_getMaxTorque(pFrictionJoint, out maxTorque);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GearJoint_setRatio")]
        internal extern static bool _wrap_love_dll_type_GearJoint_setRatio(IntPtr pGearJoint, float ration);
        internal static bool wrap_love_dll_type_GearJoint_setRatio(IntPtr pGearJoint, float ration)
        {
            return CheckCAPIException(_wrap_love_dll_type_GearJoint_setRatio(pGearJoint, ration));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GearJoint_getRatio")]
        internal extern static void _wrap_love_dll_type_GearJoint_getRatio(IntPtr pGearJoint, out float ration);
        internal static void wrap_love_dll_type_GearJoint_getRatio(IntPtr pGearJoint, out float ration)
        {
            _wrap_love_dll_type_GearJoint_getRatio(pGearJoint, out ration);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GearJoint_getJoints")]
        internal extern static bool _wrap_love_dll_type_GearJoint_getJoints(IntPtr pGearJoint, out IntPtr j1, out IntPtr j2);
        internal static bool wrap_love_dll_type_GearJoint_getJoints(IntPtr pGearJoint, out IntPtr j1, out IntPtr j2)
        {
            return CheckCAPIException(_wrap_love_dll_type_GearJoint_getJoints(pGearJoint, out j1, out j2));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joint_getType")]
        internal extern static void _wrap_love_dll_type_Joint_getType(IntPtr pJoint, out int type);
        internal static void wrap_love_dll_type_Joint_getType(IntPtr pJoint, out int type)
        {
            _wrap_love_dll_type_Joint_getType(pJoint, out type);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joint_getBodies")]
        internal extern static bool _wrap_love_dll_type_Joint_getBodies(IntPtr pJoint, out IntPtr b1, out IntPtr b2);
        internal static bool wrap_love_dll_type_Joint_getBodies(IntPtr pJoint, out IntPtr b1, out IntPtr b2)
        {
            return CheckCAPIException(_wrap_love_dll_type_Joint_getBodies(pJoint, out b1, out b2));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joint_getReactionTorque")]
        internal extern static void _wrap_love_dll_type_Joint_getReactionTorque(IntPtr pJoint, float inv_dt, out float torque);
        internal static void wrap_love_dll_type_Joint_getReactionTorque(IntPtr pJoint, float inv_dt, out float torque)
        {
            _wrap_love_dll_type_Joint_getReactionTorque(pJoint, inv_dt, out torque);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joint_getCollideConnected")]
        internal extern static void _wrap_love_dll_type_Joint_getCollideConnected(IntPtr pJoint, out bool c);
        internal static void wrap_love_dll_type_Joint_getCollideConnected(IntPtr pJoint, out bool c)
        {
            _wrap_love_dll_type_Joint_getCollideConnected(pJoint, out c);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joint_destroy")]
        internal extern static bool _wrap_love_dll_type_Joint_destroy(IntPtr pJoint);
        internal static bool wrap_love_dll_type_Joint_destroy(IntPtr pJoint)
        {
            return CheckCAPIException(_wrap_love_dll_type_Joint_destroy(pJoint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joint_isDestroyed")]
        internal extern static void _wrap_love_dll_type_Joint_isDestroyed(IntPtr pJoint, out bool destroyed);
        internal static void wrap_love_dll_type_Joint_isDestroyed(IntPtr pJoint, out bool destroyed)
        {
            _wrap_love_dll_type_Joint_isDestroyed(pJoint, out destroyed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joint_getAnchors")]
        internal extern static void _wrap_love_dll_type_Joint_getAnchors(IntPtr pJoint, out float x1, out float y1, out float x2, out float y2);
        internal static void wrap_love_dll_type_Joint_getAnchors(IntPtr pJoint, out float x1, out float y1, out float x2, out float y2)
        {
            _wrap_love_dll_type_Joint_getAnchors(pJoint, out x1, out y1, out x2, out y2);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joint_getReactionForce")]
        internal extern static void _wrap_love_dll_type_Joint_getReactionForce(IntPtr pJoint, float dt, out float x, out float y);
        internal static void wrap_love_dll_type_Joint_getReactionForce(IntPtr pJoint, float dt, out float x, out float y)
        {
            _wrap_love_dll_type_Joint_getReactionForce(pJoint, dt, out x, out y);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MotorJoint_setLinearOffset")]
        internal extern static void _wrap_love_dll_type_MotorJoint_setLinearOffset(IntPtr pMotorJoint, float x, float y);
        internal static void wrap_love_dll_type_MotorJoint_setLinearOffset(IntPtr pMotorJoint, float x, float y)
        {
            _wrap_love_dll_type_MotorJoint_setLinearOffset(pMotorJoint, x, y);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MotorJoint_setAngularOffset")]
        internal extern static void _wrap_love_dll_type_MotorJoint_setAngularOffset(IntPtr pMotorJoint, float angularOffset);
        internal static void wrap_love_dll_type_MotorJoint_setAngularOffset(IntPtr pMotorJoint, float angularOffset)
        {
            _wrap_love_dll_type_MotorJoint_setAngularOffset(pMotorJoint, angularOffset);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MotorJoint_getAngularOffset")]
        internal extern static void _wrap_love_dll_type_MotorJoint_getAngularOffset(IntPtr pMotorJoint, out float angularOffset);
        internal static void wrap_love_dll_type_MotorJoint_getAngularOffset(IntPtr pMotorJoint, out float angularOffset)
        {
            _wrap_love_dll_type_MotorJoint_getAngularOffset(pMotorJoint, out angularOffset);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MotorJoint_setMaxForce")]
        internal extern static bool _wrap_love_dll_type_MotorJoint_setMaxForce(IntPtr pMotorJoint, float maxForce);
        internal static bool wrap_love_dll_type_MotorJoint_setMaxForce(IntPtr pMotorJoint, float maxForce)
        {
            return CheckCAPIException(_wrap_love_dll_type_MotorJoint_setMaxForce(pMotorJoint, maxForce));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MotorJoint_getMaxForce")]
        internal extern static void _wrap_love_dll_type_MotorJoint_getMaxForce(IntPtr pMotorJoint, out float maxForce);
        internal static void wrap_love_dll_type_MotorJoint_getMaxForce(IntPtr pMotorJoint, out float maxForce)
        {
            _wrap_love_dll_type_MotorJoint_getMaxForce(pMotorJoint, out maxForce);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MotorJoint_setMaxTorque")]
        internal extern static bool _wrap_love_dll_type_MotorJoint_setMaxTorque(IntPtr pMotorJoint, float torque);
        internal static bool wrap_love_dll_type_MotorJoint_setMaxTorque(IntPtr pMotorJoint, float torque)
        {
            return CheckCAPIException(_wrap_love_dll_type_MotorJoint_setMaxTorque(pMotorJoint, torque));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MotorJoint_getMaxTorque")]
        internal extern static void _wrap_love_dll_type_MotorJoint_getMaxTorque(IntPtr pMotorJoint, out float torque);
        internal static void wrap_love_dll_type_MotorJoint_getMaxTorque(IntPtr pMotorJoint, out float torque)
        {
            _wrap_love_dll_type_MotorJoint_getMaxTorque(pMotorJoint, out torque);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MotorJoint_setCorrectionFactor")]
        internal extern static bool _wrap_love_dll_type_MotorJoint_setCorrectionFactor(IntPtr pMotorJoint, float factor);
        internal static bool wrap_love_dll_type_MotorJoint_setCorrectionFactor(IntPtr pMotorJoint, float factor)
        {
            return CheckCAPIException(_wrap_love_dll_type_MotorJoint_setCorrectionFactor(pMotorJoint, factor));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MotorJoint_getCorrectionFactor")]
        internal extern static void _wrap_love_dll_type_MotorJoint_getCorrectionFactor(IntPtr pMotorJoint, out float factor);
        internal static void wrap_love_dll_type_MotorJoint_getCorrectionFactor(IntPtr pMotorJoint, out float factor)
        {
            _wrap_love_dll_type_MotorJoint_getCorrectionFactor(pMotorJoint, out factor);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MotorJoint_getLinearOffset")]
        internal extern static void _wrap_love_dll_type_MotorJoint_getLinearOffset(IntPtr pMotorJoint, out float x, out float y);
        internal static void wrap_love_dll_type_MotorJoint_getLinearOffset(IntPtr pMotorJoint, out float x, out float y)
        {
            _wrap_love_dll_type_MotorJoint_getLinearOffset(pMotorJoint, out x, out y);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MouseJoint_setTarget")]
        internal extern static void _wrap_love_dll_type_MouseJoint_setTarget(IntPtr pMouseJoint, float x, float y);
        internal static void wrap_love_dll_type_MouseJoint_setTarget(IntPtr pMouseJoint, float x, float y)
        {
            _wrap_love_dll_type_MouseJoint_setTarget(pMouseJoint, x, y);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MouseJoint_setMaxForce")]
        internal extern static void _wrap_love_dll_type_MouseJoint_setMaxForce(IntPtr pMouseJoint, float force);
        internal static void wrap_love_dll_type_MouseJoint_setMaxForce(IntPtr pMouseJoint, float force)
        {
            _wrap_love_dll_type_MouseJoint_setMaxForce(pMouseJoint, force);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MouseJoint_getMaxForce")]
        internal extern static void _wrap_love_dll_type_MouseJoint_getMaxForce(IntPtr pMouseJoint, out float force);
        internal static void wrap_love_dll_type_MouseJoint_getMaxForce(IntPtr pMouseJoint, out float force)
        {
            _wrap_love_dll_type_MouseJoint_getMaxForce(pMouseJoint, out force);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MouseJoint_setFrequency")]
        internal extern static bool _wrap_love_dll_type_MouseJoint_setFrequency(IntPtr pMouseJoint, float frequency);
        internal static bool wrap_love_dll_type_MouseJoint_setFrequency(IntPtr pMouseJoint, float frequency)
        {
            return CheckCAPIException(_wrap_love_dll_type_MouseJoint_setFrequency(pMouseJoint, frequency));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MouseJoint_getFrequency")]
        internal extern static void _wrap_love_dll_type_MouseJoint_getFrequency(IntPtr pMouseJoint, out float frequency);
        internal static void wrap_love_dll_type_MouseJoint_getFrequency(IntPtr pMouseJoint, out float frequency)
        {
            _wrap_love_dll_type_MouseJoint_getFrequency(pMouseJoint, out frequency);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MouseJoint_setDampingRatio")]
        internal extern static void _wrap_love_dll_type_MouseJoint_setDampingRatio(IntPtr pMouseJoint, float ratio);
        internal static void wrap_love_dll_type_MouseJoint_setDampingRatio(IntPtr pMouseJoint, float ratio)
        {
            _wrap_love_dll_type_MouseJoint_setDampingRatio(pMouseJoint, ratio);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MouseJoint_getDampingRatio")]
        internal extern static void _wrap_love_dll_type_MouseJoint_getDampingRatio(IntPtr pMouseJoint, out float ratio);
        internal static void wrap_love_dll_type_MouseJoint_getDampingRatio(IntPtr pMouseJoint, out float ratio)
        {
            _wrap_love_dll_type_MouseJoint_getDampingRatio(pMouseJoint, out ratio);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_MouseJoint_getTarget")]
        internal extern static void _wrap_love_dll_type_MouseJoint_getTarget(IntPtr pMouseJoint, out float x, out float y);
        internal static void wrap_love_dll_type_MouseJoint_getTarget(IntPtr pMouseJoint, out float x, out float y)
        {
            _wrap_love_dll_type_MouseJoint_getTarget(pMouseJoint, out x, out y);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newWorld")]
        internal extern static bool _wrap_love_dll_physics_newWorld(float gx, float gy, bool sleep, out IntPtr world);
        internal static bool wrap_love_dll_physics_newWorld(float gx, float gy, bool sleep, out IntPtr world)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newWorld(gx, gy, sleep, out world));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newBody")]
        internal extern static bool _wrap_love_dll_physics_newBody(IntPtr world, float x, float y, int type_bodyType, out IntPtr body);
        internal static bool wrap_love_dll_physics_newBody(IntPtr world, float x, float y, int type_bodyType, out IntPtr body)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newBody(world, x, y, type_bodyType, out body));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newFixture")]
        internal extern static bool _wrap_love_dll_physics_newFixture(IntPtr body, IntPtr shape, float density, out IntPtr fixture);
        internal static bool wrap_love_dll_physics_newFixture(IntPtr body, IntPtr shape, float density, out IntPtr fixture)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newFixture(body, shape, density, out fixture));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newCircleShape")]
        internal extern static bool _wrap_love_dll_physics_newCircleShape(float x, float y, float radius, out IntPtr shape);
        internal static bool wrap_love_dll_physics_newCircleShape(float x, float y, float radius, out IntPtr shape)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newCircleShape(x, y, radius, out shape));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newRectangleShape")]
        internal extern static bool _wrap_love_dll_physics_newRectangleShape(float x, float y, float w, float h, float angle, out IntPtr shape);
        internal static bool wrap_love_dll_physics_newRectangleShape(float x, float y, float w, float h, float angle, out IntPtr shape)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newRectangleShape(x, y, w, h, angle, out shape));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newEdgeShape")]
        internal extern static bool _wrap_love_dll_physics_newEdgeShape(float x1, float y1, float x2, float y2, out IntPtr shape);
        internal static bool wrap_love_dll_physics_newEdgeShape(float x1, float y1, float x2, float y2, out IntPtr shape)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newEdgeShape(x1, y1, x2, y2, out shape));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newDistanceJoint")]
        internal extern static bool _wrap_love_dll_physics_newDistanceJoint(IntPtr body1, IntPtr body2, float x1, float y1, float x2, float y2, bool collideConnected, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newDistanceJoint(IntPtr body1, IntPtr body2, float x1, float y1, float x2, float y2, bool collideConnected, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newDistanceJoint(body1, body2, x1, y1, x2, y2, collideConnected, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newMouseJoint")]
        internal extern static bool _wrap_love_dll_physics_newMouseJoint(IntPtr body, float x, float y, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newMouseJoint(IntPtr body, float x, float y, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newMouseJoint(body, x, y, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newRevoluteJoint")]
        internal extern static bool _wrap_love_dll_physics_newRevoluteJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, bool collideConnected, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newRevoluteJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, bool collideConnected, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newRevoluteJoint(body1, body2, pA, pB, collideConnected, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newRevoluteJoint_referenceAngle")]
        internal extern static bool _wrap_love_dll_physics_newRevoluteJoint_referenceAngle(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, bool collideConnected, float referenceAngle, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newRevoluteJoint_referenceAngle(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, bool collideConnected, float referenceAngle, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newRevoluteJoint_referenceAngle(body1, body2, pA, pB, collideConnected, referenceAngle, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newPrismaticJoint")]
        internal extern static bool _wrap_love_dll_physics_newPrismaticJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, Vector2 angle, bool collideConnected, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newPrismaticJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, Vector2 angle, bool collideConnected, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newPrismaticJoint(body1, body2, pA, pB, angle, collideConnected, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newPrismaticJoint_referenceAngle")]
        internal extern static bool _wrap_love_dll_physics_newPrismaticJoint_referenceAngle(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, Vector2 angle, bool collideConnected, float referenceAngle, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newPrismaticJoint_referenceAngle(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, Vector2 angle, bool collideConnected, float referenceAngle, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newPrismaticJoint_referenceAngle(body1, body2, pA, pB, angle, collideConnected, referenceAngle, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newPulleyJoint")]
        internal extern static bool _wrap_love_dll_physics_newPulleyJoint(IntPtr body1, IntPtr body2, Vector2 g1, Vector2 g2, Vector2 pA, Vector2 pB, float ratio, bool collideConnected, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newPulleyJoint(IntPtr body1, IntPtr body2, Vector2 g1, Vector2 g2, Vector2 pA, Vector2 pB, float ratio, bool collideConnected, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newPulleyJoint(body1, body2, g1, g2, pA, pB, ratio, collideConnected, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newGearJoint")]
        internal extern static bool _wrap_love_dll_physics_newGearJoint(IntPtr joint1, IntPtr joint2, float ratio, bool collideConnected, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newGearJoint(IntPtr joint1, IntPtr joint2, float ratio, bool collideConnected, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newGearJoint(joint1, joint2, ratio, collideConnected, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newFrictionJoint")]
        internal extern static bool _wrap_love_dll_physics_newFrictionJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, bool collideConnected, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newFrictionJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, bool collideConnected, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newFrictionJoint(body1, body2, pA, pB, collideConnected, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newWeldJoint")]
        internal extern static bool _wrap_love_dll_physics_newWeldJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, bool collideConnected, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newWeldJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, bool collideConnected, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newWeldJoint(body1, body2, pA, pB, collideConnected, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newWeldJoint_referenceAngle")]
        internal extern static bool _wrap_love_dll_physics_newWeldJoint_referenceAngle(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, bool collideConnected, float referenceAngle, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newWeldJoint_referenceAngle(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, bool collideConnected, float referenceAngle, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newWeldJoint_referenceAngle(body1, body2, pA, pB, collideConnected, referenceAngle, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newWheelJoint")]
        internal extern static bool _wrap_love_dll_physics_newWheelJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, Vector2 angle, bool collideConnected, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newWheelJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, Vector2 angle, bool collideConnected, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newWheelJoint(body1, body2, pA, pB, angle, collideConnected, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newRopeJoint")]
        internal extern static bool _wrap_love_dll_physics_newRopeJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, float maxLength, bool collideConnected, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newRopeJoint(IntPtr body1, IntPtr body2, Vector2 pA, Vector2 pB, float maxLength, bool collideConnected, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newRopeJoint(body1, body2, pA, pB, maxLength, collideConnected, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newMotorJoint")]
        internal extern static bool _wrap_love_dll_physics_newMotorJoint(IntPtr body1, IntPtr body2, float correctionFactor, bool collideConnected, out IntPtr joint);
        internal static bool wrap_love_dll_physics_newMotorJoint(IntPtr body1, IntPtr body2, float correctionFactor, bool collideConnected, out IntPtr joint)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newMotorJoint(body1, body2, correctionFactor, collideConnected, out joint));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newPolygonShape")]
        internal extern static bool _wrap_love_dll_physics_newPolygonShape(Vector2[] pointList, int pointListLength, out IntPtr shape);
        internal static bool wrap_love_dll_physics_newPolygonShape(Vector2[] pointList, int pointListLength, out IntPtr shape)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newPolygonShape(pointList, pointListLength, out shape));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_newChainShape")]
        internal extern static bool _wrap_love_dll_physics_newChainShape(bool loop, Vector2[] pointList, int pointListLength, out IntPtr shape);
        internal static bool wrap_love_dll_physics_newChainShape(bool loop, Vector2[] pointList, int pointListLength, out IntPtr shape)
        {
            return CheckCAPIException(_wrap_love_dll_physics_newChainShape(loop, pointList, pointListLength, out shape));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_open_love_physics")]
        internal extern static bool _wrap_love_dll_physics_open_love_physics();
        internal static bool wrap_love_dll_physics_open_love_physics()
        {
            return CheckCAPIException(_wrap_love_dll_physics_open_love_physics());
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_setMeter")]
        internal extern static bool _wrap_love_dll_physics_setMeter(float meter);
        internal static bool wrap_love_dll_physics_setMeter(float meter)
        {
            return CheckCAPIException(_wrap_love_dll_physics_setMeter(meter));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_getMeter")]
        internal extern static void _wrap_love_dll_physics_getMeter(out float meter);
        internal static void wrap_love_dll_physics_getMeter(out float meter)
        {
            _wrap_love_dll_physics_getMeter(out meter);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_physics_getDistance")]
        internal extern static bool _wrap_love_dll_physics_getDistance(IntPtr fixtureA, IntPtr fixtureB, out float out_distance, out Vector2 out_pa, out Vector2 out_pb);
        internal static bool wrap_love_dll_physics_getDistance(IntPtr fixtureA, IntPtr fixtureB, out float out_distance, out Vector2 out_pa, out Vector2 out_pb)
        {
            return CheckCAPIException(_wrap_love_dll_physics_getDistance(fixtureA, fixtureB, out out_distance, out out_pa, out out_pb));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_getJointTranslation")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_getJointTranslation(IntPtr pPrismaticJoint, out float translation);
        internal static void wrap_love_dll_type_PrismaticJoint_getJointTranslation(IntPtr pPrismaticJoint, out float translation)
        {
            _wrap_love_dll_type_PrismaticJoint_getJointTranslation(pPrismaticJoint, out translation);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_getJointSpeed")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_getJointSpeed(IntPtr pPrismaticJoint, out float speed);
        internal static void wrap_love_dll_type_PrismaticJoint_getJointSpeed(IntPtr pPrismaticJoint, out float speed)
        {
            _wrap_love_dll_type_PrismaticJoint_getJointSpeed(pPrismaticJoint, out speed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_setMotorEnabled")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_setMotorEnabled(IntPtr pPrismaticJoint, bool ebabled);
        internal static void wrap_love_dll_type_PrismaticJoint_setMotorEnabled(IntPtr pPrismaticJoint, bool ebabled)
        {
            _wrap_love_dll_type_PrismaticJoint_setMotorEnabled(pPrismaticJoint, ebabled);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_isMotorEnabled")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_isMotorEnabled(IntPtr pPrismaticJoint, out bool enabled);
        internal static void wrap_love_dll_type_PrismaticJoint_isMotorEnabled(IntPtr pPrismaticJoint, out bool enabled)
        {
            _wrap_love_dll_type_PrismaticJoint_isMotorEnabled(pPrismaticJoint, out enabled);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_setMaxMotorForce")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_setMaxMotorForce(IntPtr pPrismaticJoint, float force);
        internal static void wrap_love_dll_type_PrismaticJoint_setMaxMotorForce(IntPtr pPrismaticJoint, float force)
        {
            _wrap_love_dll_type_PrismaticJoint_setMaxMotorForce(pPrismaticJoint, force);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_setMotorSpeed")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_setMotorSpeed(IntPtr pPrismaticJoint, float speed);
        internal static void wrap_love_dll_type_PrismaticJoint_setMotorSpeed(IntPtr pPrismaticJoint, float speed)
        {
            _wrap_love_dll_type_PrismaticJoint_setMotorSpeed(pPrismaticJoint, speed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_getMotorSpeed")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_getMotorSpeed(IntPtr pPrismaticJoint, out float speed);
        internal static void wrap_love_dll_type_PrismaticJoint_getMotorSpeed(IntPtr pPrismaticJoint, out float speed)
        {
            _wrap_love_dll_type_PrismaticJoint_getMotorSpeed(pPrismaticJoint, out speed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_getMotorForce")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_getMotorForce(IntPtr pPrismaticJoint, float inv_dt, out float force);
        internal static void wrap_love_dll_type_PrismaticJoint_getMotorForce(IntPtr pPrismaticJoint, float inv_dt, out float force)
        {
            _wrap_love_dll_type_PrismaticJoint_getMotorForce(pPrismaticJoint, inv_dt, out force);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_getMaxMotorForce")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_getMaxMotorForce(IntPtr pPrismaticJoint, out float force);
        internal static void wrap_love_dll_type_PrismaticJoint_getMaxMotorForce(IntPtr pPrismaticJoint, out float force)
        {
            _wrap_love_dll_type_PrismaticJoint_getMaxMotorForce(pPrismaticJoint, out force);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_setLimitsEnabled")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_setLimitsEnabled(IntPtr pPrismaticJoint, bool enabled);
        internal static void wrap_love_dll_type_PrismaticJoint_setLimitsEnabled(IntPtr pPrismaticJoint, bool enabled)
        {
            _wrap_love_dll_type_PrismaticJoint_setLimitsEnabled(pPrismaticJoint, enabled);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_areLimitsEnabled")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_areLimitsEnabled(IntPtr pPrismaticJoint, out bool enabled);
        internal static void wrap_love_dll_type_PrismaticJoint_areLimitsEnabled(IntPtr pPrismaticJoint, out bool enabled)
        {
            _wrap_love_dll_type_PrismaticJoint_areLimitsEnabled(pPrismaticJoint, out enabled);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_setUpperLimit")]
        internal extern static bool _wrap_love_dll_type_PrismaticJoint_setUpperLimit(IntPtr pPrismaticJoint, float limit);
        internal static bool wrap_love_dll_type_PrismaticJoint_setUpperLimit(IntPtr pPrismaticJoint, float limit)
        {
            return CheckCAPIException(_wrap_love_dll_type_PrismaticJoint_setUpperLimit(pPrismaticJoint, limit));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_setLowerLimit")]
        internal extern static bool _wrap_love_dll_type_PrismaticJoint_setLowerLimit(IntPtr pPrismaticJoint, float limit);
        internal static bool wrap_love_dll_type_PrismaticJoint_setLowerLimit(IntPtr pPrismaticJoint, float limit)
        {
            return CheckCAPIException(_wrap_love_dll_type_PrismaticJoint_setLowerLimit(pPrismaticJoint, limit));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_setLimits")]
        internal extern static bool _wrap_love_dll_type_PrismaticJoint_setLimits(IntPtr pPrismaticJoint, float lowerLimit, float upperLimit);
        internal static bool wrap_love_dll_type_PrismaticJoint_setLimits(IntPtr pPrismaticJoint, float lowerLimit, float upperLimit)
        {
            return CheckCAPIException(_wrap_love_dll_type_PrismaticJoint_setLimits(pPrismaticJoint, lowerLimit, upperLimit));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_getLowerLimit")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_getLowerLimit(IntPtr pPrismaticJoint, out float limit);
        internal static void wrap_love_dll_type_PrismaticJoint_getLowerLimit(IntPtr pPrismaticJoint, out float limit)
        {
            _wrap_love_dll_type_PrismaticJoint_getLowerLimit(pPrismaticJoint, out limit);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_getUpperLimit")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_getUpperLimit(IntPtr pPrismaticJoint, out float limit);
        internal static void wrap_love_dll_type_PrismaticJoint_getUpperLimit(IntPtr pPrismaticJoint, out float limit)
        {
            _wrap_love_dll_type_PrismaticJoint_getUpperLimit(pPrismaticJoint, out limit);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_getReferenceAngle")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_getReferenceAngle(IntPtr pPrismaticJoint, out float angle);
        internal static void wrap_love_dll_type_PrismaticJoint_getReferenceAngle(IntPtr pPrismaticJoint, out float angle)
        {
            _wrap_love_dll_type_PrismaticJoint_getReferenceAngle(pPrismaticJoint, out angle);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_getLimits")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_getLimits(IntPtr pPrismaticJoint, out float lowerLimit, out float upperLimit);
        internal static void wrap_love_dll_type_PrismaticJoint_getLimits(IntPtr pPrismaticJoint, out float lowerLimit, out float upperLimit)
        {
            _wrap_love_dll_type_PrismaticJoint_getLimits(pPrismaticJoint, out lowerLimit, out upperLimit);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PrismaticJoint_getAxis")]
        internal extern static void _wrap_love_dll_type_PrismaticJoint_getAxis(IntPtr pPrismaticJoint, out float axisX, out float axisY);
        internal static void wrap_love_dll_type_PrismaticJoint_getAxis(IntPtr pPrismaticJoint, out float axisX, out float axisY)
        {
            _wrap_love_dll_type_PrismaticJoint_getAxis(pPrismaticJoint, out axisX, out axisY);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PulleyJoint_getLengthA")]
        internal extern static void _wrap_love_dll_type_PulleyJoint_getLengthA(IntPtr pPulleyJoint, out float lengthA);
        internal static void wrap_love_dll_type_PulleyJoint_getLengthA(IntPtr pPulleyJoint, out float lengthA)
        {
            _wrap_love_dll_type_PulleyJoint_getLengthA(pPulleyJoint, out lengthA);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PulleyJoint_getLengthB")]
        internal extern static void _wrap_love_dll_type_PulleyJoint_getLengthB(IntPtr pPulleyJoint, out float lengthB);
        internal static void wrap_love_dll_type_PulleyJoint_getLengthB(IntPtr pPulleyJoint, out float lengthB)
        {
            _wrap_love_dll_type_PulleyJoint_getLengthB(pPulleyJoint, out lengthB);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PulleyJoint_getRatio")]
        internal extern static void _wrap_love_dll_type_PulleyJoint_getRatio(IntPtr pPulleyJoint, out float ratio);
        internal static void wrap_love_dll_type_PulleyJoint_getRatio(IntPtr pPulleyJoint, out float ratio)
        {
            _wrap_love_dll_type_PulleyJoint_getRatio(pPulleyJoint, out ratio);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_PulleyJoint_getGroundAnchors")]
        internal extern static void _wrap_love_dll_type_PulleyJoint_getGroundAnchors(IntPtr pPulleyJoint, out float x1, out float y1, out float x2, out float y2);
        internal static void wrap_love_dll_type_PulleyJoint_getGroundAnchors(IntPtr pPulleyJoint, out float x1, out float y1, out float x2, out float y2)
        {
            _wrap_love_dll_type_PulleyJoint_getGroundAnchors(pPulleyJoint, out x1, out y1, out x2, out y2);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_getJointAngle")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_getJointAngle(IntPtr pRevoluteJoint, out float angle);
        internal static void wrap_love_dll_type_RevoluteJoint_getJointAngle(IntPtr pRevoluteJoint, out float angle)
        {
            _wrap_love_dll_type_RevoluteJoint_getJointAngle(pRevoluteJoint, out angle);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_getJointSpeed")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_getJointSpeed(IntPtr pRevoluteJoint, out float speed);
        internal static void wrap_love_dll_type_RevoluteJoint_getJointSpeed(IntPtr pRevoluteJoint, out float speed)
        {
            _wrap_love_dll_type_RevoluteJoint_getJointSpeed(pRevoluteJoint, out speed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_setMotorEnabled")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_setMotorEnabled(IntPtr pRevoluteJoint, bool enabled);
        internal static void wrap_love_dll_type_RevoluteJoint_setMotorEnabled(IntPtr pRevoluteJoint, bool enabled)
        {
            _wrap_love_dll_type_RevoluteJoint_setMotorEnabled(pRevoluteJoint, enabled);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_isMotorEnabled")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_isMotorEnabled(IntPtr pRevoluteJoint, out bool enabled);
        internal static void wrap_love_dll_type_RevoluteJoint_isMotorEnabled(IntPtr pRevoluteJoint, out bool enabled)
        {
            _wrap_love_dll_type_RevoluteJoint_isMotorEnabled(pRevoluteJoint, out enabled);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_setMaxMotorTorque")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_setMaxMotorTorque(IntPtr pRevoluteJoint, float torque);
        internal static void wrap_love_dll_type_RevoluteJoint_setMaxMotorTorque(IntPtr pRevoluteJoint, float torque)
        {
            _wrap_love_dll_type_RevoluteJoint_setMaxMotorTorque(pRevoluteJoint, torque);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_setMotorSpeed")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_setMotorSpeed(IntPtr pRevoluteJoint, float speed);
        internal static void wrap_love_dll_type_RevoluteJoint_setMotorSpeed(IntPtr pRevoluteJoint, float speed)
        {
            _wrap_love_dll_type_RevoluteJoint_setMotorSpeed(pRevoluteJoint, speed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_getMotorSpeed")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_getMotorSpeed(IntPtr pRevoluteJoint, out float speed);
        internal static void wrap_love_dll_type_RevoluteJoint_getMotorSpeed(IntPtr pRevoluteJoint, out float speed)
        {
            _wrap_love_dll_type_RevoluteJoint_getMotorSpeed(pRevoluteJoint, out speed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_getMotorTorque")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_getMotorTorque(IntPtr pRevoluteJoint, float inv_dt, out float torque);
        internal static void wrap_love_dll_type_RevoluteJoint_getMotorTorque(IntPtr pRevoluteJoint, float inv_dt, out float torque)
        {
            _wrap_love_dll_type_RevoluteJoint_getMotorTorque(pRevoluteJoint, inv_dt, out torque);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_getMaxMotorTorque")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_getMaxMotorTorque(IntPtr pRevoluteJoint, out float torque);
        internal static void wrap_love_dll_type_RevoluteJoint_getMaxMotorTorque(IntPtr pRevoluteJoint, out float torque)
        {
            _wrap_love_dll_type_RevoluteJoint_getMaxMotorTorque(pRevoluteJoint, out torque);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_setLimitsEnabled")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_setLimitsEnabled(IntPtr pRevoluteJoint, bool enabled);
        internal static void wrap_love_dll_type_RevoluteJoint_setLimitsEnabled(IntPtr pRevoluteJoint, bool enabled)
        {
            _wrap_love_dll_type_RevoluteJoint_setLimitsEnabled(pRevoluteJoint, enabled);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_areLimitsEnabled")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_areLimitsEnabled(IntPtr pRevoluteJoint, out bool enabled);
        internal static void wrap_love_dll_type_RevoluteJoint_areLimitsEnabled(IntPtr pRevoluteJoint, out bool enabled)
        {
            _wrap_love_dll_type_RevoluteJoint_areLimitsEnabled(pRevoluteJoint, out enabled);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_setUpperLimit")]
        internal extern static bool _wrap_love_dll_type_RevoluteJoint_setUpperLimit(IntPtr pRevoluteJoint, float limit);
        internal static bool wrap_love_dll_type_RevoluteJoint_setUpperLimit(IntPtr pRevoluteJoint, float limit)
        {
            return CheckCAPIException(_wrap_love_dll_type_RevoluteJoint_setUpperLimit(pRevoluteJoint, limit));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_setLowerLimit")]
        internal extern static bool _wrap_love_dll_type_RevoluteJoint_setLowerLimit(IntPtr pRevoluteJoint, float limit);
        internal static bool wrap_love_dll_type_RevoluteJoint_setLowerLimit(IntPtr pRevoluteJoint, float limit)
        {
            return CheckCAPIException(_wrap_love_dll_type_RevoluteJoint_setLowerLimit(pRevoluteJoint, limit));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_setLimits")]
        internal extern static bool _wrap_love_dll_type_RevoluteJoint_setLimits(IntPtr pRevoluteJoint, float lowerLimit, float upperLimit);
        internal static bool wrap_love_dll_type_RevoluteJoint_setLimits(IntPtr pRevoluteJoint, float lowerLimit, float upperLimit)
        {
            return CheckCAPIException(_wrap_love_dll_type_RevoluteJoint_setLimits(pRevoluteJoint, lowerLimit, upperLimit));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_getLowerLimit")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_getLowerLimit(IntPtr pRevoluteJoint, out float limit);
        internal static void wrap_love_dll_type_RevoluteJoint_getLowerLimit(IntPtr pRevoluteJoint, out float limit)
        {
            _wrap_love_dll_type_RevoluteJoint_getLowerLimit(pRevoluteJoint, out limit);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_getUpperLimit")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_getUpperLimit(IntPtr pRevoluteJoint, out float limit);
        internal static void wrap_love_dll_type_RevoluteJoint_getUpperLimit(IntPtr pRevoluteJoint, out float limit)
        {
            _wrap_love_dll_type_RevoluteJoint_getUpperLimit(pRevoluteJoint, out limit);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_getReferenceAngle")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_getReferenceAngle(IntPtr pRevoluteJoint, out float angle);
        internal static void wrap_love_dll_type_RevoluteJoint_getReferenceAngle(IntPtr pRevoluteJoint, out float angle)
        {
            _wrap_love_dll_type_RevoluteJoint_getReferenceAngle(pRevoluteJoint, out angle);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RevoluteJoint_getLimits")]
        internal extern static void _wrap_love_dll_type_RevoluteJoint_getLimits(IntPtr pRevoluteJoint, out float lowerLimit, out float upperLimit);
        internal static void wrap_love_dll_type_RevoluteJoint_getLimits(IntPtr pRevoluteJoint, out float lowerLimit, out float upperLimit)
        {
            _wrap_love_dll_type_RevoluteJoint_getLimits(pRevoluteJoint, out lowerLimit, out upperLimit);
        }
        //

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RopeJoint_getMaxLength")]
        internal extern static void _wrap_love_dll_type_RopeJoint_getMaxLength(IntPtr pWeldJoint, out float out_maxLength);
        internal static void wrap_love_dll_type_RopeJoint_getMaxLength(IntPtr pWeldJoint, out float out_maxLength)
        {
            _wrap_love_dll_type_RopeJoint_getMaxLength(pWeldJoint, out out_maxLength);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RopeJoint_setMaxLength")]
        internal extern static void _wrap_love_dll_type_RopeJoint_setMaxLength(IntPtr pWeldJoint, float maxLength);
        internal static void wrap_love_dll_type_RopeJoint_setMaxLength(IntPtr pWeldJoint, float maxLength)
        {
            _wrap_love_dll_type_RopeJoint_setMaxLength(pWeldJoint, maxLength);
        }
        //

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WeldJoint_setFrequency")]
        internal extern static void _wrap_love_dll_type_WeldJoint_setFrequency(IntPtr pWeldJoint, float frequency);
        internal static void wrap_love_dll_type_WeldJoint_setFrequency(IntPtr pWeldJoint, float frequency)
        {
            _wrap_love_dll_type_WeldJoint_setFrequency(pWeldJoint, frequency);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WeldJoint_getFrequency")]
        internal extern static void _wrap_love_dll_type_WeldJoint_getFrequency(IntPtr pWeldJoint, out float frequency);
        internal static void wrap_love_dll_type_WeldJoint_getFrequency(IntPtr pWeldJoint, out float frequency)
        {
            _wrap_love_dll_type_WeldJoint_getFrequency(pWeldJoint, out frequency);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WeldJoint_setDampingRatio")]
        internal extern static void _wrap_love_dll_type_WeldJoint_setDampingRatio(IntPtr pWeldJoint, float ratio);
        internal static void wrap_love_dll_type_WeldJoint_setDampingRatio(IntPtr pWeldJoint, float ratio)
        {
            _wrap_love_dll_type_WeldJoint_setDampingRatio(pWeldJoint, ratio);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WeldJoint_getDampingRatio")]
        internal extern static void _wrap_love_dll_type_WeldJoint_getDampingRatio(IntPtr pWeldJoint, out float ratio);
        internal static void wrap_love_dll_type_WeldJoint_getDampingRatio(IntPtr pWeldJoint, out float ratio)
        {
            _wrap_love_dll_type_WeldJoint_getDampingRatio(pWeldJoint, out ratio);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WeldJoint_getReferenceAngle")]
        internal extern static void _wrap_love_dll_type_WeldJoint_getReferenceAngle(IntPtr pWeldJoint, out float angle);
        internal static void wrap_love_dll_type_WeldJoint_getReferenceAngle(IntPtr pWeldJoint, out float angle)
        {
            _wrap_love_dll_type_WeldJoint_getReferenceAngle(pWeldJoint, out angle);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_getJointTranslation")]
        internal extern static void _wrap_love_dll_type_WheelJoint_getJointTranslation(IntPtr pWheelJoint, out float translation);
        internal static void wrap_love_dll_type_WheelJoint_getJointTranslation(IntPtr pWheelJoint, out float translation)
        {
            _wrap_love_dll_type_WheelJoint_getJointTranslation(pWheelJoint, out translation);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_getJointSpeed")]
        internal extern static void _wrap_love_dll_type_WheelJoint_getJointSpeed(IntPtr pWheelJoint, out float speed);
        internal static void wrap_love_dll_type_WheelJoint_getJointSpeed(IntPtr pWheelJoint, out float speed)
        {
            _wrap_love_dll_type_WheelJoint_getJointSpeed(pWheelJoint, out speed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_setMotorEnabled")]
        internal extern static void _wrap_love_dll_type_WheelJoint_setMotorEnabled(IntPtr pWheelJoint, bool enabled);
        internal static void wrap_love_dll_type_WheelJoint_setMotorEnabled(IntPtr pWheelJoint, bool enabled)
        {
            _wrap_love_dll_type_WheelJoint_setMotorEnabled(pWheelJoint, enabled);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_isMotorEnabled")]
        internal extern static void _wrap_love_dll_type_WheelJoint_isMotorEnabled(IntPtr pWheelJoint, out bool enabled);
        internal static void wrap_love_dll_type_WheelJoint_isMotorEnabled(IntPtr pWheelJoint, out bool enabled)
        {
            _wrap_love_dll_type_WheelJoint_isMotorEnabled(pWheelJoint, out enabled);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_setMotorSpeed")]
        internal extern static void _wrap_love_dll_type_WheelJoint_setMotorSpeed(IntPtr pWheelJoint, float speed);
        internal static void wrap_love_dll_type_WheelJoint_setMotorSpeed(IntPtr pWheelJoint, float speed)
        {
            _wrap_love_dll_type_WheelJoint_setMotorSpeed(pWheelJoint, speed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_getMotorSpeed")]
        internal extern static void _wrap_love_dll_type_WheelJoint_getMotorSpeed(IntPtr pWheelJoint, out float speed);
        internal static void wrap_love_dll_type_WheelJoint_getMotorSpeed(IntPtr pWheelJoint, out float speed)
        {
            _wrap_love_dll_type_WheelJoint_getMotorSpeed(pWheelJoint, out speed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_setMaxMotorTorque")]
        internal extern static void _wrap_love_dll_type_WheelJoint_setMaxMotorTorque(IntPtr pWheelJoint, float torque);
        internal static void wrap_love_dll_type_WheelJoint_setMaxMotorTorque(IntPtr pWheelJoint, float torque)
        {
            _wrap_love_dll_type_WheelJoint_setMaxMotorTorque(pWheelJoint, torque);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_getMaxMotorTorque")]
        internal extern static void _wrap_love_dll_type_WheelJoint_getMaxMotorTorque(IntPtr pWheelJoint, out float torque);
        internal static void wrap_love_dll_type_WheelJoint_getMaxMotorTorque(IntPtr pWheelJoint, out float torque)
        {
            _wrap_love_dll_type_WheelJoint_getMaxMotorTorque(pWheelJoint, out torque);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_getMotorTorque")]
        internal extern static void _wrap_love_dll_type_WheelJoint_getMotorTorque(IntPtr pWheelJoint, float inv_dt, out float torque);
        internal static void wrap_love_dll_type_WheelJoint_getMotorTorque(IntPtr pWheelJoint, float inv_dt, out float torque)
        {
            _wrap_love_dll_type_WheelJoint_getMotorTorque(pWheelJoint, inv_dt, out torque);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_setSpringFrequency")]
        internal extern static void _wrap_love_dll_type_WheelJoint_setSpringFrequency(IntPtr pWheelJoint, float frequency);
        internal static void wrap_love_dll_type_WheelJoint_setSpringFrequency(IntPtr pWheelJoint, float frequency)
        {
            _wrap_love_dll_type_WheelJoint_setSpringFrequency(pWheelJoint, frequency);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_getSpringFrequency")]
        internal extern static void _wrap_love_dll_type_WheelJoint_getSpringFrequency(IntPtr pWheelJoint, out float frequency);
        internal static void wrap_love_dll_type_WheelJoint_getSpringFrequency(IntPtr pWheelJoint, out float frequency)
        {
            _wrap_love_dll_type_WheelJoint_getSpringFrequency(pWheelJoint, out frequency);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_setSpringDampingRatio")]
        internal extern static void _wrap_love_dll_type_WheelJoint_setSpringDampingRatio(IntPtr pWheelJoint, float ratio);
        internal static void wrap_love_dll_type_WheelJoint_setSpringDampingRatio(IntPtr pWheelJoint, float ratio)
        {
            _wrap_love_dll_type_WheelJoint_setSpringDampingRatio(pWheelJoint, ratio);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_getSpringDampingRatio")]
        internal extern static void _wrap_love_dll_type_WheelJoint_getSpringDampingRatio(IntPtr pWheelJoint, out float ratio);
        internal static void wrap_love_dll_type_WheelJoint_getSpringDampingRatio(IntPtr pWheelJoint, out float ratio)
        {
            _wrap_love_dll_type_WheelJoint_getSpringDampingRatio(pWheelJoint, out ratio);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_WheelJoint_getAxis")]
        internal extern static void _wrap_love_dll_type_WheelJoint_getAxis(IntPtr pWheelJoint, out float axisX, out float axisY);
        internal static void wrap_love_dll_type_WheelJoint_getAxis(IntPtr pWheelJoint, out float axisX, out float axisY)
        {
            _wrap_love_dll_type_WheelJoint_getAxis(pWheelJoint, out axisX, out axisY);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_setGravity")]
        internal extern static void _wrap_love_dll_type_World_setGravity(IntPtr pWorld, float gx, float gy);
        internal static void wrap_love_dll_type_World_setGravity(IntPtr pWorld, float gx, float gy)
        {
            _wrap_love_dll_type_World_setGravity(pWorld, gx, gy);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_translateOrigin")]
        internal extern static bool _wrap_love_dll_type_World_translateOrigin(IntPtr pWorld, float x, float y);
        internal static bool wrap_love_dll_type_World_translateOrigin(IntPtr pWorld, float x, float y)
        {
            return CheckCAPIException(_wrap_love_dll_type_World_translateOrigin(pWorld, x, y));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_setSleepingAllowed")]
        internal extern static void _wrap_love_dll_type_World_setSleepingAllowed(IntPtr pWorld, bool allowed);
        internal static void wrap_love_dll_type_World_setSleepingAllowed(IntPtr pWorld, bool allowed)
        {
            _wrap_love_dll_type_World_setSleepingAllowed(pWorld, allowed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_isSleepingAllowed")]
        internal extern static void _wrap_love_dll_type_World_isSleepingAllowed(IntPtr pWorld, out bool allowed);
        internal static void wrap_love_dll_type_World_isSleepingAllowed(IntPtr pWorld, out bool allowed)
        {
            _wrap_love_dll_type_World_isSleepingAllowed(pWorld, out allowed);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_isLocked")]
        internal extern static void _wrap_love_dll_type_World_isLocked(IntPtr pWorld, out bool locked);
        internal static void wrap_love_dll_type_World_isLocked(IntPtr pWorld, out bool locked)
        {
            _wrap_love_dll_type_World_isLocked(pWorld, out locked);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_getBodyCount")]
        internal extern static void _wrap_love_dll_type_World_getBodyCount(IntPtr pWorld, out int count);
        internal static void wrap_love_dll_type_World_getBodyCount(IntPtr pWorld, out int count)
        {
            _wrap_love_dll_type_World_getBodyCount(pWorld, out count);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_getJointCount")]
        internal extern static void _wrap_love_dll_type_World_getJointCount(IntPtr pWorld, out int count);
        internal static void wrap_love_dll_type_World_getJointCount(IntPtr pWorld, out int count)
        {
            _wrap_love_dll_type_World_getJointCount(pWorld, out count);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_getContactCount")]
        internal extern static void _wrap_love_dll_type_World_getContactCount(IntPtr pWorld, out int count);
        internal static void wrap_love_dll_type_World_getContactCount(IntPtr pWorld, out int count)
        {
            _wrap_love_dll_type_World_getContactCount(pWorld, out count);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_destroy")]
        internal extern static bool _wrap_love_dll_type_World_destroy(IntPtr pWorld);
        internal static bool wrap_love_dll_type_World_destroy(IntPtr pWorld)
        {
            return CheckCAPIException(_wrap_love_dll_type_World_destroy(pWorld));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_isDestroyed")]
        internal extern static void _wrap_love_dll_type_World_isDestroyed(IntPtr pWorld, out bool validate);
        internal static void wrap_love_dll_type_World_isDestroyed(IntPtr pWorld, out bool validate)
        {
            _wrap_love_dll_type_World_isDestroyed(pWorld, out validate);
        }
        
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_update")]
        internal extern static bool _wrap_love_dll_type_World_update(IntPtr pWorld, float dt, int velocityiterations, int positioniterations, WrapWorldContactCallbackDelegate beginContact, WrapWorldContactCallbackDelegate endContact, WrapWorldContactCallbackDelegate preSolve, WrapWorldContactCallbackDelegate postSolve, WrapWorldContactFilterCallbackDelegate filter);
        internal static bool wrap_love_dll_type_World_update(IntPtr pWorld, float dt, int velocityiterations, int positioniterations, WrapWorldContactCallbackDelegate beginContact, WrapWorldContactCallbackDelegate endContact, WrapWorldContactCallbackDelegate preSolve, WrapWorldContactCallbackDelegate postSolve, WrapWorldContactFilterCallbackDelegate filter)
        {
            return CheckCAPIException(_wrap_love_dll_type_World_update(pWorld, dt, velocityiterations, positioniterations, beginContact, endContact, preSolve, postSolve, filter));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_getGravity")]
        internal extern static void _wrap_love_dll_type_World_getGravity(IntPtr pWorld, out float x, out float y);
        internal static void wrap_love_dll_type_World_getGravity(IntPtr pWorld, out float x, out float y)
        {
            _wrap_love_dll_type_World_getGravity(pWorld, out x, out y);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_getBodies")]
        internal extern static bool _wrap_love_dll_type_World_getBodies(IntPtr pWorld, out IntPtr bodyList, out int bodyListLenght);
        internal static bool wrap_love_dll_type_World_getBodies(IntPtr pWorld, out IntPtr bodyList, out int bodyListLenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_World_getBodies(pWorld, out bodyList, out bodyListLenght));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_getJoints")]
        internal extern static bool _wrap_love_dll_type_World_getJoints(IntPtr pWorld, out IntPtr jointList, out int jointListLenght);
        internal static bool wrap_love_dll_type_World_getJoints(IntPtr pWorld, out IntPtr jointList, out int jointListLenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_World_getJoints(pWorld, out jointList, out jointListLenght));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_getContacts")]
        internal extern static bool _wrap_love_dll_type_World_getContacts(IntPtr pWorld, out IntPtr contactList, out int contactListLenght);
        internal static bool wrap_love_dll_type_World_getContacts(IntPtr pWorld, out IntPtr contactList, out int contactListLenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_World_getContacts(pWorld, out contactList, out contactListLenght));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_queryBoundingBox")]
        internal extern static bool _wrap_love_dll_type_World_queryBoundingBox(IntPtr pWorld, float topLeftX, float topLeftY, float bottomRightX, float bottomRightY, WrapWorldQueryBoundingBoxCallbackDelegate callback);
        internal static bool wrap_love_dll_type_World_queryBoundingBox(IntPtr pWorld, float topLeftX, float topLeftY, float bottomRightX, float bottomRightY, WrapWorldQueryBoundingBoxCallbackDelegate callback)
        {
            return CheckCAPIException(_wrap_love_dll_type_World_queryBoundingBox(pWorld, topLeftX, topLeftY, bottomRightX, bottomRightY, callback));
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_World_rayCast")]
        internal extern static bool _wrap_love_dll_type_World_rayCast(IntPtr pWorld, float x1, float y1, float x2, float y2, WrapWorldRayCastCallbackDelegate callback);
        internal static bool wrap_love_dll_type_World_rayCast(IntPtr pWorld, float x1, float y1, float x2, float y2, WrapWorldRayCastCallbackDelegate callback)
        {
            return CheckCAPIException(_wrap_love_dll_type_World_rayCast(pWorld, x1, y1, x2, y2, callback));
        }



        #endregion
    }


    [StructLayout(LayoutKind.Explicit)]
    public struct Half
    {
        [FieldOffset(0)] public UInt16 value;

        /// <summary>
        /// ignores the higher 16 bits
        /// <para>https://codereview.stackexchange.com/questions/45007/half-precision-reader-writer-for-c</para>
        /// </summary>
        /// <param name="hbits"></param>
        /// <returns></returns>
        public static float IntToFloat(int hbits)
        {
            int mant = hbits & 0x03ff;            // 10 bits mantissa
            int exp = hbits & 0x7c00;            // 5 bits exponent
            if (exp == 0x7c00)                   // NaN/Inf
                exp = 0x3fc00;                    // -> NaN/Inf
            else if (exp != 0)                   // normalized value
            {
                exp += 0x1c000;                   // exp - 15 + 127
                if (mant == 0 && exp > 0x1c400)  // smooth transition
                    return BitConverter.ToSingle(BitConverter.GetBytes((hbits & 0x8000) << 16
                                                    | exp << 13 | 0x3ff), 0);
            }
            else if (mant != 0)                  // && exp==0 -> subnormal
            {
                exp = 0x1c400;                    // make it normal
                do
                {
                    mant <<= 1;                   // mantissa * 2
                    exp -= 0x400;                 // decrease exp by 1
                } while ((mant & 0x400) == 0); // while not normal
                mant &= 0x3ff;                    // discard subnormal bit
            }                                     // else +/-0 -> +/-0
            return BitConverter.ToSingle(BitConverter.GetBytes(          // combine all parts
                (hbits & 0x8000) << 16          // sign  << ( 31 - 15 )
                | (exp | mant) << 13), 0);         // value << ( 23 - 10 )
        }
        /// <summary>
        /// returns all higher 16 bits as 0 for all results
        /// <para>https://codereview.stackexchange.com/questions/45007/half-precision-reader-writer-for-c</para>
        /// </summary>
        /// <param name="fval"></param>
        /// <returns></returns>
        public static int FloatToInt(float fval)
        {
            int fbits = BitConverter.ToInt32(BitConverter.GetBytes(fval), 0);
            int sign = fbits >> 16 & 0x8000;          // sign only
            int val = (fbits & 0x7fffffff) + 0x1000; // rounded value

            if (val >= 0x47800000)               // might be or become NaN/Inf
            {                                     // avoid Inf due to rounding
                if ((fbits & 0x7fffffff) >= 0x47800000)
                {                                 // is or must become NaN/Inf
                    if (val < 0x7f800000)        // was value but too large
                        return sign | 0x7c00;     // make it +/-Inf
                    return sign | 0x7c00 |        // remains +/-Inf or NaN
                        (fbits & 0x007fffff) >> 13; // keep NaN (and Inf) bits
                }
                return sign | 0x7bff;             // unrounded not quite Inf
            }
            if (val >= 0x38800000)               // remains normalized value
                return sign | val - 0x38000000 >> 13; // exp - 127 + 15
            if (val < 0x33000000)                // too small for subnormal
                return sign;                      // becomes +/-0
            val = (fbits & 0x7fffffff) >> 23;  // tmp exp for subnormal calc
            return sign | ((fbits & 0x7fffff | 0x800000) // add subnormal bit
                 + (0x800000 >> val - 102)     // round depending on cut off
              >> 126 - val);   // div by 2^(1-(exp-127+15)) and >> 13 | exp=0
        }


        public float ToFloat()
        {
            int bits = value;
            return IntToFloat(bits);
        }

        public static Half FromFloat(float v)
        {
            int bits = FloatToInt(v);
            Half half = new Half();
            half.value = (UInt16)bits;
            return half;
        }
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct RGBA8I
    {
        public UInt8 r, g, b, a;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RGBA16I
    {
        public UInt16 r, g, b, a;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RGBA16F
    {
        public Half r, g, b, a;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct RGBA32F
    {
        public float r, g, b, a;
    }



    /// <summary>
    /// <code>
    /// union Pixel
    /// {
    ///     uint8 rgba8 [4];
    ///     uint16 rgba16 [4];
    ///     half rgba16f [4];
    ///     float  rgba32f [4];
    /// };
    /// </code>
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Pixel
    {
        [FieldOffset(0)] public RGBA8I rgba8;
        [FieldOffset(0)] public RGBA16I rgba16;
        [FieldOffset(0)] public RGBA16F rgba16f;
        [FieldOffset(0)] public RGBA32F rgba32f;

        [FieldOffset(0)] public int intValue0;
        [FieldOffset(4)] public int intValue1;
        [FieldOffset(8)] public int intValue2;
        [FieldOffset(12)] public int intValue3;
        [FieldOffset(0)] public long longValue0;
        [FieldOffset(8)] public long longValue1;
        [FieldOffset(0)] public double doubleValue0;
        [FieldOffset(8)] public double doubleValue1;

        public static string StrRGBA8(Pixel p)
        {
            return $"({p.rgba8.r},{p.rgba8.g},{p.rgba8.b},{p.rgba8.a})";
        }
        public static string StrRGBA32F(Pixel p)
        {
            return $"({p.rgba32f.r},{p.rgba32f.g},{p.rgba32f.b},{p.rgba32f.a})";
        }

        /// <summary>
        /// Create pixel with given Float4 value and format
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Pixel FromFloat4(Vector4 value, PixelFormat format)
        {
            Pixel pixel = new Pixel();
            if (format == PixelFormat.RGBA8)
            {
                unchecked
                {
                    pixel.rgba8.r = (byte)(value.r * byte.MaxValue);
                    pixel.rgba8.g = (byte)(value.g * byte.MaxValue);
                    pixel.rgba8.b = (byte)(value.b * byte.MaxValue);
                    pixel.rgba8.a = (byte)(value.a * byte.MaxValue);
                }
            }
            else if (format == PixelFormat.RGBA16)
            {
                unchecked
                {
                    pixel.rgba16.r = (ushort)(value.r * ushort.MaxValue);
                    pixel.rgba16.g = (ushort)(value.g * ushort.MaxValue);
                    pixel.rgba16.b = (ushort)(value.b * ushort.MaxValue);
                    pixel.rgba16.a = (ushort)(value.a * ushort.MaxValue);
                }
            }
            else if (format == PixelFormat.RGBA16F)
            {
                pixel.rgba16f.r = Half.FromFloat(value.r);
                pixel.rgba16f.g = Half.FromFloat(value.g);
                pixel.rgba16f.b = Half.FromFloat(value.b);
                pixel.rgba16f.a = Half.FromFloat(value.a);
            }
            else if (format == PixelFormat.RGBA32F)
            {
                pixel.rgba32f.r = value.r;
                pixel.rgba32f.g = value.g;
                pixel.rgba32f.b = value.b;
                pixel.rgba32f.a = value.a;
            }
            else
            {
                throw new Exception($"Unsupported format {format}");
            }

            return pixel;
        }
    }


    [StructLayout(LayoutKind.Sequential)]
    internal struct WrapString
    {
        //[MarshalAs(UnmanagedType.LPStr)]
        internal IntPtr data;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WrapSequenceString
    {
        internal int len;
        internal IntPtr data;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct Int4
    {
        public int x, y, z, w;
        public int r { get { return x; } }
        public int g { get { return y; } }
        public int b { get { return z; } }
        public int a { get { return w; } }
        public int Width { get { return z; } }
        public int Height { get { return w; } }
        public Int4(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle
    {
        public Vector2 a, b, c;
    }

    //[StructLayout(LayoutKind.Sequential)]
    //public struct Float6
    //{
    //    public float x, y, z;
    //    public float dx, dy, dz;
    //}

}
