SELECT 
    "Temp"."Id", 
    "BG"."Title", 
    "Temp"."CountOfUserWhoLikeIt",
    CAST("Temp"."Rank" AS int) AS "Rank"
FROM (
    SELECT 
        "BG"."Id", 
        SUM(
            CASE WHEN "BGU"."UsersWhoFavoriteThisBoardGameId" IS NULL
                THEN 0
                ELSE 1
            END
        ) AS "CountOfUserWhoLikeIt",
        ROW_NUMBER() OVER (ORDER BY SUM(
            CASE WHEN "BGU"."UsersWhoFavoriteThisBoardGameId" IS NULL
                THEN 0
                ELSE 1
            END) DESC) AS "Rank"
    FROM "BoardGames" AS "BG"
    LEFT JOIN "BoardGameUser" AS "BGU" ON "BG"."Id" = "BGU"."FavoriteBoardsGamesId" 
    GROUP BY "BG"."Id"
) AS "Temp"
LEFT JOIN "BoardGames" AS "BG" ON "BG"."Id" = "Temp"."Id"
WHERE "Temp"."Rank" <= 3
ORDER BY "Temp"."Rank" 
