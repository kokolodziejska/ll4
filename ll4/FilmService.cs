using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Shared;

namespace FilmDiary;

public class FilmService : IFilmService
{
    private readonly HttpClient _httpClient;

    public FilmService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ObservableCollection<Film>> GetFilmsAsync()
    {
        try
        {
            var films = await _httpClient.GetFromJsonAsync<IEnumerable<Film>>("api/films");
            return new ObservableCollection<Film>(films ?? Enumerable.Empty<Film>());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in FilmService.GetFilmsAsync: {ex.Message}");
            return new ObservableCollection<Film>();
        }
    }

    public async Task AddFilmAsync(Film film)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/films", film);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in FilmService.AddFilmAsync: {ex.Message}");
        }
    }

    public async Task UpdateFilmAsync(Film film)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/films/{film.Title}", film);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in FilmService.UpdateFilmAsync: {ex.Message}");
        }
    }

    public async Task DeleteFilmAsync(Film film)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/films/{film.Title}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in FilmService.DeleteFilmAsync: {ex.Message}");
        }
    }
}