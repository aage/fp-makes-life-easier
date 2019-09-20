-- run on localdb : (LocalDb)\MSSQLLocalDB

CREATE DATABASE Calender
GO

USE Calender

CREATE TABLE Events (
	[Id] UNIQUEIDENTIFIER,
	[Title] NVARCHAR(100),
	[Description] NVARCHAR(1000),
	[When] DATETIME,
	[End] DATETIME
);