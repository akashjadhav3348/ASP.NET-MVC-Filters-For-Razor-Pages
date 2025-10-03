-- Drop existing procedure if needed
IF OBJECT_ID('dbo.sp_GetAuditTrail', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAuditTrail;
GO

CREATE PROCEDURE dbo.sp_GetAuditTrail
    @UserName NVARCHAR(100) = NULL,
    @Module NVARCHAR(100) = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL
AS
BEGIN
    SELECT *
    FROM dbo.AuditTrail
    WHERE (@UserName IS NULL OR UserName LIKE '%' + @UserName + '%')
      AND (@Module IS NULL OR Module LIKE '%' + @Module + '%')
      AND (@StartDate IS NULL OR Timestamp >= @StartDate)
      AND (@EndDate IS NULL OR Timestamp <= @EndDate)
    ORDER BY Timestamp DESC;
END
GO

EXEC dbo.sp_GetAuditTrail @UserName = 'Akash';
