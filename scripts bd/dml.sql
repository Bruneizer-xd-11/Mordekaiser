START TRANSACTION;

START TRANSACTION;

-- Reducir PuntosRiot de la cuenta de origen
UPDATE 5to_RiotGames.CuentaLol
SET PuntosRiot = PuntosRiot - 100
WHERE idCuenta = 1;

-- Incrementar PuntosRiot en la cuenta de destino
UPDATE 5to_RiotGames.CuentaLol
SET PuntosRiot = PuntosRiot + 100
WHERE idCuenta = 2;

COMMIT;


-- Aumentar los puntos de liga en la cuenta de LOL (en la tabla CuentaLol)
UPDATE 5to_RiotGames.CuentaLol
SET PuntosLiga = PuntosLiga + 29 -- Ajusta el nombre de la columna a PuntosLiga
WHERE IdRango = 2;

-- Si la cuenta también está en CuentaValorant, se actualizan los puntos de competitivo (en la tabla CuentaValorant)
UPDATE 5to_RiotGames.CuentaValorant
SET PuntosCompetitivo = PuntosCompetitivo + 20
WHERE idRango = 1;

COMMIT;
