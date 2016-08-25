using System.Windows;
using LoonieTrader.App.ViewModels.Windows;

namespace LoonieTrader.App.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            LoginWindowViewModel vm = DataContext as LoginWindowViewModel;

            if (vm != null)
            {
                vm.CloseAction = this.Close;
            }
        }
    }
}
