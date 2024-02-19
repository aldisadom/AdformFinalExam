--liquibase formatted sql

-- changeset Aldis:1  
-- comment: Create sellers table
CREATE TABLE sellers (
    id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    name VARCHAR(50) NOT NULL
);
 
-- rollback DROP TABLE sellers;
