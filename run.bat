@echo off
setlocal
title XuanYu Engine Editor v0.2.28.43-rz

cd /d "%~dp0"
if errorlevel 1 (
    echo [ERROR] Cannot enter repository root.
    pause
    exit /b 1
)

rem Each machine may point XUANYU_DOTNET at its own SDK executable (D:, E:, etc.).
rem Example: setx XUANYU_DOTNET "X:\MyApp\sdk-dotnet\dotnet.exe"
set "DOTNET_EXE="
if defined XUANYU_DOTNET if exist "%XUANYU_DOTNET%" set "DOTNET_EXE=%XUANYU_DOTNET%"
if not defined DOTNET_EXE for /f "delims=" %%D in ('where dotnet 2^>nul') do if not defined DOTNET_EXE set "DOTNET_EXE=%%D"
if not defined DOTNET_EXE (
    echo [ERROR] .NET SDK not found. Set XUANYU_DOTNET to the machine's dotnet.exe path.
    pause
    exit /b 1
)
call "%DOTNET_EXE%" --list-sdks | findstr /r "[0-9]" >nul
if errorlevel 1 (
    echo [ERROR] "%DOTNET_EXE%" is a runtime host without an SDK.
    echo Set XUANYU_DOTNET to the installed SDK dotnet.exe path.
    pause
    exit /b 1
)

set "PROJECT=.\XuanYu.Editor.App\XuanYu.Editor.App.csproj"

echo ========================================
echo    XuanYu Engine Editor - Build and Run
echo ========================================
echo.

rem [0/3] Kill previous editor instance and shutdown build servers to avoid
rem PDB/DLL file locks (CS2012). Only target this editor and MSBuild servers;
rem NEVER use "taskkill /IM dotnet.exe" (would kill unrelated .NET tasks).
echo [0/3] Closing previous editor instance...
taskkill /IM XuanYu.Editor.App.exe /T /F >nul 2>&1 || ver >nul
call "%DOTNET_EXE%" build-server shutdown >nul 2>&1 || ver >nul
%SystemRoot%\System32\timeout.exe /t 1 /nobreak >nul 2>&1 || ver >nul

echo.
echo [1/3] Restoring packages...
call "%DOTNET_EXE%" restore "%PROJECT%" --configfile ".\NuGet.Config" -nologo
if errorlevel 1 goto fail

echo.
echo [2/3] Building app...
set "MSBUILDDISABLENODEREUSE=1"
call "%DOTNET_EXE%" build "%PROJECT%" --no-restore -nologo -clp:Summary=false -m:1 -nr:false -p:UseSharedCompilation=false
if errorlevel 1 goto fail

echo.
echo [3/3] Starting editor...
echo.
call "%DOTNET_EXE%" run --project "%PROJECT%" --no-build
set "exitCode=%errorlevel%"

if not "%exitCode%"=="0" goto failWithCode
exit /b 0

:fail
set "exitCode=%errorlevel%"

:failWithCode
echo.
echo [ERROR] Editor failed. Exit code: %exitCode%
pause
exit /b %exitCode%
