@echo off
CLS

.paket\paket.bootstrapper.exe
IF errorlevel 1 (
    EXIT /b %errorlevel%
)

:loop
FOR /F "tokens=* USEBACKQ" %%F IN (`inotifywait -r app test`) DO (
  SET change=%%F
  ECHO %%F
)

TIMEOUT 2
CALL build %1
GOTO loop