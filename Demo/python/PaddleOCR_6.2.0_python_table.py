# -*- coding: UTF-8 -*-
import os
import ctypes
from ctypes import *
import json
from datetime import datetime

#判断操作系统
if os.name == 'posix':
   paddleOCR=cdll.LoadLibrary("./lib/PaddleOCR.so")
   print("will run in linux")
elif os.name == 'nt':
   paddleOCR=cdll.LoadLibrary(".\PaddleOCR.dll")
   print("will run in windows")
encode="gbk" 
root="./"

def main():

    print("请选择模型:\n0=CnV5;\n1=EnV5;\n2=CnServerV5;\n3=V6_tiny;\n4=V6_small;\n5=V6_medium;\n")
    modelnum = int(input("输入数字并回车确认：\n"))
    print("请选择方式:\n0=纯文本结果;    \n1=json结果; ")
    num = int(input("输入数字并回车确认：\n"))
   
    #模型路径可以指定为其他位置

    if (modelnum == 0):
  
        cls_infer =root+ "/inference/PP-OCRv5_mobile_cls_infer"
        det_infer =root+ "/inference/PP-OCRv5_mobile_det_infer"
        rec_infer=root+ "/inference/PP-OCRv5_mobile_rec_infer"
        ocrkeys =root+ "/inference/ppocr_keys.txt"#新格式模型该字段用不到，不为空即可
        print("当前模型：PP-OCRV5\n")
   
    elif (modelnum == 1):
  
        cls_infer =root+ "/inference/PP-OCRv5_mobile_cls_infer"
        det_infer =root+ "/inference/PP-OCRv5_mobile_det_infer"
        rec_infer=root+ "/inference/en_PP-OCRv5_mobile_rec_infer"
        ocrkeys =root+ "/inference/ppocr_keys.txt"#新格式模型该字段用不到，不为空即可
        print("当前模型：en_PP-OCRV5\n")
   
    elif (modelnum == 3):
  
        cls_infer =root+ "/inference/PP-OCRv5_mobile_cls_infer"
        det_infer =root+ "/inference/PP-OCRv6_tiny_det_infer"
        rec_infer=root+ "/inference/PP-OCRv6_tiny_rec_infer"
        ocrkeys =root+ "/inference/ppocr_keys.txt"#新格式模型该字段用不到，不为空即可
        print("当前模型：V6_tiny\n")


    elif (modelnum == 4):
  
        cls_infer =root+ "/inference/PP-OCRv5_mobile_cls_infer"
        det_infer =root+ "/inference/PP-OCRv6_small_det_infer"
        rec_infer=root+ "/inference/PP-OCRv6_small_rec_infer"
        ocrkeys =root+ "/inference/ppocr_keys.txt"#新格式模型该字段用不到，不为空即可
        print("当前模型：V6_small\n")

    p_cls_infer=cls_infer.encode(encode)
    p_rec_infer=rec_infer.encode(encode)
    p_det_infer=det_infer.encode(encode)
    p_ocrkeys=ocrkeys.encode(encode)

    configfile = root+"/inference/PaddleOCR.config.json"
    with open(configfile, 'r', encoding='utf-8') as file:
   	    parameterjson = file.read()
    #parameterjson为空字符串，将采用默认值
    paddleOCR.Initializejson.restype = ctypes.c_void_p
    ptr=paddleOCR.Initializejson( p_det_infer,  p_cls_infer,  p_rec_infer,  p_ocrkeys, parameterjson.encode(encode))
	
    paddleOCR.EnableJsonResult(ctypes.c_void_p(ptr),False)
    if (num == 1):
	    paddleOCR.EnableJsonResult(ctypes.c_void_p(ptr),True)

    if os.name == 'posix':
        #linux采用单字节ANSI编码
        paddleOCR.Detect.restype = ctypes.c_char_p
    elif os.name == 'nt':
        #windows采用单字节宽字节编码
        paddleOCR.Detect.restype = ctypes.c_wchar_p

    imagepath=os.path.abspath('.')+"/image/"
    imagefiles=os.listdir(imagepath)
    count=1
    whilecount=1
    timeall=0
    if (num == 2):
        whilecount=10
        
    for i in range(whilecount):
        for image in imagefiles:
            imagefile=imagepath+image
            t1= datetime.now()
             
            if os.name == 'posix':
                 #linux采用单字节ANSI编码返回结果，并解码utf8用于显示
                result= paddleOCR.Detect(ctypes.c_void_p(ptr),imagefile.encode(encode)).decode("utf-8")
            elif os.name == 'nt':
                #windows采用单字节宽字节编码
                result= paddleOCR.Detect(ctypes.c_void_p(ptr),imagefile.encode(encode))
            t2=datetime.now()
            c=t2-t1
            timeall+=c.total_seconds()*1000
            print("--",count,"-----耗时:【",c.total_seconds()*1000,"】ms,文件名【",image,"】-----")
            print(result)
            count=count+1
    print("total times：",timeall)
def table():
   
    #模型路径可以指定为其他位置

    cls_infer =root+ "/inference/PP-OCRv5_mobile_cls_infer"
    det_infer =root+ "/inference/PP-OCRv6_small_det_infer"
    rec_infer=root+ "/inference/PP-OCRv6_small_rec_infer"
    ocrkeys =root+ "/inference/ppocr_keys.txt"#新格式模型该字段用不到，不为空即可
    table_model_dir=root+ "/inference/yt_SLANet_plus_infer"
    table_char_dict_path=root+ "/inference/table_structure_dict_ch.txt"#新格式模型该字段用不到，不为空即可
    p_rec_infer=rec_infer.encode(encode)
    p_det_infer=det_infer.encode(encode)
    p_ocrkeys=ocrkeys.encode(encode)
    p_table_model=table_model_dir.encode(encode)
    p_table_char_dict=table_char_dict_path.encode(encode)

    configfile = root+"/inference/PaddleOCRStructure.config.json"
    with open(configfile, 'r', encoding='utf-8') as file:
   	    parameterjson = file.read()
    #parameterjson为空字符串，将采用默认值
    paddleOCR.StructureInitializejson.restype = ctypes.c_bool
    sucess=paddleOCR.StructureInitializejson( p_det_infer,  p_rec_infer,  p_ocrkeys,p_table_model,p_table_char_dict, parameterjson.encode(encode))
    if os.name == 'posix':
        #linux采用单字节ANSI编码
        paddleOCR.GetStructureDetectFile.restype = ctypes.c_char_p
    elif os.name == 'nt':
        #windows采用单字节宽字节编码
        paddleOCR.GetStructureDetectFile.restype = ctypes.c_wchar_p

    imagepath=os.path.abspath('.')+"/image/"
    imagefiles=os.listdir(imagepath)
    count=1
    timeall=0 
  
    for image in imagefiles:
        imagefile=imagepath+image
        t1= datetime.now()
             
        if os.name == 'posix':
            #linux采用单字节ANSI编码返回结果，并解码utf8用于显示
            result= paddleOCR.GetStructureDetectFile(imagefile.encode(encode)).decode("utf-8")
        elif os.name == 'nt':
            #windows采用单字节宽字节编码
            result= paddleOCR.GetStructureDetectFile(imagefile.encode(encode))
        t2=datetime.now()
        c=t2-t1
        timeall+=c.total_seconds()*1000
        print("--",count,"-----耗时:【",c.total_seconds()*1000,"】ms,文件名【",image,"】-----")
        print(result)
        count=count+1
    print("total times：",timeall)
    
if __name__=="__main__":
    print("请选择:\n0=OCR;\n1=table\n")
    module= int(input("输入数字并回车确认：\n"))
    if module==0:
        main()
    elif module==1:
        table()
    print("***************************end*************************")
    #防止直接退出
    input() 