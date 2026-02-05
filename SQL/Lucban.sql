CREATE TABLE Customers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    company_name VARCHAR(100) NOT NULL,
    contact_person VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    phone VARCHAR(50) NOT NULL,
    location VARCHAR(150) NOT NULL,
    stations_count INT NOT NULL,
    status ENUM('ACTIVE', 'INACTIVE') NOT NULL
);
 
 
CREATE TABLE Stations (
    id INT AUTO_INCREMENT PRIMARY KEY,
    station_name VARCHAR(100) NOT NULL,
    customer_id INT NOT NULL,
    address VARCHAR(150) NOT NULL,
    city VARCHAR(100) NOT NULL,
    status ENUM('ACTIVE', 'INACTIVE') NOT NULL,
    CONSTRAINT fk_stations_customer
        FOREIGN KEY (customer_id)
        REFERENCES Customers(id)
        ON DELETE CASCADE
);
 
 
INSERT INTO Customers
(company_name, contact_person, email, phone, location, stations_count, status)
VALUES
('Shell Philippines', 'John Smith', 'john@shell.ph', '02-8888-1111', 'Makati City', 15, 'ACTIVE'),
('Petron Corporation', 'Maria Santos', 'maria@petron.com', '02-8888-2222', 'Ortigas Center', 12, 'ACTIVE'),
('Caltex Philippines', 'Pedro Cruz', 'pedro@caltex.ph', '02-8888-3333', 'BGC Taguig', 8, 'ACTIVE'),
('Phoenix Petroleum', 'Ana Reyes', 'ana@phoenix.ph', '02-8888-4444', 'Pasig City', 5, 'ACTIVE'),
('Seaoil Philippines', 'Luis Garcia', 'luis@seaoil.com', '02-8888-5555', 'Quezon City', 2, 'INACTIVE');
 
 
INSERT INTO Stations
(station_name, customer_id, address, city, status)
VALUES
('Shell EDSA', 1, '123 EDSA Avenue', 'Quezon City', 'ACTIVE'),
('Shell Ortigas', 1, '456 Ortigas Ave', 'Pasig', 'ACTIVE'),
('Shell Commonwealth', 1, '888 Commonwealth Ave', 'Quezon City', 'INACTIVE'),
 
('Petron Makati', 2, '789 Ayala Ave', 'Makati', 'ACTIVE'),
('Petron BGC', 2, '321 Bonifacio High St', 'Taguig', 'ACTIVE'),
 
('Caltex BGC', 3, '555 Market Drive', 'Taguig', 'ACTIVE'),
 
('Phoenix Alabang', 4, '999 Alabang-Zapote Rd', 'Muntinlupa', 'ACTIVE');