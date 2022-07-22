@echo off

set appcmd="C:\Windows\System32\inetsrv\appcmd.exe"
SET application=checkout-api.7digital.local
SET appPath=Api

::setup application pool
%appcmd% list apppool "%application%"
REM IF %ERRORLEVEL% EQU 0 (
	%appcmd% delete apppool "%application%"
)
%appcmd% add apppool /name:"%application%" /managedRuntimeVersion:v4.0 /managedPipelineMode:Integrated
IF %ERRORLEVEL% NEQ 0 (
	GOTO End
)

::setup website
%appcmd% list site "%application%"
IF %ERRORLEVEL% EQU 0 (
	%appcmd% delete site "%application%"
)

SET root=%cd%
%appcmd% add site /site.name:"%application%" /bindings:http://%application%:80 /physicalPath:%root%\%appPath%
IF %ERRORLEVEL% NEQ 0 (
	GOTO End
)

%appcmd% set app "%application%/" /applicationPool:%application%
IF %ERRORLEVEL% NEQ 0 (
	GOTO End
)

::append entry to hosts file
SET findCmd="C:\Windows\system32\find.exe"
%findCmd% /c /i "127.0.0.1 %application% " C:\Windows\System32\drivers\etc\hosts
IF %ERRORLEVEL% NEQ 0 (
	echo # >> %WINDIR%\System32\drivers\etc\hosts
	echo 127.0.0.1 %application% >> %WINDIR%\System32\drivers\etc\hosts
)

:End