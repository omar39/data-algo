@echo off
cd %~dp0
for /l %%x in (1, 1, 10) do (
a.exe<input%%x.txt>output%%x.txt 
)
pause
exit