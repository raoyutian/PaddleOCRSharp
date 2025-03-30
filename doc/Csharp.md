建议使用VS2022版本编译，如果遇到无法编译，请切换成release后再切换回debug即可。 
如果因框架编译问题无法编译，请修改PaddleOCRSharp\PaddleOCRSharp.csproj文件，删除当前电脑环境没有的框架，只保留你想要的.Net框架。
具体框架说明见微软文档[SDK 样式项目中的目标框架](https://docs.microsoft.com/zh-cn/dotnet/standard/frameworks)
```
 <TargetFrameworks>
net35;net40;net45;net451;net452;net46;net461;net462;net47;net471;net472;net48;
netstandard2.0;netcoreapp3.1;
net5.0;net6.0;net7.0;net8.0;
</TargetFrameworks>
```