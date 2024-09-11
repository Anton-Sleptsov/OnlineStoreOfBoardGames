namespace OnlineStoreOfBoardGames.Services.Dtos
{
    public class ApiResponseDto<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string ErrorText { get; set; } = string.Empty;
    }
}
