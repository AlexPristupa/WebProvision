SET mypath=%~dp0
cd /d %mypath%

nssm stop "Mentol Provision Web"
nssm remove "Mentol Provision Web"
pause