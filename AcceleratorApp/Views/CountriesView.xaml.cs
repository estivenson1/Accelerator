using AcceleratorApp.ViewModels;
using AcceleratorApp.Views.PopUps;
using CommunityToolkit.Maui.Views;

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

    private async void VerPopUp_Clicked(object sender, EventArgs e)
    {
        GeneralPopUpView popup = new GeneralPopUpView();
        await Shell.Current.CurrentPage.ShowPopupAsync(popup);
    }
}