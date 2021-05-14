CREATE PROCEDURE [dbo].[uspSetProduct]
	@Command Varchar(100),

	@ProductIdentity UniqueIdentifier=NULL,
	@ProductName Varchar(100)=NULL,
	@UnitPrice float=NULL
AS
	
	DECLARE @ErrorMessage Varchar(MAX)

	IF @Command='Product-Created'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

				INSERT INTO Product
				(
					ProductIdentity,
					ProductName,
					UnitPrice
				)
				VALUES
				(
					@ProductIdentity,
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

	IF @Command='Product-Name-Changed'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

				UPDATE Product
					SET
						ProductName=@ProductName
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

	IF @Command='Product-UnitPrice-Changed'
	BEGIN

		BEGIN TRY 

			BEGIN TRANSACTION

			UPDATE Product
				SET
					UnitPrice=@UnitPrice
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

	IF @Command='Product-Removed'
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