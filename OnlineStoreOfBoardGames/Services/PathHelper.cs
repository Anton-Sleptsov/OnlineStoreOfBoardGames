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

        public string GetPathToBoardGameMainImage(int boardGameId)
        {
            var fileName = $"mainImage-{boardGameId}.jpg";
            return Path.Combine(_webHostEnvironment.WebRootPath, "images", "ImagesOfGame", fileName);
        }        

        public bool IsBoardGameMainImageExist(int id)
        {
            var path = GetPathToBoardGameMainImage(id);
            return File.Exists(path);
        }

        public string GetPathToBoardGameSideImage(int boardGameId)
        {
            var fileName = $"sideImage-{boardGameId}.jpg";
            return Path.Combine(_webHostEnvironment.WebRootPath, "images", "ImagesOfGame", fileName);
        }

        public bool IsBoardGameSideImageExist(int id)
        {
            var path = GetPathToBoardGameSideImage(id);
            return File.Exists(path);
        }
    }
}
