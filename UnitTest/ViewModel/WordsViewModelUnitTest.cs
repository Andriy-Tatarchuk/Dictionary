using Enigma.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enigma.Entity.Entities;
using Enigma.Shell.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enigma.Shell.ViewModel;
using Enigma.UnitTest.MockObjects;


namespace Enigma.UnitTest
{
    [TestClass]
    public class WordsViewModelUnitTest
    {
        private WordsViewModel WordsViewModel { get; set; }
        private IDataManager DataManager { get; set; }

        public WordsViewModelUnitTest()
        {
            DataManager = new DataManagerMockObject();
            WordsViewModel = new WordsViewModel(DataManager, new FrameNavigationMockObject());
        }

        [TestMethod()]
        public void NavigatedTest1()
        {
            var wordName = "TestWordName1";
            DataManager.SaveWordAsync(new Word(wordName, ""){DictionaryId = 1});

            var task = WordsViewModel.Navigated(new SearchData() {DictionaryId = 1, SearchText = wordName});
            task.Wait();

            Assert.IsTrue(WordsViewModel.Words != null && WordsViewModel.Words.Count > 0);
        }

        [TestMethod()]
        public void NavigatedTest2()
        {
            var wordName = "TestWordName1";
            DataManager.SaveWordAsync(new Word(wordName, "") { DictionaryId = 1 });

            var task = WordsViewModel.Navigated(1);
            task.Wait();

            Assert.IsTrue(WordsViewModel.Words != null && WordsViewModel.Words.Count > 0);
        }

        [TestMethod()]
        public void NavigatedTest3()
        {
            var wordName = "TestWordName1";
            DataManager.SaveWordAsync(new Word(wordName, "") { DictionaryId = 1 });

            var task = WordsViewModel.Navigated("1");
            task.Wait();

            Assert.IsTrue(WordsViewModel.Words != null && WordsViewModel.Words.Count > 0);
        }
    }
}
