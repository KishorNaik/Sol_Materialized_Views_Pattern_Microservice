CREATE PROCEDURE [dbo].[uspSetProduct]
	@Command Varchar(100),

	@ProductIdentity UniqueIdentifier=NULL,
	@ProductName Varchar(100)=NULL,
	@UnitPrice float=NULL
AS
	
	DECLARE @ErrorMessage Varchar(MAX)

	IF @Command='Create-Product'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

				INSERT INTO Product
				(
					ProductIdentity,
					ProductName,
					UnitPrice
				)
				OUTPUT
					inserted.ProductIdentity,
					inserted.ProductName,
					inserted.UnitPrice
				VALUES
				(
					NEWID(),
					@ProductName,
					@UnitPrice
				)

				COMMIT TRANSACTION

			END TRY 

			BEGIN CATCH
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1)
				ROLLBACK TRANSACTION
			END CATCH

		END

	IF @Command='Update-Product'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

				UPDATE Product
					SET 
						ProductName=@ProductName,
						UnitPrice=@UnitPrice
					OUTPUT
						inserted.ProductIdentity,
						inserted.ProductName,
						deleted.ProductName As 'ProductNameOldValue',
						inserted.UnitPrice,
						deleted.UnitPrice AS 'UnitPriceOldValue'
					WHERE
						ProductIdentity=@ProductIdentity

				COMMIT TRANSACTION

			END TRY 

			BEGIN CATCH
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1)
				ROLLBACK TRANSACTION
			END CATCH

		END

	IF @Command='Remove-Product'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

				DELETE FROM Product
					WHERE
						ProductIdentity=@ProductIdentity

				COMMIT TRANSACTION

			END TRY 

			BEGIN CATCH
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1)
				ROLLBACK TRANSACTION
			END CATCH

		END

GO