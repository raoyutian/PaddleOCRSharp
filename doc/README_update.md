
### 更新计划

###  （计划）

1..支持多实例

2.适配Paddle3.0推理框架

3.适配新版模型


###  v4.5.0 （2025-03-21）

1..NET库PaddleOCRSharp.dll增加图像内存指针接口，对于视频帧Mat、bmp等不用转BMP可以直传对象指针，减少对象转换与内存拷贝;

2..NET库PaddleOCRSharp.dll增加.NET9.0框架支持;

3.优化PaddleOCR推理性能;

4.增加ARM架构的CPU版本支持;

5.增加linux版本CPU加速适配;

6.修复det部分检测参数修改无效问题;


1..NET库PaddleOCRSharp.dll增加图像内存指针接口，对于视频帧Mat、bmp等不用转BMP可以直传对象指针，减少对象转换与内存拷贝;
 python\C++\GO等语言直接调用 PaddleOCR.dll的不用更新，接口早已存在;

2..NET库PaddleOCRSharp.dll增加.NET9.0框架支持;

3.优化PaddleOCR推理性能，CPU与GPU性能均大幅提高70%以上，GPU版本提高70%~200%，相对上一版本;

4.增加ARM架构的CPU版本支持;

5.增加linux版本CPU加速适配。支持x86\ARM64CPU;



###  v4.4.0 （2024-08-13）

1.跨平台支持Linux。

2.优化坐标排序。

3.优化net6.0以上采用System.Text.Json进行反序列化以提高性能。

4.优化灰色背景图片以提高识别精度。

5.PaddleOCR库新增一个函数EnableJsonResult（bool），用于设置是否返回带坐标的json字符串还是返回仅文本字符串。

6.优化进程环境变量字符串超长问题。

7.修复上一版本mkldnn.dll的问题。

8.取消use_angle_cls参数判断。

9.PaddleOCR库增加一个函数，用于启用把单字的旋转矩形改为外接正矩形。

###  v4.3.0 （2024-05-23）

1.优化提速10~15%。

2.降低包大小。

3.优化内存使用。

4.修复Pascal语言调用出现的一个浮点运算问题

5.修复接口未能正确获取底层错误信息的问题

6.修复cls_score默认值在Json反序列化时报错的问题

7.ModifyParameter参数增加两个属性字段：m_rec,m_m_max_side_len

###  v4.2.0 （2024-03-26）

1.增加ModifyParameter方法，以便运行时可动态传入检测参数。

2.Newtonsoft.Json升级到13.0.3版本

3.重新开放.net8.0框架下的Image参数类型的接口

4.优化det、cls为false时，模型路径判断问题。


###  v4.1.0 （2024-01-10）

1.修复在web项目中使用报错问题。

2.优化win7系统的支持。

3.优化.net core项目引用库后，在解决方案中显示有关dll文件的问题。

4.适配百度飞桨官方的2.6版本预测库paddle_inference.dll。

5.修复当同时开启cls与use_angle_cls参数，初始化异常问题。

6.修复当文本框数量超过一定数量被丢弃的问题。

7.优化内存使用与速度。

8.修改PaddleOCR.dll中初始化接口Initialize、Initializejson的返回类型为布尔类型；
  True表示成功，False则表示初始化失败，可以调用GetError()获取错误信息。


### v4.0.2 （2023-11-02）

1.修复表格识别返回空数据问题。

2.修复CPU识别内存增长无法释放问题。

3.修复其他已知问题。

4.优化速度。


### v4.0.1 （2023-09-02）

1.修复上一个版本V4模型慢的问题。


###  v4.0.0（2023-08-08）

1.适配百度飞桨官方的2.5.1版本预测库paddle_inference.dll。

2.适配百度飞桨PaddleOCR的V4版本模型。

3.把默认的OpenCV依赖版本从4.1.1升级为4.7.0。

4.支持.net8.0框架。

5.移除yt_CPUCheck.dll的依赖，项目中不再自动检测CPU，默认目录下提供一个CPU检测工具。

6.修复当返回结果不是json格式时的异常问题。

7.增加接口GetError（），用于获取非托管代码内的错误内容。

8.针对asp.net使用中遇到的异常问题修复。

9.nuget包发布默认带中英文V4模型。


### v3.1.0 （2023-05-17）

1.修复PaddleOCRSharp.dll内存释放问题

2.优化OCR识别性能，提高识别速度。

3.优化缓存导致的内存持续增加问题。

4.修复PaddleOCRSharp.dll已知问题。

5.当开启角度分类cls参数时，输出角度分类的置信度信息。

6.修复已知问题。

### v3.0.0 （2023-04-03）

1.修改PaddleOCR.dll，以满足python，go，rust等语言的调用

2.修改PaddleOCR.dll，增加引擎初始化接口重载，可以传输json字符串格式的OCR参数

3.修改PaddleOCR.dll所有OCR识别接口，取消原来的需要传入OCR结果指针，改为接口返回json字符串格式的OCR结果

4.修改PaddleOCRSharp.dll，增加引用json类库Newtonsoft.Json.dll,用于OCR结果反序列化

5.修改PaddleOCRSharp.dll，OCR结果对象增加一个字段属性，用于存储PaddleOCR.dll返回的Json文本。

6.修改PaddleOCRSharp.dll，判断是win7操作系统，自动加载win7系统下的依赖项。

7.修改PaddleOCRSharp.dll，EngineBase增加一静态属性PaddleOCRdllPath，允许在引擎初始化前指定PaddleOCR.dll等C++动态库的路径。

8.修改PaddleOCRSharp.dll部分类cs文件，整合到一个cs文件中。

9.修改PaddleOCRSharp.dll，修复程序使用单文件发布时程序根目录找不到的问题。


### v2.3.0 （2023-2-22）

1.增加表格识别功能

2.同步更新飞桨PaddleOCR最新版本C++代码

3.适配paddle_inference预测库到最新2.4.1版本

4.识别参数增加use_tensorrt属性，当使用GPU预测时，是否启用tensorrt，默认false


### v2.2.0 （2022-12-20）

1.恢复.net framework 4.5的支持，新增.net framework 4.8.1支持；

2.移除Detect接口（可用识别接口代替，关闭rec参数）；

3.优化CPU指令集检测(CPU指令集检测放在yt_CPUCheck.dll中)；

4.修复内存增加的问题（需要调用PaddleOCREngine对象的Dispose方法）；

5.修复PaddleOCREngine使用using报错的问题；

6.完善32位项目引用nuget包编译的错误提示；

### v2.1.0 （2022-11-09）

1.屏蔽有关日志输出

2.添加NET7.0的支持

3.移除.net framework 4.5的支持

### v2.0.4 （2022-09-20）

1.优化VC++2017依赖，部署OCR不再要求设备必须安装VC++2017运行库包

2.添加PaddleOCR源代码支持Cmake编译

3.优化PaddleOCR源代码支持Linux平台编译

4.优化代码，支持多线程异步代码调用。



### v2.0.3 （2022-07-20）

1.增加VC++2017环境检测与提示

2.增加CPU是否支持的环境检测与提示

### v2.0.2（2022-05-31）

1.修复nuget包默认为PP-OCRv3模型没有复制到输出目录的问题


### v2.0.1（2022-05-24）

1.优化OCR内存使用；

2.修复部分参数设置导致的错误；

3.优化nuget包默认为PP-OCRv3模型

### v1.3.1

1.修复IIS部署时依赖文件报找不到的问题。

2.优化nuget包打包设置，兼容新旧版nuget设置，不用修改设置，即装即用。

3.优化内存使用

4.参数类OCRParameter增加参数use_custom_model（默认关闭=0），用于控制是否使用自己训练的模型

5.编译生成带XML的API帮助文档

6.修复识别结果分值结果返回不正确的问题

### v1.3.0

1.PaddleOCR.dll增加DetectMat、DetectByte、DetectBase64接口

2.PaddleOCRSharp.dll增DetectTextBase64接口、DetectText重载（Byte数组参数）

3.优化.net传输图像数据不再落地生成文件。

4.小图缩放优化功能移到C++代码中。

5.优化PaddleOCRSharp.dll中的识别结果OCRResult类，满足可序列化需要。

6.添加更多框架支持。

7.默认关闭CPU加速开关，即Enable_mkldnn默认等于0.

### v1.2.2

1.增加net35;net48,net6框架支持

2.补齐参数类OCRParameter参数（对标官方PaddleOCR参数）[官方PaddleOCR参数](https://gitee.com/paddlepaddle/PaddleOCR/tree/release/2.4/deploy/cpp_infer)

3.增加纯检测接口DetectImage

4.增加检测得分结果输出

5.Demo增加显示识别结果的文字区域

### v1.2.0

1.调整模型加载模式，可以加载一次模型，多次识别；

2.增加参数类OCRParameter增加Enable_mkldnn参数，默认开启；

3.优化模型库路径可自定义修改位置；

4.增加C++demo示例项目

 **PaddleOCRHelper静态类的检测方法，将在下一版本移除，请使用PaddleOCREngine类代替。** 

### v1.1.0

1.移除命令行日志输出；

2.修复PaddleOCRSharp.dll文本识别不到报错的问题
