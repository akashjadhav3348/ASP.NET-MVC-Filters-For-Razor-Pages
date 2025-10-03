CREATE TABLE AuditTrail (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Module NVARCHAR(100) NOT NULL,
    Action NVARCHAR(200) NOT NULL,
    Timestamp DATETIME NOT NULL DEFAULT GETDATE()
);

-- Optional: Insert sample data
INSERT INTO AuditTrail (UserName, Module, Action, Timestamp)
VALUES
('Akash', 'Login', 'User logged in successfully', GETDATE()),
('Nikita', 'Certificate', 'Generated course completion certificate', GETDATE()),
('Ganesh', 'Payment', 'Processed payment of 5000', GETDATE()),
('Ravi', 'Login', 'Failed login attempt', GETDATE()),
('Sonia', 'Audit', 'Viewed audit logs', GETDATE());
