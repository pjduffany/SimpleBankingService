#!/bin/bash
# Wait until SQL Server is ready
echo "Waiting for SQL Server to start..."
sleep 20

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "CS492SBSPROJ!" -Q "RESTORE DATABASE SBS FROM DISK = '/var/opt/mssql/backup/SBS.bak' WITH MOVE 'SBS' TO '/var/opt/mssql/data/SBS.mdf', MOVE 'SBS_log' TO '/var/opt/mssql/data/SBS_log.ldf', REPLACE;"