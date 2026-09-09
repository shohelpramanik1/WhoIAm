-- WHO I AM Database Setup
-- Run this script in SQL Server Management Studio

-- Create Database
CREATE DATABASE WhoIAm;
GO

USE WhoIAm;
GO

-- Enable datetime2 for all timestamp columns
ALTER DATABASE WhoIAm SET DATEFORMAT dmy;
GO

PRINT 'Database WhoIAm created successfully!';
