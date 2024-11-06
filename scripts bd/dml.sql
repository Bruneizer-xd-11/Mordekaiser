START TRANSACTION;

SELECT @TotalPuntosRiot := SUM(PuntosRiot) 
FROM 5to_RiotGames.CuentaLol 
WHERE idCuenta = 0; 

UPDATE 5to_RiotGames.CuentaLol
SET PuntosLiga = PuntosLiga + 50, EsenciaAzul = EsenciaAzul + 20
WHERE idCuenta = 0;

UPDATE 5to_RiotGames.CuentaValorant
SET Experiecia = Experiecia + 100, PuntosCompetitivo = PuntosCompetitivo + 20
WHERE idCuenta = 1;

COMMIT;
