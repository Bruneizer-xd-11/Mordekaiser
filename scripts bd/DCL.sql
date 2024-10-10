CREATE USER 'Admin'@'LocalHost' IDENTIFIED BY '12345678';
Create User 'Usuario'@'%' IDENTIFIED BY 'holaUpwu';
grant all on Cuenta.* to 'Admin'@'LocalHost';
grant {select} on {Cuenta} to 'Usuario'@'%';
