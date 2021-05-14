CREATE PROCEDURE [dbo].[uspSetCustomer]
	@Command Varchar(100)=NULL,

	@CustomerIdentity UniqueIdentifier=null,
	@FirstName Varchar(50),
	@LastName varchar(50),

	@MobileNo Varchar(10)
AS
	
	DECLARE @ErrorMessage Varchar(MAX)

	IF @Command='Customer-Created'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

				INSERT INTO Customer
				(
					CustomerIdentity,
					FirstName,
					LastName,
					MobileNo
				)
				VALUES
				(
					@CustomerIdentity,
					@FirstName,
					@LastName,
					@MobileNo
				)

				COMMIT TRANSACTION

			END TRY 

			BEGIN CATCH
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1)
				ROLLBACK TRANSACTION
			END CATCH

		END
	IF @Command='Customer_Name_Changed'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

				UPDATE Customer
					SET 
						FirstName=@FirstName,
						LastName=@LastName
					WHERE	
						CustomerIdentity=@CustomerIdentity


				COMMIT TRANSACTION

			END TRY 

			BEGIN CATCH
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1)
				ROLLBACK TRANSACTION
			END CATCH

		END
	IF @Command='Customer_MobileNo_Changed'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

				UPDATE Customer
					SET 
						MobileNo=@MobileNo
					WHERE	
						CustomerIdentity=@CustomerIdentity


				COMMIT TRANSACTION

			END TRY 

			BEGIN CATCH
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1)
				ROLLBACK TRANSACTION
			END CATCH

		END

	IF @Command='Customer-Removed'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

				DELETE Customer
					WHERE
						CustomerIdentity=@CustomerIdentity

				COMMIT TRANSACTION

			END TRY 

			BEGIN CATCH
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1)
				ROLLBACK TRANSACTION
			END CATCH

		END


GO