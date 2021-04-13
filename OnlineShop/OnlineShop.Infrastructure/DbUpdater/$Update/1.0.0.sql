GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;
GO

CREATE DATABASE OnlineShop
-- OnlineShop

CREATE TABLE Products(
Id bigint primary key,
Brand nvarchar(35) NULL,
Color nvarchar(35) NULL,
CreateTime date NULL,
[Description] nvarchar(500) NULL,
Discount float NULL,
Expiration date NULL,
DiscountPrice money NULL,
ForBaby bit NULL,
Gender tinyint NULL,
IsDeleted bit NULL,
[Name] nvarchar(50) NULL,
Price money NOT NULL,
ProductType nvarchar(30) NULL,
[Weight] varchar(max) NULL, -- JSON,
Size varchar(20) NULL
);

CREATE TABLE [Image](
Id bigint primary key,
[Url] varchar(max) NOT NULL,
MainImage bit NOT NULL,
ProductId bigint,
FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

