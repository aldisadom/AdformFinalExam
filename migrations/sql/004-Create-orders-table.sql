--liquibase formatted sql

-- changeset Aldis:4
-- comment: Create orders table
CREATE TABLE orders (
    id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    status VARCHAR(255) NOT NULL,
    user_id INTEGER NOT NULL,
    seller_id UUID NOT NULL,
    CONSTRAINT fk_users
      FOREIGN KEY(user_id) 
        REFERENCES users(id),
    CONSTRAINT fk_sellers
      FOREIGN KEY(seller_id) 
        REFERENCES sellers(id)
);

CREATE INDEX idx_orders_user_id ON orders (user_id);
CREATE INDEX idx_orders_seller_id ON orders (seller_id);

-- rollback DROP TABLE orders;
