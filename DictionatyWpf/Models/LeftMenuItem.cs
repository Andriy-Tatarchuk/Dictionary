using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewLayer.Models;

namespace DictionatyWpf.Models
{
    public class LeftMenuItem : DependencyObject
    {
        #region Declarations

        #endregion

        #region Properties

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(LeftMenuItem), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty ScreenIdProperty = DependencyProperty.Register(
            "ScreenId", typeof(ScreenId), typeof(LeftMenuItem), new PropertyMetadata(default(ScreenId)));

        public ScreenId ScreenId
        {
            get { return (ScreenId) GetValue(ScreenIdProperty); }
            set { SetValue(ScreenIdProperty, value); }
        }

        #endregion

        #region Constructorss

        public LeftMenuItem(string text, ScreenId screenId)
        {
            Text = text;
            ScreenId = screenId;
        }

        #endregion


        #region Private Methods



        #endregion

        #region Public Methods




        #endregion

        #region Events



        #endregion
    }
}
