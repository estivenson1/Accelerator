using Accelerator.Entities.Backend.Response;
using Accelerator.Frontend.Contracts.Business;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AcceleratorApp.ViewModels
{
    public partial class CountriesViewModel : ViewModelBase
    {
        private readonly ICountryBL _countryBL;

        [ObservableProperty] ObservableCollection<CountryResponse> countryList;

        public CountriesViewModel(ICountryBL countryBL)
        {
            _countryBL=countryBL; 
        }

        public override async Task InitializeAsync()
        {
            IsBusy = true;
            var resoonse=  await _countryBL.GetCountries();
            CountryList = new ObservableCollection<CountryResponse>(resoonse.Data);
            IsBusy = false;
        }
    }
}
