
CREATE TYPE user_role AS ENUM ('Admin', 'User', 'Guest', 'Staff', 'Manager');
CREATE TYPE account_status AS ENUM ('active', 'inactive', 'suspended');

CREATE TABLE role (
    id SERIAL PRIMARY KEY,
    role_name VARCHAR(50) NOT NULL,
    description TEXT
);

create table account (
  	account_id SERIAL primary key,
    username VARCHAR(50) not null,
    fullname VARCHAR(100),
    email VARCHAR(255),
    password_hash VARCHAR(255) not null,
    --role int references role(id),
    role VARCHAR(255) not null,
    status VARCHAR(50) not null,
    phone VARCHAR(12),
    
    created_at TIMESTAMP default CURRENT_TIMESTAMP,
    created_by INT,
    updated_at TIMESTAMP default CURRENT_TIMESTAMP,
	  updated_by INT,
	  is_deleted BOOLEAN not null default false
);



-- Table for fuel types such as fuel, oil, etc.
CREATE TABLE fuel_type (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255),
    
    created_at TIMESTAMP default CURRENT_TIMESTAMP,
    created_by INT,
    updated_at TIMESTAMP default CURRENT_TIMESTAMP,
	  updated_by INT,
	  is_deleted BOOLEAN not null default false
);

-- Table for prices of fuel (fuel, oil) at different times
CREATE TABLE fuel_price (
    id SERIAL PRIMARY KEY,
    fuel_type_id INT REFERENCES fuel_type(id),
    selling_price DECIMAL(13, 2) NOT NULL,
    import_price DECIMAL(13, 2) NOT null,
    
    created_at TIMESTAMP default CURRENT_TIMESTAMP,
    created_by INT,
    updated_at TIMESTAMP default CURRENT_TIMESTAMP,
	  updated_by INT,
	  is_deleted BOOLEAN not null default false
);

-- Table for managing tanks (fuel, oil, etc.)
CREATE TABLE tank (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    type_id INT REFERENCES fuel_type(id),
    height DECIMAL(13, 2),
    current_volume DECIMAL(13, 2),
    capacity DECIMAL(13, 2),
    
    created_at TIMESTAMP default CURRENT_TIMESTAMP,
    created_by INT,
    updated_at TIMESTAMP default CURRENT_TIMESTAMP,
	  updated_by INT,
	  is_deleted BOOLEAN not null default false
);

-- Table for fuel import history
CREATE TABLE fuel_import (
    id SERIAL PRIMARY KEY,
    tank_id INT REFERENCES tank(id),
    import_volume DECIMAL(13, 2) NOT NULL,
    import_price DECIMAL(13, 2) NOT NULL,
    import_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    weight DECIMAL(13, 2),
    total_cost DECIMAL(15, 2),
    
    created_at TIMESTAMP default CURRENT_TIMESTAMP,
    created_by INT,
    updated_at TIMESTAMP default CURRENT_TIMESTAMP,
	  updated_by INT,
	  is_deleted BOOLEAN not null default false
);

-- Table for managing lubricants (oil and related fuels)
CREATE TABLE lubricant (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    current_stock INT NOT NULL,
    import_price DECIMAL(13, 2),
    sell_price DECIMAL(13, 2),
    import_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    created_at TIMESTAMP default CURRENT_TIMESTAMP,
    created_by INT,
    updated_at TIMESTAMP default CURRENT_TIMESTAMP,
	  updated_by INT,
	  is_deleted BOOLEAN not null default false
);

-- Table for sessions, representing periods of sales and tracking
CREATE TABLE session (
    id SERIAL PRIMARY KEY,
    total_revenue DECIMAL(15, 2),
    cash_for_change DECIMAL(13, 2),
    start_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    end_date TIMESTAMP,
    fuel_price DECIMAL(13, 2),
    
    created_at TIMESTAMP default CURRENT_TIMESTAMP,
    created_by INT,
    updated_at TIMESTAMP default CURRENT_TIMESTAMP,
	  updated_by INT,
	  is_deleted BOOLEAN not null default false
);

-- Table for session details tracking volume and revenue per tank
CREATE TABLE session_detail (
    id SERIAL PRIMARY KEY,
    session_id INT REFERENCES session(id),
    tank_id INT REFERENCES tank(id),
    start_volume DECIMAL(13, 2) NOT NULL,
    end_volume DECIMAL(13, 2) NOT NULL,
    revenue DECIMAL(13, 2),
    
    created_at TIMESTAMP default CURRENT_TIMESTAMP,
    created_by INT,
    updated_at TIMESTAMP default CURRENT_TIMESTAMP,
	  updated_by INT,
	  is_deleted BOOLEAN not null default false
);

-- Table for expense types, e.g., debts, borrowed money, miscellaneous expenses
CREATE TABLE expense_type (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255),
    
    created_at TIMESTAMP default CURRENT_TIMESTAMP,
    created_by INT,
    updated_at TIMESTAMP default CURRENT_TIMESTAMP,
	  updated_by INT,
	  is_deleted BOOLEAN not null default false
);

-- Table for tracking expenses within a session
CREATE TABLE expense (
    id SERIAL PRIMARY KEY,
    session_id INT REFERENCES session(id),
    expense_type_id INT REFERENCES expense_type(id),
    amount DECIMAL(13, 2) NOT NULL,
    note TEXT,
    expense_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    debtor VARCHAR(255),
    image VARCHAR(255),
    
    created_at TIMESTAMP default CURRENT_TIMESTAMP,
    created_by INT,
    updated_at TIMESTAMP default CURRENT_TIMESTAMP,
	  updated_by INT,
	  is_deleted BOOLEAN not null default false
);
