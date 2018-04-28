using Enigma.Shell.Navigation;

namespace Enigma.UnitTest.MockObjects
{
    internal class FrameNavigationMockObject : IFrameNavigationService
    {
        public object Parameter { get; set; }

        public string CurrentPageKey { get; set; }

        public void GoBack()
        {
            
        }

        public void NavigateTo(string pageKey)
        {
            
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            
        }
    }
}
