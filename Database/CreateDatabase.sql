-- Use the newly created database
USE PCPartsWebsiteDb;
GO

-- Create Categories table
CREATE TABLE Categories (
    Id int IDENTITY(1,1) NOT NULL,
    Name nvarchar(100) NOT NULL,
    Description nvarchar(500) NULL,
    CreatedDate datetime2(7) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_Categories PRIMARY KEY (Id),
    CONSTRAINT UK_Categories_Name UNIQUE (Name)
);

-- Create Products table
CREATE TABLE Products (
    Id int IDENTITY(1,1) NOT NULL,
    Name nvarchar(200) NOT NULL,
    Description nvarchar(1000) NULL,
    Price decimal(18,2) NOT NULL,
    ImageUrl nvarchar(500) NULL,
    StockQuantity int NOT NULL DEFAULT 0,
    AverageRating float NOT NULL DEFAULT 0,
    ReviewCount int NOT NULL DEFAULT 0,
    CreatedDate datetime2(7) NOT NULL DEFAULT GETDATE(),
    IsActive bit NOT NULL DEFAULT 1,
    CategoryId int NOT NULL,
    CONSTRAINT PK_Products PRIMARY KEY (Id),
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- ASP.NET Identity Tables
CREATE TABLE AspNetUsers (
    Id nvarchar(450) NOT NULL,
    UserName nvarchar(256) NULL,
    NormalizedUserName nvarchar(256) NULL,
    Email nvarchar(256) NULL,
    NormalizedEmail nvarchar(256) NULL,
    EmailConfirmed bit NOT NULL DEFAULT 0,
    PasswordHash nvarchar(max) NULL,
    SecurityStamp nvarchar(max) NULL,
    ConcurrencyStamp nvarchar(max) NULL,
    PhoneNumber nvarchar(max) NULL,
    PhoneNumberConfirmed bit NOT NULL DEFAULT 0,
    TwoFactorEnabled bit NOT NULL DEFAULT 0,
    LockoutEnd datetimeoffset(7) NULL,
    LockoutEnabled bit NOT NULL DEFAULT 0,
    AccessFailedCount int NOT NULL DEFAULT 0,
    FirstName nvarchar(100) NULL,
    LastName nvarchar(100) NULL,
    CreatedDate datetime2(7) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_AspNetUsers PRIMARY KEY (Id)
);

CREATE TABLE AspNetRoles (
    Id nvarchar(450) NOT NULL,
    Name nvarchar(256) NULL,
    NormalizedName nvarchar(256) NULL,
    ConcurrencyStamp nvarchar(max) NULL,
    CONSTRAINT PK_AspNetRoles PRIMARY KEY (Id)
);

CREATE TABLE AspNetUserRoles (
    UserId nvarchar(450) NOT NULL,
    RoleId nvarchar(450) NOT NULL,
    CONSTRAINT PK_AspNetUserRoles PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_AspNetUserRoles_Users FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
    CONSTRAINT FK_AspNetUserRoles_Roles FOREIGN KEY (RoleId) REFERENCES AspNetRoles(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserClaims (
    Id int IDENTITY(1,1) NOT NULL,
    UserId nvarchar(450) NOT NULL,
    ClaimType nvarchar(max) NULL,
    ClaimValue nvarchar(max) NULL,
    CONSTRAINT PK_AspNetUserClaims PRIMARY KEY (Id),
    CONSTRAINT FK_AspNetUserClaims_Users FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserLogins (
    LoginProvider nvarchar(450) NOT NULL,
    ProviderKey nvarchar(450) NOT NULL,
    ProviderDisplayName nvarchar(max) NULL,
    UserId nvarchar(450) NOT NULL,
    CONSTRAINT PK_AspNetUserLogins PRIMARY KEY (LoginProvider, ProviderKey),
    CONSTRAINT FK_AspNetUserLogins_Users FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserTokens (
    UserId nvarchar(450) NOT NULL,
    LoginProvider nvarchar(450) NOT NULL,
    Name nvarchar(450) NOT NULL,
    Value nvarchar(max) NULL,
    CONSTRAINT PK_AspNetUserTokens PRIMARY KEY (UserId, LoginProvider, Name),
    CONSTRAINT FK_AspNetUserTokens_Users FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetRoleClaims (
    Id int IDENTITY(1,1) NOT NULL,
    RoleId nvarchar(450) NOT NULL,
    ClaimType nvarchar(max) NULL,
    ClaimValue nvarchar(max) NULL,
    CONSTRAINT PK_AspNetRoleClaims PRIMARY KEY (Id),
    CONSTRAINT FK_AspNetRoleClaims_Roles FOREIGN KEY (RoleId) REFERENCES AspNetRoles(Id) ON DELETE CASCADE
);

-- CartItems table
CREATE TABLE CartItems (
    Id int IDENTITY(1,1) NOT NULL,
    UserId nvarchar(450) NOT NULL,
    ProductId int NOT NULL,
    Quantity int NOT NULL DEFAULT 1,
    DateAdded datetime2(7) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_CartItems PRIMARY KEY (Id),
    CONSTRAINT FK_CartItems_Users FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
    CONSTRAINT FK_CartItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE
);

-- Insert Sample Data
INSERT INTO Categories (Name, Description) VALUES
('Processors', 'CPUs and processors for high-performance computing'),
('Graphics Cards', 'GPUs and video cards for gaming and professional work'),
('Memory', 'RAM and memory modules for system performance'),
('Storage', 'SSDs, HDDs, and storage devices for data storage'),
('Motherboards', 'Motherboards and chipsets for system foundation'),
('Power Supplies', 'PSUs and power supplies for stable power delivery'),
('Cases', 'PC cases and enclosures for system protection'),
('Cooling', 'Fans, coolers, and thermal solutions for temperature management');

INSERT INTO Products (Name, Description, Price, CategoryId, StockQuantity, AverageRating, ReviewCount, ImageUrl) VALUES
('AMD Ryzen 9 7950X', '16-Core, 32-Thread Unlocked Desktop Processor', 699.99, 1, 50, 4.8, 245, '/images/products/ryzen-9-7950x.jpg'),
('NVIDIA GeForce RTX 4090', '24GB GDDR6X Graphics Card', 1599.99, 2, 25, 4.9, 189, '/images/products/rtx-4090.jpg'),
('Corsair Vengeance DDR5', '32GB (2x16GB) DDR5-5600 C36 Memory Kit', 299.99, 3, 75, 4.7, 156, '/images/products/corsair-vengeance-ddr5.jpg'),
('Samsung 980 PRO SSD', '2TB NVMe M.2 Internal SSD', 199.99, 4, 100, 4.6, 312, '/images/products/samsung-980-pro.jpg'),
('ASUS ROG STRIX X670E-E', 'AM5 ATX Gaming Motherboard', 449.99, 5, 30, 4.5, 89, '/images/products/asus-rog-strix-x670e.jpg'),
('Corsair RM850x', '850W 80+ Gold Certified PSU', 139.99, 6, 60, 4.8, 203, '/images/products/corsair-rm850x.jpg'),
('NZXT H7 Flow', 'Mid-Tower ATX PC Case', 129.99, 7, 40, 4.4, 167, '/images/products/nzxt-h7-flow.jpg'),
('Noctua NH-D15', 'Premium CPU Cooler', 99.99, 8, 80, 4.9, 278, '/images/products/noctua-nh-d15.jpg');

INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES
(NEWID(), 'Admin', 'ADMIN', NEWID()),
(NEWID(), 'Customer', 'CUSTOMER', NEWID()),
(NEWID(), 'Manager', 'MANAGER', NEWID());

-- Verify creation
SELECT 'Database created successfully!' AS Status;
SELECT COUNT(*) AS Categories FROM Categories;
SELECT COUNT(*) AS Products FROM Products;
