namespace FilmDiary;

public partial class MainPage : ContentPage
{
    public MainPage(FilmsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
