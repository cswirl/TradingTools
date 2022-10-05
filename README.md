# Time Horizon Trading Tools

This application will equip traders with a quick and detailed Risk/Reward calculation with note taking functionality written in C#. It can be used as a paper trading and trading journal for future study and reference which is very valuable for a trader. It may also be used as a record keeping for tax reporting purpose.

## Technologies
+ .NET 6
+ Windows Forms
+ Entity Framework Core
+ SQL Server 2019

## Features
+ Risk/Reward Calculator with notes taking
  - Easy and quick calculation of loss or profit before entering a position
  - Create trade prospects or watch list

+ Trade Challenges
  - This functionality may help  a trader re-frame his trading goal.
  - The user can create Trade Challenges which is a group of trades arranged in a sequential order with a couple of rules: only 1 active trade is allowed and a maximum trade limit.
  - It has a calendar control marking the dates of entry of each trades.
  
## Installation
The application was published using ClickOnce.

Click the link below to download all files and run the Setup.exe to begin installation. 
The application will create the database automatically via EF Core migration.

Choose the database of your choice. SQLite is a more lightweight and portable database with no additional installation . However, the application is developed using SQL Server.

Current Release: v2.1.2-beta

__TradingTools_SQL Server__ *(database installation required)*: [download here](https://1drv.ms/u/s!AlGVeLI71LLXi7lEOZCrOWRqk6sYKg?e=4wNgaM)

__TradingTools_SQLite__ : [download here](https://1drv.ms/u/s!AlGVeLI71LLXi7lDM88vL-DRKuNbYQ?e=2CfqqM/)


## Uninstallation
The application can be easily removed via 'Add or remove programs'

## User Interface
### Dashboard
![dashboard](https://res.cloudinary.com/dbccui5km/image/upload/v1662430399/TradingToolsUI/dashboard_f9znym.jpg)

### Risk / Reward Calculator
- #### Long Position - Open State
![risk reward calculator - long open](https://res.cloudinary.com/dbccui5km/image/upload/v1662430326/TradingToolsUI/rrc-open_h89xds.jpg)

- #### Long Position - Closed State
![risk reward calculator - long closed](https://res.cloudinary.com/dbccui5km/image/upload/v1662430326/TradingToolsUI/rrc-closed_xs2oma.jpg)

- #### Short Position - Closed State
![risk reward calculator - short closed](https://res.cloudinary.com/dbccui5km/image/upload/v1662430326/TradingToolsUI/rrc-closed-short_re9qtu.jpg)


### Trade Challenge
![trade challenge form](https://res.cloudinary.com/dbccui5km/image/upload/v1662434635/TradingToolsUI/tradeChallenge_dy0v5c.jpg)

### Trade Master File
![trade master file form](https://res.cloudinary.com/dbccui5km/image/upload/v1662430326/TradingToolsUI/tradeMasterFile_yxktst.jpg)
