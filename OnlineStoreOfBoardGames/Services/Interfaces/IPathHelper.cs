namespace OnlineStoreOfBoardGames.Services.Interfaces
{
    public interface IPathHelper
    {
        string GetPathToBoardGameMainImage(int boardGameId);
        string GetPathToBoardGameSideImage(int boardGameId);
        bool IsBoardGameMainImageExist(int id);
        bool IsBoardGameSideImageExist(int id);
    }
}