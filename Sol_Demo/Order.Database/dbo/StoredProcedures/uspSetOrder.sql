CREATE PROCEDURE [dbo].[uspSetOrder]
	@Command Varchar(100)=NULL,

	@OrderIdentity uniqueidentifier=null,
	@CustomerIdentity uniqueidentifier=null,
	@ProductIdentity uniqueidentifier=null,
	@OrderDate Date=null,
	@SalesOrderNumber uniqueidentifier=null,
	@Quantity int


AS
	
	DECLARE @ErrorMessage Varchar(MAX)

	IF @Command='Create-Order'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

					INSERT INTO Orders
					(
						OrderIdentity,
						CustomerIdentity,
						ProductIdentity,
						OrderDate,
						SalesOrderNumber,
						Quantity
					)
					VALUES
					(
						NEWID(),
						@CustomerIdentity,
						@ProductIdentity,
						FORMAT(GETDATE(),'dd/MM/yyyy'),
						NEWID(),
						@Quantity
					)
				
				COMMIT TRANSACTION

			END TRY 

			BEGIN CATCH
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1)
				ROLLBACK TRANSACTION
			END CATCH

		END
	


GO