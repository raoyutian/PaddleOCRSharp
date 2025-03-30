## 文字识别
----

#### 1. 参数说明（OCRModelConfig模型配置对象）：

|属性名称|类型|默认值|说明|
|---|---|---|---|
|det_infer           |string  | |文本检测 det_infer模型路径|
|cls_infer            |string  | | 文本角度检测cls_infer模型路径|
|rec_infer           |string  |  |文本识别rec_infer模型路径|
|keys         |string   |   |  ppocr_keys.txt文件名全路径|

#### 2. 参数说明（oCRParameter参数对象）：

|属性名称|类型|默认值|说明|
|---|---|---|---|
|use_gpu           |bool  |false | 是否使用GPU，免费版此属性无效|
|gpu_id            |int   |false | GPU id，使用GPU时有效|
|gpu_mem           |int   |4000  |申请的GPU内存|
|cpu_math_library_num_threads         |int   |10    | CPU预测时的线程数，在机器核数充足的情况下，该值越大，预测速度越快|
|enable_mkldnn     |bool  |true | 是否使用mkldnn库，即CPU加速|
|det               |bool  |true | 是否执行文字检测，单行文本可以关闭该参数来提高速度|
|rec               |bool  |true | 是否执行文字识别|
|cls               |bool  |false | 是否执行文字方向分类|
|max_side_len        |int   |960 | 输入图像长宽大于960时，等比例缩放图像，使得图像最长边为960。适当调小可以加快速度，但影响精度|
|det_db_thresh         |float |0.3 | 用于过滤DB预测的二值化图像，设置为0.-0.3对结果影响不明显|
|det_db_box_thresh    |float |0.5 |  DB后处理过滤box的阈值，如果检测存在漏框情况，可酌情减小|
|det_db_unclip_ratio       |float |1.6 |  表示文本框的紧致程度，越小则文本框更靠近文本|
|use_dilation      |bool  |false  | 是否在输出映射上使用膨胀|
|det_db_score_mode |bool  |true | true:使用多边形框计算bbox score，false:使用矩形框计算。矩形框计算速度更快，多边形框对弯曲文本区域计算更准确|
|visualize         |bool  |false | 是否对结果进行可视化，为true时，预测结果会在当前目录下保存一个ocr_vis.png文件。默认false|
|use_angle_cls     |bool  |false | 是否使用方向分类器|
|cls_thresh        |float |0.9 | 方向分类器的得分阈值|
|cls_batch_num     |int   |1 | 方向分类器batchsize|
|rec_batch_num     |int   |6 | 识别模型batchsize，适当调大，可以加快速度|
|rec_img_h         |int   |48 | 识别模型输入图像高度，v2模型需要设置位32|
|rec_img_w         |int   |320 | 识别模型输入图像宽度|
|show_img_vis      |bool  |false | 是否显示预测结果|
|use_tensorrt      |bool  |false | 使用GPU预测时，是否启动tensorrt，默认false|

#### 3. 动态修改参数对象（ModifyParameter对象）：

|属性名称|类型|默认值|说明|
|---|---|---|---|
|m_det               |bool  |true | 动态修改是否检测。在OCRParameter.det=true时有效|
|m_rec               |bool  |true | 动态修改是否识别。在OCRParameter.rec=true时有效|
|m_max_side_len      |int |960 | 输入图像长宽大于960时，等比例缩放图像，使得图像最长边为960,；默认960，当m_det=true时有效|
|m_det_db_thresh         |float |0.3 | 用于过滤DB预测的二值化图像，设置为0.-0.3对结果影响不明显，当m_det=true时有效|
|m_det_db_box_thresh    |float |0.5 |  DB后处理过滤box的阈值，如果检测存在漏框情况，可酌情减小，当m_det=true时有效|
|m_det_db_unclip_ratio       |float |1.6 |  表示文本框的紧致程度，越小则文本框更靠近文本，当m_det=true时有效|



#### 4. 返回结果：

#### OCRResult对象

|属性名称|类型|说明|
|---|---|---|
|TextBlocks     |List<TextBlock>| 文本块TextBlock对象列表，由JsonText反序列化得到|
|Text           |string         | TextBlocks所有文本块的字符串拼接字符串，没有换行符|
|JsonText       |string         |非托管代码返回的原始json格式字符串|

#### TextBlock

|属性名称|类型|说明|
|---|---|---|
|BoxPoints     |List<OCRPoint>| 文本块四周点坐标列表，左上、右上、右下、左下四个点|
|Text          |string        | 文本块识别的文本|
|Score         |float         |文本识别置信度|
|cls_score     |float         |角度分类置信度,oCRParameter参数cls和use_angle_cls同时为true有效，否则返回0|
|cls_label     |float         |角度分类标签，oCRParameter参数cls和use_angle_cls同时为true有效，否则返回-1|

#### OCRPoint

|属性名称|类型|说明|
|---|---|---|
|X|int  | X坐标，单位像素|
|Y|int  | Y坐标，单位像素|




#### 5. OCR识别示例
```
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.*|*.bmp;*.jpg;*.jpeg;*.tiff;*.tiff;*.png";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            //使用默认中英文V4模型
            PaddleOCRSharp.OCRModelConfig config = null;
            //使用默认参数
            PaddleOCRSharp. OCRParameter oCRParameter = new PaddleOCRSharp.OCRParameter();
            //识别结果对象
            PaddleOCRSharp.OCRResult ocrResult = new PaddleOCRSharp.OCRResult();
            //建议程序全局初始化一次即可，不必每次识别都初始化，容易报错。     
            PaddleOCRSharp.PaddleOCREngine engine = new PaddleOCRSharp.PaddleOCREngine(config, oCRParameter);
            ocrResult = engine.DetectText(ofd.FileName);
            if (ocrResult != null) MessageBox.Show(ocrResult.Text, "识别结果");

```

#### 6. 如何使用自定义模型路径

```

 //自带轻量版中英文模型PP-OCRv4
 OCRModelConfig config = null;
 OCRParameter oCRParameter = new OCRParameter();

 //服务器中英文模型v2
 OCRModelConfig config = new OCRModelConfig();
 string modelPathroot = "你的模型绝对路径文件夹";
 config.det_infer = modelPathroot + @"\ch_ppocr_server_v2.0_det_infer";
 config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
 config.rec_infer = modelPathroot + @"\ch_ppocr_server_v2.0_rec_infer";
 config.keys = modelPathroot + @"\ppocr_keys.txt";

 //英文和数字模型v3
 OCRModelConfig config = new OCRModelConfig();
 string modelPathroot = "你的模型绝对路径文件夹";
 config.det_infer = modelPathroot + @"\en_PP-OCRv3_det_infer";
 config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
 config.rec_infer = modelPathroot + @"\en_PP-OCRv3_rec_infer";
 config.keys = modelPathroot + @"\en_dict.txt";

  //中英文模型V4
  config = new OCRModelConfig();
   string modelPathroot = "你的模型绝对路径文件夹";
  config.det_infer = modelPathroot + @"\ch_PP-OCRv4_det_infer";
  config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
  config.rec_infer = modelPathroot + @"\ch_PP-OCRv4_rec_infer";
  config.keys = modelPathroot + @"\ppocr_keys.txt";

  //服务器中英文模型V4
   config = new OCRModelConfig();
   string modelPathroot = "你的模型绝对路径文件夹";
   config.det_infer = modelPathroot + @"\ch_PP-OCRv4_det_server_infer";
   config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
   config.rec_infer = modelPathroot + @"\ch_PP-OCRv4_rec_server_infer";
   config.keys = modelPathroot + @"\ppocr_keys.txt";

   engine = new PaddleOCREngine(config, oCRParameter);

```

#### 6. 如何使用参数配置文件

```
//使用参数配置文件
//自定义模型路径
engine = new PaddleOCREngine(config, "");
//默认模型路径
engine = new PaddleOCREngine(null, "");
//将采用./inference/PaddleOCR.config.json

//自定义配置文件json
string json=File.ReadAllText(自定义配置文件路径)
engine = new PaddleOCREngine(null, json);


```


## 表格识别
----

#### 1. 表格识别参数说明（StructureModelConfig模型配置对象）：

|属性名称|类型|默认值|说明|
|---|---|---|---|
|det_infer           |string  | |文本检测 det_infer模型路径|
|cls_infer            |string  | | 文本角度检测cls_infer模型路径|
|rec_infer           |string  |  |文本识别rec_infer模型路径|
|keys                |string   |   |  ppocr_keys.txt文件名全路径|
|table_model_dir     |string   |   |  table_model_dir模型路径|
|table_char_dict_path|string   |   |  表格识别字典|


#### 2. 表格识别示例
```
public void Test()
{
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.*|*.bmp;*.jpg;*.jpeg;*.tiff;*.tiff;*.png";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            
            //模型配置，使用默认值
            StructureModelConfig structureModelConfig = null;
            
            //表格识别参数配置，使用默认值
            StructureParameter structureParameter = new StructureParameter();
            //初始化表格识别引擎
            PaddleStructureEngine engine = new PaddleOCRSharp.PaddleStructureEngine(null, structureParameter);
            //表格识别，返回结果是html格式的表格形式
            string result = engine.StructureDetectFile(ofd.FileName);
           
            //添加边框线，方便查看效果
            string css = "<style>table{ border-spacing: 0pt;} td { border: 1px solid black;}</style>";
            result = result.Replace("<html>", "<html>" + css);

            //保存到本地
            string name=Path.GetFileNameWithoutExtension(ofd.FileName);
            if (!Directory.Exists(Environment.CurrentDirectory + "\\out"))
            { Directory.CreateDirectory(Environment.CurrentDirectory + "\\out"); }
            string savefile = $"{Environment.CurrentDirectory}\\out\\{name}.html";
            File.WriteAllText(savefile, result);
           
            //打开网页查看效果
            Process.Start("explorer.exe", savefile);
 }

```