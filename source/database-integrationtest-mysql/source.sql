CREATE USER 'testuser'@'localhost' IDENTIFIED BY 'Start123!';

GRANT ALL PRIVILEGES ON *.* TO 'testuser'@'localhost';

FLUSH PRIVILEGES;

CREATE DATABASE Test;

USE Test;

CREATE TABLE TestTable (
    Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    SomeString VARCHAR(200) NOT NULL DEFAULT '',
    LongString MEDIUMTEXT, 
    NumberColumn INT,
    DecimalColumn DECIMAL(15,4),
    BooleanColumn TINYINT
);



