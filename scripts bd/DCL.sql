
CREATE USER 'AdminRiot'@'LocalHost' IDENTIFIED BY 'PeruChile123!';
Create User 'Usuario1'@'%' IDENTIFIED BY 'ChilePeru123!';
grant UPDATE,SELECT,INSERT on Cuenta.* to 'AdminRiot'@'LocalHost';
grant select on Cuenta.* to 'Usuario1'@'%';

