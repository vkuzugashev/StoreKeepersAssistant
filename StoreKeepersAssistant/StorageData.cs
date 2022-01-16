using StoreKeepersAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant
{
    public static class StorageData
    {
        public static void Initialize(StorageContext context)
        {
            if (!context.Storages.Any())
            {
                context.Storages.AddRange(
                    new Storage
                    {
                        Id = "Store1",
                        Name = "Склад 1",
                    },
                    new Storage
                    {
                        Id = "Store2",
                        Name = "Склад 2",
                    },
                    new Storage
                    {
                        Id = "Store3",
                        Name = "Склад 3",
                    }
                );

                context.SaveChanges();
            }

            if (!context.Items.Any())
            {
                context.Items.AddRange(
                    new Item
                    {
                        Id = "001",
                        Name = "Номенклатура 1",
                    },
                    new Item
                    {
                        Id = "002",
                        Name = "Номенклатура 2",
                    },
                    new Item
                    {
                        Id = "003",
                        Name = "Номенклатура 3",
                    },
                    new Item
                    {
                        Id = "004",
                        Name = "Номенклатура 4",
                    },
                    new Item
                    {
                        Id = "005",
                        Name = "Номенклатура 5",
                    },
                    new Item
                    {
                        Id = "006",
                        Name = "Номенклатура 6",
                    },
                    new Item
                    {
                        Id = "007",
                        Name = "Номенклатура 7",
                    }
                );

                context.SaveChanges();
            }

            if (!context.Invoices.Any())
            {
                var storage1 = context.Storages.Find("Store1");
                var storage2 = context.Storages.Find("Store1");
                context.Invoices.AddRange(
                    new Invoice
                    {
                        InvoiceTime = DateTime.Now,
                        InvoceNumber = "1",
                        FromStorage = storage1,
                        ToStorage = storage2
                    },
                    new Invoice
                    {
                        InvoiceTime = DateTime.Now,
                        InvoceNumber = "2",
                        FromStorage = storage1,
                        ToStorage = storage1
                    },
                    new Invoice
                    {
                        InvoiceTime = DateTime.Now,
                        InvoceNumber = "2",
                        FromStorage = storage1,
                        ToStorage = storage1
                    },
                    new Invoice
                    {
                        InvoiceTime = DateTime.Now,
                        InvoceNumber = "2",
                        FromStorage = storage1,
                        ToStorage = storage1
                    },
                    new Invoice
                    {
                        InvoiceTime = DateTime.Now,
                        InvoceNumber = "2",
                        FromStorage = storage1,
                        ToStorage = storage1
                    },
                    new Invoice
                    {
                        InvoiceTime = DateTime.Now,
                        InvoceNumber = "2",
                        FromStorage = storage1,
                        ToStorage = storage1
                    }

                );

                context.SaveChanges();
            }

            if (!context.InvoiceItems.Any())
            {

                var invoice1 = context.Invoices.Find(1);
                var invoice2 = context.Invoices.Find(2);

                var item1 = context.Items.Find("001");
                var item2 = context.Items.Find("002");
                var item3 = context.Items.Find("003");

                context.InvoiceItems.AddRange(
                    new InvoiceItem
                    {
                        Invoice = invoice1,
                        Item = item1,
                        Qty = 10.0
                    },
                    new InvoiceItem
                    {
                        Invoice = invoice1,
                        Item = item2,
                        Qty = 20.0
                    },
                    new InvoiceItem
                    {
                        Invoice = invoice1,
                        Item = item3,
                        Qty = 30.0
                    },
                    new InvoiceItem
                    {
                        Invoice = invoice2,
                        Item = item1,
                        Qty = 10.0
                    },
                    new InvoiceItem
                    {
                        Invoice = invoice2,
                        Item = item2,
                        Qty = 20.0
                    },
                    new InvoiceItem
                    {
                        Invoice = invoice2,
                        Item = item3,
                        Qty = 30.0
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
