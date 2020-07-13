namespace refactor_gym_warmup_2020.cashier
{
    public class LineItem
    {
        private const double TaxRate = .10;

        public string desc;
        private double price;
        private int qty;

        public LineItem(string desc, double price, int qty)
        {
            this.desc = desc;
            this.price = price;
            this.qty = qty;
        }

        public string GetDescription()
        {
            return desc;
        }

        public double GetPrice()
        {
            return price;
        }

        public int GetQuantity()
        {
            return qty;
        }

        public double TotalAmount()
        {
            return price * qty;
        }

        public double Tax()
        {
            return TotalAmount() * TaxRate;
        }

        public double TotalPrice()
        {
            return TotalAmount() + Tax();
        }
    }
}