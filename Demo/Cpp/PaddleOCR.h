#pragma once
#include <iostream>
#include <Windows.h>
#include <tchar.h>
#include "string"
#include <string.h>
#include <io.h> 
#include <chrono>
#include <include/yt_Parameter.h>
#pragma comment (lib,"PaddleOCR.lib")

extern "C" {

	/// <summary>
	/// 获取最后一次错误信息
	/// </summary>
	/// <returns></returns>
	__declspec(dllimport) char* GetError();

	/// <summary>
	/// 是否使用单字节编码（适用于go,rust）,C#,Python不用打开此开关
	/// </summary>
	/// <param name="useANSI"></param>
	__declspec(dllimport) void EnableANSIResult(bool useANSI);
	
	/// <summary>
	/// 是否使用json格式返回结果，默认false
	/// </summary>
	/// <param name="useANSI"></param>
	__declspec(dllimport) void EnableJsonResult(bool enable);

	/// <summary>
	/// 是否启用检测结果矩形处理，默认false
	/// </summary>
	/// <param name="useANSI"></param>
	__declspec(dllimport) void libEnableDetUseRect(bool enable);
	/// <summary>
	/// 动态修改参数
	/// </summary>
	/// <param name="parameter"></param>
	__declspec(dllimport) bool libModifyParameter(ModifyParameter parameter);
	
	/// <summary>
	/// 文字识别引擎初始化
	/// </summary>
	/// <param name="det_infer">det模型全路径</param>
	/// <param name="cls_infer">cls模型全路径</param>
	/// <param name="rec_infer">rec模型全路径</param>
	/// <param name="keys">字典全路径</param>
	/// <param name="parameter">识别参数对象</param>
	/// <returns></returns>
	__declspec(dllimport) bool Initialize(char* det_infer, char* cls_infer, char* rec_infer, char* keys, OCRParameter parameter);
	/// <summary>
	/// PaddleOCREngine引擎初始化
	/// </summary>
	/// <param name="det_infer">det模型全路径</param>
	/// <param name="cls_infer">cls模型全路径</param>
	/// <param name="rec_infer">rec模型全路径</param>
	/// <param name="keys">字典全路径</param>
	/// <param name="parameterjson">识别参数对象json字符串</param>
	/// <returns></returns>
	__declspec(dllimport) bool Initializejson(char* modelPath_det_infer, char* modelPath_cls_infer, char* modelPath_rec_infer, char* keys, char* parameterjson);
	/// <summary>
	/// 文本检测识别-图像文件路径
	/// </summary>
	/// <param name="imagebytedata">图像文件路径</param>
	/// <returns></returns>
	__declspec(dllimport) char* Detect(char* imagefile);

	/*/// <summary>
	/// 文本检测识别-OpenCV Mat
	/// </summary>
	/// <param name="cvmat">Mat对象</param>
	/// <returns></returns>
	__declspec(dllimport)char* DetectMat(cv::Mat& cvmat);*/
	/// <summary>
	/// 文本检测识别-图像字节流
	/// </summary>
	/// <param name="imagebytedata">图像字节流</param>
	/// <param name="size">图像字节流长度</param>
	/// <returns></returns>
	__declspec(dllimport) char* DetectByte(char* imagebytedata, size_t* size);

	/// <summary>
	/// 文本检测识别-图像字节流
	/// </summary>
	/// <param name="img">图像地址</param>
	/// <param name="nWidth">图像宽度</param>
	///  <param name="nHeight">图像高度</param>
	///  <param name="nChannel">图像通道数</param>
	/// <returns></returns>
	__declspec(dllimport) char* DetectByteData(const char* img, int nWidth, int nHeight, int nChannel);

	/// <summary>
	/// 文本检测识别-图像base64
	/// </summary>
	/// <param name="imagebase64">图像base64</param>
	/// <returns></returns>
	__declspec(dllimport) char* DetectBase64(char* imagebase64);

	/// <summary>
	/// 释放引擎对象
	/// </summary>
	__declspec(dllimport) void FreeEngine();


	/// <summary>
	/// 表格识别引擎初始化
	/// </summary>
	/// <param name="modelPath_det_infer">det模型全路径</param>
	/// <param name="modelPath_rec_infer">cls模型全路径</param>
	/// <param name="keys">字典全路径</param>
	/// <param name="table_model_dir">表格模型全路径</param>
	/// <param name="table_char_dict_path">表格字典全路径</param>
	/// <param name="parameter">参数对象</param>
	/// <returns></returns>
	__declspec(dllimport) bool StructureInitialize(char* modelPath_det_infer, char* modelPath_rec_infer, char* keys, char* table_model_dir, char* table_char_dict_path, StructureParameter parameter);
	/// <summary>
	/// 表格识别引擎初始化json格式
	/// </summary>
	/// <param name="modelPath_det_infer">det模型全路径</param>
	/// <param name="modelPath_rec_infer">cls模型全路径</param>
	/// <param name="keys">字典全路径</param>
	/// <param name="table_model_dir">表格模型全路径</param>
	/// <param name="table_char_dict_path">表格字典全路径</param>
	/// <param name="parameterjson">参数对象json格式</param>
	/// <returns></returns>
	__declspec(dllimport) bool StructureInitializejson(char* modelPath_det_infer, char* modelPath_rec_infer, char* keys, char* table_model_dir, char* table_char_dict_path, char* parameterjson);
	/// <summary>
	///表格识别
	/// </summary>
	/// <param name="imagefile">文件路径</param>
	/// <returns></returns>
	__declspec(dllimport) char* GetStructureDetectFile(char* imagefile);
	///// <summary>
	/////表格识别
	///// </summary>
	///// <param name="cvmat">opencv Mat对象</param>
	///// <returns></returns>
	//__declspec(dllimport) char* GetStructureDetectMat(cv::Mat& cvmat);
	/// <summary>
	///表格识别
	/// </summary>
	/// <param name="imagebytedata">图像字节流</param>
	/// <param name="size">图像字节流长度</param>
	/// <returns></returns>
	__declspec(dllimport) char* GetStructureDetectByte(char* imagebytedata, size_t* size);
	/// <summary>
	/// 表格识别
	/// </summary>
	/// <param name="imagebase64">图像base64</param>
	/// <returns></returns>
	__declspec(dllimport) char* GetStructureDetectBase64(char* imagebase64);
	/// <summary>
	/// 释放引擎对象
	/// </summary>
	__declspec(dllimport) void FreeStructureEngine();

};