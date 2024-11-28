using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shared;

namespace FilmDiary
{
    public class FilmService : IFilmService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FilmService> _logger;

        public FilmService(HttpClient httpClient, ILogger<FilmService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ObservableCollection<Film>> GetFilmsAsync()
        {
            try
            {
                _logger.LogInformation("Sending GET request to API: api/films");
                var films = await _httpClient.GetFromJsonAsync<IEnumerable<Film>>("films");
                if (films == null)
                {
                    _logger.LogWarning("API returned null for GET request.");
                    return new ObservableCollection<Film>();
                }

                _logger.LogInformation($"Successfully retrieved {films.Count()} films from API.");
                return new ObservableCollection<Film>(films);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching films from API.");
                return new ObservableCollection<Film>();
            }
        }


        public async Task AddFilmAsync(Film film)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("films", film);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfully added film: {film.Title}");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Failed to add film. StatusCode: {response.StatusCode}, Content: {errorContent}");
                }

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in FilmService.AddFilmAsync: {ex.Message}");
                throw;
            }
        }


        public async Task UpdateFilmAsync(Film film)
        {
            try
            {
                _logger.LogInformation("Updating film: {Title}", film.Title);
                var response = await _httpClient.PutAsJsonAsync($"films/{film.Title}", film);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation("Successfully updated film: {Title}", film.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating film: {Title}", film.Title);
            }
        }

        public async Task DeleteFilmAsync(Film film)
        {
            try
            {
                _logger.LogInformation("Deleting film: {Title}", film.Title);
                var response = await _httpClient.DeleteAsync($"films/{film.Title}");
                response.EnsureSuccessStatusCode();
                _logger.LogInformation("Successfully deleted film: {Title}", film.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting film: {Title}", film.Title);
            }
        }
    }
}
