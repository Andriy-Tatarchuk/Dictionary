using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enigma.Shell.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Data;
using Enigma.Entity.Entities;
using Enigma.UnitTest.MockObjects;
using Enigma.Translate;

namespace Enigma.UnitTest
{
    [TestClass()]
    public class AddEditWordViewModelTests
    {
        private AddEditWordViewModel ViewModel { get; set; }
        private IDataManager DataManager { get; set; }

        public AddEditWordViewModelTests()
        {
            DataManager = new DataManagerMockObject();
            ViewModel = new AddEditWordViewModel(DataManager, new FrameNavigationMockObject(), new Translator());
        }

        [TestMethod()]
        public void NavigatedTest1()
        {
            var word = new Word();
            var task = ViewModel.Navigated(word);
            task.Wait();

            Assert.IsTrue(ViewModel.Word == word);
        }

        [TestMethod()]
        public void NavigatedTest2()
        {
            var task = ViewModel.Navigated(1);
            task.Wait();

            Assert.IsTrue(ViewModel.Word != null && ViewModel.Word.DictionaryId == 1);
        }
    }
}