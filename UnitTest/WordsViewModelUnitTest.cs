using Enigma.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enigma.Entity.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enigma.Shell.ViewModel;


namespace Enigma.UnitTest
{
    [TestClass]
    public class WordsViewModelUnitTest
    {
        [TestMethod()]
        public void GetWordsTest()
        {
            var viewModel = new WordsViewModel(new DataManager(), null);
            
            Assert.IsTrue(viewModel != null);

            var task = viewModel.GetWords();
            task.Wait();

            Assert.IsTrue(viewModel.Words != null);
        }


        private int CreateDictionary()
        {
            var dataManager = new DataManager();
            var dictionary = new Dictionary();
            var task = dataManager.SaveDictionaryAsync(dictionary);
            task.Wait();

            return dictionary.Id;
        }

        private int AddWordToDictionary(int dictionaryId)
        {
            var dataManager = new DataManager();
            var word = new Word();

            var task = dataManager.AddWordToDictionaryAsync(dictionaryId, word);
            task.Wait();

            return word.Id;
        }

        private void DeleteDictionary(int dictionaryId)
        {
            var dataManager = new DataManager();
            var task = dataManager.DeleteDictionaryAsync(dictionaryId);
            task.Wait();
        }

        private void DeleteWord(int wordId)
        {
            var dataManager = new DataManager();
            var task = dataManager.DeleteWordAsync(wordId);
            task.Wait();
        }
    }
}
