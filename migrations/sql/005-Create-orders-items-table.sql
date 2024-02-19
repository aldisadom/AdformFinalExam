--liquibase formatted sql

-- changeset Aldis:5
-- comment: Create orders_items table
CREATE TABLE orders_items (
    order_id UUID NOT NULL,
    item_id UUID NOT NULL,
    CONSTRAINT fk_orders
      FOREIGN KEY(order_id) 
        REFERENCES orders(id),
    CONSTRAINT fk_items
      FOREIGN KEY(item_id) 
        REFERENCES items(id)
);

CREATE INDEX idx_orders_items_order_id ON orders_items (order_id);
CREATE INDEX idx_orders_items_item_id ON orders_items (item_id);

-- rollback DROP TABLE orders_items;
