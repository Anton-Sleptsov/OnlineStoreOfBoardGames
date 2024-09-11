document.addEventListener("DOMContentLoaded", function () {

  const boardGames = document.querySelectorAll(".board-game-item");

  boardGames.forEach((game) => {
    game.addEventListener("click", () => {
      const isCurrentGame = game.classList.contains("active-board-game");

      boardGames.forEach((game) => { game.classList.remove("active-board-game") });

      hideAllAdminBlocks(boardGames);

      if (isCurrentGame) {
        return;
      }

      game.classList.add("active-board-game");
      const adminBlock = game.querySelector(".update-and-delete");
      if (!adminBlock) {
        return;
      }
      adminBlock.style.display = 'flex';
    });
  });

  function hideAllAdminBlocks(boardGames) {
    boardGames.forEach((game) => {
      const adminBlock = game.querySelector(".update-and-delete");
      if (!adminBlock) {
        return;
      }
      adminBlock.style.display = 'none';
    });
  }
});