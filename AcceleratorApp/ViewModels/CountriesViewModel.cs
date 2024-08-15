using Accelerator.Entities.Backend.Response;
using Accelerator.Frontend.Contracts.Business;
using AcceleratorApp.ViewModels.PopUps;
using AcceleratorApp.Views.PopUps;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AcceleratorApp.ViewModels
{
    public partial class CountriesViewModel : ViewModelBase
    {
        private readonly ICountryBL _countryBL;
        private readonly IPopupService _popupService;

        [ObservableProperty] ObservableCollection<CountryResponse> countryList;

        public CountriesViewModel(ICountryBL countryBL, IPopupService popupService)
        {
            _countryBL = countryBL;
            _popupService = popupService;
        }

        public override async Task InitializeAsync()
        {
            IsBusy = true;
            var resoonse=  await _countryBL.GetCountries();
            CountryList = new ObservableCollection<CountryResponse>(resoonse.Data);
            IsBusy = false;
        }

        #region RelayCommand  **************************************************
        [RelayCommand]
        private async void SelectedItem(CountryResponse Country)
        {
            //var _popupService = App.Current.Handler.MauiContext.Services.GetService<IPopupService>();
            //await _popupService.ShowPopupAsync<GeneralPopUpViewModel>(
            //    onPresenting: viewModel =>
            //    {
            //        //viewModel.Country=Country;
            //    });

            //GeneralPopUpViewModel viewModel


            //var viewModel =new GeneralPopUpViewModel {Country= Country };

            //GeneralPopUpView popup = new GeneralPopUpView();
            //await Shell.Current.CurrentPage.ShowPopupAsync(popup);



        }

        #endregion
    }
}
