using System.Collections.ObjectModel;

namespace FilmDiary;

public class FilmService : IFilmService
{
    private ObservableCollection<Film> _films = new ObservableCollection<Film>();

    public async Task<ObservableCollection<Film>> GetFilmsAsync()
    {
        await Task.Delay(100); // Simulate async call
        return _films;
    }

    public async Task AddFilmAsync(Film film)
    {
        await Task.Delay(100); // Simulate async call
        _films.Add(film);
    }

    public async Task UpdateFilmAsync(Film film)
    {
        await Task.Delay(100); // Simulate async call
        var oldFilm = _films.FirstOrDefault(f => f.Title == film.Title);
        if (oldFilm != null)
        {
            oldFilm.Rating = film.Rating;
        }
    }

    public async Task DeleteFilmAsync(Film film)
    {
        await Task.Delay(100); // Simulate async call
        _films.Remove(film);
    }
}