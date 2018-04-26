using Enigma.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enigma.Entity.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Enigma.UnitTest
{
    [TestClass]
    public class DataManagerUnitTest
    {
        [TestMethod()]
        public void SaveWordAsyncTest()
        {
            var dataManager = new DataManager();
            var word = new Word();
            var task = dataManager.SaveWordAsync(word);
            task.Wait();

            task = dataManager.GetWordAsync(word.Id);
            task.Wait();
            var newWord = ((Task<Word>)task).Result;

            DeleteWord(word.Id);

            Assert.IsTrue(newWord != null);
        }

        [TestMethod()]
        public void InitializeDataContextAsyncTest()
        {
            try
            {
                var task = DataManager.InitializeDataContextAsync();
                task.Wait();
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void SearchWordsAsyncTest()
        {
            var dataManager = new DataManager();
            var task = dataManager.SearchWordsAsync();
            task.Wait();
            var words = task.Result;

            Assert.IsTrue(words != null);
        }

        [TestMethod()]
        public void DeleteWordAsyncTest()
        {
            var dataManager = new DataManager();
            var word = new Word();
            var task = dataManager.SaveWordAsync(word);
            task.Wait();

            task = dataManager.DeleteWordAsync(word.Id);
            task.Wait();

            task = dataManager.GetWordAsync(word.Id);
            task.Wait();
            var lastWord = ((Task<Word>) task).Result;

            Assert.IsTrue(lastWord == null);
        }

        [TestMethod()]
        public void DeleteDictionaryAsyncTest()
        {
            var dataManager = new DataManager();
            var dictionaryId = CreateDictionary();

            var task2 = dataManager.DeleteDictionaryAsync(dictionaryId);
            task2.Wait();

            task2 = dataManager.GetDictionaryAsync(dictionaryId);
            task2.Wait();
            var lastDictionary = ((Task<Dictionary>)task2).Result;

            Assert.IsTrue(lastDictionary == null);
        }

        [TestMethod()]
        public void GetAllDictionariesAsyncTest()
        {
            var dataManager = new DataManager();
            var dictionaryId = CreateDictionary();

            var task2 = dataManager.GetAllDictionariesAsync();
            task2.Wait();

            var dictionaries = ((Task<List<Dictionary>>)task2).Result;

            DeleteDictionary(dictionaryId);

            Assert.IsTrue(dictionaries != null && dictionaries.Count >= 1);
        }

        [TestMethod()]
        public void SaveDictionaryAsyncTest()
        {
            var dataManager = new DataManager();
            var dictionary = new Dictionary();
            var task = dataManager.SaveDictionaryAsync(dictionary);
            task.Wait();
            var dictionaryId = dictionary.Id;

            var task2 = dataManager.GetDictionaryAsync(dictionaryId);
            task2.Wait();

            var newDictionary = ((Task<Dictionary>)task2).Result;

            DeleteDictionary(dictionaryId);

            Assert.IsTrue(newDictionary != null);
        }

        [TestMethod()]
        public void AddDictionaryAsynkTest()
        {
            var dataManager = new DataManager();
            var task = dataManager.AddDictionaryAsynk("test name");
            task.Wait();
            var dictionary = task.Result;
            var dictionaryId = dictionary.Id;

            var task2 = dataManager.GetDictionaryAsync(dictionaryId);
            task2.Wait();

            var newDictionary = ((Task<Dictionary>)task2).Result;

            DeleteDictionary(dictionaryId);

            Assert.IsTrue(newDictionary != null);
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
            var word = new Word() { DictionaryId = dictionaryId };

            var task = dataManager.SaveWordAsync(word);
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
