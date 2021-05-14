CREATE VIEW [dbo].[viewOrderHistory]
	WITH SCHEMABINDING
	AS 
		SELECT 
			O.OrderId,
			O.OrderIdentity,
			O.SalesOrderNumber,
			O.OrderDate,
			O.Quantity,
			P.ProductIdentity,
			P.ProductName,
			P.UnitPrice,
			C.CustomerIdentity,
			C.FirstName,
			C.LastName,
			C.MobileNo
		FROM 
			dbo.Orders AS O 
		INNER JOIN 
			dbo.Customer AS C
		ON 
			O.CustomerIdentity=C.CustomerIdentity
		INNER JOIN
			dbo.Product As P
		ON
			O.ProductIdentity=P.ProductIdentity
