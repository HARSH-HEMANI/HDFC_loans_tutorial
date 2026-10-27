
CREATE DATABASE [HDFC-Loans];
GO

USE [HDFC-Loans];
GO

CREATE TABLE Users
(
    UserEmail VARCHAR(50) PRIMARY KEY,
    Password VARCHAR(100) NOT NULL
);

CREATE TABLE Account
(
    SavingActNo INT PRIMARY KEY,
    ActHolderName VARCHAR(50),
    Profession VARCHAR(50),
    Email VARCHAR(50),
    ContactNo VARCHAR(50)
);

CREATE TABLE Loan
(
    LoanNo INT IDENTITY(1,1) PRIMARY KEY,
    AccountNo INT,
    LoanCategory VARCHAR(50),
    LoanType VARCHAR(50),
    IssueDate DATE,
    Amount DECIMAL(18,0),
    CurrentAddress VARCHAR(50),
    LoanRemarks VARCHAR(50),

    FOREIGN KEY (AccountNo)
        REFERENCES Account(SavingActNo)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);

INSERT INTO Users (UserEmail, Password)
VALUES ('jay@gmail.com', 'jay@1234');

INSERT INTO Account
    (SavingActNo, ActHolderName, Profession, Email, ContactNo)
VALUES
    (1001, 'Jay Patel', 'Engineer',
     'jay@gmail.com', '9876543210');

INSERT INTO Loan
    (AccountNo, LoanCategory, LoanType,
     IssueDate, Amount, CurrentAddress, LoanRemarks)
VALUES
    (1001, 'Property Loan', 'Home Loan',
     '2026-10-26', 200000, 'Rajkot', 'Sample application');
GO
