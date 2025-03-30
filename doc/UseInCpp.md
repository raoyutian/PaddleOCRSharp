#### 1.1 PaddleOCR的公开接口

```
        /// <summary>
	/// 获取最后一次错误信息
	/// </summary>
	/// <returns></returns>
	__declspec(dllimport)	char* GetError();

	/// <summary>
	/// 是否使用单字节编码（适用于go,rust）,C#,Python不用打开此开关
	/// </summary>
	/// <param name="useANSI"></param>
	__declspec(dllimport) void EnableANSIResult(bool useANSI);
	/// <summary>
	/// 文字识别引擎初始化
	/// </summary>
	/// <param name="det_infer">det模型全路径</param>
	/// <param name="cls_infer">cls模型全路径</param>
	/// <param name="rec_infer">rec模型全路径</param>
	/// <param name="keys">字典全路径</param>
	/// <param name="parameter">识别参数对象</param>
	/// <returns>是否初始化成功</returns>
	__declspec(dllimport) bool Initialize(char* det_infer, char* cls_infer, char* rec_infer, char* keys, OCRParameter parameter);
	/// <summary>
	/// PaddleOCREngine引擎初始化
	/// </summary>
	/// <param name="det_infer">det模型全路径</param>
	/// <param name="cls_infer">cls模型全路径</param>
	/// <param name="rec_infer">rec模型全路径</param>
	/// <param name="keys">字典全路径</param>
	/// <param name="parameterjson">识别参数对象json字符串</param>
	/// <returns>是否初始化成功</returns>
	__declspec(dllimport) bool Initializejson(char* modelPath_det_infer, char* modelPath_cls_infer, char* modelPath_rec_infer, char* keys, char* parameterjson);
	/// <summary>
	/// 文本检测识别-图像文件路径
	/// </summary>
	/// <param name="imagebytedata">图像文件路径</param>
	/// <returns>返回json格式字符串</returns>
	__declspec(dllimport) char* Detect(char* imagefile);

	/*/// <summary>
	/// 文本检测识别-OpenCV Mat
	/// </summary>
	/// <param name="cvmat">Mat对象</param>
	/// <returns>返回json格式字符串</returns>
	__declspec(dllimport)char* DetectMat(cv::Mat& cvmat);*/
	/// <summary>
	/// 文本检测识别-图像字节流
	/// </summary>
	/// <param name="imagebytedata">图像字节流</param>
	/// <param name="size">图像字节流长度</param>
	/// <returns>返回json格式字符串</returns>
	__declspec(dllimport) char* DetectByte(char* imagebytedata, size_t* size);

	/// <summary>
	/// 文本检测识别-图像字节流
	/// </summary>
	/// <param name="img">图像地址</param>
	/// <param name="nWidth">图像宽度</param>
	///  <param name="nHeight">图像高度</param>
	///  <param name="nChannel">图像通道数</param>
	/// <returns>返回json格式字符串</returns>
	__declspec(dllimport) char* DetectByteData(const char* img, int nWidth, int nHeight, int nChannel);

	/// <summary>
	/// 文本检测识别-图像base64
	/// </summary>
	/// <param name="imagebase64">图像base64</param>
	/// <returns>返回json格式字符串</returns>
	__declspec(dllimport) char* DetectBase64(char* imagebase64);

	/// <summary>
	/// 释放引擎对象
	/// </summary>
	__declspec(dllimport) void FreeEngine();
```