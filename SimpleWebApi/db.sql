CREATE DATABASE SimpleDatabase;

USE SimpleDatabase;

CREATE TABLE Users (
                    UserId INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
                    Name VARCHAR(200),
                    IsDeleted BOOLEAN DEFAULT 0,
                    Phone VARCHAR(10);

CREATE TABLE Products (
                          ProductId INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
                          Name VARCHAR(200)
);

CREATE TABLE UserProducts (
                              UserProductId INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
                              UserId INT NOT NULL,
                              ProductId INT NOT NULL,
                              Quantity INT NOT NULL DEFAULT (0),
                              FOREIGN KEY (UserId) REFERENCES Users(UserId),
                              FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);


DELIMITER $$

CREATE PROCEDURE sp_RetrieveAllUserProducts()
BEGIN
SELECT up.UserProductId, up.ProductId, up.UserId, up.Quantity, u.Name as `UserName`, p.Name as `ProductName` FROM UserProducts up INNER JOIN Users u ON up.UserId=u.UserId INNER JOIN Products p ON p.ProductId=up.ProductId;
END$$
DELIMITER ;



DELIMITER $$

CREATE PROCEDURE sp_RetrieveUserProducts(IN Id INT)
BEGIN
SELECT UserProductId, UserId, ProductId, Quantity FROM UserProducts WHERE UserId=Id;
END$$
DELIMITER ;

# CALL sp_RetrieveUserProducts(1);

# SELECT up.UserProductId, up.ProductId, up.UserId, up.Quantity, u.Name as `UserName`, p.Name as `ProductName` FROM UserProducts up INNER JOIN Users u ON up.UserId=u.UserId INNER JOIN Products p ON p.ProductId=up.ProductId;
# GO

DELIMITER $$

CREATE PROCEDURE sp_GetAllUsers()
BEGIN
SELECT * FROM Users;
END$$
DELIMITER ;


DELIMITER $$

CREATE PROCEDURE sp_GetUserById(IN Id INT)
BEGIN
SELECT * FROM Users WHERE UserId=Id;
END$$
DELIMITER ;


DELIMITER $$

CREATE PROCEDURE sp_UpdateUserById(Id INT, NameIn VARCHAR(200), isDeletedIn BOOLEAN, phoneIn VARCHAR(10))
BEGIN
UPDATE Users SET Name=NameIn, IsDeleted=isDeletedIn, Phone=phoneIn WHERE UserId=Id;
END$$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE sp_AddUser(IN Id INT, IN nameIn VARCHAR(200), IN phoneIn VARCHAR(10))
BEGIN
INSERT INTO Users (Name, Phone, IsDeleted) VALUES (nameIn, phoneIn, false);
SELECT LAST_INSERT_ID();
END$$
DELIMITER ;


