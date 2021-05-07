CREATE TABLE Carts(
Id bigint NOT NULL,
ProductId bigint NOT NULL,
UserId nvarchar(450) NOT NULL,
Quantity tinyint NOT NULL,

CONSTRAINT PK_Carts PRIMARY KEY (Id),

CONSTRAINT FK_Carts_ProductId FOREIGN KEY (ProductId)
      REFERENCES Products (Id)
      ON DELETE CASCADE
      ON UPDATE CASCADE,

CONSTRAINT FK_Carts_UserId FOREIGN KEY (UserId)
      REFERENCES Users (Id)
      ON DELETE CASCADE
      ON UPDATE CASCADE,
);

