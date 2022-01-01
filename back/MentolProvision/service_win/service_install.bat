
SET mypath=%~dp0
cd /d %mypath%..\
set apppath=%cd%
cd %mypath%


nssm install "Mentol Provision Web" %apppath%\run.bat

nssm set "Mentol Provision Web" AppDirectory %apppath%\
rem аргументы запуска
rem nssm set "Mentol Provision Web" AppParameters server

nssm set "Mentol Provision Web" DisplayName "Mentol Provision Web"
nssm set "Mentol Provision Web" Description "Предоставляет доступ к API для Web";
nssm set "Mentol Provision Web" Start SERVICE_AUTO_START

nssm set "Mentol Provision Web" ObjectName LocalSystem
nssm set "Mentol Provision Web" Type SERVICE_WIN32_OWN_PROCESS

rem зависимости
rem nssm set "Mentol Provision Web" DependOnService MpsSvc


nssm set "Mentol Provision Web" AppPriority NORMAL_PRIORITY_CLASS
nssm set "Mentol Provision Web" AppNoConsole 1
nssm set "Mentol Provision Web" AppAffinity All


nssm set "Mentol Provision Web" AppStopMethodSkip 0
nssm set "Mentol Provision Web" AppStopMethodConsole 30000
nssm set "Mentol Provision Web" AppStopMethodWindow 5000
nssm set "Mentol Provision Web" AppStopMethodThreads 5000

nssm set "Mentol Provision Web" AppThrottle 5000
nssm set "Mentol Provision Web" AppExit Default Restart
nssm set "Mentol Provision Web" AppRestartDelay 1000

nssm set "Mentol Provision Web" AppStdout %mypath%service.log
nssm set "Mentol Provision Web" AppStderr %mypath%service.log
pause