import os
import ctypes
from ctypes import *
import json
from datetime import datetime
import numpy as np

class Parameter(Structure):
	_fields_ =[
		("use_gpu",c_bool),
		("gpu_id",c_int),
		("gpu_mem",c_int),
		("cpu_math_library_num_threads",c_int),
		("enable_mkldnn",c_bool),
		("det",c_bool),
		("rec",c_bool),
		("cls",c_bool),
		("max_side_len",c_int),
		("det_db_thresh",c_float),
		("det_db_box_thresh",c_float),
	    ("det_db_unclip_ratio",c_float),
		("use_dilation",c_bool),
		 
		("det_db_score_mode",c_bool),
		("visualize",c_bool),
		("use_angle_cls",c_bool),
		("cls_thresh",c_float),
		("cls_batch_num",c_int),
		("rec_batch_num",c_int),
		("rec_img_h",c_int),
		("rec_img_w",c_int),
		("show_img_vis",c_bool),
	    ("use_tensorrt",c_bool)
	  
		]
	def __init__(self):
		self.use_gpu =False
		self.gpu_id = 0
		self.gpu_mem = 4000
		self.cpu_math_library_num_threads = 10
		self.enable_mkldnn = True
		self.det=True
		self.rec=True
		self.cls=False
		self.max_side_len = 960
		self.det_db_thresh = 0.3
		self.det_db_box_thresh = 0.618
		self.det_db_unclip_ratio = 1.6
		self.use_dilation = False
		self.det_db_score_mode = True
		self.visualize = False
		self.use_angle_cls = False
		self.cls_thresh = 0.9
		self.cls_batch_num = 1
		self.rec_batch_num = 6
		self.rec_img_h = 48
		self.rec_img_w = 320
		self.show_img_vis = False
		self.use_tensorrt = False
		
def Parameter2dict(param):
    return {
		"use_gpu":param.use_gpu,
		"gpu_id":param.gpu_id,
		"gpu_mem":param.gpu_mem,
		"cpu_math_library_num_threads":param.cpu_math_library_num_threads,
		"enable_mkldnn":param.enable_mkldnn,
		"det":param.det,
		"rec":param.rec,
		"cls":param.cls,
		"max_side_len":param.max_side_len,
		"det_db_thresh":param.det_db_thresh,
		"det_db_box_thresh":param.det_db_box_thresh,
		"det_db_unclip_ratio":param.det_db_unclip_ratio,
		"use_dilation":param.use_dilation,
		"det_db_score_mode":param.det_db_score_mode,
		"visualize":param.visualize,
		"use_angle_cls":param.use_angle_cls,
		"cls_thresh":param.cls_thresh,
		"cls_batch_num":param.cls_batch_num,
		"rec_batch_num":param.rec_batch_num,
		"rec_img_h":param.rec_img_h,
		"rec_img_w":param.rec_img_w,
		"show_img_vis":param.show_img_vis,
		"use_tensorrt":param.use_tensorrt ,
		
    }

class StructureParameter(Structure):
	_fields_ =[
		("use_gpu",c_bool),
		("gpu_id",c_int),
		("gpu_mem",c_int),
		("cpu_math_library_num_threads",c_int),
		("enable_mkldnn",c_bool),
		("det",c_bool),
		("rec",c_bool),
		("cls",c_bool),
		("max_side_len",c_int),
		("det_db_thresh",c_float),
		("det_db_box_thresh",c_float),
	    ("det_db_unclip_ratio",c_float),
		("use_dilation",c_bool),
		 
		("det_db_score_mode",c_bool),
		("visualize",c_bool),
		("use_angle_cls",c_bool),
		("cls_thresh",c_float),
		("cls_batch_num",c_int),
		("rec_batch_num",c_int),
		("rec_img_h",c_int),
		("rec_img_w",c_int),
		("show_img_vis",c_bool),
	    ("use_tensorrt",c_bool),
		("table_max_len",c_int),
	    ("merge_no_span_structure",c_bool),
		("table_batch_num",c_int),
		]
	def __init__(self):
		self.use_gpu =False
		self.gpu_id = 0
		self.gpu_mem = 4000
		self.cpu_math_library_num_threads = 10
		self.enable_mkldnn = True
		self.det=True
		self.rec=True
		self.cls=False
		self.max_side_len = 960
		self.det_db_thresh = 0.3
		self.det_db_box_thresh = 0.618
		self.det_db_unclip_ratio = 1.6
		self.use_dilation = False
		self.det_db_score_mode = True
		self.visualize = False
		self.use_angle_cls = False
		self.cls_thresh = 0.9
		self.cls_batch_num = 1
		self.rec_batch_num = 6
		self.rec_img_h = 48
		self.rec_img_w = 320
		self.show_img_vis = False
		self.use_tensorrt = False
		self.table_max_len = 488
		self.merge_no_span_structure = True
		self.table_batch_num = 1

def StructureParameter2dict(param):
    return {
		"use_gpu":param.use_gpu,
		"gpu_id":param.gpu_id,
		"gpu_mem":param.gpu_mem,
		"cpu_math_library_num_threads":param.cpu_math_library_num_threads,
		"enable_mkldnn":param.enable_mkldnn,
		"det":param.det,
		"rec":param.rec,
		"cls":param.cls,
		"max_side_len":param.max_side_len,
		"det_db_thresh":param.det_db_thresh,
		"det_db_box_thresh":param.det_db_box_thresh,
		"det_db_unclip_ratio":param.det_db_unclip_ratio,
		"use_dilation":param.use_dilation,
		"det_db_score_mode":param.det_db_score_mode,
		"visualize":param.visualize,
		"use_angle_cls":param.use_angle_cls,
		"cls_thresh":param.cls_thresh,
		"cls_batch_num":param.cls_batch_num,
		"rec_batch_num":param.rec_batch_num,
		"rec_img_h":param.rec_img_h,
		"rec_img_w":param.rec_img_w,
		"show_img_vis":param.show_img_vis,
		"use_tensorrt":param.use_tensorrt, 
		"table_max_len":param.table_max_len,
		"merge_no_span_structure":param.merge_no_span_structure,
		"table_batch_num":param.table_batch_num 
    }


paddleOCR=cdll.LoadLibrary(".\PaddleOCR.dll")
encode="gbk" 

root="./"
cls_infer =root+"/inference/ch_ppocr_mobile_v2.0_cls_infer"
rec_infer = root+"/inference/ch_PP-OCRv4_rec_infer"
det_infer = root+"/inference/ch_PP-OCRv4_det_infer"
ocrkeys = root+"/inference/ppocr_keys.txt"
parameter=Parameter()
p_cls_infer=cls_infer.encode(encode)
p_rec_infer=rec_infer.encode(encode)
p_det_infer=det_infer.encode(encode)
p_ocrkeys=ocrkeys.encode(encode)

def main():
    parameterjson= json.dumps(parameter,default=Parameter2dict)
    #禁用json结果返回格式
    paddleOCR.EnableJsonResult(False)
    paddleOCR.Initializejson( p_det_infer,  p_cls_infer,  p_rec_infer,  p_ocrkeys, parameterjson.encode(encode))
    result=""
    paddleOCR.Detect.restype = ctypes.c_wchar_p
    imagepath=os.path.abspath('.')+"\\image\\"
    imagefiles=os.listdir(imagepath)
    total=[]
    for image in imagefiles:
       imagefile=imagepath+image
       t1= datetime.now()
       result= paddleOCR.Detect(imagefile.encode(encode))
       t2=datetime.now()
       c=t2-t1
       total.append(c)
       print("time:",c.total_seconds()*1000)
       print(result)
    print("平均时间:",   np.mean(total))
if __name__=="__main__":
    main()
    input() 
   
