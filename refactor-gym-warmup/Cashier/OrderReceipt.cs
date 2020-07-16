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
            var output = new StringBuilder();
            output.Append(lineItem.desc);
            output.Append('\t');
            output.Append(lineItem.GetPrice());
            output.Append('\t');
            output.Append(lineItem.GetQuantity());
            output.Append('\t');
            output.Append(lineItem.TotalAmount());
            output.Append('\n');
            return output;
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

        private StringBuilder PrintTotalPrice()
        {
            return new StringBuilder().Append(TotalAmountText).Append('\t').Append(order.TotalPrice());
        }

        private StringBuilder PrintSalesTax()
        {
            return new StringBuilder().Append(SalesTaxText).Append('\t').Append(order.GetTax());
        }
        
        private StringBuilder PrintDiscount()
        {
            return new StringBuilder().Append(DiscountText).Append('\t').Append(order.GetDiscount());
        }
    }
}