using Template10.Controls;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Controls;

namespace WindowsApp1.Views
{
    public sealed partial class Shell : Page
    {
        public static Shell Instance { get; set; }
        public static HamburgerMenu HamburgerMenu => Instance.MyHamburgerMenu;
        Services.SettingsServices.SettingsService _settings;

        public Shell()
        {
            Instance = this;
            InitializeComponent();
            _settings = Services.SettingsServices.SettingsService.Instance;
        }        

        public Shell(INavigationService navigationService) : this()
        {
            SetNavigationService(navigationService);
        }

        public void SetNavigationService(INavigationService navigationService)
        {
            MyHamburgerMenu.NavigationService = navigationService;
            HamburgerMenu.RefreshStyles(_settings.AppTheme, true);
            HamburgerMenu.IsFullScreen = _settings.IsFullScreen;

            //Commented:AA:hamburgerButton never to set visible
            //HamburgerMenu.HamburgerButtonVisibility = _settings.ShowHamburgerButton ?
            //    Visibility.Visible : Visibility.Collapsed;
        }      
    }
}

