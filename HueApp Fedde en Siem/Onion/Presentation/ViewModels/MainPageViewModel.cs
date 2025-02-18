using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace HueApp_Fedde_en_Siem.Onion.Presentation.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {

        public ICommand LampCommand { get; }

        public MainPageViewModel()
        {
            LampCommand = new RelayCommand<string>(ShowLampPopup);
        }

        private void ShowLampPopup(string lampId)
        {
            var popupViewModel = new PopupTestViewModel();
            popupViewModel.Initialize(lampId);

            var popup = new PopupTest();
            popup.BindingContext = popupViewModel;

            Microsoft.Maui.Controls.Application.Current.MainPage.ShowPopup(popup);
        }
    }
}
