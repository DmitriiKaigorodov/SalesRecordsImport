# SalesRecordsImport

## How to run from Visual Studio
1. Install [npm](https://nodejs.org/en/).
2. Go to project root directory and execute next commands in cmd:

    `cd SalesRecordsImport/ClientApp`  
    `npm install --save`
3. Execute `database.sql` script to your sql server via SSMS or other tool.
4. Specify your connection string in `SalesRecordImport\appsettings.development.json`
5. Launch visual studio and start project `SalesRecordImport.WebApp`

## TODO

1. Cover more code with unit\integration tests.
2. Added toast notifications to display result or error after delete\update.
3. Fix markup on reports page.
