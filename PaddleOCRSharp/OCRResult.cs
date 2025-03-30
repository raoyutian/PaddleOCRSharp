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
using System.Linq;
using System.Runtime.InteropServices;
namespace PaddleOCRSharp
{
    /// <summary>
    /// OCR识别结果
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class OCRResult
    {
        /// <summary>
        /// 文本块列表
        /// </summary>
        public List<TextBlock> TextBlocks { get; set; } = new List<TextBlock>();
        /// <summary>
        /// 识别结果文本
        /// </summary>
        public string Text => this.ToString();

        /// <summary>
        /// 识别结果文本Json格式
        /// </summary>
        public string JsonText { get; set; }
        /// <summary>
        /// 返回字符串格式
        /// </summary>
        public override string ToString()
        {
            if (TextBlocks == null) return "";
            return string.Join("", TextBlocks.Select(x => x.Text).ToArray());
        }
    }

    /// <summary>
    /// 识别的文本块
    /// </summary>
    public class TextBlock
    { /// <summary>
      /// 文本块四周顶点坐标列表
      /// </summary>
        public List<OCRPoint> BoxPoints { get; set; } = new List<OCRPoint>();
        /// <summary>
        /// 文本块文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        ///文本识别置信度
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        ///角度分类置信度
        /// </summary>
        public float cls_score { get; set; }
        /// <summary>
        ///角度分类标签
        /// </summary>
        public int cls_label { get; set; }

        /// <summary>
        /// 返回字符串格式
        /// </summary>
        public override string ToString()
        {
            if (BoxPoints == null) return "";
            string str = string.Join(",", BoxPoints.Select(x => x.ToString()).ToArray());
            return $"{Text},Score:{Score},[{str}],cls_label:{cls_label},cls_score:{cls_score}";
        }
    }

    /// <summary>
    /// 点对象
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class OCRPoint
    {
        /// <summary>
        /// X坐标，单位像素
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y坐标，单位像素
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        ///默认构造函数
        /// </summary>
        public OCRPoint()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public OCRPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// 返回字符串格式
        /// </summary>
        public override string ToString() => $"({X},{Y})";
    }
}
