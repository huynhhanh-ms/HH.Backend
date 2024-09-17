
CREATE TYPE user_role AS ENUM ('admin', 'user', 'guest', 'staff', 'manager');
CREATE TYPE account_status AS ENUM ('active', 'inactive', 'suspended');

create table account (
  	account_id SERIAL primary key,
    username VARCHAR(50) not null,
    fullname VARCHAR(100),
    email VARCHAR(255),
    password_hash VARCHAR(255) not null,
    role VARCHAR(50) not null,
    status VARCHAR(50) not null,
    phone VARCHAR(12),
    created_at TIMESTAMP default CURRENT_TIMESTAMP,
    created_by INT,
    updated_at TIMESTAMP default CURRENT_TIMESTAMP,
	updated_by INT,
	is_deleted BOOLEAN default false
);

ALTER TABLE account 
ALTER COLUMN created_at SET NOT NULL,
ALTER COLUMN created_by SET NOT NULL,
ALTER COLUMN updated_at SET NOT NULL,
ALTER COLUMN updated_by SET NOT null,
alter column is_deleted set not null;