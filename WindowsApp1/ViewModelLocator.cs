using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using WindowsApp1.Services;
using WindowsApp1.ViewModels;

namespace WindowsApp1
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                // We are at design time.
                //SimpleIoc.Default.Register<IDataService, DesignTimeDataService>();
            }
            else
            {
                // We are at runtime.
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<DetailPageViewModel>();
        }


        
        //MainPageVm
        public MainPageViewModel MainPageVm
        {
            get { return ServiceLocator.Current.GetInstance<MainPageViewModel>(); }
        }

        //DetailPageVm
        public DetailPageViewModel DetailPageVm
        {
            get { return ServiceLocator.Current.GetInstance<DetailPageViewModel>(); }
        }

    }
}
