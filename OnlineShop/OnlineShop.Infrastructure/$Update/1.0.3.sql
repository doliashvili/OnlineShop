﻿GO
ALTER TABLE Products ADD Quantity TINYINT NOT NULL
CONSTRAINT df_Quantity DEFAULT 1;
GO