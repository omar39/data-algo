@echo off
xcopy /q /y a.exe tests
cd %cd%
cd tests
cls
for /l %%a in (1, 1, 10) do (
 a.exe<input%%a.txt
 echo /f
)
del /f a.exe
exit