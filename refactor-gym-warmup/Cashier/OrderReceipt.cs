using System.Linq;
using System.Text;

namespace refactor_gym_warmup_2020.cashier
{
    public class OrderReceipt
    {
        private Order order;

        public OrderReceipt(Order order)
        {
            this.order = order;
        }

        public string PrintReceipt()
        {
            StringBuilder output = new StringBuilder();
            output.Append(PrintHeaderText());
            order.GetLineItems().ForEach(item => output.Append(PrintLineItem(item)));
            
            output.Append(PrintSalesTax());
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

        private StringBuilder PrintHeaderText()
        {
            var output = new StringBuilder();
            output.Append("======Printing Orders======\n");
            output.Append(order.GetCustomerName());
            output.Append(order.GetCustomerAddress());
            return output;
        }

        private StringBuilder PrintTotalPrice()
        {
            return new StringBuilder().Append("Total Amount").Append('\t').Append(order.GetLineItems().Sum(item => item.TotalPrice()));
        }

        private StringBuilder PrintSalesTax()
        {
            return new StringBuilder().Append("Sales Tax").Append('\t').Append(order.GetLineItems().Sum(item => item.Tax()));

        }
    }
}