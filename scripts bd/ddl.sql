SET @OLD_UNIQUE_CHECKS = @@UNIQUE_CHECKS; 
SET UNIQUE_CHECKS = 0;

SET @OLD_FOREIGN_KEY_CHECKS = @@FOREIGN_KEY_CHECKS; 
SET FOREIGN_KEY_CHECKS = 0;

SET @OLD_SQL_MODE = @@SQL_MODE;
SET SQL_MODE = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- Schema
DROP SCHEMA IF EXISTS `5to_RiotGames`;
CREATE SCHEMA IF NOT EXISTS `5to_RiotGames` 
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;
USE `5to_RiotGames`;

-- Servidor
DROP TABLE IF EXISTS Servidor;
CREATE TABLE IF NOT EXISTS Servidor (
  idServidor TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  Nombre     VARCHAR(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  Abreviado  CHAR(4) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  PRIMARY KEY (idServidor),
  UNIQUE INDEX Abreviado_UNIQUE (Abreviado),
  UNIQUE INDEX Nombre_UNIQUE    (Nombre)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Cuenta
DROP TABLE IF EXISTS Cuenta;
CREATE TABLE IF NOT EXISTS Cuenta (
  idCuenta   INT UNSIGNED NOT NULL AUTO_INCREMENT,
  idServidor TINYINT UNSIGNED NOT NULL,
  Nombre     VARCHAR(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  Contrasena CHAR(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  eMail      VARCHAR(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  Rol INT NOT NULL DEFAULT 2,
  PRIMARY KEY (idCuenta),
  INDEX fk_Cuenta_Servidor1_idx (idServidor),
  UNIQUE INDEX uq_CuentaNombre (idServidor, Nombre),
  UNIQUE INDEX uq_CuentaEmail  (eMail, idServidor),
  CONSTRAINT fk_Cuenta_Servidor1
    FOREIGN KEY (idServidor) REFERENCES Servidor (idServidor)
    
    ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- RangoLol
DROP TABLE IF EXISTS RangoLol;
CREATE TABLE IF NOT EXISTS RangoLol (
  idRango               TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  Nombre                VARCHAR(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  PuntosLigaNecesario   MEDIUMINT   NOT NULL,
  Numero                TINYINT UNSIGNED NULL,
  PRIMARY KEY (idRango)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- CuentaLol
DROP TABLE IF EXISTS CuentaLol;
CREATE TABLE IF NOT EXISTS CuentaLol (
  idCuenta     INT UNSIGNED NOT NULL,
  Nombre       VARCHAR(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  Nivel        INT UNSIGNED NOT NULL DEFAULT 0,
  EsenciaAzul  INT UNSIGNED NULL DEFAULT 0,
  PuntosRiot   INT UNSIGNED NULL DEFAULT 0,
  PuntosLiga   MEDIUMINT   NULL DEFAULT 0,
  idRango      TINYINT UNSIGNED NULL DEFAULT 0,
  PRIMARY KEY (idCuenta),
  
  INDEX fk_Cuenta_de_lol_rango_idx (idRango),
  CONSTRAINT fk_Cuenta_de_lol_rango
    FOREIGN KEY (idRango) REFERENCES RangoLol (idRango)
    ON DELETE SET NULL,
    
  CONSTRAINT fk_CuentaLol_Cuenta
    FOREIGN KEY (idCuenta) REFERENCES Cuenta (idCuenta)
    ON DELETE CASCADE

) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
-- TipoObjeto
DROP TABLE IF EXISTS TipoObjeto;
CREATE TABLE IF NOT EXISTS TipoObjeto (
  idTipoObjeto TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  Nombre       VARCHAR(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (idTipoObjeto),
  UNIQUE INDEX nombre_UNIQUE (Nombre)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Objeto
DROP TABLE IF EXISTS Objeto;
CREATE TABLE IF NOT EXISTS Objeto (
  idObjeto     SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  Nombre       VARCHAR(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  PrecioEA     INT UNSIGNED NULL,
  PrecioRP     INT UNSIGNED NULL,
  Venta        INT UNSIGNED NULL,
  idTipoObjeto TINYINT UNSIGNED NOT NULL,
  PRIMARY KEY (idObjeto),
  UNIQUE INDEX Nombre_UNIQUE (Nombre),
  INDEX fk_Objeto_TipoObjeto1_idx (idTipoObjeto),
  CONSTRAINT fk_Objeto_TipoObjeto1
    FOREIGN KEY (idTipoObjeto) REFERENCES TipoObjeto (idTipoObjeto)
    ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Inventario
DROP TABLE IF EXISTS Inventario;
CREATE TABLE IF NOT EXISTS Inventario (
  idCuenta INT UNSIGNED NOT NULL,
  idObjeto SMALLINT UNSIGNED NOT NULL,
  Cantidad INT NULL,
  PRIMARY KEY (idCuenta, idObjeto),
  CONSTRAINT fk_Inventario_Objeto1
    FOREIGN KEY (idObjeto) REFERENCES Objeto (idObjeto)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_Inventario_CuentaLol1
    FOREIGN KEY (idCuenta) REFERENCES CuentaLol (idCuenta)
    ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- CuentaValorant
DROP TABLE IF EXISTS CuentaValorant;
CREATE TABLE IF NOT EXISTS CuentaValorant (
  idCuenta           INT UNSIGNED NOT NULL,
  Nombre             VARCHAR(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  Nivel              INT UNSIGNED NOT NULL DEFAULT 0,
  Experiencia        INT UNSIGNED NOT NULL DEFAULT 0,
  PuntosCompetitivo  MEDIUMINT NOT NULL DEFAULT 0,
  idRango            SMALLINT UNSIGNED NULL DEFAULT NULL,
  INDEX fk_Cuenta_de_valorant_Rango_valorant1_idx (idRango),
  PRIMARY KEY (idCuenta),
  CONSTRAINT fk_Cuenta_de_valorant_Rango_valorant1
    FOREIGN KEY (idRango) REFERENCES RangoValorant (idRango)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_CuentaValorant_Cuenta1
    FOREIGN KEY (idCuenta) REFERENCES Cuenta (idCuenta)
    ON DELETE CASCADE          
    ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- RangoValorant
DROP TABLE IF EXISTS RangoValorant;
CREATE TABLE IF NOT EXISTS RangoValorant (
  idRango           SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  Nombre            VARCHAR(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  Numero            SMALLINT UNSIGNED NULL,
  PuntosNecesarios  MEDIUMINT NOT NULL,
  PRIMARY KEY (idRango),
  UNIQUE INDEX idRango_UNIQUE (idRango),
  UNIQUE INDEX PuntosNecesarios_UNIQUE (PuntosNecesarios)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- InsertServidor 
DROP PROCEDURE IF EXISTS InsertServidor;
DELIMITER $$
CREATE PROCEDURE InsertServidor (
  IN UnidServidor TINYINT UNSIGNED,
  IN UnNombre     VARCHAR(45),
  IN UnAbreviado  CHAR(4)
)
BEGIN
  INSERT INTO Servidor (idServidor, Nombre, Abreviado)
  VALUES (UnidServidor, UnNombre, UnAbreviado);
END $$
DELIMITER ;

-- DeleteObjeto
DROP PROCEDURE IF EXISTS DeleteObjeto;
DELIMITER $$
CREATE PROCEDURE DeleteObjeto (IN UnidObjeto SMALLINT UNSIGNED)
BEGIN
  DELETE FROM Objeto WHERE idObjeto = UnidObjeto;
END $$
DELIMITER ;

-- InsertCuenta 
DROP PROCEDURE IF EXISTS InsertCuenta;
DELIMITER $$
CREATE PROCEDURE InsertCuenta (
  IN UnidCuenta    INT,
  IN UnidServidor  TINYINT UNSIGNED,
  IN UnNombre      VARCHAR(45),
  IN UnContrasena  CHAR(64),
  IN UneMail       VARCHAR(45),
  IN UnRol         INT
)
BEGIN
  INSERT INTO Cuenta (idCuenta, idServidor, Nombre, Contrasena, eMail, Rol)
  VALUES (UnidCuenta, UnidServidor, UnNombre, UnContrasena, UneMail, UnRol);
END $$
DELIMITER ;

-- InsertCuentaValorant
DROP PROCEDURE IF EXISTS InsertCuentaValorant;
DELIMITER $$
CREATE PROCEDURE InsertCuentaValorant (
  IN UnidCuenta           INT,
  IN UnNombre             VARCHAR(45),
  IN UnNivel              INT,
  IN UnExperiencia        INT,
  IN UnPuntosCompetitivo  MEDIUMINT,
  IN UnidRango            SMALLINT UNSIGNED
)
BEGIN
  INSERT INTO CuentaValorant (idCuenta, Nombre, Nivel, Experiencia, PuntosCompetitivo, idRango)
  VALUES (UnidCuenta, UnNombre, UnNivel, UnExperiencia, UnPuntosCompetitivo, UnidRango);
END $$
DELIMITER ;

-- InsertCuentaLol
DROP PROCEDURE IF EXISTS InsertCuentaLol;
DELIMITER $$
CREATE PROCEDURE InsertCuentaLol(
  IN UnidCuenta     INT UNSIGNED,
  IN UnNombre       VARCHAR(45),
  IN UnNivel        INT UNSIGNED,
  IN UnEsenciaAzul  INT UNSIGNED,
  IN UnPuntosRiot   INT UNSIGNED,
  IN UnPuntosLiga   MEDIUMINT,
  IN UnidRango      TINYINT UNSIGNED
)
BEGIN
  INSERT INTO CuentaLol (idCuenta, Nombre, Nivel, EsenciaAzul, PuntosRiot, PuntosLiga, idRango)
  VALUES (UnidCuenta, UnNombre, UnNivel, UnEsenciaAzul, UnPuntosRiot, UnPuntosLiga, UnidRango);
END $$
DELIMITER ;

-- InsertRangoValorant 
DROP PROCEDURE IF EXISTS InsertRangoValorant;
DELIMITER $$
CREATE PROCEDURE InsertRangoValorant (
  OUT UnidRango          SMALLINT UNSIGNED,
  IN  UnNombre           VARCHAR(45),
  IN  UnNumero           SMALLINT UNSIGNED,
  IN  UnPuntosNecesarios MEDIUMINT
)
BEGIN
  INSERT INTO RangoValorant (Nombre, Numero, PuntosNecesarios)
  VALUES (UnNombre, UnNumero, UnPuntosNecesarios);
  SET UnidRango = LAST_INSERT_ID();
END $$
DELIMITER ;

-- InsertRangoLol
DROP PROCEDURE IF EXISTS InsertRangoLol;
DELIMITER $$
CREATE PROCEDURE InsertRangoLol (
  OUT UnidRango             TINYINT,
  IN  UnNombre              VARCHAR(45),
  IN  UnPuntosLigaNecesario MEDIUMINT,
  IN  UnNumero              INT
)
BEGIN
  INSERT INTO RangoLol (Nombre, PuntosLigaNecesario, Numero)
  VALUES (UnNombre, UnPuntosLigaNecesario, UnNumero);
  SET UnidRango = LAST_INSERT_ID();
END $$
DELIMITER ;

  -- Deletes pa borrar
 DROP PROCEDURE IF EXISTS DeleteCuentaLol;
DELIMITER $$
CREATE PROCEDURE DeleteCuentaLol (IN p_idCuenta INT)
BEGIN
    DELETE FROM CuentaLol WHERE idCuenta = p_idCuenta;
END $$
DELIMITER ;


DROP PROCEDURE IF EXISTS DeleteCuenta;
DELIMITER $$
CREATE PROCEDURE DeleteCuenta (IN UnidCuenta INT)
BEGIN
  DELETE FROM Cuenta WHERE idCuenta = UnidCuenta;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS DeleteCuentaValorant;
DELIMITER $$

CREATE PROCEDURE DeleteCuentaValorant (IN p_idCuenta INT)
BEGIN
  DELETE FROM CuentaValorant WHERE idCuenta = p_idCuenta;
END $$

DELIMITER ;

DROP PROCEDURE IF EXISTS DeleteServidor;
DELIMITER $$
CREATE PROCEDURE DeleteServidor (IN p_unidServidor TINYINT UNSIGNED)
BEGIN
  DELETE FROM Servidor WHERE idServidor = p_unidServidor;
END $$
DELIMITER ;

-- Funciones 
DROP FUNCTION IF EXISTS CalcularTotalPuntosLiga;
DELIMITER $$
CREATE FUNCTION CalcularTotalPuntosLiga (UnidCuenta INT)
RETURNS INT READS SQL DATA
BEGIN
  DECLARE TotalPuntos INT;
  SELECT SUM(PuntosLiga) INTO TotalPuntos
    FROM CuentaLol
    WHERE idCuenta = UnidCuenta;
  RETURN TotalPuntos;
END $$
DELIMITER ;

DROP FUNCTION IF EXISTS ObtenerNivelValorant;
DELIMITER $$
CREATE FUNCTION ObtenerNivelValorant (UnidCuenta INT)
RETURNS INT READS SQL DATA
BEGIN
  DECLARE Nivel INT;
  SELECT Nivel INTO Nivel
    FROM CuentaValorant
   WHERE idCuenta = UnidCuenta;
  RETURN Nivel;
END $$
DELIMITER ;

-- Actualizaciones
DROP PROCEDURE IF EXISTS anadirValorLol;
DELIMITER $$
CREATE PROCEDURE anadirValorLol (
  IN UnidCuenta INT,
  IN UnNivel INT,
  IN UnEsenciaAzul INT,
  IN UnPuntosRiot INT,
  IN UnPuntosLiga MEDIUMINT
)
BEGIN
  UPDATE CuentaLol
     SET Nivel = Nivel + UnNivel,
         EsenciaAzul = EsenciaAzul + UnEsenciaAzul,
         PuntosRiot = GREATEST(PuntosRiot + UnPuntosRiot, 0),
         PuntosLiga = PuntosLiga + UnPuntosLiga
   WHERE idCuenta = UnidCuenta;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS anadirValorVal;
DELIMITER $$
CREATE PROCEDURE anadirValorVal (
  IN UnidCuenta INT,
  IN UnNivel INT,
  IN UnExperiencia INT,
  IN UnPuntosCompetitivo MEDIUMINT
)
BEGIN
  UPDATE CuentaValorant
     SET Nivel = Nivel + UnNivel,
         Experiencia = Experiencia + UnExperiencia,
         PuntosCompetitivo = PuntosCompetitivo + UnPuntosCompetitivo
   WHERE idCuenta = UnidCuenta;
END $$
DELIMITER ;

-- InsertTipoObjeto
DROP PROCEDURE IF EXISTS InsertTipoObjeto;
DELIMITER $$
CREATE PROCEDURE InsertTipoObjeto (
  IN UnidTipoObjeto TINYINT,
  IN UnNombre VARCHAR(45)
)
BEGIN
  INSERT INTO TipoObjeto (idTipoObjeto, Nombre)
  VALUES (UnidTipoObjeto, UnNombre);
END $$
DELIMITER ;

-- InsertObjeto
DROP PROCEDURE IF EXISTS InsertObjeto;
DELIMITER $$
CREATE PROCEDURE InsertObjeto (
  IN UnidObjeto    SMALLINT,
  IN UnNombre      VARCHAR(45),
  IN UnPrecioEA    INT,
  IN UnPrecioRP    INT,
  IN UnVenta       INT,
  IN UnidTipoObjeto TINYINT
)
BEGIN
  INSERT INTO Objeto (idObjeto, Nombre, PrecioEA, PrecioRP, Venta, idTipoObjeto)
  VALUES (UnidObjeto, UnNombre, UnPrecioEA, UnPrecioRP, UnVenta, UnidTipoObjeto);
END $$
DELIMITER ;

-- altaProductoInventario
DROP PROCEDURE IF EXISTS altaProductoInventario;
DELIMITER $$
CREATE PROCEDURE altaProductoInventario (
  IN UnidCuenta INT,
  IN UnidObjeto SMALLINT,
  IN UnaCantidad INT
)
BEGIN
  INSERT INTO Inventario (idCuenta, idObjeto, Cantidad)
  VALUES (UnidCuenta, UnidObjeto, UnaCantidad);
END $$
DELIMITER ;

-- Trigger de Cuenta con cifrado xd
DROP TRIGGER IF EXISTS Cuenta_BEFORE_INSERT;
DELIMITER $$
CREATE TRIGGER Cuenta_BEFORE_INSERT
BEFORE INSERT ON Cuenta
FOR EACH ROW
BEGIN
  SET NEW.Contrasena = SHA2(NEW.Contrasena, 256);
END $$
DELIMITER ;


DROP TRIGGER IF EXISTS Cuenta_AFTER_DELETE;
DELIMITER $$
CREATE TRIGGER Cuenta_AFTER_DELETE
AFTER DELETE ON Cuenta
FOR EACH ROW
BEGIN
  DELETE FROM CuentaValorant WHERE idCuenta = OLD.idCuenta;
  DELETE FROM CuentaLol      WHERE idCuenta = OLD.idCuenta;
  DELETE FROM Inventario     WHERE idCuenta = OLD.idCuenta;
END $$
DELIMITER ;


-- Inserts

DROP PROCEDURE IF EXISTS Inserts;
DELIMITER $$
CREATE PROCEDURE Inserts()
BEGIN

  CALL InsertTipoObjeto(1,'Skins');

  CALL InsertServidor(1, 'Norteamérica',       'NA');
  CALL InsertServidor(2, 'Europa Occidental',  'EUW');
  CALL InsertServidor(3, 'Brasil',             'BR');
  CALL InsertServidor(4, 'Corea',              'KR');
  CALL InsertServidor(5, 'Oceanía',            'OC');
  CALL InsertServidor(6, 'Japón',              'JP');
  CALL InsertServidor(7, 'América Latina Norte','LAN');
  CALL InsertServidor(8, 'América Latina Sur', 'LAS');
  CALL InsertServidor(9, 'Turquía',            'TR');
  CALL InsertServidor(10,'Rusia',              'RU');
  -- Cuentas 
  Call InsertCuenta(1, 9, 'Admin',   '123', 'admin@gmail.com',1);
  CALL InsertCuenta(2, 1, 'Luis',   '123', 'Luis@gmail.com',2);
  CALL InsertCuenta(3, 8, 'Ruben',  '123',  'Ruben@gmail.com',2);
  CALL InsertCuenta(4, 3, 'Carlos', '123',   'Carlos@gmail.com',2);
  -- TipoObjeto
  CALL InsertTipoObjeto(2,'Centinelas');
  CALL InsertTipoObjeto(3,'Campeones');
  CALL InsertTipoObjeto(4,'FragmentosSkin');
  CALL InsertTipoObjeto(5,'FragmentosCentinelas');
  CALL InsertTipoObjeto(6,'Gestos');
  CALL InsertTipoObjeto(7,'Accesorios');
  CALL InsertCuentaValorant(3,'CarlosValorant',10,2500,150,5);
  -- Cuentas LoL
-- Cuentas LoL

CALL InsertCuentaLol(1, 'LuisLoL',     5,   40,  10,  120, 1);
CALL InsertCuentaLol(2, 'RubenLoL',   12,  120,  25,  250, 3);
CALL InsertCuentaLol(3, 'CarlosLoL',  22,  350,  70,  550, 5);




-- Rangos LoL
CALL InsertRangoLol(@HierroUno,      'Hierro I',      0,    1);
CALL InsertRangoLol(@HierroDos,      'Hierro II',     100,  2);
CALL InsertRangoLol(@BronceUno,      'Bronce I',      200,  3);
CALL InsertRangoLol(@BronceDos,      'Bronce II',     300,  4);
CALL InsertRangoLol(@PlataUno,       'Plata I',       400,  5);
CALL InsertRangoLol(@PlataDos,       'Plata II',      500,  6);
CALL InsertRangoLol(@OroUno,         'Oro I',         600,  7);
CALL InsertRangoLol(@OroDos,         'Oro II',        700,  8);
CALL InsertRangoLol(@PlatinoUno,     'Platino I',     800,  9);
CALL InsertRangoLol(@PlatinoDos,     'Platino II',    900,  10);

  -- Rangos Valorant
  CALL InsertRangoValorant(@V_Hierro1, 'Hierro', 1, 10);
  CALL InsertRangoValorant(@V_Hierro2, 'Hierro', 2, 20);
  CALL InsertRangoValorant(@V_Hierro3, 'Hierro', 3, 30);
  CALL InsertRangoValorant(@V_Bronce1, 'Bronce', 1, 40);
  CALL InsertRangoValorant(@V_Bronce2, 'Bronce', 2, 50);
  CALL InsertRangoValorant(@V_Bronce3, 'Bronce', 3, 60);
  CALL InsertRangoValorant(@V_Plata1,  'Plata',  1, 80);
  CALL InsertRangoValorant(@V_Plata2,  'Plata',  2, 90);
  CALL InsertRangoValorant(@V_Plata3,  'Plata',  3, 100);
  CALL InsertRangoValorant(@V_Oro1,    'Oro',    1, 110);
  CALL InsertRangoValorant(@V_Oro2,    'Oro',    2, 120);
  CALL InsertRangoValorant(@V_Oro3,    'Oro',    3, 130);
  CALL InsertRangoValorant(@V_Platino1,'Platino',1, 140);
  CALL InsertRangoValorant(@V_Platino2,'Platino',2, 150);
  CALL InsertRangoValorant(@V_Platino3,'Platino',3, 160);
  CALL InsertRangoValorant(@V_Diam1,   'Diamante',1,170);
  CALL InsertRangoValorant(@V_Diam2,   'Diamante',2,180);
  CALL InsertRangoValorant(@V_Diam3,   'Diamante',3,190);
END $$
DELIMITER ;

CALL Inserts();
SET SQL_MODE = @OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS = @OLD_UNIQUE_CHECKS;
