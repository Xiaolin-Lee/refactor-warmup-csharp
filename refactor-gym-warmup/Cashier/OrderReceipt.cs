using System.Linq;
using System.Text;

namespace refactor_gym_warmup_2020.cashier
{
    public class OrderReceipt
    {
        private Order order;
        private const string TotalAmountText = "总价";
        private const string HeaderText = "======老王超市,值得信赖======\n";
        private const string SalesTaxText = "税额";
        private const string DiscountText = "折扣";
        private const string FooterText = "---------------";

        public OrderReceipt(Order order)
        {
            this.order = order;
        }

        public string PrintReceipt()
        {
            StringBuilder output = new StringBuilder();
            output.Append(PrintHeader());
            order.GetLineItems().ForEach(item => output.Append(PrintLineItem(item)));
            
            output.Append(FooterText);
            
            output.Append(PrintSalesTax());
            if (order.HasDiscount())
            {
                output.Append(PrintDiscount());
            }
            output.Append(PrintTotalPrice());
            return output.ToString();
        }

        private static StringBuilder PrintLineItem(LineItem lineItem)
        {
            return new StringBuilder().Append($"{lineItem.desc}, {lineItem.GetPrice()} X {lineItem.GetQuantity()}, {lineItem.TotalAmount()}\n");
        }

        private StringBuilder PrintHeader()
        {
            var output = new StringBuilder();
            output.Append(HeaderText);
            output.Append(order.GetOrderTime());
            output.Append(order.GetCustomerName());
            output.Append(order.GetCustomerAddress());
            
            return output;
        }

        private string PrintTotalPrice()
        {
            return $"{TotalAmountText}: {order.TotalPrice()}";
        }

        private string PrintSalesTax()
        {
            return $"{SalesTaxText }: {order.GetTax()}";
        }
        
        private string PrintDiscount()
        {
            return $"{DiscountText}: {order.GetDiscount()}";
        }
    }
}