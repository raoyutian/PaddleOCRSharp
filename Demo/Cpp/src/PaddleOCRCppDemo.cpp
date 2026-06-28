#include <string.h>
#include <string>
#include <codecvt>
#include <vector>

#include <iostream>
#include <fstream>
#include <sstream>
#include <iterator> 
#include <chrono>

#include <include/yt_PaddleOCR.h>
#ifdef _WIN32
#include <Windows.h>
#include <tchar.h>
#include <io.h>
#else
#include <sys/types.h>
#include <sys/stat.h>
#include <unistd.h>
#include <dirent.h>
#endif // __WIN32

#ifdef _WIN32
#define OS_PATH_SEP "\\"
#else
#define OS_PATH_SEP "/"
#endif

#define coutEnd(str) std::cout << (str) << std::endl

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
	std::wcout.imbue(std::locale("chs"));//防止中文显示乱码
	 
	
	//获取当前exe目录
	char path[MAX_PATH];
	GetCurrentDirectoryA(MAX_PATH, path);
	
	//使用PP-OCRv5_mobile
	string cls_infer(path);
	cls_infer += "\\inference\\ch_ppocr_mobile_v2.0_cls_infer";
	string rec_infer(path);
	rec_infer += "\\inference\\PP-OCRv5_mobile_rec_infer";
	string det_infer(path);
	det_infer += "\\inference\\PP-OCRv5_mobile_det_infer";
	string ocrkeys(path);
	ocrkeys += "\\inference\\ppocr_keys.txt";
	
	//待识别图片文件夹
	string imagepath(path);
	imagepath += "\\image"; 
	vector<string> images;
	getFiles(imagepath, images);

	string  modelpath(path);
	modelpath = modelpath + OS_PATH_SEP + "inference";
	string config(path);
	config = modelpath + OS_PATH_SEP + "PaddleOCR.config.json";
	char* parameterjson;
	std::ifstream inputFile(config, ios::in);

	stringstream strstream;
	strstream << inputFile.rdbuf(); // 将文件内容读入stringstream

	string json = strstream.str();
	parameterjson = (char*)json.c_str();
	coutEnd(json);
	printf("读取配置文件/inference/PaddleOCR.config.json成功。\n");

	auto engine_p=Initializejson(const_cast<char*>(det_infer.c_str()),
							 const_cast<char*>(cls_infer.c_str()), 
						     const_cast<char*>(rec_infer.c_str()),
							 const_cast<char*>(ocrkeys.c_str()),
	           	 const_cast<char*>(json.c_str()));
	
	
	EnableJsonResult(engine_p,false);//直接显示纯文本，如果开启，则显示json格式字符串，包括坐标，置信度等

	if (images.size() > 0)
	{
		for (size_t i = 0; i < images.size(); i++)
		{ 
		    auto	start = std::chrono::steady_clock::now();
			  wstring  result = (WCHAR*)Detect(engine_p, const_cast<char*>(images[i].c_str()));
	      	std::wcout << result << endl;
			auto	end = std::chrono::steady_clock::now();
			auto duration=	duration_cast<milliseconds>(end - start);
		 
			std::cout << duration.count() <<"ms"<< endl;
		}
	}
	try
	{
		FreeEngine(engine_p);
	}
	catch (const std::exception& e)
	{
		std::wcout << e.what();
	}
	//阻塞界面，防止控制台直接关闭
	std::cin.get();
}

