using OnlineStoreOfBoardGames.Services.Interfaces;

namespace OnlineStoreOfBoardGames.Services
{
    public class PathHelper : IPathHelper
    {
        private IWebHostEnvironment _webHostEnvironment;

        public PathHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetPathToPostCover(int postId)
        {
            var fileName = $"cover-{postId}.jpg";
            //return GetPathByFolder("images\\Blog", fileName);
            return Path.Combine(_webHostEnvironment.WebRootPath, "images", "Blog", fileName);
        }

        public string GetPathToTravelingImageFolder()
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Traveling", "UserPictures");
            return path;
        }

        public string GetPathToGameCover(int gameId)
        {
            var fileName = $"cover-{gameId}.jpg";
            //return GetPathByFolder("images\\Game", fileName);
            return Path.Combine(_webHostEnvironment.WebRootPath, "images", "Game", fileName);
        }

        public string GetPathToGoodCover(int goodId)
        {
            var fileName = $"goodImage-{goodId}.jpg";
            return Path.Combine(_webHostEnvironment.WebRootPath, "images", "Store", "MainPage", "ImagesForGoods", fileName);
            //return GetPathByFolder("images\\Store\\MainPage\\ImagesForGoods", fileName);
        }

        public string GetPathToMovieImage(int movieId)
        {
            var fileName = $"cover-{movieId}.jpg";
            //return GetPathByFolder("images\\Movie", fileName);
            return Path.Combine(_webHostEnvironment.WebRootPath, "images", "Movie", fileName);
        }

        public bool IsGameCoverExist(int id)
        {
            var path = GetPathToGameCover(id);
            return File.Exists(path);
        }

        public string GetPathToBoardGameMainImage(int boardGameId)
        {
            var fileName = $"mainImage-{boardGameId}.jpg";
            return Path.Combine(_webHostEnvironment.WebRootPath, "images", "BoardGame", fileName);
            //return GetPathByFolder("images\\BoardGame", fileName);
        }
        
        public bool IsPostCoverExist (int postId)
        {
            var path = GetPathToPostCover(postId);
            return File.Exists(path);
        }

        public bool IsBoardGameMainImageExist(int id)
        {
            var path = GetPathToBoardGameMainImage(id);
            return File.Exists(path);
        }

        public string GetPathToBoardGameSideImage(int boardGameId)
        {
            var fileName = $"sideImage-{boardGameId}.jpg";
            return Path.Combine(_webHostEnvironment.WebRootPath, "images", "BoardGame", fileName);
            //return GetPathByFolder("images\\BoardGame", fileName);
        }

        public bool IsBoardGameSideImageExist(int id)
        {
            var path = GetPathToBoardGameSideImage(id);
            return File.Exists(path);
        }

        public bool IsMovieImageExist(int id)
        {
            var path = GetPathToMovieImage(id);
            return File.Exists(path);
        }

        public string GetPathToGameStoreCover(int gameId)
        {
            var fileName = $"caver-{gameId}.jpg";
            return Path.Combine(_webHostEnvironment.WebRootPath, "images", "GameStore", fileName);
            //return GetPathByFolder("images\\GameStore", fileName);
        }

        public bool IsGameStoreCoverExist(int id)
        {
            var path = GetPathToGameStoreCover(id);
            return File.Exists(path);
        }

        public bool IsGoodCoverExist(int id)
        {
            var path = GetPathToGoodCover(id);
            return File.Exists(path);
        }

        private string GetPathByFolder(string pathToFolder, string fileName)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, pathToFolder, fileName);
            return path;
        }
    }
}
