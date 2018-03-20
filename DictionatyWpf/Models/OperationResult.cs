using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DictionatyWpf.Models
{
    public class OperationResult : DependencyObject
    {
        #region Declarations

        #endregion

        #region Properties

        public static readonly DependencyProperty HasErrorProperty = DependencyProperty.Register(
            "HasError", typeof(bool), typeof(OperationResult), new PropertyMetadata(default(bool)));

        public bool HasError
        {
            get { return (bool) GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(OperationResult), new PropertyMetadata(default(string)));

        public string Message
        {
            get { return (string) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty ResultProperty = DependencyProperty.Register(
            "Result", typeof(object), typeof(OperationResult), new PropertyMetadata(default(object)));

        public object Result
        {
            get { return (object) GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        #endregion

        #region Constructorss

        public OperationResult()
        {

        }

        public OperationResult(object result, bool hasError = false, string message = null)
        {
            Result = result;
            HasError = hasError;
            Message = message;
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
