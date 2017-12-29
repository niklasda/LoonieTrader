
using Fluent;
using FontAwesome.WPF;
using OfflineAnalyst.App.Constants;

namespace OfflineAnalyst.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var fore = ColorConfig.ForeGround;

            LoadFileButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Plus, fore);
            LoadFileButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Plus, fore);

            CloseFileButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Minus, fore);
            CloseFileButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Minus, fore);
        }
    }
}
