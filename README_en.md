##  English |  [简体中文](https://gitee.com/raoyutian/paddle-ocrsharp/blob/master/README.md)|[Version update record](https://gitee.com/raoyutian/paddle-ocrsharp/blob/master/doc/README_update_en.md)

### If it's useful or enjoyable for you, then give it a star ☆. Thank you!

### The latest code is  in https://gitee.com/raoyutian/paddle-ocrsharp

## 1. Introduce


This project is a C + + code modification and encapsulation based on  Baidu [PaddleOCR](https://github.com/paddlepaddle/PaddleOCR) Net tool class library. It includes the table recognition function of text recognition, text detection and statistical analysis based on text detection results. At the same time, it is optimized to improve the recognition accuracy in the case of inaccurate small image recognition. It contains ultra lightweight Chinese OCR with a total model of only 8.6M size. The single model supports Chinese and English digit combination recognition, vertical text recognition and long text recognition. Support multiple text detection at the same time.

The project encapsulation is extremely simplified, and the actual call is only a few lines of code, which greatly facilitates the use of middle and downstream developers and reduces the entry level of paddleocr. At the same time, different functions are provided Net framework to facilitate application development and deployment in various industries. Nuget package is a high-precision Chinese and English OCR that can be installed and used immediately, can be deployed offline, and can be recognized without network. 

Paddleocr DLL file is a C++ dynamic library modified from the C++ code of the open source project [PaddleOCR](https://github.com/paddlepaddle/PaddleOCR) and compiled based on x64 of OpenCV.

This project can only be compiled and used on x64 CPU, so 32-bit is not supported.

Linux platform is not supported for the time being. If there are cross platform requirements, please refer to System.Drawing.dll, System.Drawing.Common.dll reference is deleted and recompiled.

The project currently supports the following net frameworks:

```
net35;net40;net45;net451;net452;net46;net461;net462;net47;net471;net472;net48;
netstandard2.0;netcoreapp3.1;
net5.0;net6.0;net7.0;

```

OCR recognition model library supports both official models and self-trained models. Bridge completely according to the  OCR interface.

The project deploys its own lightweight version of 8.6m model library and server version model library (more accurate and need to be downloaded). The model library can be changed to meet the actual needs.

[PaddleOCR models download address](https://gitee.com/paddlepaddle/PaddleOCR/blob/dygraph/doc/doc_ch/models_list.md)

If it needs to be modified into a server version model library, the reference code is as follows: (assuming that the server version model library is under the folder inforenceserver of the running directory)

```
OpenFileDialog ofd = new OpenFileDialog();
ofd.Filter = "*.*|*.bmp;*.jpg;*.jpeg;*.tiff;*.tiff;*.png";
if (ofd.ShowDialog() != DialogResult.OK) return;
var imagebyte = File.ReadAllBytes(ofd.FileName);
 Bitmap bitmap = new Bitmap(new MemoryStream(imagebyte));

 //With lightweight Chinese and English model
 // OCRModelConfig config = null;
 //Server Chinese and English model
 //OCRModelConfig config = new OCRModelConfig();
 //string root = Environment.CurrentDirectory;
 //string modelPathroot = root + @"\inferenceserver";
 //config.det_infer = modelPathroot + @"\ch_ppocr_server_v2.0_det_infer";
 //config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
 //config.rec_infer = modelPathroot + @"\ch_ppocr_server_v2.0_rec_infer";
 //config.keys = modelPathroot + @"\ppocr_keys.txt";

 //English and digital models
 OCRModelConfig config = new OCRModelConfig();
 string root = Environment.CurrentDirectory;
 string modelPathroot = root + @"\en";
 config.det_infer = modelPathroot + @"\ch_PP-OCRv2_det_infer";
 config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
 config.rec_infer = modelPathroot + @"\en_number_mobile_v2.0_rec_infer";
 config.keys = modelPathroot + @"\en_dict.txt";


 OCRParameter oCRParameter = new  OCRParameter ();
 OCRResult ocrResult = new OCRResult();

//It is suggested that the program can be initialized once globally. It is not necessary to initialize every time of identification, which is easy to report errors.  
 PaddleOCREngine engine = new PaddleOCREngine(config, oCRParameter);
  {
    ocrResult = engine.DetectText(bitmap );
  }
 if (ocrResult != null)
 {
    MessageBox.Show(ocrResult.Text,"Recognition results");
 }

//When OCR is no longer used, please release paddleocrengine

```

 [C++ Download address of prediction Library for windows](https://paddleinference.paddlepaddle.org.cn/user_guides/download_lib.html#windows)



[All  parameters](https://gitee.com/raoyutian/paddle-ocrsharp/blob/master/PaddleOCRSharp/OCRParameter.cs)

[PaddleOCR official  parameters](https://gitee.com/paddlepaddle/PaddleOCR/tree/release/2.4/deploy/cpp_infer)


## 2. Folder structure

```

PaddleOCRSharp               //.NET library
PaddleOCRDemo                //Demo folder
|--Cpp                       //Cpp folder
|--PaddleOCRCppDemo          //C++ demo
|--PaddleOCRSharpDemo        //.NET demo
|--Python                    //Python demo
|--Go                        //Go demo

```

## 3. Source code compilation

 **As for the source code compilation, it is recommended to use vs2022 version. If you cannot compile, please switch to release and then switch back to debug。** 

If you cannot compile due to the problem of framework compilation, please modify paddleocrsharp \ paddleocrsharp Csproj file, delete the frame not available on the current computer,
See Microsoft documentation for specific framework description[Target framework in SDK style project](https://docs.microsoft.com/zh-cn/dotnet/standard/frameworks)
```
 <TargetFrameworks>
net35;net40;net45;net451;net452;net46;net461;net462;net47;net471;net472;net48;
netstandard2.0;netcoreapp3.1;
net5.0;net6.0;net7.0;
</TargetFrameworks>
```

## 4. .NET example

```
  OpenFileDialog ofd = new OpenFileDialog();
  ofd.Filter = "*.*|*.bmp;*.jpg;*.jpeg;*.tiff;*.tiff;*.png";
  if (ofd.ShowDialog() != DialogResult.OK) return;
  var imagebyte = File.ReadAllBytes(ofd.FileName);
  Bitmap bitmap = new Bitmap(new MemoryStream(imagebyte));
  OCRModelConfig config = null;
  OCRParameter oCRParameter = new  OCRParameter ();

  OCRResult ocrResult = new OCRResult();

  //It is suggested that the program can be initialized once globally. It is not necessary to initialize every time of identification, which is easy to report errors。     
  PaddleOCREngine engine = new PaddleOCREngine(config, oCRParameter);
   {
    ocrResult = engine.DetectText(bitmap );
   }
 if (ocrResult != null)
 {
    MessageBox.Show(ocrResult.Text,"results");
 }

 

```

[C++example code](https://gitee.com/raoyutian/paddle-ocrsharp/blob/master/PaddleOCRDemo/PaddleOCRCppDemo/PaddleOCRCppDemo.cpp)


## 5. [Common problems and Solutions](https://gitee.com/raoyutian/paddle-ocrsharp/blob/master/doc/README_question_en.md)
---------------------------------------------------------------------------------------------------------------------
### If you like it, starred it.

### QQ group：318860399    
