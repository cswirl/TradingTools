# Time Horizon Trading Tools

This application will equip traders with a quick and detailed Risk/Reward calculation with note taking functionality written in C#. It can be used as a trading journal for future study and reference which is very valuable for a trader. It may also be used as a record keeping for tax reporting purpose.

## Technologies
+ .NET 5
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

__TradingTools_SQL Server__ *(database installation required)*: [download here](https://1drv.ms/u/s!AlGVeLI71LLXi7lEOZCrOWRqk6sYKg?e=4wNgaM)

__TradingTools_SQLite__ : [download here](https://1drv.ms/u/s!AlGVeLI71LLXi7lDM88vL-DRKuNbYQ?e=2CfqqM/)

## Uninstallation
The application can be easily removed via 'Add or remove programs'
