// Copyright (c) 2021 raoyutian Authors. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.IO;

namespace PaddleOCRSharp
{
    /// <summary>
    /// PaddleOCR表格识别引擎对象
    /// </summary>
    public  class PaddleStructureEngine:EngineBase
    {
        #region PaddleOCR API
       
        [DllImport(dllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern bool StructureInitialize(string det_infer, string rec_infer, string keys, string table_model_dir, string table_char_dict_path, StructureParameter parameter);
        [DllImport(dllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern bool StructureInitializejson(string det_infer, string rec_infer, string keys, string table_model_dir, string table_char_dict_path, string parameter);

        [DllImport(dllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern IntPtr GetStructureDetectFile(  string imagefile);
       
        [DllImport(dllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern IntPtr GetStructureDetectByte(  byte[] imagebytedata, long size);

        [DllImport(dllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern IntPtr GetStructureDetectBase64( string imagebase64);
       
        [DllImport(dllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern void FreeStructureEngine( );
        #endregion

        /// <summary>
        /// PaddleStructureEngine识别引擎对象初始化
        /// </summary>
        public PaddleStructureEngine() : this(null,new StructureParameter())
        {  
        }
        /// <summary>
        /// PaddleStructureEngine识别引擎对象初始化
        /// </summary>
        /// <param name="config">模型配置对象，如果为空则按默认值</param>
        public PaddleStructureEngine(StructureModelConfig config) : this(config, new StructureParameter())
        {
        }
        /// <summary>
        /// PaddleStructureEngine识别引擎对象初始化
        /// </summary>
        /// <param name="config">模型配置对象，如果为空则按默认值</param>
        /// <param name="parameter">识别参数，为空均按缺省值</param>
        public PaddleStructureEngine(StructureModelConfig config, StructureParameter parameter) : base()
        {
            if (parameter == null) parameter = new StructureParameter();
            if (config == null)
            {
                string root = GetRootDirectory();
                config = GetDefaultConfig(root);
            }
             bool sucess =  StructureInitialize(config.det_infer,  config.rec_infer, config.keys, config.table_model_dir, config.table_char_dict_path, parameter);
            if (!sucess) throw new Exception("Initialize err:" + GetLastError());
        }
        /// <summary>
        /// PaddleStructureEngine识别引擎对象初始化
        /// </summary>
        /// <param name="config">模型配置对象，如果为空则按默认值</param>
        /// <param name="parameterjson">识别参数Json格式，为空均按缺省值</param>
        public PaddleStructureEngine(StructureModelConfig config, string parameterjson) : base()
        {
            if (config == null)
            {
                string root = GetRootDirectory();
                config = GetDefaultConfig(root);
            }
            if (string.IsNullOrEmpty(parameterjson))
            {
                parameterjson = GetRootDirectory();
#if NET35
                parameterjson += @"\inference\PaddleOCRStructure.config.json";
#else
                parameterjson =Path.Combine( parameterjson ,"inference","PaddleOCRStructure.config.json");    
#endif
                if (!File.Exists(parameterjson)) throw new FileNotFoundException(parameterjson);
                parameterjson = File.ReadAllText(parameterjson);
            }
            bool sucess =  StructureInitializejson(config.det_infer, config.rec_infer, config.keys, config.table_model_dir, config.table_char_dict_path, parameterjson);
            if (!sucess) throw new Exception("Initialize err:" + GetLastError());
        }


        /// <summary>
        /// 获取缺省配置
        /// </summary>
        /// <param name="rootpath">根目录</param>
        private StructureModelConfig GetDefaultConfig(string rootpath)
        {
            StructureModelConfig config = new StructureModelConfig();
#if NET35
            string modelPathroot = rootpath + @"/inference";
            config.det_infer = modelPathroot + @"/ch_PP-OCRv4_det_infer";
            config.rec_infer = modelPathroot + @"/ch_PP-OCRv4_rec_infer";
            config.keys = modelPathroot + @"/ppocr_keys.txt";
            config.table_model_dir = modelPathroot + @"/ch_ppstructure_mobile_v2.0_SLANet_infer";
            config.table_char_dict_path = modelPathroot + @"/table_structure_dict_ch.txt";
#else
                string modelPathroot =Path.Combine( rootpath ,"inference");
                config.det_infer = Path.Combine(modelPathroot,"ch_PP-OCRv4_det_infer");
                config.rec_infer = Path.Combine(modelPathroot , "ch_PP-OCRv4_rec_infer");
                config.keys = Path.Combine(modelPathroot , "ppocr_keys.txt");
                config.table_model_dir = Path.Combine(modelPathroot,"ch_ppstructure_mobile_v2.0_SLANet_infer");
                config.table_char_dict_path = Path.Combine(modelPathroot,"table_structure_dict_ch.txt");
#endif
            return config;
        }


        /// <summary>
        /// 对图像文件进行表格文本识别
        /// </summary>
        /// <param name="imagefile">图像文件</param>
        /// <returns>表格识别结果</returns>
        public string StructureDetectFile(string imagefile)
        {
            if (!System.IO.File.Exists(imagefile)) throw new Exception($"文件{imagefile}不存在");
            IntPtr presult =  GetStructureDetectFile( imagefile);
            return ConvertResult(presult);
        }

        /// <summary>
        ///对图像对象进行表格文本识别
        /// </summary>
        /// <param name="image">图像</param>
        /// <returns>表格识别结果</returns>
        public string StructureDetect(Image image)
        {
            if (image == null) throw new ArgumentNullException("image");
            var imagebyte = ImageToBytes(image);
            var result = StructureDetect(imagebyte);
            imagebyte = null;
            return result;
        }

        /// <summary>
        /// 对图像Byte数组进行表格文本识别
        /// </summary>
        /// <param name="imagebyte">图像字节数组</param>
        /// <returns>表格识别结果</returns>
        public string StructureDetect(byte[] imagebyte)
        {
           if (imagebyte == null) throw new ArgumentNullException("imagebyte");
            IntPtr presult=  GetStructureDetectByte(imagebyte, imagebyte.LongLength);
            return ConvertResult(presult);
        }
        /// <summary>
        /// 对图像Base64进行表格文本识别
        /// </summary>
        /// <param name="imagebase64">图像Base64</param>
        /// <returns>表格识别结果</returns>
        public string StructureDetectBase64(string imagebase64)
        {
            if (imagebase64 == null || imagebase64 == "") throw new ArgumentNullException("imagebase64");
            IntPtr presult= GetStructureDetectBase64( imagebase64);
            return ConvertResult(presult);
           
        }

        /// <summary>
        /// 结果解析
        /// </summary>
        /// <param name="ptrResult"></param>
        /// <returns></returns>
        private string ConvertResult(IntPtr ptrResult)
        {
            string result = "";
            if (ptrResult == IntPtr.Zero)
            {
                var err = GetLastError();
                if (!string.IsNullOrEmpty(err))
                {
                    throw new Exception("内部遇到错误：" + err);
                }
                return result;
            }
            try
            {
                /*
                   说明：
                   Marshal.PtrToStringAnsi
                  将非托管 ANSI 或 UTF-8 字符串中第一个空字符之前的所有字符复制到托管 String，并将每个字符扩展为 UTF-16 字符。
                   ANSI（适用于 Windows）或 UTF-8（适用于 Unix）字符串。
                   而Marshal.PtrToStringUTF8不支持所有.net框架，OCR结果可能含有中文，因此
                   Windows下用 Marshal.PtrToStringUni，linux下用 Marshal.PtrToStringAnsi解析
                */
                 result = Marshal.PtrToStringUni(ptrResult);
#if NET6_0_OR_GREATER
  if (!OperatingSystem.IsWindows())
  {
  result = Marshal.PtrToStringAnsi(ptrResult);
  //result=result.Replace("\\r","");
  }
#endif    
            }
            catch (Exception ex)
            {
                throw new Exception("表格字符串指针处理失败", ex);
            }
            finally
            {
                Marshal.FreeHGlobal(ptrResult);
            }
            return result;
        }


        #region Dispose
        /// <summary>
        /// 释放对象
        /// </summary>
        public override void Dispose()
        {
            FreeStructureEngine();
        }
        #endregion
    }
}
