GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;
GO

-- dbo

--CREATE TABLE [dbo].[SysSettings](
--	[Id] [varchar](50) NOT NULL,
--	[Value] [nvarchar](255) NOT NULL,
--	CONSTRAINT [PK_dbo_SysSettings] PRIMARY KEY CLUSTERED ([Id])
--	WITH (FILLFACTOR = 100)
--) 
--GO

--CREATE PROCEDURE [dbo].[SetSysSetting] (@id varchar(50), @value nvarchar(255))
--AS
--BEGIN
--	SET NOCOUNT ON;

--	UPDATE [dbo].[SysSettings] WITH (ROWLOCK)
--	SET [Value] = @value
--	WHERE [Id] = @id;
--	IF @@ROWCOUNT = 0
--	BEGIN
--		INSERT INTO [dbo].[SysSettings] WITH (ROWLOCK) ([Id], [Value])
--		VALUES (@id, @value);
--	END;
--	RETURN 0;
--END;
--GO

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

