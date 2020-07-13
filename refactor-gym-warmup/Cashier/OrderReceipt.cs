using System.Linq;
using System.Text;

namespace refactor_gym_warmup_2020.cashier
{
    /**
 * OrderReceipt prints the details of order including customer name, address, description, quantity,
 * price and amount. It also calculates the sales tax @ 10% and prints as part
 * of order. It computes the total order amount (amount of individual lineItems +
 * total sales tax) and prints it.
 *
 */
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
            PrintHeaderText(output);
            
            order.GetLineItems().ForEach(item => PrintLineItem(output, item));
            
            PrintSalesTax(output);
            PrintTotalPrice(output);
            return output.ToString();
        }

        private static void PrintLineItem(StringBuilder output, LineItem lineItem)
        {
            output.Append(lineItem.desc);
            output.Append('\t');
            output.Append(lineItem.GetPrice());
            output.Append('\t');
            output.Append(lineItem.GetQuantity());
            output.Append('\t');
            output.Append(lineItem.TotalAmount());
            output.Append('\n');
        }

        private void PrintHeaderText(StringBuilder output)
        {
            output.Append("======Printing Orders======\n");
            output.Append(order.GetCustomerName());
            output.Append(order.GetCustomerAddress());
        }

        private void PrintTotalPrice(StringBuilder output)
        {
            output.Append("Total Amount").Append('\t').Append(order.GetLineItems().Sum(item => item.TotalPrice()));
        }

        private void PrintSalesTax(StringBuilder output)
        {
            output.Append("Sales Tax").Append('\t').Append(order.GetLineItems().Sum(item => item.Tax()));
        }
    }
}