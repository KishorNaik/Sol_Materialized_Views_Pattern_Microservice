CREATE UNIQUE NONCLUSTERED  INDEX [IX_Product_ProductIdentity]
	ON [dbo].[Product]
	(ProductIdentity)
	INCLUDE
	(
		ProductName,
		UnitPrice
	)
