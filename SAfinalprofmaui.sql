
CREATE DATABASE IF NOT EXISTS `villagerentals1`;
USE `villagerentals1`;

drop table IF EXISTS rental;
drop table IF EXISTS equipment;
drop table IF EXISTS equip_cate;
drop table IF EXISTS customers;

USE `villagerentals1`;
-- Table for Customers
CREATE TABLE IF NOT EXISTS `customers` (
  `customer_id` INT AUTO_INCREMENT,
  `f_name` VARCHAR(50),
  `l_name` VARCHAR(50),
  `phone_num` int(11), 
  `email` VARCHAR(50),
  PRIMARY KEY (`customer_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

USE `villagerentals1`;
-- Table for equip_cate
CREATE TABLE IF NOT EXISTS `equip_cate` (
  `cate_num` INT AUTO_INCREMENT,
  `cate_desc` VARCHAR(200),
  PRIMARY KEY (`cate_num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

USE `villagerentals1`;
-- Table for equipment
CREATE TABLE IF NOT EXISTS `equipment` (
  `equip_id` INT AUTO_INCREMENT,
  `cate_num` INT,
  `equip_name` VARCHAR(50),
  `daily_cost` INT(10), 
  `description` VARCHAR(50),
  PRIMARY KEY (`equip_id`),
  FOREIGN KEY (`cate_num`) REFERENCES `equip_cate`(`cate_num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

USE `villagerentals1`;
-- Table for rental
CREATE TABLE IF NOT EXISTS `rental` (
  `rental_id` INT AUTO_INCREMENT,
  `customer_id` INT,
  `equip_id` INT, -- Added this line to include equipment
  `current_date` DATETIME,
  `rental_date` DATETIME,
  `return_date` DATETIME,
  `rental_cost` INT(10),
  PRIMARY KEY (`rental_id`),
  FOREIGN KEY (`customer_id`) REFERENCES `customers`(`customer_id`),
  FOREIGN KEY (`equip_id`) REFERENCES `equipment`(`equip_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;