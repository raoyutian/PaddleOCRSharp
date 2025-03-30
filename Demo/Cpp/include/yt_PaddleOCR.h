#pragma once
// Copyright (c) 2021 饶玉田 Authors. All Rights Reserved.
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

/*
OCR识别返回数据结构
OCRResult
-Text   识别的文本（所有TextBlocks内的文本拼接）
-TextBlocks
--Text   该分割区域下的识别文本
--BoxPoints   该分割区域的四个点坐标，围成一个范围
---Point   点
----X     X坐标
----Y     Y坐标
*/
//#include "opencv2/core.hpp"
#include <include/yt_Parameter.h>
#include <vector>
#include <codecvt>

#ifdef _WIN32
#define DllExport   __declspec( dllexport )
#else
#define DllExport    
#endif 
using namespace std;

//导出C函数
extern "C" {
	DllExport void EnableANSIResult(bool enable);
	DllExport void EnableJsonResult(bool enable);
	DllExport void libEnableDetUseRect(bool enable);
	DllExport bool Initialize(char* det_infer, char* cls_infer, char* rec_infer, char* keys, OCRParameter parameter);
	DllExport bool Initializejson(char* modelPath_det_infer, char* modelPath_cls_infer, char* modelPath_rec_infer, char* keys, char* parameterjson);
	DllExport bool libModifyParameter(ModifyParameter parameter);
	DllExport char* Detect(char* imagefile);
	DllExport char* DetectMat(cv::Mat& cvmat);
	DllExport char* DetectByte(char* imagebytedata, size_t* size);
	DllExport char* DetectBase64(char* imagebase64);
	DllExport char* DetectByteData(char* imgdata, int nWidth, int nHeight, int nChannel);
	DllExport void FreeEngine();
	
	DllExport bool StructureInitialize(char* modelPath_det_infer, char* modelPath_rec_infer, char* keys, char* table_model_dir, char* table_char_dict_path, StructureParameter parameter);
	DllExport bool StructureInitializejson(char* modelPath_det_infer, char* modelPath_rec_infer, char* keys, char* table_model_dir, char* table_char_dict_path, char* parameterjson);
	DllExport char* GetStructureDetectFile(char* imagefile);
	DllExport char* GetStructureDetectMat(cv::Mat& cvmat);
	DllExport char* GetStructureDetectByte(char* imagebytedata, size_t* size);
	DllExport char* GetStructureDetectBase64(char* imagebase64);
	DllExport void FreeStructureEngine();
	DllExport char* GetError();

};
