// Copyright (c) 2021 raoyutian  All Rights Reserved.
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
using System.Drawing;
using System.Runtime.InteropServices;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace PaddleOCRSharp
{
    /// <summary>
    /// 引擎对象基类
    /// </summary>
    public abstract class EngineBase
    {
        #region DllImport
        /// <summary>
        /// 非托管推理引擎实例内存地址
        /// </summary>
        internal ulong enginePtr { get; set; }
        /// <summary>
        /// PaddleOCR.dll自定义加载路径，默认为空，如果指定则需在引擎实例化前赋值。
        /// </summary>
        public static string PaddleOCRdllPath { get; set; }
        internal const string dllName = "PaddleOCR";

        [DllImport(dllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern IntPtr GetError();

        [DllImport(dllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern void libaddLicense(string lic);

     
        #endregion

        #region public

        /// <summary>
        /// 添加授权文件,免费版无需关注
        /// </summary>
        /// <param name="lic">授权文件</param>
        public static void AddLicense(string lic)
        {
            libaddLicense(lic);
        }
        /// <summary>
        /// 获取程序的当前路径;
        /// </summary>
        /// <returns></returns>
        public static string GetRootDirectory()
        {
            //如果指定路径，全部按照指定路径
            if (!string.IsNullOrEmpty(PaddleOCRdllPath)) return PaddleOCRdllPath;
            
            List<string> paths = new List<string>();
            string path1 = AppDomain.CurrentDomain.BaseDirectory;
#if NET461_OR_GREATER || NETCOREAPP || NET6_0_OR_GREATER
            path1 = AppContext.BaseDirectory;
#endif
            string path2 = Path.GetDirectoryName(typeof(EngineBase).Assembly.Location);
            if(!string.IsNullOrEmpty(path1))
            {
                paths.Add(path1);
                paths.AddRange(Directory.GetDirectories(path1, "*").ToList());
            }
            if (!string.IsNullOrEmpty(path2))
            {
                paths.Add(path2);
                paths.AddRange(Directory.GetDirectories(path2, "*").ToList());
            }
            foreach (string path in paths)
            {
                if(string.IsNullOrEmpty(path)) continue;
                
                if (File.Exists(Path.Combine(path, dllName + ".dll")))
                {
                    return path;
                }
            }
            return path1;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public EngineBase()
        {
            //此行代码无实际意义，用于后面的JsonHelper.DeserializeObject的首次加速，首次初始化会比较慢，放在此处预热。
            var temp = JsonHelper.DeserializeObject<TextBlock>("{}");
            try
            {
#if NET6_0_OR_GREATER
             if(OperatingSystem.IsWindows())
             {
                SetEnvironment();
             }
#else
                SetEnvironment();
#endif
            }
            catch (Exception e)
            {
                throw new FileLoadException($" Load the library [{dllName}] fail.\n{e.Message}");
            }
        }
        #endregion

        #region private

        /// <summary>
        /// 设置自动加载dll路径;
        /// </summary>
        /// <returns></returns>
        private static void SetEnvironment()
        {
            if (string.IsNullOrEmpty(PaddleOCRdllPath))
            {
                PaddleOCRdllPath = GetRootDirectory();
            }
            if (!string.IsNullOrEmpty(PaddleOCRdllPath))
            {
                string Envpath = Environment.GetEnvironmentVariable("path", EnvironmentVariableTarget.Process);
                if (!string.IsNullOrEmpty(Envpath) && !Envpath.EndsWith(PaddleOCRdllPath))
                {
                    Environment.SetEnvironmentVariable("path", Envpath + ";" + PaddleOCRdllPath, EnvironmentVariableTarget.Process);
                }
            }
        }
        /// <summary>
        /// Convert Image to Byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        internal protected byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Guid == ImageFormat.Jpeg.Guid)
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Guid == ImageFormat.Png.Guid)
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Guid == ImageFormat.Bmp.Guid)
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Guid == ImageFormat.Gif.Guid)
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Guid == ImageFormat.Icon.Guid)
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                else
                {
                    image.Save(ms, ImageFormat.Png);
                }
                byte[] buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// 获取底层错误信息
        /// </summary>
        /// <returns></returns>
        internal virtual string GetLastError()
        {
            string err = "";
            try
            {
                var errptr = GetError();
                if (errptr != IntPtr.Zero)
                {
                    err = Marshal.PtrToStringAnsi(errptr);
                    Marshal.FreeHGlobal(errptr);
                }
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return err;
        }
        /// <summary>
        /// 检测指针是否为0
        /// </summary>
        /// <returns></returns>
        internal void ZeroThrow(ulong enginePtr)
        {
            if (enginePtr == 0) throw new Exception("Please create an engine object first");
        }
#endregion
    }
}