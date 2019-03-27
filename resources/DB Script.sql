Drop Database CruiseDB
Go

Create Database CruiseDB
Go

use CruiseDB
Go

IF NOT EXISTS (SELECT * FROM sys.objects WHERE name= N'SalesUnits' and type='U')
BEGIN
Create table SalesUnits
(
	id int not null Primary Key,
	name nvarchar(200),
	country nvarchar(200),
	currency nvarchar(200)
)
End
Go

IF NOT EXISTS (SELECT * FROM sys.objects WHERE name= N'Ships' and type='U')
BEGIN
Create table Ships
(
	id int not null Primary Key,
	salesUnitId int not null,
	name nvarchar(200),
	FOREIGN KEY (salesUnitId) REFERENCES SalesUnits(id)
)
End
Go

IF NOT EXISTS (SELECT * FROM sys.objects WHERE name= N'Bookings' and type='U')
BEGIN
Create table Bookings
(
	id int not null Primary Key,
	shipId int not null,
	bookingDate datetime,
	price money,
	FOREIGN KEY (shipId) REFERENCES Ships(id)
)
End
Go