document.addEventListener("DOMContentLoaded", function () {

  const hub = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/boardGame")
    .build();

  hub.on("NotifyAboutDeleteBoardGame", function (gameId) {
    deleteBoardGameFromHTML(gameId);
    updateTop();
  });

  hub.on("NotifyAboutChangeFavorites", function () {
    updateTop();
  });

  hub.start();

  const deleteButtons = document.querySelectorAll(".delete-link");
  let deleteButtonIsClickable = true;
  if (!deleteButtons.length) {
    return;
  };

  deleteButtons.forEach((deleteButton) => {
    deleteButton.addEventListener("click", () => {
      if (!deleteButtonIsClickable) {
        return;
      }
      const deletedId = deleteButton
        .closest(".board-game-item")
        .querySelector(".board-game-id")
        .value;

      deleteButtonIsClickable = false;
      deleteBoardGame(deletedId);
    });
  });


  function deleteBoardGameFromHTML(deletedId) {
    document
      .querySelector(`input[value="${deletedId}"]`)
      .closest(".board-game-item")
      .remove();
  }

  async function deleteBoardGame(deletedId) {
    const url = `/api/BoardGame/Delete?id=${deletedId}`;
    await $.get(url)
      .done((isSuccessful) => {
        if (isSuccessful) {
          deleteBoardGameFromHTML(deletedId);
          updateTop();
          hub.invoke("DeleteBoardGame", deletedId - 0);
        } else {
          console.error("Deletion error");
        }
      })
      .fail(() => {
        console.error("Server error");
      });

    deleteButtonIsClickable = true;
  }

  function updateTop() {

    const topContainer = document.querySelector(".top-board-games");
    topContainer.innerHTML = "";

    $.get(`/api/BoardGame/GetTop3`)
      .done((top3FavoriteBoardGames) => {

        top3FavoriteBoardGames.forEach((favoriteBoardGame) => {
          const topGame =
          `<li class="board-game-item top-board-game-item">
            <input class="top-board-game-id" type="hidden" value="${favoriteBoardGame.id}" />
            <img class="top-icon" src="/images//BoardGame/top-${favoriteBoardGame.rank}.png"/>
            <a class="board-game-title text-dark top-board-game-title" href="/BoardGame/BoardGame?Id=${favoriteBoardGame.id}">${favoriteBoardGame.title}</a>
            <p class="like-count">${favoriteBoardGame.countOfUserWhoLikeIt}</p>
          </li>`;

        topContainer.insertAdjacentHTML("beforeend", topGame);
        });
      })
      .fail(() => {
        console.error("Error getting the top");
      });
  }
});