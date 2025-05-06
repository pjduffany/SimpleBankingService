### Spin up SQL Server with SBS.bak
1. Clone this repo
2. Ensure docker is installed and running: https://docs.docker.com/engine/install/
3. Run:
   - windows/bash (in the project directory)
       a.  chmod +x docker/init-db.sh
       b.  docker-compose up --build
         - This command will...
           1. Pull the SQL Server image
           2. Mount the docker/ folder
           3. Restore the SBS.bak database automatically
4. Connect to the SQL Server using SSMS or Azure Data Studio
   - Server: localhost
   - User: sa
   - Password: CS492SBSPROJ!
5. You should see the SBS database and the users & accounts table populated with some basic test data
