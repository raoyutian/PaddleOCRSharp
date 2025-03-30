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
	/// ��ȡ���һ�δ�����Ϣ
	/// </summary>
	/// <returns></returns>
	__declspec(dllimport) char* GetError();

	/// <summary>
	/// �Ƿ�ʹ�õ��ֽڱ��루������go,rust��,C#,Python���ô򿪴˿���
	/// </summary>
	/// <param name="useANSI"></param>
	__declspec(dllimport) void EnableANSIResult(bool useANSI);
	
	/// <summary>
	/// �Ƿ�ʹ��json��ʽ���ؽ����Ĭ��false
	/// </summary>
	/// <param name="useANSI"></param>
	__declspec(dllimport) void EnableJsonResult(bool enable);

	/// <summary>
	/// �Ƿ����ü�������δ���Ĭ��false
	/// </summary>
	/// <param name="useANSI"></param>
	__declspec(dllimport) void libEnableDetUseRect(bool enable);
	/// <summary>
	/// ��̬�޸Ĳ���
	/// </summary>
	/// <param name="parameter"></param>
	__declspec(dllimport) bool libModifyParameter(ModifyParameter parameter);
	
	/// <summary>
	/// ����ʶ�������ʼ��
	/// </summary>
	/// <param name="det_infer">detģ��ȫ·��</param>
	/// <param name="cls_infer">clsģ��ȫ·��</param>
	/// <param name="rec_infer">recģ��ȫ·��</param>
	/// <param name="keys">�ֵ�ȫ·��</param>
	/// <param name="parameter">ʶ���������</param>
	/// <returns></returns>
	__declspec(dllimport) bool Initialize(char* det_infer, char* cls_infer, char* rec_infer, char* keys, OCRParameter parameter);
	/// <summary>
	/// PaddleOCREngine�����ʼ��
	/// </summary>
	/// <param name="det_infer">detģ��ȫ·��</param>
	/// <param name="cls_infer">clsģ��ȫ·��</param>
	/// <param name="rec_infer">recģ��ȫ·��</param>
	/// <param name="keys">�ֵ�ȫ·��</param>
	/// <param name="parameterjson">ʶ���������json�ַ���</param>
	/// <returns></returns>
	__declspec(dllimport) bool Initializejson(char* modelPath_det_infer, char* modelPath_cls_infer, char* modelPath_rec_infer, char* keys, char* parameterjson);
	/// <summary>
	/// �ı����ʶ��-ͼ���ļ�·��
	/// </summary>
	/// <param name="imagebytedata">ͼ���ļ�·��</param>
	/// <returns></returns>
	__declspec(dllimport) char* Detect(char* imagefile);

	/*/// <summary>
	/// �ı����ʶ��-OpenCV Mat
	/// </summary>
	/// <param name="cvmat">Mat����</param>
	/// <returns></returns>
	__declspec(dllimport)char* DetectMat(cv::Mat& cvmat);*/
	/// <summary>
	/// �ı����ʶ��-ͼ���ֽ���
	/// </summary>
	/// <param name="imagebytedata">ͼ���ֽ���</param>
	/// <param name="size">ͼ���ֽ�������</param>
	/// <returns></returns>
	__declspec(dllimport) char* DetectByte(char* imagebytedata, size_t* size);

	/// <summary>
	/// �ı����ʶ��-ͼ���ֽ���
	/// </summary>
	/// <param name="img">ͼ���ַ</param>
	/// <param name="nWidth">ͼ����</param>
	///  <param name="nHeight">ͼ��߶�</param>
	///  <param name="nChannel">ͼ��ͨ����</param>
	/// <returns></returns>
	__declspec(dllimport) char* DetectByteData(const char* img, int nWidth, int nHeight, int nChannel);

	/// <summary>
	/// �ı����ʶ��-ͼ��base64
	/// </summary>
	/// <param name="imagebase64">ͼ��base64</param>
	/// <returns></returns>
	__declspec(dllimport) char* DetectBase64(char* imagebase64);

	/// <summary>
	/// �ͷ��������
	/// </summary>
	__declspec(dllimport) void FreeEngine();


	/// <summary>
	/// ���ʶ�������ʼ��
	/// </summary>
	/// <param name="modelPath_det_infer">detģ��ȫ·��</param>
	/// <param name="modelPath_rec_infer">clsģ��ȫ·��</param>
	/// <param name="keys">�ֵ�ȫ·��</param>
	/// <param name="table_model_dir">���ģ��ȫ·��</param>
	/// <param name="table_char_dict_path">����ֵ�ȫ·��</param>
	/// <param name="parameter">��������</param>
	/// <returns></returns>
	__declspec(dllimport) bool StructureInitialize(char* modelPath_det_infer, char* modelPath_rec_infer, char* keys, char* table_model_dir, char* table_char_dict_path, StructureParameter parameter);
	/// <summary>
	/// ���ʶ�������ʼ��json��ʽ
	/// </summary>
	/// <param name="modelPath_det_infer">detģ��ȫ·��</param>
	/// <param name="modelPath_rec_infer">clsģ��ȫ·��</param>
	/// <param name="keys">�ֵ�ȫ·��</param>
	/// <param name="table_model_dir">���ģ��ȫ·��</param>
	/// <param name="table_char_dict_path">����ֵ�ȫ·��</param>
	/// <param name="parameterjson">��������json��ʽ</param>
	/// <returns></returns>
	__declspec(dllimport) bool StructureInitializejson(char* modelPath_det_infer, char* modelPath_rec_infer, char* keys, char* table_model_dir, char* table_char_dict_path, char* parameterjson);
	/// <summary>
	///���ʶ��
	/// </summary>
	/// <param name="imagefile">�ļ�·��</param>
	/// <returns></returns>
	__declspec(dllimport) char* GetStructureDetectFile(char* imagefile);
	///// <summary>
	/////���ʶ��
	///// </summary>
	///// <param name="cvmat">opencv Mat����</param>
	///// <returns></returns>
	//__declspec(dllimport) char* GetStructureDetectMat(cv::Mat& cvmat);
	/// <summary>
	///���ʶ��
	/// </summary>
	/// <param name="imagebytedata">ͼ���ֽ���</param>
	/// <param name="size">ͼ���ֽ�������</param>
	/// <returns></returns>
	__declspec(dllimport) char* GetStructureDetectByte(char* imagebytedata, size_t* size);
	/// <summary>
	/// ���ʶ��
	/// </summary>
	/// <param name="imagebase64">ͼ��base64</param>
	/// <returns></returns>
	__declspec(dllimport) char* GetStructureDetectBase64(char* imagebase64);
	/// <summary>
	/// �ͷ��������
	/// </summary>
	__declspec(dllimport) void FreeStructureEngine();

};