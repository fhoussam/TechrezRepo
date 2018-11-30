using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Api.Dto;

namespace Ut
{
    [TestClass]
    public class SearchFilterSettingTests
    {
        [TestMethod]
        public void Ok()
        {
            //input
            ProductSearchSetting productSearchSetting = new ProductSearchSetting()
            {
                Description = "e",
                MinPrice = 100,
                MaxPrice = 600,
                MinStock = 10,
                MaxStock = 50,
                CategoryID = 2
            };

            //expected
            string expected =
                string.Format("description like '%{0}%' and stock > {1} and stock < {2} and price >= {3} and price <= {4} and categoryid = {5}"
                ,productSearchSetting.Description,
                productSearchSetting.MinStock,
                productSearchSetting.MaxStock,
                productSearchSetting.MinPrice,
                productSearchSetting.MaxPrice,
                productSearchSetting.CategoryID
            );

            string result = productSearchSetting.GetWhereLine();
            Assert.AreEqual(expected, result);
        }
    }
}
