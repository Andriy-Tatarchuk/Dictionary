using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DictionatyWpf.Data;
using DictionatyWpf.Views;

namespace DictionatyWpf.ViewModels
{
    public class VMBase : DependencyObject
    {
        #region Declarations
        protected DataManager DM { get; set; }
        protected ViewManager ViewManager { get; set; }

        #endregion

        #region Properties

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(string), typeof(VMBase), new PropertyMetadata(default(string)));

        public string Header
        {
            get { return (string) GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register(
            "IsLoading", typeof(bool), typeof(VMBase), new PropertyMetadata(default(bool), (o, args) =>
            {
                var obj = o as VMBase;
                if (obj != null)
                {
                    if ((bool) args.NewValue)
                    {
                        obj.StatusBarText = "Loading...";
                    }
                    else
                    {
                        obj.StatusBarText = string.Empty;
                    }
                }
            }));

        public bool IsLoading
        {
            get { return (bool) GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public static readonly DependencyProperty HasErrorProperty = DependencyProperty.Register(
            "HasError", typeof(bool), typeof(VMBase), new PropertyMetadata(default(bool)));

        public bool HasError
        {
            get { return (bool) GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(VMBase), new PropertyMetadata(default(string)));

        public string Message
        {
            get { return (string) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty StatusBarTextProperty = DependencyProperty.Register(
            "StatusBarText", typeof(string), typeof(VMBase), new PropertyMetadata(default(string)));

        public string StatusBarText
        {
            get { return (string) GetValue(StatusBarTextProperty); }
            set { SetValue(StatusBarTextProperty, value); }
        }

        #endregion

        #region Constructorss

        public VMBase(ViewManager viewManager, DataManager dm)
        {
            ViewManager = viewManager;
            DM = dm;
        }

        #endregion


        #region Private Methods



        #endregion

        #region Public Methods

        public virtual bool Command_CanExecute(Command command, object param)
        {
            var res = false;

            return res;
        }

        public virtual void Command_Executed(Command command, object param)
        {
        }


        public void OpenScreen(ScreenId screenId, object param = null)
        {
            if (ViewManager != null)
            {
                ViewManager.OpenScreen(screenId, param);
            }
        }

        #endregion

        #region Events



        #endregion

        
    }
}
