-- Active: 1691412339871@@127.0.0.1@3306@5to_RiotGames
BEGIN TRANSACTION;

BEGIN TRY
    -- Paso 1: Declarar variables
    DECLARE @nuevo_usuario NVARCHAR(50) = 'Summoner1';
    DECLARE @nivel INT = 1;
    DECLARE @esencia_azul INT = 500;
    DECLARE @puntos_riot INT = 100;
    DECLARE @puntos_liga INT;
    DECLARE @id_rango BYTE;
    
    -- Paso 2: Seleccionar puntos de liga y ID de rango desde la tabla de rangos de League of Legends
    SELECT @puntos_liga = PuntosLigaNecesarios, @id_rango = IdRango
    FROM RangoLol
    WHERE Nombre = 'Plata';  -- Suponiendo que queremos asignar el rango "Plata"

    -- Paso 3: Insertar la nueva cuenta en cuentas_lol con los valores seleccionados
    INSERT INTO cuentas_lol (nombre_usuario, nivel, esencia_azul, puntos_riot, puntos_liga, id_rango)
    VALUES (@nuevo_usuario, @nivel, @esencia_azul, @puntos_riot, @puntos_liga, @id_rango);

    -- Confirmar la transacción
    COMMIT;
    PRINT 'Transacción completada exitosamente.';

END TRY
BEGIN CATCH
    -- Si ocurre un error, revertir la transacción
    ROLLBACK;
    PRINT 'Ocurrió un error en la transacción. Se ha revertido.';
    THROW;
END CATCH;

-- Consultar los valores para verificar la inserción
SELECT * FROM cuentas_lol;
