SELECT "Temp"."Id", "BG"."Title", "BG"."Price", "Temp"."CountOfUserWhoLikeIt"
FROM (
    SELECT "BG"."Id", SUM(
        CASE WHEN "BGU"."UsersWhoFavoriteThisBoardGameId" IS NULL
            THEN 0
            ELSE 1
        END
    ) AS "CountOfUserWhoLikeIt"
    FROM "BoardGames" AS "BG"
    LEFT JOIN "BoardGameUser" AS "BGU" ON "BG"."Id" = "BGU"."FavoriteBoardsGamesId"
    GROUP BY "BG"."Id"
    ORDER BY "CountOfUserWhoLikeIt" DESC
    LIMIT 1
) AS "Temp"
LEFT JOIN "BoardGames" AS "BG" ON "BG"."Id" = "Temp"."Id"
