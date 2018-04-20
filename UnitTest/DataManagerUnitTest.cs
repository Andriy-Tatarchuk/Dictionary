using Enigma.Data;
using System;
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
            Assert.IsTrue(true);
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
        public void GetAllWordsAsyncTest()
        {
            var dataManager = new DataManager();
            var task = dataManager.GetAllWordsAsync();
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
            var id = word.Id;

            task = dataManager.DeleteWordAsync(id);
            task.Wait();

            task = dataManager.GetWordAsync(id);
            task.Wait();
            var lastWord = ((Task<Word>) task).Result;

            Assert.IsTrue(lastWord == null);
        }
    }
}
