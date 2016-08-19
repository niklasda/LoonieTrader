using System.Windows;
using LoonieTrader.App.ViewModels.Windows;

namespace LoonieTrader.App.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
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
