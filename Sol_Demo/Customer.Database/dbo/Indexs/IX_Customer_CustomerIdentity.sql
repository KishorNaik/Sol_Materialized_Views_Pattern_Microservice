CREATE UNIQUE NONCLUSTERED INDEX [IX_Customer_CustomerIdentity]
	ON [dbo].Customer
	(CustomerIdentity)
	INCLUDE
	(
		FirstName,
		LastName,
		MobileNo
	)
