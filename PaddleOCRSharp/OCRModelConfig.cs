// Copyright (c) 2021 raoyutian   All Rights Reserved.
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
using PaddleOCRSharp;

using System.IO;

namespace PaddleOCRSharp
{
    /// <summary>
    /// 模型配置对象
    /// </summary>
    public class OCRModelConfig
    {
        /// <summary>
        /// 默认构造函数默认V6_Small模型
        /// </summary>
        public OCRModelConfig()
        {
            string rootpath = EngineBase.GetRootDirectory();
            string modelPathroot = Path.Combine(rootpath, "inference");
            det_infer = Path.Combine(modelPathroot, "PP-OCRv6_small_det_infer");
            cls_infer = Path.Combine(modelPathroot, "PP-OCRv5_mobile_cls_infer");
            rec_infer = Path.Combine(modelPathroot, "PP-OCRv6_small_rec_infer");
            keys = Path.Combine(modelPathroot, "ppocr_keys.txt");
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="det_infer_path">检测模型文件夹</param>
        /// <param name="cls_infer_path">角度分类模型文件夹</param>
        /// <param name="rec_infer_path">识别模型文件夹</param>
        /// <param name="dict">字典模型文件</param>
        public OCRModelConfig(string det_infer_path, string cls_infer_path, string rec_infer_path, string dict)
        {
            this.det_infer = det_infer_path;
            this.cls_infer = cls_infer_path;
            this.rec_infer = rec_infer_path;
            this.keys = dict;
        }

        /// <summary>
        /// det_infer模型路径
        /// </summary>
        public string det_infer { get; set; }
        /// <summary>
        /// cls_infer模型路径
        /// </summary>
        public string cls_infer { get; set; }
        /// <summary>
        /// rec_infer模型路径
        /// </summary>
        public string rec_infer { get; set; }
        /// <summary>
        /// ppocr_keys.txt文件名全路径
        /// </summary>
        public string keys { get; set; }
        /// <summary>
        /// 缺省,V6_Small模型
        /// </summary>
        public static OCRModelConfig Default => new OCRModelConfig();

        /// <summary>
        /// PPOCR_V5中英文模型
        /// </summary>
        public static OCRModelConfig V5_CN
        {
            get
            {
                string rootpath = EngineBase.GetRootDirectory();
                OCRModelConfig config = new OCRModelConfig();
                string modelPathroot = Path.Combine(rootpath, "inference");
                config.det_infer = Path.Combine(modelPathroot, "PP-OCRv5_mobile_det_infer");
                config.cls_infer = Path.Combine(modelPathroot, "PP-OCRv5_mobile_cls_infer");
                config.rec_infer = Path.Combine(modelPathroot, "PP-OCRv5_mobile_rec_infer");
                config.keys = Path.Combine(modelPathroot, "ppocr_keys.txt");
                return config;
            }
        }
        /// <summary>
        /// PPOCR_V5英文数字模型
        /// </summary>
        public static OCRModelConfig V5_EN
        {
            get
            {
                string rootpath = EngineBase.GetRootDirectory();
                OCRModelConfig config = new OCRModelConfig();
                string modelPathroot = Path.Combine(rootpath, "inference");
                config.det_infer = Path.Combine(modelPathroot, "PP-OCRv5_mobile_det_infer");
                config.cls_infer = Path.Combine(modelPathroot, "PP-OCRv5_mobile_cls_infer");
                config.rec_infer = Path.Combine(modelPathroot, "en_PP-OCRv5_mobile_rec_infer");
                config.keys = Path.Combine(modelPathroot, "en_dict.txt");
                return config;
            }
        }
        /// <summary>
        /// PPOCR_V6  Tiny模型
        /// </summary>
        public static OCRModelConfig V6_Tiny
        {
            get
            {
                string rootpath = EngineBase.GetRootDirectory();
                OCRModelConfig config = new OCRModelConfig();
                string modelPathroot = Path.Combine(rootpath, "inference");
                config.det_infer = Path.Combine(modelPathroot, "PP-OCRv6_tiny_det_infer");
                config.cls_infer = Path.Combine(modelPathroot, "PP-OCRv5_mobile_cls_infer");
                config.rec_infer = Path.Combine(modelPathroot, "PP-OCRv6_tiny_rec_infer");
                config.keys = Path.Combine(modelPathroot, "ppocr_keys.txt");
                return config;
            }
        }
        /// <summary>
        /// PPOCR_V6  Small模型
        /// </summary>
        public static OCRModelConfig V6_Small
        {
            get
            {
                string rootpath = EngineBase.GetRootDirectory();
                OCRModelConfig config = new OCRModelConfig();
                string modelPathroot = Path.Combine(rootpath, "inference");
                config.det_infer = Path.Combine(modelPathroot, "PP-OCRv6_small_det_infer");
                config.cls_infer = Path.Combine(modelPathroot, "PP-OCRv5_mobile_cls_infer");
                config.rec_infer = Path.Combine(modelPathroot, "PP-OCRv6_small_rec_infer");
                config.keys = Path.Combine(modelPathroot, "ppocr_keys.txt");
                return config;
            }
        }
        
    }

    /// <summary>
    /// 表格模型配置对象
    /// </summary>
    public class StructureModelConfig : OCRModelConfig
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public StructureModelConfig():base()
        {
            string rootpath = EngineBase.GetRootDirectory();
            string modelPathroot = Path.Combine(rootpath, "inference");
            table_model_dir = Path.Combine(modelPathroot, "SLANet_infer");
            table_char_dict_path = Path.Combine(modelPathroot, "table_structure_dict_ch.txt");
        }

        /// <summary>
        /// table_model_dir模型路径
        /// </summary>
        public string table_model_dir { get; set; }
        /// <summary>
        /// 表格识别字典
        /// </summary>
        public string table_char_dict_path { get; set; }

        /// <summary>
        /// 缺省
        /// </summary>
        public static new StructureModelConfig Default => new StructureModelConfig();
       
    }
}
