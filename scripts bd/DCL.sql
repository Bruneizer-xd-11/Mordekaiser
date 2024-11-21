delimiter ;
CREATE USER if not EXISTS 'AdminRiot'@'localhost' IDENTIFIED BY 'PeruChile123!';
Create User if not EXISTS 'Usuario1'@'%' IDENTIFIED BY 'ChilePeru123!';
grant UPDATE,SELECT,INSERT on 5to_RiotGames.* to 'AdminRiot'@'localhost';
grant select on 5to_RiotGames.* to 'Usuario1'@'%';

