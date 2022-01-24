using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreKeepersAssistant.Services;
using StoreKeepersAssistant.ViewModels;

namespace TestProject1
{
    [TestClass]
    public class InvoiceServiceTests : TestBase
    {
        IInvoiceService service;
        public InvoiceServiceTests() : base()
        {
            service = new InvoiceService(this.db);
        }

        [TestMethod]
        public void InvoiceCRUDTest()
        {
            var addTask = this.service.AddAsync(new InvoiceDTO { 
                InvoiceNumber = "1", 
                FromStorage = new StorageDTO { Id = "Store1" }, 
                ToStorage = new StorageDTO { Id = "Store2" } 
            });
            addTask.Wait();
            var invoice = addTask.Result;
            Assert.IsTrue(invoice.Id > 0, "Error create invoice!");

            invoice.InvoiceNumber = "2";
            var updateTast = this.service.UpdateAsync(invoice);
            updateTast.Wait();
            var invoiceAfterUpdate = updateTast.Result;
            Assert.AreEqual(invoiceAfterUpdate.InvoiceNumber, "2", "Error invoice update!");

            var deleteTask = this.service.DeleteAsync(invoice.Id);
            deleteTask.Wait();
            Assert.IsTrue(deleteTask.Result == 0, "Error invoice delete!");


        }

    }
}
