
//
// Copyright (c) 2025 raoyutian All Rights Reserved.
//
//2025-03-15 优化读取配置文件
//2025-06-12 修改支持多实例
//2026-06-12 修改支持V6模型
package main

import (
    "fmt"
    "syscall"
    "unsafe"
    "os"
    "bufio"
    "C"
    "io/ioutil"
    "time"
    "runtime"
)

// 获取字符串的长度指针
func lenPtr(s string) uintptr {
    return uintptr(len(s))
}

// 获取数字的指针
func intPtr(n int) uintptr {
    return uintptr(n)
}

// 获取字符串的指针
func strPtr(s string) uintptr {
    return uintptr(unsafe.Pointer(syscall.StringBytePtr(s)))
}
// 获取当前目录
func getCurrentAbPathBywd() string {
	dir, err := os.Getwd()
	if err!=nil {
        fmt.Println(err)
	}
	return dir
}

func main() {  
    // 获取当前操作系统
	osname := runtime.GOOS
	// 判断系统
	switch osname {
	case "linux":
		fmt.Println("will run in linux")
	case "windows":
		fmt.Println("will run in Windows")
      
	}

dll,_:= syscall.LoadDLL("PaddleOCR.dll")//加载PaddleOCR.dll动态库文件，与go的exe相同目录下
 Initjson,_:=dll.FindProc("Initializejson")//查找动态库的接口函数
 detect,_:=dll.FindProc("Detect")//查找动态库的接口函数
 enableANSI,_:=dll.FindProc("EnableANSIResult")//查找动态库的接口函数
 enableJson,_:=dll.FindProc("EnableJsonResult")//查找动态库的接口函数
 root:=getCurrentAbPathBywd();// 获取当前目录

 jsonconfig, err := ioutil.ReadFile(root+"/inference/PaddleOCR.config.json")// 读取配置文件
 if err != nil {
    fmt.Println(err)
 }
    // 新版模型keys.txt字段文件可以不存在，但不要为空
    ptr,_,_:=Initjson.Call(strPtr(root+"/inference/PP-OCRv6_small_det_infer"),
    strPtr(root+"/inference/PP-OCRv5_mobile_cls_infer"),
    strPtr(root+"/inference/PP-OCRv6_small_rec_infer"),
    strPtr(root+"/inference/keys.txt"),
    strPtr(string(jsonconfig)))//调用Initializejson方法进行初始化

    //启用单字节编码返回json串（默认为Unicode，有空格，go不能显示全）
    enableANSI.Call(uintptr(uint64(ptr)),1)//0，unicode编码，1，ANSI编码
    enableJson.Call(uintptr(uint64(ptr)),0)//0，返回纯字符串结果，1，返回json字符串结果

    files, err := ioutil.ReadDir(root+"/image")//读取image文件夹内的所有文件
        if err != nil {
            fmt.Println(err)
        }

        //遍历image文件夹内的所有文件进行处理
    for _, file := range files {
        fmt.Println(file.Name())
        start := time.Now() // 记录开始时间   
        res, _, _:=detect.Call(uintptr(uint64(ptr)),strPtr(root+"/image/"+file.Name()))  //调用Detect方法进行识别
        p_result := (*C.char)(unsafe.Pointer(res))//处理字符串指针
        end := time.Now()   // 记录结束时间
        elapsed := end.Sub(start) // 计算开始和结束时间的差值
        ms := float64(elapsed)/float64(time.Millisecond)
        fmt.Printf("耗时: %.2fms\n", ms) // 打印执行时间
        ocrresult:= C.GoString(p_result)//C语言字符串处理，char*
        fmt.Println(ocrresult)
        }

    input := bufio.NewScanner(os.Stdin)
    input.Scan() 
}