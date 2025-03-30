using PaddleOCRSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PaddleOCRSharpDemo
{
    /// <summary>
    /// PaddleOCRSharp使用示例
    /// </summary>
    public partial class MainForm : Form
    {
        private string[] bmpFilters = new string[] { ".bmp", ".jpg", ".jpeg", ".tiff", ".tif", ".png" };
        private string fileFilter = "*.*|*.bmp;*.jpg;*.jpeg;*.tiff;*.tif;*.png";
        private string title = "PaddleOCR v{0} C# Demo  QQ群：318860399，定制开发QQ:277784829   by {1}";
        private PaddleOCREngine engine;
        private PaddleStructureEngine structengine;
        Bitmap bmp;
        OCRResult lastocrResult;
        string outpath = Path.Combine(Environment.CurrentDirectory, "out");
        DateTime dt1 = DateTime.Now;
        DateTime dt2 = DateTime.Now;
        public MainForm()
        {
            InitializeComponent();
            var fileVersion = Assembly.GetAssembly(typeof(PaddleOCREngine)).GetCustomAttribute<AssemblyFileVersionAttribute>();
            var company = Assembly.GetAssembly(typeof(PaddleOCREngine)).GetCustomAttribute<AssemblyCompanyAttribute>();
            this.Text = string.Format(title, fileVersion.Version, company.Company);
            imageView1.AllowDrop = true;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(outpath))
            { Directory.CreateDirectory(outpath); }

            //自带轻量版中英文模型V4模型
             OCRModelConfig config = null;

            //服务器中英文模型
            // OCRModelConfig config = new OCRModelConfig();
            //string root = PaddleOCRSharp.EngineBase.GetRootDirectory();
            //string modelPathroot = root + @"\inferenceserver";
            //config.det_infer = modelPathroot + @"\ch_ppocr_server_v2.0_det_infer";
            //config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
            //config.rec_infer = modelPathroot + @"\ch_ppocr_server_v2.0_rec_infer";
            //config.keys = modelPathroot + @"\ppocr_keys.txt";

            //英文和数字模型V3
            //OCRModelConfig config = new OCRModelConfig();
            //string root = PaddleOCRSharp.EngineBase.GetRootDirectory();
            //string modelPathroot = root + @"\en_v3";
            //config.det_infer = modelPathroot + @"\en_PP-OCRv3_det_infer";
            //config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
            //config.rec_infer = modelPathroot + @"\en_PP-OCRv3_rec_infer";
            //config.keys = modelPathroot + @"\en_dict.txt";

            //中英文模型V3
            //config = new OCRModelConfig();
            //string root = EngineBase.GetRootDirectory();
            //string modelPathroot = root + @"\inference";
            //config.det_infer = modelPathroot + @"\ch_PP-OCRv3_det_infer";
            //config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
            //config.rec_infer = modelPathroot + @"\ch_PP-OCRv3_rec_infer";
            //config.keys = modelPathroot + @"\ppocr_keys.txt";



            //初始化OCR引擎
            engine = new PaddleOCREngine(config, "");
           
             //模型配置，使用默认值
             StructureModelConfig structureModelConfig = null;
            //表格识别参数配置，使用默认值
            StructureParameter structureParameter = new StructureParameter();
            structengine =new PaddleStructureEngine(structureModelConfig, structureParameter);

        }
        private Bitmap GetClipboardImage()
        {
            bmp = (Bitmap)Clipboard.GetImage();
            if (bmp == null)
            {
                var files = Clipboard.GetFileDropList();

                string[] Filtersarr = new string[files.Count];
                files.CopyTo(Filtersarr, 0);
                Filtersarr = Filtersarr.Where(x => bmpFilters.Contains(Path.GetExtension(x).ToLower())).ToArray();
                if (Filtersarr.Length > 0)
                {
                    var imagebyte = File.ReadAllBytes(Filtersarr[0]);
                    bmp = new Bitmap(new MemoryStream(imagebyte));
                }
            }
            return bmp;
        }
        private void imageView1_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data;
            if (data == null) return;
            string[] files = data.GetData(DataFormats.FileDrop, autoConvert: true) as string[];

            string[] Filtersarr = new string[files.Count()];
            files.CopyTo(Filtersarr, 0);
            Filtersarr = Filtersarr.Where(x => bmpFilters.Contains(Path.GetExtension(x).ToLower())).ToArray();
            if (Filtersarr.Length > 0)
            {
                var imagebyte = File.ReadAllBytes(Filtersarr[0]);
                bmp = new Bitmap(new MemoryStream(imagebyte));
                imageView1.Image = bmp;

                richTextBox1.Clear();
                richTextBox1.Show();
                dataGridView1.Hide();
                if (bmp == null) return;
                dt1 = DateTime.Now;
                OCRResult ocrResult = engine.DetectText(imagebyte);
                dt2 = DateTime.Now;
                ShowOCRResult(ocrResult);

            }
        }
       
        
        private void imageView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
     
        private void imageView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        /// <summary>
        /// 打开本地图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripopenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = fileFilter;
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var imagebyte = File.ReadAllBytes(ofd.FileName);
            bmp = new Bitmap(new MemoryStream(imagebyte));
            imageView1.Image = bmp;

            richTextBox1.Clear();
            richTextBox1.Show();
            dataGridView1.Hide();
            if (bmp == null) return;

            dt1 = DateTime.Now;
            OCRResult ocrResult = engine.DetectText(imagebyte);
            dt2 = DateTime.Now;
            ShowOCRResult(ocrResult);

        }

        /// <summary>
        /// 识别截图文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            this.Hide();
            System.Threading.Thread.Sleep(200);
            Application.DoEvents();
            richTextBox1.Clear();
            richTextBox1.Show();
            dataGridView1.Hide();
            ScreenCapturer.ScreenCapturerTool screenCapturer = new ScreenCapturer.ScreenCapturerTool();
            if (screenCapturer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bmp = (Bitmap)screenCapturer.Image;
                imageView1.Image = bmp;
                try
                {
                    dt1 = DateTime.Now;
                    OCRResult ocrResult = engine.DetectText(bmp);
                    dt2 = DateTime.Now;
                    ShowOCRResult(ocrResult);
                }
                catch (Exception ex)
                {
                }

            }
            this.Show();
        }
        /// <summary>
        /// 剪切板识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void toolStripsnapocr_Click(object sender, EventArgs e)
        {
            bmp = GetClipboardImage();

            imageView1.Image = bmp;
            
                try
            {
                dt1 = DateTime.Now;
                OCRResult ocrResult = engine.DetectText(bmp);
                dt2 = DateTime.Now;
                ShowOCRResult(ocrResult);

                }
                catch (Exception ex)
                {
                }
            
        }
        /// <summary>
        /// 本地文件表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = fileFilter;
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var imagebyte = File.ReadAllBytes(ofd.FileName);
            bmp = new Bitmap(new MemoryStream(imagebyte));
            imageView1.Image = bmp;
            if (bmp == null) return;
            string  result = structengine.StructureDetect(bmp);
            ShowOCRResult(result, Path.GetFileNameWithoutExtension(ofd.FileName));
        }
        /// <summary>
        /// 识别截图表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            this.Hide();

            System.Threading.Thread.Sleep(200);
            Application.DoEvents();

            ScreenCapturer.ScreenCapturerTool screenCapturer = new ScreenCapturer.ScreenCapturerTool();
            if (screenCapturer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bmp = (Bitmap)screenCapturer.Image;
                imageView1.Image = bmp;
                string result = structengine.StructureDetect(bmp);
                ShowOCRResult(result, Path.GetRandomFileName());
                this.Show();
            }
        }
        /// <summary>
        /// 识别剪切板表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripsnaptable_Click(object sender, EventArgs e)
        {
            bmp = GetClipboardImage();

            imageView1.Image = bmp;
            if (bmp == null) return;
            string result = structengine.StructureDetect(bmp);
            ShowOCRResult(result, Path.GetRandomFileName());
        }
        /// <summary>
        /// 显示结果
        /// </summary>
        private void ShowOCRResult(OCRResult ocrResult)
        {
            lastocrResult = ocrResult;
            richTextBox1.Clear();
            Bitmap bitmap = (Bitmap)this.imageView1.Image;

            if (toolStripComboBox1.Text == "简单显示")
            {
                foreach (var item in ocrResult.TextBlocks)
                {
                    richTextBox1.AppendText(item.Text + "\n");

                }
            }
            else if (toolStripComboBox1.Text == "详细显示")
            {
                foreach (var item in ocrResult.TextBlocks)
                {
                    richTextBox1.AppendText(item.ToString() + "\n");
                }
            }

            try
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    foreach (var item in ocrResult.TextBlocks)
                    {
                        g.DrawPolygon(new Pen(Brushes.Red, 2), item.BoxPoints.Select(x => new PointF() { X = x.X, Y = x.Y }).ToArray());
                    }
                }
                imageView1.Image = null;
                imageView1.Image = bitmap;
            }
            catch (Exception ex)
            {
                Bitmap bmp = new Bitmap(bitmap);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    foreach (var item in ocrResult.TextBlocks)
                    {
                        g.DrawPolygon(new Pen(Brushes.Red, 2), item.BoxPoints.Select(x => new PointF() { X = x.X, Y = x.Y }).ToArray());
                    }
                }
                imageView1.Image = null;
                imageView1.Image = bmp;
            }
           

            richTextBox1.AppendText("-----------------------------------\n");
            richTextBox1.AppendText($"耗时：{(dt2 - dt1).TotalMilliseconds}ms\n");
            
        }
        /// <summary>
        /// 显示表格结果
        /// </summary>
        private void ShowOCRResult(string result,string name)
        {
            string css = "<style>table{ border-spacing: 0pt;} td { border: 1px solid black;}</style>";
            result = result.Replace("<html>", "<html>" + css);
            string savefile = $"{Environment.CurrentDirectory}\\out\\{name}.html";
            File.WriteAllText(savefile, result);
            //打开网页查看效果
            Process.Start("explorer.exe", savefile);
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lastocrResult != null) ShowOCRResult(lastocrResult);
        } 
    }
}
