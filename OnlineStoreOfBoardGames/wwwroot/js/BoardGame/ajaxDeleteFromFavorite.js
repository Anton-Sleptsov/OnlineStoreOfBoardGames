document.addEventListener("DOMContentLoaded", function () {
  const hub = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/boardGame")
    .build();
  hub.start();

  const deleteButtons = document.querySelectorAll(".delete-link");

  const absenceOfGamesText = document.querySelector(".absence-of-games-text").value;

  let deleteButtonIsClickable = true;

  deleteButtons.forEach((deleteButton) => {
    deleteButton.addEventListener("click", () => {
      if (!deleteButtonIsClickable) {
        return;
      };
      const deletedId = deleteButton
        .closest(".board-game-item")
        .querySelector(".board-game-id")
        .value;

      deleteButtonIsClickable = false;
      deleteFromFavorite(deletedId);
    });

    async function deleteFromFavorite(gameId) {
      await $.get(`/api/BoardGame/RemoveFavoriteBoardGameForUser?GameId=${gameId}`)
        .done(() => {
          deleteFromHTML(gameId);
          hub.invoke("ChangeFavorites");
        })
        .fail(() => {
          window.location.href = "/Auth/Login";
        });
      deleteButtonIsClickable = true;
    }

    function deleteFromHTML(deletedId) {
      document
        .querySelector(`input[value="${deletedId}"]`)
        .closest(".board-game-item")
        .remove();

      if (!document.querySelector(".board-game-item")) {
        document
          .querySelector("table")
          .remove();

        const newParagraph = document.createElement('p');
        newParagraph.innerHTML = absenceOfGamesText;

        document
          .querySelector(".board-game-list")
          .appendChild(newParagraph);

      }
    };

  });
});