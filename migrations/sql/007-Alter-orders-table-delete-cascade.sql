--liquibase formatted sql

-- changeset Aldis:6
-- comment: Create orders_items table
ALTER TABLE orders
ADD create_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(0);

-- rollback ALTER TABLE orders DROP COLUMN create_date;;
