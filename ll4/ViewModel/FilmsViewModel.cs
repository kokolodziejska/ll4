using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using Shared;

namespace FilmDiary;

public partial class FilmsViewModel : ObservableObject
{
    private readonly IFilmService _filmService;

    [ObservableProperty]
    private ObservableCollection<Film> _films = new ObservableCollection<Film>();

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private int _rating;

    public ICommand AddFilmCommand { get; }
    public ICommand DeleteFilmCommand { get; }
    public ICommand DecreaseRatingCommand { get; }
    public ICommand IncreaseRatingCommand { get; }

    public FilmsViewModel(IFilmService filmService)
    {
        _filmService = filmService;
        AddFilmCommand = new RelayCommand(async () => await AddFilm());
        DeleteFilmCommand = new RelayCommand<Film>(async film => await DeleteFilm(film));
        DecreaseRatingCommand = new RelayCommand<Film>(async film => await DecreaseRating(film));
        IncreaseRatingCommand = new RelayCommand<Film>(async film => await IncreaseRating(film));
        LoadFilms();
    }

    private async void LoadFilms()
    {
        try
        {
            Films = await _filmService.GetFilmsAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in LoadFilms: {ex.Message}");
        }
    }

    private async Task AddFilm()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(Title) && Rating > 0 && Rating <= 10)
            {
                var newFilm = new Film { Title = Title, Rating = Rating };
                await _filmService.AddFilmAsync(newFilm);
                Films.Add(newFilm);
                LoadFilms();

                Title = string.Empty;
                Rating = 0;

                OnPropertyChanged("Title");
                OnPropertyChanged(nameof(Rating));
            }
            else
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Score out of range", "Please enter a valid title and a rating between 1 and 10.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in AddFilm: {ex.Message}");
        }
    }

    private async Task DeleteFilm(Film film)
    {
        try
        {
            if (film != null)
            {
                Debug.WriteLine($"Deleting film: {film.Title}");
                await _filmService.DeleteFilmAsync(film);
                LoadFilms();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in DeleteFilm: {ex.Message}");
        }
    }

    private async Task IncreaseRating(Film film)
    {
        try
        {
            if (film != null && film.Rating < 10)
            {
                film.Rating++;
                await _filmService.UpdateFilmAsync(film);
                LoadFilms();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in IncreaseRating: {ex.Message}");
        }
    }

    private async Task DecreaseRating(Film film)
    {
        try
        {
            if (film != null && film.Rating > 1)
            {
                film.Rating--;
                await _filmService.UpdateFilmAsync(film);
                LoadFilms();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in DecreaseRating: {ex.Message}");
        }
    }
}
