namespace OnlineStoreOfBoardGames.Models.BoardGame
{
    public class ApiMethodViewModel
    {
        public string Name { get; set; }
        public string ReturnTypeName { get; set; }
        public List<ParameterViewModel> Parameters { get; set; }
    }
}
