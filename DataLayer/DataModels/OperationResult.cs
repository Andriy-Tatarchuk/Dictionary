using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DictionatyWpf.Models
{
    public class OperationResult
    {
        #region Declarations

        #endregion

        #region Properties

        public bool HasError { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

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
