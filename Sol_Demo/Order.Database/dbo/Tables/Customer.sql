CREATE TABLE [dbo].[Customer]
(
	[CustomerId] NUMERIC IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [CustomerIdentity] UNIQUEIDENTIFIER NULL, 
    [FirstName] VARCHAR(50) NULL, 
    [LastName] VARCHAR(50) NULL, 
    [MobileNo] VARCHAR(10) NULL
)

