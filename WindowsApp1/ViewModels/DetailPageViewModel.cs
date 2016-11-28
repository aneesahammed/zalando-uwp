using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Attribute = WindowsApp1.Models.Attribute;

namespace WindowsApp1.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        #region <-PrivateMembers->
        #endregion

        #region <-Properties->
        private string _Value = "Default";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        private string _brandImageUrl;
        public string BrandImageUrl
        {
            get { return _brandImageUrl; }
            set
            {
                _brandImageUrl = value;
                RaisePropertyChanged();
            }
        }

        private List<Attribute> _attributes = new List<Attribute>();
        public List<Attribute> Attributes
        {
            get { return _attributes; }
            set
            {
                _attributes = value;
                RaisePropertyChanged();
            }
        }

        private List<Models.Image> _mediaImages = new List<Models.Image>();
        public List<Models.Image> MediaImages
        {
            get { return _mediaImages; }
            set
            {
                _mediaImages = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region <-Constructor->
        public DetailPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }
        #endregion       

        #region <-PublicMethods->
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Value = (suspensionState.ContainsKey(nameof(Value))) ? suspensionState[nameof(Value)]?.ToString() : parameter?.ToString();
            await Task.CompletedTask;


            var content = parameter as Models.Content;

            BrandImageUrl = content.Brand.LogoUrl;
            Attributes = new List<Models.Attribute>(content.Attributes);
            MediaImages = new List<Models.Image>(content.Media.Images);
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }
        #endregion

        #region <-PrivateMethods->
        #endregion

    }
}

