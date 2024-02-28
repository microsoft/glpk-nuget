@echo off
setlocal

rem Build GLPK DLL with Microsoft Visual Studio Enterprise 2022.

set PLAT=%1

if "%PLAT%"=="" (set PLAT=x64)
set OUTDIR=xout_%PLAT%

if "%PLAT%"=="x64" (
    @echo Building for x64 to directory %OUTDIR%
) else if "%PLAT%"=="x86" (
    @echo Building for x86 to directory %OUTDIR%
) else (
    @echo Platform must be x64 or x86, not %PLAT%
    goto LDone
)

rem NOTE: Make sure that HOME_VCVARS variable specifies correct path.
set HOME_VCVARS="C:\Program Files\Microsoft Visual Studio\2022\Enterprise\VC\Auxiliary\Build"
echo %HOME_VCVARS%

call %HOME_VCVARS%\vcvarsall.bat %PLAT%

mkdir %OUTDIR%\obj
cl.exe /Fe:%OUTDIR%\glpk_5_0.dll /Fo%OUTDIR%\obj\ /Fd:%OUTDIR%\ @flags.rsp @files.rsp

:LDone

@echo on
