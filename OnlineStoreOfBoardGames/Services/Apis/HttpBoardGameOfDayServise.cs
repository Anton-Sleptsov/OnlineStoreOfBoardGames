using BestBoardGameApi.Dtos;
using BoardGameOfDayApi.Dtos;
using OnlineStoreOfBoardGames.Services.Dtos;

namespace OnlineStoreOfBoardGames.Services.Apis
{
    public class HttpBoardGameOfDayServise
    {
        private readonly HttpClient _httpClient;

        public HttpBoardGameOfDayServise(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponseDto<DtoBoardGameOfDay>> GetBoardGameOfDayAsync()
        {
            try
            {

                var response = await _httpClient.GetAsync("/getBoardGameOfDay");
                var dto = await response
                    .Content
                    .ReadFromJsonAsync<DtoBoardGameOfDay>();

                return new ApiResponseDto<DtoBoardGameOfDay> { Data = dto};
            }
            catch (Exception ex) 
            {
                return new ApiResponseDto<DtoBoardGameOfDay> { IsSuccess = false, ErrorText = ex.Message};
            }
        }
    }
}
