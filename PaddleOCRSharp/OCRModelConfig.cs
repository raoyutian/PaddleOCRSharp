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
namespace PaddleOCRSharp
{ 
   /// <summary>
   /// 模型配置对象
   /// </summary>
    public  class OCRModelConfig
    {
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
    }

    /// <summary>
    /// 表格模型配置对象
    /// </summary>
    public class StructureModelConfig : OCRModelConfig
    {
        /// <summary>
        /// table_model_dir模型路径
        /// </summary>
        public string table_model_dir { get; set; }
        /// <summary>
        /// 表格识别字典
        /// </summary>
        public string table_char_dict_path { get; set; }
    }
}
