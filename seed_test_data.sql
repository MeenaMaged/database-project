SET NOCOUNT ON;
DECLARE @custId INT, @vendUserId INT, @vendId INT;
DECLARE @cat1 INT, @cat2 INT, @cat3 INT;

INSERT INTO Users (Email, Password, Role, FirstName, LastName, Phone, AddressName, Lat, Long)
VALUES ('customer_test@mail.com', 'password', 'User', 'Test', 'Customer', '0100', 'Cairo', 30.04, 31.23);
SET @custId = SCOPE_IDENTITY();

INSERT INTO Users (Email, Password, Role, FirstName, LastName)
VALUES ('vendor_test@mail.com', 'password', 'Vendor', 'Test', 'Vendor');
SET @vendUserId = SCOPE_IDENTITY();

INSERT INTO Vendor (UserId, StoreName) VALUES (@vendUserId, 'Test Store');
SET @vendId = SCOPE_IDENTITY();

INSERT INTO Category (Name) VALUES ('Electronics');  SET @cat1 = SCOPE_IDENTITY();
INSERT INTO Category (Name) VALUES ('Books');        SET @cat2 = SCOPE_IDENTITY();
INSERT INTO Category (Name) VALUES ('Clothing');     SET @cat3 = SCOPE_IDENTITY();

INSERT INTO Product (VendorId, CategoryId, Name, Description, Price, Stock) VALUES
 (@vendId, @cat1, 'Laptop',     'Fast laptop',   25000, 5),
 (@vendId, @cat1, 'Headphones', 'Noise-cancel',  800,   0),
 (@vendId, @cat2, 'SQL Book',   'Elmasri DB',    350,   12),
 (@vendId, @cat3, 'T-Shirt',    'Cotton tee',    200,   30);

PRINT 'Seed complete';

