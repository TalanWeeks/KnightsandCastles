CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS castles(
  id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
  castleName VARCHAR(64) comment 'castle full name',
  population int NOT NULL DEFAULT 1 comment 'population of the castle'
) default charset utf8 comment '';

CREATE TABLE IF NOT EXISTS knights(
  id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
  knightName VARCHAR(64) comment 'knights full name',
  weapon VARCHAR(128) comment 'knights weapon'

) default charset utf8 comment '';

ALTER TABLE knights ADD COLUMN
  creatorId VARCHAR(128) NOT NULL;

