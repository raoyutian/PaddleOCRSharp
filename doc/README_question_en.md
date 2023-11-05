#### Q： 是否支持多线程调用？

不支持多线程、并发。OCR内部已经有多线程预测机制。

#### Q： 项目是否支持x86？（32位操作系统）

本项目只支持X64位程序;
如确实需要，请构建64位其他服务模式或者web模式，然后32位程序调用。

#### Q： Web IIS部署问题

部署遇到错误：could not execute a primitive
CPU加速则会遇到，目前官方bug。关闭cpu加速    oCRParameter.Enable_mkldnn = 0;
部署IIS情况下需要删除 web.config文件中 hostingModel="inprocess"这段。
使用IIS调试，需要设置“进程外”模式


#### Q：找不到依赖项或无法加载依赖项：PaddleOCR.dll

本项目只支持X64位程序，请编译时注意切换，请确保运行目录下存在一下dll文件，如果不存在，建议手动复制到输出目录。
```
|--libiomp5md.dll            //第三方引用库
|--mkldnn.dll                //第三方引用库
|--mklml.dll                 //第三方引用库
|--opencv_world411.dll       //第三方引用库
|--paddle_inference.dll      //飞桨库
|--PaddleOCR.dll  

本项目依赖VC++2017X64运行库，请检查机器上是否安装VC++依赖库，2.0.4及以上版本，免安装VC++2017X64运行库

```

#### Q： Windows server 操作系统上使用，OCR崩溃，或者找不到依赖项或无法加载依赖项：PaddleOCR.dll

因PaddleOCR引用了Opencv,在windows server 上 使用opencv出现 DLL load failed错误,发现缺失部分dll：MFPlat.dll、MF.dll、MFReadWrite.dll等等，原因：服务器版本默认没有安装windows media player。
使用如下步骤安装windows media player：
1）、打开“服务器管理器”，点击【添加角色和功能】，然后下一步，直到【功能】界面；
2）、勾选【媒体基础】、【墨迹和手写服务】，【用户界面和基础结构】下的【桌面体验】
3）、单击【安装】按钮；等安装完毕后，根据提示重新启动计算机即可。


#### Q： 内存增加严重

net项目中注意识别完后的图像内存清理。另外，使用CPU加速也会导致内存上升，如有顾虑，建议关闭CPU加速参数，
```
oCRParameter.Enable_mkldnn = 0

```
