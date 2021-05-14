CREATE TABLE [dbo].[Product]
(
	[ProductId] Numeric(18,0) IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ProductIdentity] UNIQUEIDENTIFIER NULL,
	ProductName Varchar(100),
	UnitPrice float
    
)
