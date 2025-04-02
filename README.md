### 简体中文 | [English](https://gitee.com/raoyutian/PaddleOCRSharp/blob/master/README_en.md)     |[更新记录](https://gitee.com/raoyutian/PaddleOCRSharp/blob/master/doc/README_update.md)


#### 如果项目对你有用或者喜欢，那就点个赞&#9733; 。谢谢！

## 介绍
-----
   **PaddleOCRSharp** 是一个.NET版本OCR可离线使用类库。项目核心组件PaddleOCR.dll目前已经支持C\C++、.NET、Python、Golang、Rust、java等众多开发语言的直接API接口调用。项目包含文本识别、文本检测、表格识别功能。本项目做了大量优化，提高了识别率和推理性能。包含总模型仅8.6M的超轻量级中文OCR，单模型支持中英文数字组合识别、竖排文本识别、长文本识别。同时支持中英文、纯英文以及多种语言文本检测识别。

**PaddleOCRSharp**封装极其简化，实际调用仅几行代码，极大的方便了中下游开发者的使用和降低了PaddleOCR的使用入门级别，同时提供不同的.NET框架使用，方便各个行业应用开发与部署。Nuget包即装即用，可以离线部署，不需要网络就可以识别的高精度中英文OCR。  

本项目支持官方所有公开的通用OCR模型，如：PPOCRV2、PPOCRV3、PPOCRV4、PP-OCRv4_server、PP-OCRv4_server_doc（1.5万字符字典模型）。最新版默认使用中英文V4模型（ch_PP-OCRv4）：

&#9733;windows系统支持:win7SP1、win10、win11、winserver2012R2、winserver2016、winserver2019、winserver2022等。免费版支持在x86的CPU上使用，CPU指令集需要包含AVX指令集。

&#9733;windows下版本有免费开源版、CPU加速版(付费版)、GPU加速版(付费版)

&#9733;linux系统支持(付费版)：统信UOS、麒麟、ubuntu、CentOS8等绿色离线部署，支持docker部署。支持国产CPU如华为鲲鹏、飞腾、海光、兆芯等CPU。

本项目目前支持以下.NET框架（linux版本仅支持net6.0及以上框架）：

```
net35;net40;net45;net451;net452;net46;net461;net462;net47;net471;net472;net48;net481;
netstandard2.0;netcoreapp3.1;
net5.0;net6.0;net7.0;net8.0;net9.0

```

## 特点

&#9733; $\color{#0000FF}{高度集成}$：**PaddleOCRSharp**将百度飞桨PaddleOCR的核心功能完美集成到.NET平台，让开发者无需关心底层实现，只需调用相应接口即可实现OCR功能。

&#9733; $\color{#0000FF}{性能卓越}$：得益于百度飞桨PaddleOCR的高效算法和对PaddleOCR代码的部分算法优化，**PaddleOCRSharp**在保持高度集成的同时，也保证了卓越的性能表现。

&#9733; $\color{#0000FF}{易于使用}$：**PaddleOCRSharp**提供了丰富的API接口和详细的文档说明，让开发者能够轻松上手，快速实现OCR功能。

&#9733; $\color{#0000FF}{扩展性强}$：**PaddleOCRSharp**支持自定义模型加载和训练，开发者可以根据自己的需求进行模型扩展和优化。

&#9733; $\color{#FF0000}{离线免费}$：**PaddleOCRSharp**支持离线绿色部署，无其他依赖需要安装，满足了众多开发者的福音。

## 应用场景
**PaddleOCRSharp**适用于各种需要OCR技术的.NET开发场景，如文档数字化处理、自动识别表单数据、车牌识别等。无论是企业级应用还是个人开发者，**PaddleOCRSharp**都能提供强大的OCR支持。

## 如何使用

本项目提供了两个SDK，一个是C++版本，一个是.net版本，.net版本是对C++版本的二次封装，其他语言开发亦是调用C++版本。同时也提供了Go、Python、C++的调用示例代码

[博客园文章：NET框架下如何使用PaddleOCRSharp](https://www.cnblogs.com/raoyutian/p/15912470.html)

#### 1.文件夹结构

```
PaddleOCRSharp      //该文件夹包含.NET对PaddleOCR封装类库项目
Demo                //该文件夹包含OCR示例Demo文件夹
|--Cpp              //C++调用示例项目
|--CSharp           //.NET调用示例项目
|--python           //python调用示例项目
|--Go               //Golang调用示例项目
|--win_runtime_x64  // windows平台全部依赖文件
```
 
#### 2. .NET版编译

[.NET版编译](https://gitee.com/raoyutian/PaddleOCRSharp/blob/master/doc/Csharp.md) 


## 使用与部署
------

#### 1. [C++使用PaddleOCR](https://gitee.com/raoyutian/PaddleOCRSharp/tree/master/Demo/Cpp) 

#### 2. [.NET使用PaddleOCRSharp](https://gitee.com/raoyutian/PaddleOCRSharp/blob/master/doc/UseInCsharp.md) 

#### 3. [python使用PaddleOCR](https://gitee.com/raoyutian/PaddleOCRSharp/blob/master/Demo/python/PaddleOCRCppPython.py) 

#### 4. [Go使用PaddleOCR](https://gitee.com/raoyutian/PaddleOCRSharp/blob/master/Demo/Go/PaddleOCRgo.go) 

#### 5. [Rust使用PaddleOCR](https://gitee.com/toobo/PaddleOCRRust) 


## OCR模型
------
OCR识别模型库支持官方所有的模型，也支持自己训练的模型。完全按照飞桨OCR接口搭桥。
本项目部署自带的一种轻量版8.6M模型库、服务器版模型库（更准确，需要自行下载），可以自行更改模型库适用实际需求。

|模型名称|模型大小|下载地址|备注|
|---|---|---|---|
|ch_PP-OCRv2  |10M  |[中英文轻量v2](https://gitee.com/raoyutian/PaddleOCRSharp/raw/master/models/PP-OCRv2/inference.zip)  | |
|en_PP-OCRv2  |4M   |[英文数字v2](https://gitee.com/raoyutian/PaddleOCRSharp/raw/master/models/PP-OCRv2/en.zip)  |  |
|ch_PP-OCRv3  |12M  |[中英文轻量v3](https://gitee.com/raoyutian/PaddleOCRSharp/raw/master/models/PP-OCRv3/inference_v3.zip)|   |
|en_PP-OCRv3  |10M  |[英文数字v3](https://gitee.com/raoyutian/PaddleOCRSharp/raw/master/models/PP-OCRv3/en_v3.zip)|   |
|ch_PP-OCRv4  |14M  |[中英文轻量v4](https://gitee.com/raoyutian/PaddleOCRSharp/raw/master/models/PP-OCRv4/ch_PP-OCRv4.zip)|   |
|en_PP-OCRv4  |12M  |[英文数字v4](https://gitee.com/raoyutian/PaddleOCRSharp/raw/master/models/PP-OCRv4/en_PP-OCRv4.zip)|   |


## PaddleOCRSharp适合哪些场景

PaddleOCRSharp主要应用场景：

 **文档数字化处理：** 
对于大量的纸质文档，PaddleOCRSharp可以快速地将其转化为电子文档，方便存储、检索和编辑。这在企业级应用中尤为重要，如图书馆、档案馆、政府部门等需要对大量文档进行数字化处理的场景。

 **自动识别表单数据：** 
在需要自动化处理表单数据的场景中，PaddleOCRSharp可以识别表单中的文字信息，并将其转化为结构化数据。这大大提高了数据录入的效率和准确性，特别适用于银行、保险、医疗等行业需要处理大量表单数据的场景。

 **车牌识别：** 
PaddleOCRSharp也支持车牌识别功能，可以准确地识别出车辆的车牌号码。这对于交通管理、停车场管理、安防监控等场景非常有用，可以帮助实现车辆的快速识别和追踪。

 **图像文字提取：** 
在需要从图像中提取文字信息的场景中，如从截图、图片或PDF文件中提取文字，PaddleOCRSharp都能提供高精度的识别结果。这对于研究人员、学者、学生等需要处理大量图像文字信息的用户来说非常便捷。

 **多语言识别：** 
PaddleOCRSharp支持多种语言的识别，包括中文、英文等常用语言。这使得它可以在国际化的应用场景中发挥重要作用，如跨境电商、国际交流等领域。

 **定制化需求：** 
对于有特殊需求的用户，PaddleOCRSharp提供了丰富的接口和模型库，可以根据具体需求进行定制化和扩展。这为用户提供了更多的灵活性和可能性，可以满足不同场景下的特定需求。
PaddleOCRSharp凭借其强大的OCR功能和广泛的应用场景，成为了.NET开发者在处理OCR任务时的有力工具。无论是企业级应用还是个人开发者，都可以通过PaddleOCRSharp快速实现OCR功能，提高工作效率和数据处理的准确性。

## 常见问题与解决方案

[常见问题与解决方案](https://gitee.com/raoyutian/PaddleOCRSharp/blob/master/doc/README_question.md)


##  技术交流方式
------
#### QQ技术交流群：318860399
#### 微信公众号：明月心技术学堂。
![输入图片说明](doc/%E5%85%AC%E4%BC%97%E5%8F%B7%E4%BA%8C%E7%BB%B4%E7%A0%81.jpg)
![输入图片说明](doc/%E6%98%8E%E6%9C%88%E5%BF%83%E6%8A%80%E6%9C%AF%E5%AD%A6%E5%A0%82%E7%BE%A4%E8%81%8A%E4%BA%8C%E7%BB%B4%E7%A0%81.png)
#### [个人博客地址： https://www.cnblogs.com/raoyutian/]( https://www.cnblogs.com/raoyutian/)
-----
#### 定制开发联系QQ：277784829
-----

-----
###  如果您在工作中受益于该项目，请考虑下支持我的工作。
![输入图片说明](doc/%E8%B5%9E%E8%B5%8F%E7%A0%81.jpg)
