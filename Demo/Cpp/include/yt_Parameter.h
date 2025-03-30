// Copyright (c) 2021 饶玉田 Authors. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#pragma once
#pragma pack(push,1)
#include <vector>
using namespace std;
/// <summary>
/// OCR识别参数
/// </summary>
struct OCRParameter
{
	//通用参数
	bool use_gpu=false;//是否使用GPU；默认false
	int gpu_id=0;//GPU id，使用GPU时有效；默认0;
	int gpu_mem=4000;//申请的GPU内存;默认4000
	int cpu_math_library_num_threads=10;//CPU预测时的线程数，在机器核数充足的情况下，该值越大，预测速度越快；默认10
	bool enable_mkldnn=true;//是否使用mkldnn库；默认true
    

	//前向相关
	bool det=true;//是否执行文字检测；默认true
	bool rec=true;//是否执行文字识别；默认true
	bool cls=false;//是否执行文字方向分类；默认false

	//检测模型相关
	int    max_side_len=960;//输入图像长宽大于960时，等比例缩放图像，使得图像最长边为960,；默认960
	float  det_db_thresh=0.3f;//用于过滤DB预测的二值化图像，设置为0.-0.3对结果影响不明显；默认0.3
	float   det_db_box_thresh=0.5f;//DB后处理过滤box的阈值，如果检测存在漏框情况，可酌情减小；默认0.5
	float   det_db_unclip_ratio=1.6f;//表示文本框的紧致程度，越小则文本框更靠近文本;默认1.6
	bool use_dilation=false;//是否在输出映射上使用膨胀,默认false
	bool det_db_score_mode=true;//1:使用多边形框计算bbox score，0:使用矩形框计算。矩形框计算速度更快，多边形框对弯曲文本区域计算更准确。
	bool visualize=false;//是否对结果进行可视化，为true时，预测结果会在当前目录下保存一个ocr_vis.png文件。默认false
	 
	
	//方向分类器相关
	bool use_angle_cls=false;//是否使用方向分类器,默认false
	float   cls_thresh=0.9f;//方向分类器的得分阈值，默认0.9
	int cls_batch_num=1;//方向分类器batchsize，默认1

	//识别模型相关
	int rec_batch_num=6;//识别模型batchsize，默认6
	int   rec_img_h=48;//识别模型输入图像高度，默认48
	int rec_img_w=320;//识别模型输入图像宽度，默认320
	
	bool show_img_vis=false;//是否显示预测结果，默认false
	bool use_tensorrt=false;//使用GPU预测时，是否启动tensorrt，默认false
};
/// <summary>
/// 表格识别参数
/// </summary>
struct StructureParameter:OCRParameter
{
	int  table_max_len=488;//输入图像长宽大于488时，等比例缩放图像,默认488
	bool merge_no_span_structure=true;//是否合并空单元格，默认true
	int table_batch_num = 1;//批量识别数量，默认1
};
/// <summary>
/// OCR可修改参数
/// </summary>
struct ModifyParameter
{
	//前向相关
	bool m_det = true;//动态修改是否检测，默认true。当OCRParameter.det=true时有效。
    bool m_rec = true;//动态修改是否识别，默认true。在OCRParameter.rec=true时，m_rec可动态关闭参数rec
	//检测模型相关,在m_det为true时生效
	int m_max_side_len = 960;// 输入图像长宽大于960时，等比例缩放图像，使得图像最长边为960,；默认960
	float  m_det_db_thresh = 0.3f;//用于过滤DB预测的二值化图像，设置为0.-0.3对结果影响不明显；默认0.3
	float   m_det_db_box_thresh = 0.5f;//DB后处理过滤box的阈值，如果检测存在漏框情况，可酌情减小；默认0.5
	float   m_det_db_unclip_ratio = 1.6f;//表示文本框的紧致程度，越小则文本框更靠近文本;默认1.6
};
#pragma pack(pop)  
