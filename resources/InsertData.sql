Declare @JSON1 nvarchar(max)
Declare @JSON2 nvarchar(max)
Declare @JSON3 nvarchar(max)

--inserting salesUnits data
SELECT @JSON1 = BulkColumn
FROM OPENROWSET (BULK 'C:\Users\vagrant\Desktop\salesUnits.json', SINGLE_CLOB) as j

Insert into SalesUnits
SELECT * FROM OPENJSON (@JSON1) 
WITH(id int,name nvarchar(200),country nvarchar(200),currency nvarchar(200)) as salesUnits

--inserting ships data
SELECT @JSON2 = BulkColumn
FROM OPENROWSET (BULK 'C:\Users\vagrant\Desktop\ships.json', SINGLE_CLOB) as j

Insert into ships
SELECT * FROM OPENJSON (@JSON2) 
WITH(id int,salesUnitId int ,name nvarchar(200)) as ships


--inserting bookings data
SELECT @JSON3 = BulkColumn
FROM OPENROWSET (BULK 'C:\Users\vagrant\Desktop\bookings.json', SINGLE_CLOB) as j

Insert into bookings
SELECT * FROM OPENJSON (@JSON3) 
WITH(id int,shipId int ,bookingDate datetime, price money) as bookings