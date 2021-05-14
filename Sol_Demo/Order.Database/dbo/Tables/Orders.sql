CREATE TABLE [dbo].[Orders]
(
	[OrderId] NUMERIC IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [OrderIdentity] UNIQUEIDENTIFIER NULL, 
    [CustomerIdentity] UNIQUEIDENTIFIER NULL,
    [ProductIdentity] UNIQUEIDENTIFIER NULL, 
    OrderDate Date NULL,
    [SalesOrderNumber] UNIQUEIDENTIFIER NULL, 
    [Quantity] INT NULL
)
