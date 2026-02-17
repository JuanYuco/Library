using Library.Web.Models.DTOs.Author;
using Library.Web.Models.DTOs.Common;
using Library.Web.Services.Interfaces;

namespace Library.Web.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient _httpClient;
        public AuthorService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.Constants.Setting.HttpClientApiKey);
        }

        public async Task<AuthorCollectionResponseDTO> GetAllAsync()
        {
            var response = new AuthorCollectionResponseDTO() { Successful = false };

            try
            {
                var apiResponse = await _httpClient.GetAsync(Constants.Constants.Author.EndPoints.GetAll);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    var errorResponse = await apiResponse.Content.ReadFromJsonAsync<ResponseBaseDTO>();
                    response.UserMessage = errorResponse?.UserMessage ?? "";
                    return response;
                }

                response.Authors = await apiResponse.Content.ReadFromJsonAsync<List<AuthorDTO>>() ?? new List<AuthorDTO>();
                response.Successful = true;
                
            }
            catch (Exception ex) {
                response.UserMessage = Constants.Constants.Author.Messages.ExceptionApiError;
            }

            return response;
        }

        public async Task<AuthorMinfiedCollectionResponseDTO> GetAllMinifiedAsync()
        {
            var response = new AuthorMinfiedCollectionResponseDTO() { Successful = false };

            try
            {
                var apiResponse = await _httpClient.GetAsync(Constants.Constants.Author.EndPoints.GetAllMinified);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    var errorResponse = await apiResponse.Content.ReadFromJsonAsync<ResponseBaseDTO>();
                    response.UserMessage = errorResponse?.UserMessage ?? "";
                    return response;
                }

                response.Authors = await apiResponse.Content.ReadFromJsonAsync<List<AuthorMinifiedDTO>>() ?? new List<AuthorMinifiedDTO>();
                response.Successful = true;
                
            } catch(Exception ex)
            {
                response.UserMessage = Constants.Constants.Author.Messages.ExceptionApiError;
            }

            return response;
        }

        public async Task<AuthorDeleteResponseDTO> DeleteAsync(int id)
        {
            var response = new AuthorDeleteResponseDTO() { Successful = false };

            try
            {
                var apiResponse = await _httpClient.DeleteAsync(string.Format(Constants.Constants.Author.EndPoints.Delete, id));
                var message = await apiResponse.Content.ReadFromJsonAsync<ResponseBaseDTO>();
                response.UserMessage = message?.UserMessage ?? "";
                if (apiResponse.IsSuccessStatusCode)
                {
                    response.Successful = true;
                }
            } catch (Exception ex)
            {
                response.UserMessage = Constants.Constants.Author.Messages.ExceptionApiError;
            }

            return response;
        }

        public async Task<AuthorSaveResponseDTO> CreateAsync(AuthorDTO authorDTO)
        {
            var response = new AuthorSaveResponseDTO() { Successful = false };
            
            try
            {
                var apiResponse = await _httpClient.PostAsJsonAsync(Constants.Constants.Author.EndPoints.Create, authorDTO);
                var message = await apiResponse.Content.ReadFromJsonAsync<ResponseBaseDTO>();
                response.UserMessage = message?.UserMessage ?? "";
                if (apiResponse.IsSuccessStatusCode)
                {
                    response.Successful = true;
                }
            } catch (Exception ex)
            {
                response.UserMessage = Constants.Constants.Author.Messages.ExceptionApiError;
            }

            return response;
        }
    }
}
