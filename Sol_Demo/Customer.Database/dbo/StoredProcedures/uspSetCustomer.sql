CREATE PROCEDURE [dbo].[uspSetCustomer]
	@Command Varchar(100)=NULL,

	@CustomerIdentity UniqueIdentifier=null,
	@FirstName Varchar(50),
	@LastName varchar(50),

	@MobileNo Varchar(10)
AS
	
	DECLARE @ErrorMessage Varchar(MAX)

	IF @Command='Create-Customer'
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
				OUTPUT
					inserted.CustomerIdentity,
					inserted.FirstName,
					inserted.LastName,
					inserted.MobileNo
				VALUES
				(
					NEWID(),
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

	IF @Command='Update-Customer'
		BEGIN

			BEGIN TRY 

				BEGIN TRANSACTION

				UPDATE Customer
					SET 
						FirstName=@FirstName,
						LastName=@LastName,
						MobileNo=@MobileNo
					OUTPUT
						inserted.CustomerIdentity,
						inserted.FirstName,
						deleted.FirstName As 'FirstNameOldValue',
						inserted.LastName,
						deleted.LastName As 'LastNameOldValue',
						inserted.MobileNo,
						deleted.MobileNo As 'MobileNoOldValue'
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

	IF @Command='Remove-Customer'
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