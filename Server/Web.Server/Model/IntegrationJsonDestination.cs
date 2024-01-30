namespace CapitalClue.Web.Server.Model
{
    public class IntegrationJsonDestination
    {
        public class DestinationJson
        {
            public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        }

        public class Transaction
        {
            public string bellTransactionId { get; set; }
            public string posTransactionId { get; set; }
            public string dateTime { get; set; }
            public string user { get; set; }
            public string dealerCode { get; set; }
            public string transactionType { get; set; }
            public string originalSale { get; set; }
            public string customerName { get; set; }
            public string customerContact { get; set; }
            public string taxExemption { get; set; }
            public string taxTotal { get; set; }
            public string downPayment { get; set; }
            public List<Order> orders { get; set; } = new();
        }

        public class Order
        {
            public string productType { get; set; }
            public string orderNumber { get; set; }
            public string orderDate { get; set; }
            public string orderType { get; set; }
            public List<Subscriber> subscribers { get; set; } = new();
        }

        public class Subscriber
        {
            public string saleType { get; set; }
            public string device { get; set; }
            public string deviceSKU { get; set; }
            public string deviceSerial { get; set; }
            public string deviceUnitPrice { get; set; }
            public string deviceCredit { get; set; }
            public string deviceNetPrice { get; set; }
            public string deviceInstallment { get; set; }
            public string deviceDRO { get; set; }
            public string deviceDownPayment { get; set; }
            public string phoneNumber { get; set; }
            public string sim { get; set; }
            public string simSKU { get; set; }
            public string simSerial { get; set; }
            public string simUnitPrice { get; set; }
            public string plan { get; set; }
            public string planSKU { get; set; }
            public string planCommission { get; set; }
            public string planIncentive { get; set; }
            public string _return { get; set; }
        }
    }
}