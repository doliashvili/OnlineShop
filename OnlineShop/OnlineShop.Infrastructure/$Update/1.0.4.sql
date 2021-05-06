CREATE TABLE Carts(
Id bigint NOT NULL,
ProductId bigint NOT NULL,
Price smallmoney NOT NULL,
DiscountPrice smallmoney NULL,
[Name] nvarchar(50) NOT NULL,
Quantity tinyint NOT NULL,
ImageUrl nvarchar(2048) NOT NULL,
CONSTRAINT PK_Carts PRIMARY KEY (Id)
);

CREATE TABLE UsersCarts(
	UserId nvarchar(36) NOT NULL,
	CartId bigint NOT NULL,
	CONSTRAINT PK_UsersCarts PRIMARY KEY (UserId,CartId)
);