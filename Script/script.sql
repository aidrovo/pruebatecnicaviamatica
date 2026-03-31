CREATE PROCEDURE GetUsersByName
    @term NVARCHAR(50)
AS
BEGIN
    SELECT * 
    FROM Users
    WHERE Name LIKE '%' + @term + '%'
      AND IsDeleted = 0
END