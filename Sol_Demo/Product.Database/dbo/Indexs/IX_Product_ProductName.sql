CREATE UNIQUE NONCLUSTERED INDEX [IX_Product_ProductName]
	ON [dbo].[Product]
	(ProductName)
	INCLUDE
	(
		ProductIdentity,
		UnitPrice
	)
