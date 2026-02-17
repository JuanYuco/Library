using Library.Web.Models.DTOs.Book;
using Library.Web.Models.DTOs.Common;
using Library.Web.Services.Interfaces;

namespace Library.Web.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;
        public BookService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.Constants.Setting.HttpClientApiKey);
        }

        public async Task<BookCollectionResponseDTO> GetAllAsync()
        {
            var response = new BookCollectionResponseDTO() { Successful = false };

            try
            {
                var apiResponse = await _httpClient.GetAsync(Constants.Constants.Book.EndPoints.GetAll);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    var errorResponse = await apiResponse.Content.ReadFromJsonAsync<ResponseBaseDTO>();
                    response.UserMessage = errorResponse?.UserMessage ?? "";
                    return response;
                }

                response.Books = await apiResponse.Content.ReadFromJsonAsync<List<BookInformationDTO>>() ?? new List<BookInformationDTO>();
                response.Successful = true;
            } catch (Exception ex)
            {
                response.UserMessage = Constants.Constants.Book.Messages.ExceptionApiError;
            }
            
            return response;
        }

        public async Task<BookDeleteResponseDTO> DeleteAsync(int id)
        {
            var response = new BookDeleteResponseDTO() { Successful = false };

            try
            {
                var apiResponse = await _httpClient.DeleteAsync(string.Format(Constants.Constants.Book.EndPoints.Delete, id));
                var message = await apiResponse.Content.ReadFromJsonAsync<ResponseBaseDTO>();
                response.UserMessage = message?.UserMessage ?? "";
                if (apiResponse.IsSuccessStatusCode)
                {
                    response.Successful = true;
                }
            } catch(Exception ex)
            {
                response.UserMessage = Constants.Constants.Book.Messages.ExceptionApiError;
            }
            

            return response;
        }

        public async Task<BookSaveResponseDTO> CreateAsync(BookDTO book)
        {
            var response = new BookSaveResponseDTO() { Successful = false };

            try
            {
                var apiResponse = await _httpClient.PostAsJsonAsync(Constants.Constants.Book.EndPoints.Create, book);
                var message = await apiResponse.Content.ReadFromJsonAsync<ResponseBaseDTO>();
                response.UserMessage = message?.UserMessage ?? "";
                if (apiResponse.IsSuccessStatusCode)
                {
                    response.Successful = true;
                }
            } catch (Exception ex)
            {
                response.UserMessage = Constants.Constants.Book.Messages.ExceptionApiError;
            }
            
            return response;
        }
    }
}
