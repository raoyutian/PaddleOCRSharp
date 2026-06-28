Imports System.Drawing
Imports System.IO
Imports PaddleOCRSharp
Module PaddleOCRVBNETDemo
    Dim engine As PaddleOCREngine = Nothing

    Sub Main()

        Dim imageroot As String = Path.Combine(Environment.CurrentDirectory, "image")
        If (Not Directory.Exists(imageroot)) Then Directory.CreateDirectory(imageroot)
        Dim directoryInfo As DirectoryInfo = New DirectoryInfo(imageroot)
        Dim files = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories)
        If (files.Length = 0) Then
            Console.WriteLine("image文件夹内没有待识别的图片文件")
            Console.ReadKey()
            Return
        End If



        Console.OutputEncoding = System.Text.Encoding.Unicode

        If (False) Then
            '自定义模型路径,v5模型为例
            Dim config As New OCRModelConfig()
            Dim root As String = PaddleOCREngine.GetRootDirectory()
            config.det_infer = Path.Combine(root, "inference", "PP-OCRv5_mobile_det_infer")
            config.rec_infer = Path.Combine(root, "inference", "PP-OCRv5_mobile_rec_infer")
            config.cls_infer = Path.Combine(root, "inference", "ch_ppocr_mobile_v5.0_cls_infer")
            config.keys = Path.Combine(root, "inference", "ppocr_keys.txt")

            '自定义OCR参数
            Dim parameter As New OCRParameter()
            engine = New PaddleOCREngine(config, parameter)
        Else
            '使用缺省参数V5模型，默认读配置文件参数:根目录下inference\PaddleOCR.config.json
            engine = New PaddleOCREngine(Nothing, "")
        End If

        Dim result As OCRResult = Nothing

        For Each f In files

            Dim bytes = File.ReadAllBytes(f.FullName)
            Dim bitmap = New Bitmap(New MemoryStream(bytes))
            Try
                result = engine.DetectText(f.FullName)
            Catch ex As Exception
                Console.WriteLine($"遇到错误：{ex.Message}")
            End Try

            Dim info = ""
            For Each tb In result.TextBlocks
                info += tb.Text + vbCrLf
            Next
            Console.WriteLine(info)
        Next
        GC.Collect()
        Console.ReadKey()
    End Sub
End Module
