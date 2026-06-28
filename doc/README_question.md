
#### Q： PaddleOCRSharp是否支持linux?

PaddleOCRSharp从4.4.0版本开始，支持跨平台linux。

#### Q： win7与Windows server部署相关问题？

.net6、net7、net core3.1部署，环境依赖参见微软的[在Windows上安装.NET](https://learn.microsoft.com/zh-cn/dotnet/core/install/windows?tabs=net60)

#### Q： 是否支持多线程调用？

支持多线程排队。OCR内部已经有多线程预测机制。

#### Q： 项目是否支持x86_32？ 

本项目只支持x86_64位程序;实测32位库速度相当慢。如确实需要，请构建64位其他服务模式或者web模式，然后32位程序调用。

#### Q： Web IIS部署问题

IIS部署情况下需要删除 web.config文件中 hostingModel="inprocess"这个配置。
使用IIS调试，需要设置“进程外”模式，IIS需要设置为64位


#### Q： Web IIS部署遇到错误

```
Initialize err:
--------------------------------------
C++ Traceback (most recent call last):
--------------------------------------
Not support stack backtrace yet.

----------------------
Error Message Summary:
----------------------
PreconditionNotMetError: The third-party dynamic library (mklml.dll) that Paddle depends on is not configured correctly. (error code is 126)
  Suggestions:
  1. Check if the third-party dynamic library (e.g. CUDA, CUDNN) is installed correctly and its version is matched with paddlepaddle you installed.
  2. Configure third-party dynamic library environment variables as follows:
  - Linux: set LD_LIBRARY_PATH by `export LD_LIBRARY_PATH=...`
  - Windows: set PATH by `set PATH=XXX; (at ...\paddle\phi\backends\dynload\dynamic_loader.cc:312)
```

IIS部署情况下需要删除 web.config文件中 hostingModel="inprocess"这个配置。
使用IIS调试，需要设置“进程外”模式


#### Q：找不到依赖项或无法加载依赖项：PaddleOCR.dll

请使用CPU工具检查当前CPU是否支持。
如果支持，则检查对应文件是否存在。
如果存在，则调试检查程序加载dll的目录是否与PaddleOCR.dll目录一致。
默认使用：
```
 string root = AppDomain.CurrentDomain.BaseDirectory；
#if NET461_OR_GREATER || NETCOREAPP || NET6_0_OR_GREATER
            root = AppContext.BaseDirectory;
#endif
```


#### Q： Windows server2012R2 操作系统上使用问题

进QQ群下载补丁：PaddleOCRSharp免安装VC++2017依赖文件_win server2012

#### Q：  Windows server2008R2 操作系统上使用问题

需要安装VC++2017运行时，可以安装vc++2015-2019运行时合集

#### Q： Windows7 操作系统上使用问题

需要安装VC++2017运行时，可以安装vc++2015-2019运行时合集

