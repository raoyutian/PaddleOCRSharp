#include <PaddleOCR.h>
using namespace std;
using namespace chrono;

void getFiles(string path, vector<string>& files)
{
	intptr_t   hFile = 0;//文件句柄，过会儿用来查找
	struct _finddata_t fileinfo;//文件信息
	string p;
	if ((hFile = _findfirst(p.assign(path).append("\\*").c_str(), &fileinfo)) != -1)
		//如果查找到第一个文件
	{
		do
		{
			if ((fileinfo.attrib & _A_SUBDIR))//如果是文件夹
			{
				if (strcmp(fileinfo.name, ".") != 0 && strcmp(fileinfo.name, "..") != 0)
					getFiles(p.assign(path).append("\\").append(fileinfo.name), files);
			}
			else//如果是文件
			{
				files.push_back(p.assign(path).append("\\").append(fileinfo.name));
			}
		} while (_findnext(hFile, &fileinfo) == 0);	//能寻找到其他文件

		_findclose(hFile);	//结束查找，关闭句柄
	}
}

int main()
{  
	OCRParameter parameter;
	parameter.enable_mkldnn = true;
	parameter.cpu_math_library_num_threads = 4;
	parameter.max_side_len = 960;
	char path[MAX_PATH];
	GetCurrentDirectoryA(MAX_PATH, path);
	string cls_infer(path);
	//V4
	cls_infer += "\\inference\\ch_ppocr_mobile_v2.0_cls_infer";
	string rec_infer(path);
	rec_infer += "\\inference\\ch_PP-OCRv4_rec_infer";
	string det_infer(path);
	det_infer += "\\inference\\ch_PP-OCRv4_det_infer";
	string ocrkeys(path);
	ocrkeys += "\\inference\\ppocr_keys.txt";


	string imagepath(path);
	imagepath += "\\image"; 
	vector<string> images;
	getFiles(imagepath, images);

	Initialize(const_cast<char*>(det_infer.c_str()),
							 const_cast<char*>(cls_infer.c_str()), 
						     const_cast<char*>(rec_infer.c_str()),
							 const_cast<char*>(ocrkeys.c_str()),
		                     parameter);
	
	std::wcout.imbue(std::locale("chs"));//防止中文显示乱码
	EnableJsonResult(false);//直接显示纯文本，如果开启，则显示json格式字符串，包括坐标，置信度等

	if (images.size() > 0)
	{
		for (size_t i = 0; i < images.size(); i++)
		{ 
		    auto	start = std::chrono::steady_clock::now();
			  wstring  result = (WCHAR*)Detect( const_cast<char*>(images[i].c_str()));
	      	std::wcout << result << endl;
			auto	end = std::chrono::steady_clock::now();
			auto duration=	duration_cast<milliseconds>(end - start);
		 
			std::cout << duration.count() <<"ms"<< endl;
		}
	}
	try
	{
		FreeEngine();
	}
	catch (const std::exception& e)
	{
		std::wcout << e.what();
	}
	std::cin.get();
}

