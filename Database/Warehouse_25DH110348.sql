/*
  Warehouse_25DH110348 — run this script in SQL Server Management Studio.
  The sample rows provide stock quantities 10, 4, 3, 0, and 5 for the exam tests.
*/
IF DB_ID(N'Warehouse_25DH110348') IS NULL
    CREATE DATABASE [Warehouse_25DH110348];
GO

USE [Warehouse_25DH110348];
GO

IF OBJECT_ID(N'dbo.Products', N'U') IS NOT NULL
    DROP TABLE dbo.Products;
GO

CREATE TABLE dbo.Products
(
    ProductID   int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Products PRIMARY KEY,
    ProductName nvarchar(100) NOT NULL,
    Category    nvarchar(100) NOT NULL,
    Price       decimal(18,2) NOT NULL,
    Quantity    int NOT NULL,
    CONSTRAINT CK_Products_Price CHECK (Price > 0),
    CONSTRAINT CK_Products_Quantity CHECK (Quantity >= 0)
);
GO

INSERT INTO dbo.Products (ProductName, Category, Price, Quantity)
VALUES
    (N'Laptop Nguyễn Nhật Hào 25DH110348',     N'Điện tử Nguyễn Nhật Hào 25DH110348', 18500000.00, 10),
    (N'Màn hình Nguyễn Nhật Hào 25DH110348',   N'Điện tử Nguyễn Nhật Hào 25DH110348',  4200000.00,  4),
    (N'Chuột Nguyễn Nhật Hào 25DH110348',      N'Phụ kiện Nguyễn Nhật Hào 25DH110348',  350000.00,  3),
    (N'Bàn phím Nguyễn Nhật Hào 25DH110348',   N'Phụ kiện Nguyễn Nhật Hào 25DH110348',  650000.00,  0),
    (N'Tai nghe Nguyễn Nhật Hào 25DH110348',   N'Phụ kiện Nguyễn Nhật Hào 25DH110348',  890000.00,  5);
GO

SELECT ProductID, ProductName, Category, Price, Quantity FROM dbo.Products ORDER BY ProductID;
