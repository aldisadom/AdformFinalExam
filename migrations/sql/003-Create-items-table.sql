--liquibase formatted sql

-- changeset Aldis:3  
-- comment: Create items table
CREATE TABLE items (
    id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    price MONEY NOT NULL,
    seller_id UUID NOT NULL,
    CONSTRAINT fk_sellers
      FOREIGN KEY(seller_id) 
        REFERENCES sellers(id)
);

CREATE INDEX idx_items_seller_id ON items (seller_id);
 
-- rollback DROP TABLE items;
