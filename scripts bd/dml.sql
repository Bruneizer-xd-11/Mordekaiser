START TRANSACTION;

UPDATE CuentaLol
SET PuntosLiga = PuntosLiga - 20
WHERE idCuenta = 1;

UPDATE CuentaLol
SET PuntosLiga = PuntosLiga + 20
WHERE idCuenta = 2;

INSERT INTO Inventario (idCuenta, idObjeto, Cantidad)
VALUES (2, 10, 1) 

IF ROW_COUNT() = 0 THEN
    ROLLBACK;
ELSE
    COMMIT;
END IF;
