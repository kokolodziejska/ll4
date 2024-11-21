using System.Collections.ObjectModel;
using System.Diagnostics;

namespace FilmDiary;

public class FilmService : IFilmService
{
    private ObservableCollection<Film> _films = new ObservableCollection<Film>();

    public async Task<ObservableCollection<Film>> GetFilmsAsync()
    {
        await Task.Delay(1);
        return _films;
    }

    public async Task AddFilmAsync(Film film)
    {
        await Task.Delay(1);
        _films.Add(film);
    }

    public async Task UpdateFilmAsync(Film film)
    {
        /*var oldFilm = _films.FirstOrDefault(f => f.Title == film.Title);
        Debug.WriteLine(film.Rating);
        if (oldFilm != null)
        {
            oldFilm.Rating = film.Rating;
        }*/
        await Task.Delay(1);
        ObservableCollection<Film> copy = new ObservableCollection<Film>();
        foreach (Film originalFilm in _films)
        {
            copy.Add(new Film { Title = originalFilm.Title, Rating = originalFilm.Rating });
        }
        while(_films.Count > 0)
        {
            _films.RemoveAt(0);
        }
        foreach (Film copyFilm in copy)
        {
            _films.Add(copyFilm);
        }
    }

    public async Task DeleteFilmAsync(Film film)
    {
        await Task.Delay(1);
        _films.Remove(film);
    }
}