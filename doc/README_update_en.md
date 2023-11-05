
### v2.x.x （计划）

1.增加表格识别功能

2.增加给定图像的给定区域文本识别功能

3.

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
