using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceleratorApp.ViewModels
{
    public partial class ViewModelBase : ObservableObject, IViewModelBase
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    ((App)Application.Current).IsBusy = value;
                }
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

        }

        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }

    public interface IViewModelBase : IQueryAttributable
    {
        Task InitializeAsync();
    }
}
