using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Shared;

namespace FilmDiary;

public interface IFilmService
{
    Task<ObservableCollection<Film>> GetFilmsAsync();
    Task AddFilmAsync(Film film);
    Task UpdateFilmAsync(Film film);
    Task DeleteFilmAsync(Film film);
}