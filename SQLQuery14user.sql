CREATE TABLE users(
    id INT IDENTITY(1,1) PRIMARY KEY,
    userId VARCHAR(255) NOT NULL,
    tc VARCHAR(11) NOT NULL,
    email VARCHAR(255) NOT NULL,
    pwd VARCHAR(255) NOT NULL
);