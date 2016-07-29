using System.Windows;
using LoonieTrader.App.ViewModels;

namespace LoonieTrader.App.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            LoginViewModel vm = DataContext as LoginViewModel;

            if (vm != null)
            {
                vm.CloseAction = this.Close;
            }
        }
    }
}
