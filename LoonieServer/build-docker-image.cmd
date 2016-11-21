@echo off

.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

docker build -t loonie-trader-server .
if errorlevel 1 (
  exit /b %errorlevel%
)
