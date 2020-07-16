using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace refactor_gym_warmup_2020.cashier
{
    public class Order
    {
        string cName;
        string addr;
        List<LineItem> lineItemList;
        private DateTime orderTime;


        public Order(string cName, string addr, List<LineItem> lineItemList, IDateTimeProvider dateTimeProvider)
        {
            this.cName = cName;
            this.addr = addr;
            this.lineItemList = lineItemList;
            orderTime = dateTimeProvider.Now();
        }

        private double OriginalPrice()
        {
            return lineItemList.Sum(item => item.TotalPrice());
        }
        
        public string GetCustomerName()
        {
            return cName;
        }

        public string GetCustomerAddress()
        {
            return addr;
        }

        public List<LineItem> GetLineItems()
        {
            return lineItemList;
        }

        public string GetOrderTime()
        {
            return orderTime.ToString("yyyy年M月d日, dddd", new CultureInfo("zh-cn"));
        }

        public double GetDiscount()
        {
            return OriginalPrice() * 0.02;
        }

        public double GetTax()
        {
            return lineItemList.Sum(item => item.Tax());
        }

        public double TotalPrice()
        {
            return HasDiscount() ? OriginalPrice() * 0.98 : OriginalPrice();
        }

        public bool HasDiscount()
        {
            return orderTime.DayOfWeek == DayOfWeek.Wednesday;
        }
    }
}