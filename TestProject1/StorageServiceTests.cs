using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreKeepersAssistant.Controllers;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.Services;
using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class StorageServiceTests : TestBase
    {
        IStorageService service;
        public StorageServiceTests() : base()
        {
            service = new StorageService(this.db);
        }

        [TestMethod]
        public void GetAllStorages()
        {

            var task = service.GetAllAsync();
            task.Wait();
            Assert.IsTrue(task.Result is List<StorageViewModel> && task.Result.Count > 0);
        }

    }
}
