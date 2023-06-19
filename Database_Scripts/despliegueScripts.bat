@ECHO OFF
setlocal enableextensions disabledelayedexpansion

 ECHO *
 ECHO **
 ECHO *****
 ECHO ********************************************************************************************************************
 ECHO *
 ECHO *	EQUIPO DESARROLLO CUBA: ESTABLECIMIENTO ENTORNO BASE DATOS OnecoreProgrammingTest
 ECHO *
 ECHO ********************************************************************************************************************
 ECHO *****
 ECHO **
 ECHO *
 
rem Eliminamos el fichero de salida en caso de existir
IF EXIST prepararBD-Logs.txt (
    del prepararBD-Logs.txt
)
rem Comenzamos con el header del fichero log (prepararBD-Logs.txt)
ECHO * >>prepararBD-Logs.txt
ECHO ** >>prepararBD-Logs.txt
ECHO ***** >>prepararBD-Logs.txt
ECHO ******************************************************************************************************************** >>prepararBD-Logs.txt
ECHO * >>prepararBD-Logs.txt
ECHO *	EQUIPO DESARROLLO CUBA: ESTABLECIMIENTO ENTORNO BASE DATOS PORTAL COLABORADOR - %DATE% %TIME% >>prepararBD-Logs.txt
ECHO * >>prepararBD-Logs.txt
ECHO ******************************************************************************************************************** >>prepararBD-Logs.txt
ECHO ***** >>prepararBD-Logs.txt
ECHO ** >>prepararBD-Logs.txt
ECHO * >>prepararBD-Logs.txt

::============================================================================================================================
:: Función prepararBD
::============================================================================================================================
:prepararBD
    rem Verificamos conectividad con el servidor especificado como parámetro de ejecución
    ping %1 -4 -n 1 | find /i "bytes=" > nul
    IF ErrorLevel 1 (
        call :infoErrorConexion
        pause
    ) ELSE (
        ECHO CONEXION ESTABLECIDA CORRECTAMENTE CON EL SERVIDOR %1 - %DATE% %TIME% >>prepararBD-Logs.txt
        ECHO INFO:Conexion establecida correctamente con el servidor de base de datos %1 MSSQLSERVER
        rem ECHO %USERNAME% inicio el proceso de preparar base de datos para uso de Portal del Colaborador a la hora: %TIME%  >prepararBD-Logs.txt
        WHERE sqlcmd >nul 2>&1 && (
            rem sqlcmd.exe -S %1 -U sa -P %3 -d %2 -Q "IF DB_ID('%2') IS NOT NULL"
            call :Main %1 %2 %3
        ) || (
            ECHO ERROR:NO SE HA ENCONTRADO EL UTILITARIO sqlcmd PARA EL TRABAJO MEDIANTE LINEA DE COMANDO CON EL SERVIDOR MSSQLSERVER - %DATE% %TIME% >>prepararBD-Logs.txt
            ECHO ERROR:No se ha encontrado el utilitario sqlcmd para el trabajo mediante linea de comandos con el servidor MSSQLSERVER
            pause
        )
        @REM WHERE sqlcmd >nul 2>nul
        @REM IF %ERRORLEVEL% EQU 0 ECHO sqlcmd found
        @REM pause
    )
    goto :eof
::============================================================================================================================
:: Función Main
::============================================================================================================================
:Main 
    ECHO PREPARANDO BASE DE DATOS %2 PARA USO DEL PORTAL COLABORADOR - %DATE% %TIME% >>prepararBD-Logs.txt
    ECHO *
    ECHO **
    ECHO *****
    ECHO ********************************************************************************************************************
    ECHO *
    ECHO *	PREPARANDO BASE DE DATOS %2 PARA USO DEL PORTAL COLABORADOR - %DATE% %TIME%
    ECHO *
    ECHO ********************************************************************************************************************
    ECHO *****
    ECHO **
    ECHO *
    :IMP_CU
    REM ----------------------------------------------------TABLAS-----------------------------------------------------------
    ECHO CREANDO TABLAS - %DATE% %TIME% >>prepararBD-Logs.txt
    ECHO *
    ECHO ********************************************************************************************************************
    ECHO *
    ECHO *	CREANDO TABLAS - %DATE% %TIME%
    ECHO *
    ECHO ********************************************************************************************************************
    ECHO *
    rem Iteramos primeramente los scripts para crear las tablas necesarias para el uso/explotación del Portal del Colaborador
    set /a count = 1
    for /R %%i in (Database_Scripts\01-TABLAS\*.sql) do (
        ECHO Ejecutando fichero %%~ni.sql : %TIME%  >>prepararBD-Logs.txt
        sqlcmd.exe -S %1 -U sa -P %3 -d %2 -i "%%i"  >>prepararBD-Logs.txt
        rem if %ERRORLEVEL% neq 0 PAUSE /b %ERRORLEVEL%
        timeout /t 1 /nobreak > NUL
        set /a count += 1
        call :drawProgressBar count "Creando tablas"
        rem call :drawProgressBar %%~nxA "Ejecutando ficheros para crear tablas del Portal del Colaborador"
        rem call :drawProgressBar !random! "random test"
    )
    ::pause
    rem Clean all after use
    call :finalizeProgressBar 1
    REM ----------------------------------------------------TABLAS-----------------------------------------------------------
    REM ----------------------------------------------------PROCEDIMIENTOS---------------------------------------------------
    ECHO POBLANDO DE DATOS LA BASE DE DATOS - %DATE% %TIME% >>prepararBD-Logs.txt
    ECHO *
    ECHO ********************************************************************************************************************
    ECHO *
    ECHO *	POBLANDO DE DATOS LA BASE DE DATOS - %DATE% %TIME%
    ECHO *
    ECHO ********************************************************************************************************************
    ECHO *
    rem Iteramos sobre los scripts para crear los procedimientos necesarias para el uso/explotación del Portal del Colaborador
    set /a count = 1
    for /R Database_Scripts\02-DATA_SEEDER %%i in (*.sql) do (
        ECHO Ejecutando fichero %%~ni.sql : %TIME%  >>prepararBD-Logs.txt
        sqlcmd.exe -S %1 -U sa -P %3 -d %2 -i "%%i"  >>prepararBD-Logs.txt
        rem if %ERRORLEVEL% neq 0 PAUSE /b %ERRORLEVEL%
        timeout /t 1 /nobreak > NUL
        set /a count += 1
        call :drawProgressBar count "Ejecutando scripts para poblar la base de datos de información"
    )
    ::pause
    rem Clean all after use
    call :finalizeProgressBar 1
    REM ----------------------------------------------------PROCEDIMIENTOS---------------------------------------------------
    REM ----------------------------------------------------PROCEDIMIENTOS CLOUD---------------------------------------------
    ECHO CREANDO PROCEDIMIENTOS - %DATE% %TIME% >>prepararBD-Logs.txt
    ECHO *
    ECHO ********************************************************************************************************************
    ECHO *
    ECHO *	CREANDO PROCEDIMIENTOS - %DATE% %TIME%
    ECHO *
    ECHO ********************************************************************************************************************
    ECHO *
    rem Iteramos sobre los scripts para crear los procedimientos necesarias para el uso/explotación del Portal del Colaborador
    rem https://ss64.com/nt/for_r.html
    rem List every .bak file in every subfolder starting at C:\temp\
    rem For /R C:\temp\ %%G IN (*.bak) do Echo "%%G"
    set /a count = 1
    for /R Database_Scripts\03_Procedimientos %%i in (*.sql) do (
        ECHO Ejecutando fichero %%~ni.sql : %TIME%  >>prepararBD-Logs.txt
        sqlcmd.exe -S %1 -U sa -P %3 -d %2 -i "%%i"  >>prepararBD-Logs.txt
        rem if %ERRORLEVEL% neq 0 PAUSE /b %ERRORLEVEL%
        timeout /t 1 /nobreak > NUL
        set /a count += 1
        call :drawProgressBar count "Creando procedimientos"
    )
    ::pause
    rem Clean all after use
    call :finalizeProgressBar 1
    @REM REM ----------------------------------------------------PROCEDIMIENTOS CLOUD---------------------------------------------
    @REM REM ----------------------------------------------------PROCEDIMIENTOS CLOUD---------------------------------------------
    @REM ECHO CREANDO OTROS PROCEDIMIENTOS CLOUD - %DATE% %TIME% >>prepararBD-Logs.txt
    @REM ECHO *
    @REM ECHO ********************************************************************************************************************
    @REM ECHO *
    @REM ECHO *	CREANDO OTROS PROCEDIMIENTOS CLOUD - %DATE% %TIME%
    @REM ECHO *
    @REM ECHO ********************************************************************************************************************
    @REM ECHO *
    @REM rem Iteramos sobre los scripts para crear los procedimientos necesarias para el uso/explotación del Portal del Colaborador
    @REM rem https://ss64.com/nt/for_r.html
    @REM rem List every .bak file in every subfolder starting at C:\temp\
    @REM rem For /R C:\temp\ %%G IN (*.bak) do Echo "%%G"
    @REM set /a count = 1
    @REM for /R "Scripts\SPs de Codeas Cloud" %%i in (*.sql) do (
    @REM     ECHO Ejecutando fichero %%~ni.sql : %TIME%  >>prepararBD-Logs.txt
    @REM     sqlcmd.exe -S %1 -U sa -P %3 -d %2 -i "%%i"  >>prepararBD-Logs.txt
    @REM     rem if %ERRORLEVEL% neq 0 PAUSE /b %ERRORLEVEL%
    @REM     timeout /t 1 /nobreak > NUL
    @REM     set /a count += 1
    @REM     call :drawProgressBar count "Creando otros procedimientos cloud"
    @REM )
    ::pause
    rem Clean all after use
    call :finalizeProgressBar 1
    REM ----------------------------------------------------PROCEDIMIENTOS CLOUD---------------------------------------------
    GOTO :OK_EXP
    :OK_EXP
    ECHO PROCESO DE PREPARACION DE BASE DE DATOS %2  TERMINADA CON EXITO - %DATE% %TIME% >>prepararBD-Logs.txt
    ECHO *
    ECHO **
    ECHO *****
    ECHO ********************************************************************************************************************
    ECHO *
    ECHO *	PROCESO DE PREPARACION DE BASE DE DATOS %2  TERMINADA CON EXITO - %DATE% %TIME%
    ECHO *		
    ECHO ********************************************************************************************************************
    ECHO *****
    ECHO **
    ECHO *
    ECHO Presione una tecla para continuar...
    PAUSE >nul
    EXIT
    :NOT_EXP
    ECHO *
    ECHO **
    ECHO *****
    ECHO ********************************************************************************************************************
    ECHO *
    ECHO *	HA OCURRIDO UN ERROR EN EL PROCESO DE PREPARACION DE LA BASE DE DATOS %2 - %DATE% %TIME%
    ECHO *
    ECHO ********************************************************************************************************************
    ECHO *****
    ECHO **
    ECHO *
    ECHO Presione una tecla para continuar...
    PAUSE >nul
    EXIT
    goto :eof
::============================================================================================================================
:: Función drawProgressBar
::============================================================================================================================
:drawProgressBar value [text]
    if "%~1"=="" goto :eof
    if not defined pb.barArea call :initProgressBar
    setlocal enableextensions enabledelayedexpansion
    set /a "pb.value=%~1 %% 101", "pb.filled=pb.value*pb.barArea/100", "pb.dotted=pb.barArea-pb.filled", "pb.pct=1000+pb.value"
    set "pb.pct=%pb.pct:~-3%"
    if "%~2"=="" ( set "pb.text=" ) else ( 
        set "pb.text=%~2%pb.back%" 
        set "pb.text=!pb.text:~0,%pb.textArea%!"
    )
    <nul set /p "pb.prompt=[!pb.fill:~0,%pb.filled%!!pb.dots:~0,%pb.dotted%!][ %pb.pct% ] %pb.text%!pb.cr!"
    endlocal
    goto :eof
::============================================================================================================================
:: Función initProgressBar
::============================================================================================================================
:initProgressBar [fillChar] [dotChar]
    if defined pb.cr call :finalizeProgressBar
    for /f %%a in ('copy "%~f0" nul /z') do set "pb.cr=%%a"
    if "%~1"=="" ( set "pb.fillChar=#" ) else ( set "pb.fillChar=%~1" )
    if "%~2"=="" ( set "pb.dotChar=." ) else ( set "pb.dotChar=%~2" )
    set "pb.console.columns="
    for /f "tokens=2 skip=4" %%f in ('mode con') do if not defined pb.console.columns set "pb.console.columns=%%f"
    set /a "pb.barArea=pb.console.columns/2-2", "pb.textArea=pb.barArea-9"
    set "pb.fill="
    setlocal enableextensions enabledelayedexpansion
    for /l %%p in (1 1 %pb.barArea%) do set "pb.fill=!pb.fill!%pb.fillChar%"
    set "pb.fill=!pb.fill:~0,%pb.barArea%!"
    set "pb.dots=!pb.fill:%pb.fillChar%=%pb.dotChar%!"
    set "pb.back=!pb.fill:~0,%pb.textArea%!
    set "pb.back=!pb.back:%pb.fillChar%= !"
    endlocal & set "pb.fill=%pb.fill%" & set "pb.dots=%pb.dots%" & set "pb.back=%pb.back%"
    goto :eof
::============================================================================================================================
:: Función finalizeProgressBar
::============================================================================================================================
:finalizeProgressBar [erase]
    if defined pb.cr (
        if not "%~1"=="" (
            setlocal enabledelayedexpansion
            set "pb.back="
            for /l %%p in (1 1 %pb.console.columns%) do set "pb.back=!pb.back! "
            <nul set /p "pb.prompt=!pb.cr!!pb.back:~1!!pb.cr!"
            endlocal
        )
    )
    for /f "tokens=1 delims==" %%v in ('set pb.') do set "%%v="
    goto :eof
::============================================================================================================================
:: Función infoErrorConexion
::============================================================================================================================
:infoErrorConexion
    ECHO NO SE HA PODIDO ESTABLECER CONEXION CON EL SERVIDOR %1, VERIFIQUE E INTENTE NUEVAMENTE - %DATE% %TIME% >>prepararBD-Logs.txt
    ECHO ERROR: NO SE HA PODIDO ESTABLECER CONEXION CON EL SERVIDOR %1 VERIFIQUE E INTENTE NUEVAMENTE - %DATE% %TIME%
    @REM ECHO *
    @REM ECHO **
    @REM ECHO *****
    @REM ECHO ********************************************************************************************************************
    @REM ECHO *
    @REM ECHO *	NO SE HA PODIDO ESTABLECER CONEXIONO CON EL SERVIDOR %1 VERIFIQUE E INTENTE NUEVAMENTE - %DATE% %TIME%
    @REM ECHO *
    @REM ECHO ********************************************************************************************************************
    @REM ECHO *****
    @REM ECHO **
    @REM ECHO *
    ECHO Presione una tecla para continuar...
    PAUSE >nul 
    goto :eof   

