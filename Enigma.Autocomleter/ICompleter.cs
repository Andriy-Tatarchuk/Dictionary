using System.Threading.Tasks;

namespace Enigma.Autocomplete
{
    public interface ICompleter
    {
        Task InitializeWordsAsync();
        Task<string> GetFirstWordStartedWith(string prefix);
    }
}