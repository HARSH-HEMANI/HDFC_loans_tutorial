
USE [HDFC-Loans];
GO

UPDATE dbo.Users
SET UserEmail = 'harsh@gmail.com',
    [Password] = 'Abc@123'
WHERE UserEmail = 'jay@gmail.com';
GO
