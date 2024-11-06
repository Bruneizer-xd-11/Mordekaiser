START TRANSACTION;

-- Obtener la suma de PuntosLiga necesarios de una cuenta específica en RangoLol
SELECT @TotalPuntosLigaNecesario := SUM(PuntosLigaNecesarios) 
FROM 5to_RiotGames.RangoLol 
WHERE IdRango = 2;

-- Obtener la suma de PuntosCompetitivo de una cuenta específica en RangoValorant
SELECT @TotalPuntosCompetitivo := SUM(PuntosCompetitivo) 
FROM 5to_RiotGames.RangoValorant 
WHERE idRango = 1;

-- Aumentar los puntos de liga en la cuenta de LOL (en la tabla CuentaLol)
UPDATE 5to_RiotGames.CuentaLol
SET PuntosLiga = PuntosLiga + 29 -- Ajusta el nombre de la columna a PuntosLiga
WHERE IdRango = 2;

-- Si la cuenta también está en CuentaValorant, se actualizan los puntos de competitivo (en la tabla CuentaValorant)
UPDATE 5to_RiotGames.CuentaValorant
SET PuntosCompetitivo = PuntosCompetitivo + 20
WHERE idRango = 1;

COMMIT;
