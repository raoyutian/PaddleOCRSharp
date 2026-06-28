@echo on 
set root=%~dp0
call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\VC\Auxiliary\Build\vcvars64.bat
mkdir build
cd build 
cmake .. -GNinja -DCMAKE_BUILD_TYPE=Release   -DPaddleOCR_LIB=%root%ytPaddleOCR
ninja -j8
@pause
