@echo off
set MSBUILD=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
set TARGET=Rebuild
set SOLFILE=%1
set CONFIG=%2
set RUNTESTS=%3
rem Verbosity ( q[uiet], m[inimal], n[ormal], d[etailed], diag[nostic]
set VERBOSITY=n
set lib=

echo Building %SOLFILE% with %CONFIG% Configuration...
%MSBUILD% CSharp.targets /target:BuildSolution /p:ShouldRunTests=%RUNTESTS% /p:SolutionName=%SOLFILE% /p:Targets=%TARGET% /p:Configuration=%CONFIG% /verbosity:%VERBOSITY% /logger:FileLogger,Microsoft.Build.Engine;logfile=%CONFIG%.log /consoleloggerparameters:NoItemAndPropertyList,PerformanceSummary 
pause