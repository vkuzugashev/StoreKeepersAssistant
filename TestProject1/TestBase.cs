using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreKeepersAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class TestBase
    {
        string connection = "Server=(localdb)\\mssqllocaldb;Database=storedb;Trusted_Connection=True;MultipleActiveResultSets=true";
        protected StorageContext db;

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<StorageContext>().UseSqlServer(this.connection).Options;
            this.db = new StorageContext(options);
        }

    }
}
