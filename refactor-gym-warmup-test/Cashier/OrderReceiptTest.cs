using System;
using System.Collections.Generic;
using System.Globalization;
using refactor_gym_warmup_2020.cashier;
using Xunit;

namespace refactor_gym_warmup_test.cashier
{
    public class OrderReceiptTest
    {
        [Fact]
        public void shouldPrintCustomerInformationOnOrder()
        {
            Order order = new Order("Mr X", "Chicago, 60601", new List<LineItem>());
            OrderReceipt receipt = new OrderReceipt(order);

            String output = receipt.PrintReceipt();
            
            Assert.Contains(DateTime.Now.ToString("yyyy年M月d日, dddd", new CultureInfo("zh-cn")), output);
            Assert.Contains("Mr X", output);
            Assert.Contains("Chicago, 60601", output);
        }

        [Fact]
        public void ShouldPrintLineItemAndSalesTaxInformation()
        {
            List<LineItem> lineItems = new List<LineItem>()
            {
                new LineItem("milk", 10.0, 2),
                new LineItem("biscuits", 5.0, 5),
                new LineItem("chocolate", 20.0, 1),
            };
            OrderReceipt receipt = new OrderReceipt(new Order(null, null, lineItems));
            String output = receipt.PrintReceipt();

            Assert.Contains("milk\t10\t2\t20\n", output);
            Assert.Contains("biscuits\t5\t5\t25\n", output);
            Assert.Contains("chocolate\t20\t1\t20\n", output);
            Assert.Contains("税额\t6.5", output);
            Assert.Contains("总价\t71.5", output);
        }
    }
}