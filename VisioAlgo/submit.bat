@echo off
cd %cd%
set /p place=
xcopy /q /i /y a.exe %place%
cd %place%
cls
for /l %%x in (1, 1, 10) do (
a.exe<input%%x.txt
echo /f
)
del /f a.exe
exit