using System;
using System.Collections.Generic;
using System.Globalization;
using Moq;
using refactor_gym_warmup_2020.cashier;
using Xunit;

namespace refactor_gym_warmup_test.cashier
{
    public class OrderReceiptTest
    {
        [Fact]
        public void shouldPrintCustomerInformationOnOrder()
        {
            var dateTimeInstance = new Mock<IDateTimeProvider>();
            var dateTime = new DateTime(2020, 7, 15);
            dateTimeInstance.Setup(_ => _.Now()).Returns(dateTime);
            Order order = new Order("Mr X", "Chicago, 60601", new List<LineItem>(), dateTimeInstance.Object);
            OrderReceipt receipt = new OrderReceipt(order);

            String output = receipt.PrintReceipt();
            
            Assert.Contains(dateTime.ToString("yyyy年M月d日, dddd", new CultureInfo("zh-cn")), output);
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
            
            var dateTime = new DateTime(2020, 7, 13);
            var dateTimeInstance = new Mock<IDateTimeProvider>();
            dateTimeInstance.Setup(_ => _.Now()).Returns(dateTime);
            
            OrderReceipt receipt = new OrderReceipt(new Order(null, null, lineItems, dateTimeInstance.Object));
            String output = receipt.PrintReceipt();

            Assert.Contains("milk\t10\t2\t20\n", output);
            Assert.Contains("biscuits\t5\t5\t25\n", output);
            Assert.Contains("chocolate\t20\t1\t20\n", output);
            Assert.Contains("税额\t6.5", output);
            Assert.DoesNotContain("折扣", output);
            Assert.Contains("总价\t71.5", output);
        }
        
        [Fact]
        public void ShouldPrintDiscountWhenWednesday()
        {    
            var dateTime = new DateTime(2020, 7, 15);

            List<LineItem> lineItems = new List<LineItem>()
            {
                new LineItem("milk", 10.0, 2),
                new LineItem("biscuits", 5.0, 5),
                new LineItem("chocolate", 20.0, 1),
            };
            var dateTimeInstance = new Mock<IDateTimeProvider>();
            dateTimeInstance.Setup(_ => _.Now()).Returns(dateTime);

            String output = "";
            OrderReceipt receipt = new OrderReceipt(new Order(null, null, lineItems, dateTimeInstance.Object));
            output = receipt.PrintReceipt();
            Assert.Contains("milk\t10\t2\t20\n", output);
            Assert.Contains("biscuits\t5\t5\t25\n", output);
            Assert.Contains("chocolate\t20\t1\t20\n", output);
            Assert.Contains("税额\t6.5", output);
            Assert.Contains("折扣\t1.43", output);
            Assert.Contains("总价\t70.07", output);
        }
    }
}