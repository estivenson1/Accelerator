using AcceleratorApp.ViewModels;

namespace AcceleratorApp.Views;

public partial class CountriesView : ContentPage
{
	private readonly CountriesViewModel _viewModel;
	public CountriesView(CountriesViewModel viewModel)
	{
		BindingContext = _viewModel = viewModel;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
       await _viewModel.InitializeAsync();
        base.OnAppearing();
    }

    private async void Contries_Clicked(object sender, EventArgs e)
    {
        await _viewModel.InitializeAsync();
    }
}