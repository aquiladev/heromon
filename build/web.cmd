@echo off

IF "%~1"=="" (SET "task='Build'") ELSE (SET "task='%1'")

:build
powershell.exe -NonInteractive -Command "&{ .\web.ps1 %task% }"