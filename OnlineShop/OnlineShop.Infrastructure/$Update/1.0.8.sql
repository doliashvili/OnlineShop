ALTER TABLE Products
DROP CONSTRAINT df_Quantity;

ALTER TABLE Products
DROP COLUMN Quantity;

ALTER TABLE Products ADD Quantity smallint NOT NULL
CONSTRAINT df_Quantity DEFAULT 1;

ALTER TABLE Carts
ALTER COLUMN Quantity smallint NOT NULL;