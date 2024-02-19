--liquibase formatted sql

-- changeset Aldis:2  
-- comment: Create users table
CREATE TABLE users (
    id INTEGER NOT NULL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL
);

CREATE INDEX idx_users_email ON users (email);
 
-- rollback DROP TABLE users;