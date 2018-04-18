using System.Windows;
using Enigma.Data;
using GalaSoft.MvvmLight.Threading;

namespace Enigma.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
            //InitializeDBConnecction();
        }

        //private static async void InitializeDBConnecction()
        //{
        //    if (!await DataManager.InitializeDataContextAsync())
        //    {
        //    }
        //    else
        //    {
        //    }
        //}
    }
}
