using BestBoardGameApi.Dtos;
using BoardGameOfDayApi.Dtos;
using OnlineStoreOfBoardGames.Services.Dtos;

namespace OnlineStoreOfBoardGames.Services.Apis
{
    public class HttpBestBoardGameServise
    {
        private readonly HttpClient _httpClient;

        public HttpBestBoardGameServise(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponseDto<DtoBestBoardGame>> GetBestBoardGameAsync()
        {
            try
            {

                var response = await _httpClient.GetAsync("/getBestBoardGame");
                var dto = await response
                    .Content
                    .ReadFromJsonAsync<DtoBestBoardGame>();

                return new ApiResponseDto<DtoBestBoardGame> { Data = dto };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<DtoBestBoardGame> { IsSuccess = false, ErrorText = ex.Message };
            }
        }
    }
}
