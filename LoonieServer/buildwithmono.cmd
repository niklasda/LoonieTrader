REM Try to build with mono on windows

CALL "C:\Program Files (x86)\Mono\bin\setmonopath.bat"

mono .paket\paket.bootstrapper.exe
IF errorlevel 1 (
    EXIT /b %errorlevel%
)

mono .paket\paket.exe install
IF errorlevel 1 (
    EXIT /b %errorlevel%
)

REM mono packages\FAKE\tools\FAKE.exe --fsiargs -d:MONO build.fsx %*

xbuild LoonieServer.sln