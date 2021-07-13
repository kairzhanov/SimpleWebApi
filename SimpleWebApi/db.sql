CREATE DATABASE SimpleDatabase;

USE SimpleDatabase;

CREATE TABLE Users (
                       UserId INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
                       Name VARCHAR(200));

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

SELECT up.UserProductId, up.ProductId, up.UserId, up.Quantity, u.Name as `UserName`, p.Name as `ProductName` FROM UserProducts up INNER JOIN Users u ON up.UserId=u.UserId INNER JOIN Products p ON p.ProductId=up.ProductId

