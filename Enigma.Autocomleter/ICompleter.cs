using System.Threading.Tasks;

namespace Enigma.Autocomplete
{
    public interface ICompleter
    {
        Task<string> GetFirstWordStartedWith(string prefix);
    }
}