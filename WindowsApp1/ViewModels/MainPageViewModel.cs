using Template10.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using WindowsApp1.Models;
using WindowsApp1.Common;
using WindowsApp1.Services;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml.Controls;
using WindowsApp1.Enums;
using System;

namespace WindowsApp1.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region <-PrivateMembers->        
        private IDataService _dataService;
        private static int _pgIndex = 0;
        private static bool _isResetRequired = false;
        #endregion

        #region <-Properties->
        private string _Value = "Empty";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        private ObservableCollection<CustomFilter> _filterList = new ObservableCollection<CustomFilter>();
        public ObservableCollection<CustomFilter> FilterList
        {
            get { return _filterList; }
            set
            {
                _filterList = value;
                RaisePropertyChanged();
            }
        }

        private CustomFilter _selectedOption = CustomFilter.All;
        public CustomFilter SelectedOption
        {
            get { return _selectedOption; }
            set
            {
                if (_selectedOption != value)
                {
                    _selectedOption = value;
                    RaisePropertyChanged();

                    OnOptionSelectionChange();
                }                
            }
        }

        private IncrementalLoadingCollection<Content> _contentCollection;
        public IncrementalLoadingCollection<Content> ContentCollection
        {
            get { return _contentCollection; }
            set
            {
                _contentCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region <-Commands->
        public ICommand OptionSelectionChangeCommand
        {
            get { return new RelayCommand<object>(OptionSelectionChangeCommandExecute); }
        }
        public ICommand ItemSelectedCommand
        {
            get { return new RelayCommand<object>(ItemSelectedCommandExecute); }
        }        
        #endregion

        #region <-Constructor->
        public MainPageViewModel(IDataService dataService)
        {
            _dataService = dataService;
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }

            var list = Enum.GetValues(typeof(CustomFilter)).Cast<CustomFilter>(); ;
            FilterList = new ObservableCollection<CustomFilter>(list);

            ContentCollection = new IncrementalLoadingCollection<Content>((cancellationToken, count)
                => Task.Run(GetMoreArticleData, cancellationToken));            
        }
        #endregion              

        #region <-PublicMethods->       

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
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

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);
        #endregion

        #region <-CommandMethods->       

        private async void OptionSelectionChangeCommandExecute(object o)
        {
            System.Diagnostics.Debug.WriteLine(o.ToString());
            await GetMoreArticleData();
        }

        private void ItemSelectedCommandExecute(object o)
        {
            Content content;
            if (o is ItemClickEventArgs)
                content = ((o as ItemClickEventArgs).ClickedItem) as Content;
            else
                content = o as Content;

            if (content != null)
                NavigationService.Navigate(typeof(Views.DetailPage), content);

        }
        #endregion

        #region <-PrivateMethods->
        private void OnOptionSelectionChange()
        {
            //reset PageIndex=1,when reload is needed.
            _isResetRequired = true;

            ContentCollection.Clear();
            ContentCollection = null;

            ContentCollection = new IncrementalLoadingCollection<Content>((cancellationToken, count)
                => Task.Run(GetMoreArticleData, cancellationToken));
        }

        private async Task<ObservableCollection<Content>> GetMoreArticleData()
        {
            try
            {
                Views.Busy.SetBusy(true, "Loading...");

                //filter
                var contentFilter = new ContentFilter();
                var query = contentFilter.GetFilter(SelectedOption);

#if DEBUG
                System.Diagnostics.Debug.WriteLine(string.Format("SELECTED-OPTION-->{0}", SelectedOption.ToString()));
                System.Diagnostics.Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~GETTING DATA~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
#endif
                var s = await _dataService.GetData(query, _isResetRequired);

                Views.Busy.SetBusy(false);

                return new ObservableCollection<Content>(s.Content);
            }
            catch (Exception)
            {
                //log exception
            }
            finally
            {
                _isResetRequired = false;
            }
            //TODO:test-code,to be commented.
            return new ObservableCollection<Content>();//test
        }

        #endregion//private methods

    }
}


