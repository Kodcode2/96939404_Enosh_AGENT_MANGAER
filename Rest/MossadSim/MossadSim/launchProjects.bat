@echo off

REM Change to the MossadProject-APIServerMock directory and run the simulation API server
cd MossadProject-APIServerMock
start cmd /k "MossadSimulationDemoAPIServer.exe --apiServerUrl=http://localhost:8080 --syncDelay=10 --moveDelay=10"

REM Change to the MossadProject-SimulationServer directory and run the simulation server
cd ..
cd MossadProject-SimulationServer
start cmd /k "MossadSimulationServer.exe --apiServerUrl=http://localhost:5000 --syncDelay=1"

REM Return to the initial directory
cd ..

echo Processes started successfully.
pause
