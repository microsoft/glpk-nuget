set CMD_DIR=%~dp0
echo %CMD_DIR%

set FILE=glpk-5.0.tar.gz
set DST_DIR=%CMD_DIR%glpk\
set DST_FILE=%DST_DIR%%FILE%

if exist "%DST_FILE%" goto LExtract

md %DST_DIR%
if ERRORLEVEL 1 goto LEnd

curl -o "%DST_FILE%" https://ftp.gnu.org/gnu/glpk/%FILE%
if ERRORLEVEL 1 goto LEnd

:LExtract
pushd "%DST_DIR%"
tar -xkf "%DST_FILE%"
popd

:LEnd
