﻿using System.Windows;
using LoonieTrader.App.Services;
using LoonieTrader.App.ViewModels.Windows;
using Microsoft.Practices.ServiceLocation;
using FontAwesome.WPF;
using LoonieTrader.App.Constants;

namespace LoonieTrader.App.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            var ls = ServiceLocator.Current.GetInstance<LayoutService>();
            base.SourceInitialized += (s, e) => ls.Tracker.Configure(this).Apply();

            var vm = DataContext as LoginWindowViewModel;
            if (vm != null)
            {
                // Needed to close the window from the ViewModel
                vm.CloseAction = base.Close;
            }

            var fore = ColorConfig.ForeGround;

            LoginUseButtonIcon.Source = ImageAwesome.CreateImageSource(FontAwesomeIcon.Check, fore);

        }
    }
}
