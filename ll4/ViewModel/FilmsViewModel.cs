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
        AddFilmCommand = new RelayCommand(AddFilm);
        DeleteFilmCommand = new RelayCommand<Film>(DeleteFilm);
        DecreaseRatingCommand = new RelayCommand<Film>(DecreaseRating);
        IncreaseRatingCommand = new RelayCommand<Film>(IncreaseRating);
        LoadFilms();
    }

    private void LoadFilms()
    {
        Films = _filmService.GetFilms();
    }

    private void AddFilm()
    {
        if (!string.IsNullOrWhiteSpace(Title) && Rating > 0 && Rating <= 10)
        {
            var newFilm = new Film { Title = Title, Rating = Rating };
            _filmService.AddFilm(newFilm);
            
            Title = string.Empty;
            Rating = 0;

            // Notify changes to UI to update input fields after adding the film
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Rating));
        }
        else if (Rating < 1 || Rating > 10)
        {
            Application.Current.MainPage.DisplayAlert("Score out of range", "Please enter a valid title and a rating between 1 and 10.", "OK");
        }
    }

    private void DeleteFilm(Film film)
    {
        if (film != null)
        {
            _filmService.DeleteFilm(film);  
        }
    }

    private void IncreaseRating(Film film)
    {
        if (film != null && film.Rating < 10)
        {
            film.Rating++;
            _filmService.UpdateFilms();
        }
    }

    private void DecreaseRating(Film film)
    {
        if (film != null && film.Rating > 1)
        {
            film.Rating--;
            _filmService.UpdateFilms();
        }
    }
}
