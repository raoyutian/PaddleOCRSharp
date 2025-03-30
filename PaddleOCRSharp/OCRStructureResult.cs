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
using System.Collections.Generic;
namespace PaddleOCRSharp
{
    /// <summary>
    /// OCR结构化识别结果
    /// </summary>
    public sealed class OCRStructureResult
    {
       /// <summary>
       /// 表格识别结果
       /// </summary>
        public OCRStructureResult()
        {
            Cells = new List<StructureCells>();
            TextBlocks = new List<TextBlock>();
        }

        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// 列数
        /// </summary>
        public int ColCount { get; set; }
        /// <summary>
        /// 单元格 列表
        /// </summary>
        public List<StructureCells> Cells { get; set; }

        /// <summary>
        /// 文本块列表
        /// </summary>
        public List<TextBlock> TextBlocks { get; set; }

    }

    /// <summary>
    /// 单元格
    /// </summary>
    public sealed class StructureCells
    {
        /// <summary>
        /// 单元格构造函数
        /// </summary>
        public StructureCells()
        {
            TextBlocks = new List<TextBlock>();
        }

        /// <summary>
        /// 行数
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// 列数
        /// </summary>
        public int Col { get; set; }
        /// <summary>
        /// 文本块
        /// </summary>
        public List<TextBlock> TextBlocks { get; set; }
        /// <summary>
        /// 识别文本
        /// </summary>
        public string Text { get; set; }
    }

}

