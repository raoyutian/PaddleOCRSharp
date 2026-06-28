#pragma once
// Copyright (c) 2022 raoyutian. All Rights Reserved.
//
#ifdef _WIN32
#define YT_API   __declspec( dllexport )
#else
#define YT_API    
#endif
#include <include/yt_Parameter.h>
using namespace std;

#ifdef __cplusplus
//extern "C"
extern "C" {
#endif // __cplusplus

	YT_API void libaddLicense(char* licfile);
	YT_API uintptr_t Initialize(char* det_infer, char* cls_infer, char* rec_infer, char* keys, OCRParameter parameter);
	YT_API uintptr_t Initializejson(char* modelPath_det_infer, char* modelPath_cls_infer, char* modelPath_rec_infer, char* keys, char* parameterjson);
	YT_API void EnableANSIResult(uintptr_t engine_p, bool enable);
	YT_API void EnableJsonResult(uintptr_t engine_p, bool enable);
	YT_API void libEnableDetUseRect(uintptr_t engine_p, bool enable);
	YT_API bool libModifyParameter(uintptr_t engine_p, ModifyParameter parameter);
	//YT_API char* DetectMat(uintptr_t engine, cv::Mat& cvmat);
	YT_API char* Detect(uintptr_t engine, char* imagefile);
	YT_API char* DetectByte(uintptr_t engine, char* imagebytedata, size_t* size);
	YT_API char* DetectBase64(uintptr_t engine, char* imagebase64);
	YT_API char* DetectByteData(uintptr_t engine, char* img, int nWidth, int nHeight, int nChannel);
	YT_API void FreeEngine(uintptr_t engine);
	
	YT_API bool StructureInitialize(char* modelPath_det_infer, char* modelPath_rec_infer, char* keys, char* table_model_dir, char* table_char_dict_path, StructureParameter parameter);
	YT_API bool StructureInitializejson(char* modelPath_det_infer, char* modelPath_rec_infer, char* keys, char* table_model_dir, char* table_char_dict_path, char* parameterjson);
	//YT_API char* GetStructureDetectMat(uintptr_t engine, cv::Mat& cvmat);
	YT_API char* GetStructureDetectFile(char* imagefile);
	YT_API char* GetStructureDetectByte(char* imagebytedata, size_t* size);
	YT_API char* GetStructureDetectBase64(char* imagebase64);
	YT_API void FreeStructureEngine();
	
	YT_API  char* GetError();

#ifdef __cplusplus
}   //extern "C"
#endif // __cplusplus

