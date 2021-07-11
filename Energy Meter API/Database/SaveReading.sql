CREATE PROCEDURE SaveReading
	@accountId int,
	@dateTime datetime,
	@readValue int,
	@success bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @success = 0;

	--do not save if account does not exist
	IF NOT EXISTS (SELECT 1 FROM Accounts WHERE AccountId = @accountId) BEGIN
		RETURN
	END

	--do not save if reading already exists
	IF EXISTS (SELECT 1 FROM Readings WHERE AccountId = @accountId AND MeterReadingDateTime = @dateTime AND MeterReadValue = @readValue) BEGIN
		RETURN
	END

	--save new reading and mark as successful
	INSERT INTO Readings VALUES (@accountId, @dateTime, @readValue)
	SET @success = 1;
END
GO