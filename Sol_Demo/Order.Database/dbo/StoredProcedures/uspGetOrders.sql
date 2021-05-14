CREATE PROCEDURE [dbo].[uspGetOrders]
	@Command Varchar(100),

	@CustomerIdentity UNIQUEIDENTIFIER=NULL,

	@FromOrderDate Date=NULL,
	@ToOrderDate Date=NULL,

	@PageNumber int=NULL,
	@RowsOfPage int=NULL
AS
	
	DECLARE @ErrorMessage Varchar(MAX)=NULL;

	IF @Command='Get-Order-History'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

				SELECT 
					OH.OrderIdentity,
					OH.OrderDate,
					OH.SalesOrderNumber,
					OH.Quantity,
					'' as 'Split',
					OH.ProductIdentity,
					OH.ProductName,
					OH.UnitPrice
				FROM	
					viewOrderHistory As OH WITH(NOEXPAND)
				WHERE
					OH.CustomerIdentity=@CustomerIdentity
					AND
					OH.OrderDate
					BETWEEN
					@FromOrderDate AND @ToOrderDate
				ORDER BY 
					OH.OrderId
				OFFSET (@PageNumber-1) * @RowsOfPage ROWS
                        FETCH NEXT @RowsOfPage ROWS ONLY

				
				COMMIT TRANSACTION

			END TRY 

			BEGIN CATCH
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1)
				ROLLBACK TRANSACTION
			END CATCH

		END

GO