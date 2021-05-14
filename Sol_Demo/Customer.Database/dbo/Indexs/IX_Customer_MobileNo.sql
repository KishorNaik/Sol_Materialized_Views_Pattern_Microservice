CREATE UNIQUE NONCLUSTERED INDEX [IX_Customer_MobileNo]
	ON [dbo].Customer
	(MobileNo)
	INCLUDE
	(
		CustomerIdentity,
		FirstName,
		LastName
	)
