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
    public class ItemServiceTests : TestBase
    {
        IItemService service;
        public ItemServiceTests() : base()
        {
            service = new ItemService(this.db);
        }


        [TestMethod]
        public void GetAllItems()
        {
            var service = new ItemService(this.db);

            var task = service.GetAllAsync();
            task.Wait();
            Assert.IsTrue(task.Result is List<ItemDTO> && task.Result.Count > 0);
        }

    }
}
