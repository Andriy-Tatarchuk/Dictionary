
namespace Enigma.Data.DataModels
{
    public class OperationResult
    {
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
    }
}
