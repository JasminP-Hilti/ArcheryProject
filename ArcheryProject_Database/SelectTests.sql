

-- Alle Spieler die im Event 1 mitgespielt haben
SELECT p.id, p.firstName, p.lastName, p.nickname
FROM events_has_players ehp
JOIN players p ON ehp.players_id = p.id
WHERE ehp.events_id = 1;

-- Zusätzlich totalPoints und parcoursName ausgeben
SELECT p.id AS playerId, p.firstName, p.lastName, p.nickname, ehp.pointsTotal AS totalPoints, pr.name AS parcoursName
FROM events_has_players ehp
JOIN players p ON ehp.players_id = p.id
JOIN events e ON ehp.events_id = e.id
JOIN parcours pr ON e.parcours_id = pr.id
WHERE ehp.events_id = 1;

-- Alle Parcours von Person Id 2
SELECT e.id AS eventId, e.parcours_id, ehp.pointsTotal AS totalPoints, pr.name AS parcoursName
FROM events_has_players ehp
JOIN events e ON ehp.events_id = e.id
JOIN parcours pr ON e.parcours_id = pr.id
WHERE ehp.players_id = 2;



