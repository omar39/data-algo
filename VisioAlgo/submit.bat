@echo off
cd /d %cd%
set /p place=
copy a.exe "%place%"
cd /d %place%
cls
for /l %%x in (1, 1, 10) do (
a.exe<input%%x.txt
echo /f
)
pause
exit