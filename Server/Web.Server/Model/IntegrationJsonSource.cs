namespace CapitalClue.Web.Server.Model
{
    public class IntegrationJsonSource
    {
        public class SourceJson
        {
            public Transaction[] transactions { get; set; }
        }

        public class Transaction
        {
            public string masterId { get; set; }
            public int version { get; set; }
            public string status { get; set; }
            public Pos pos { get; set; }
            public Vendor vendor { get; set; }
            public OriginalSale originalSale { get; set; }
            public Contact[] contacts { get; set; }
            public Tax tax { get; set; }
            public Downpayment downPayment { get; set; }
            public object[] tenders { get; set; }
            public Order[] orders { get; set; }
        }

        public class OriginalSale
        {
            public string vendorTransactionId { get; set; }
            public DateTime? posDateTime { get; set; }
            public string posStore { get; set; }
            public string posRegister { get; set; }
            public string posTransactionId { get; set; }
        }

        public class Pos
        {
            public DateTime dateTime { get; set; }
            public string store { get; set; }
            public string register { get; set; }
            public string transactionId { get; set; }
            public string user { get; set; }
        }

        public class Vendor
        {
            public DateTime dateTime { get; set; }
            public string clientId { get; set; }
            public string dealerCode { get; set; }
            public string transactionId { get; set; }
            public string transactionType { get; set; }
            public bool eSigned { get; set; }
        }

        public class Tax
        {
            public float totalDevicesTax { get; set; }
            public float totalTaxFromBell { get; set; }
            public string provinceCode { get; set; }
            public object exemptionAccountNumber { get; set; }
            public bool federalExempted { get; set; }
            public bool provincialExempted { get; set; }
        }

        public class Downpayment
        {
            public float taxAmountHST { get; set; }
            public float taxAmountGST { get; set; }
            public float taxAmountPST { get; set; }
            public float totalAmount { get; set; }
            public float amount { get; set; }
        }

        public class Contact
        {
            public string[] tags { get; set; }
            public string language { get; set; }
            public object company { get; set; }
            public Person person { get; set; }
            public Email[] emails { get; set; }
            public Phone[] phones { get; set; }
            public Address[] addresses { get; set; }
        }

        public class Person
        {
            public string firstName { get; set; }
            public string middleName { get; set; }
            public string lastName { get; set; }
        }

        public class Email
        {
            public string[] tags { get; set; }
            public string address { get; set; }
        }

        public class Phone
        {
            public string[] tags { get; set; }
            public string number { get; set; }
            public object extension { get; set; }
        }

        public class Address
        {
            public string[] tags { get; set; }
            public string[] street { get; set; }
            public string city { get; set; }
            public string provinceCode { get; set; }
            public string province { get; set; }
            public string postalCode { get; set; }
        }

        public class Order
        {
            public int index { get; set; }
            public string productType { get; set; }
            public string orderNumber { get; set; }
            public DateTime orderDate { get; set; }
            public string orderType { get; set; }
            public string orderSubType { get; set; }
            public string serviceType { get; set; }
            public string accountType { get; set; }
            public string customerSubType { get; set; }
            public string brandIndicatorType { get; set; }
            public Soldby soldBy { get; set; }
            public Account account { get; set; }
            public Subscriber[] subscribers { get; set; }
        }

        public class Soldby
        {
            public string userId { get; set; }
            public string employeeId { get; set; }
        }

        public class Account
        {
            public string number { get; set; }
            public object deposit { get; set; }
        }

        public class Subscriber
        {
            public int id { get; set; }
            public string saleType { get; set; }
            public string phoneNumber { get; set; }
            public Item item { get; set; }
            public object deposit { get; set; }
            public object bundleDiscount { get; set; }
            public object promo { get; set; }
            public Sim sim { get; set; }
            public Plan plan { get; set; }
            public Service[] services { get; set; }
            public Return _return { get; set; }
        }

        public class Return
        {
            public string code { get; set; }
            public string description { get; set; }
        }

        public class Item
        {
            public Serialnumber serialNumber { get; set; }
            public string code { get; set; }
            public string itemId { get; set; }
            public object upcCode { get; set; }
            public int quantity { get; set; }
            public string model { get; set; }
            public string descriptionEn { get; set; }
            public string descriptionFr { get; set; }
            public float? unitPrice { get; set; }
            public float upfrontDiscount { get; set; }
            public float totalDiscount { get; set; }
            public Netprice netPrice { get; set; }
            public Installment installment { get; set; }
            public Residualvalue residualValue { get; set; }
            public Downpayment1 downPayment { get; set; }
        }

        public class Serialnumber
        {
            public string value { get; set; }
            public string format { get; set; }
        }

        public class Netprice
        {
            public float taxAmountHST { get; set; }
            public float taxAmountGST { get; set; }
            public float taxAmountPST { get; set; }
            public float totalAmount { get; set; }
            public float amount { get; set; }
        }

        public class Installment
        {
            public bool deferredTax { get; set; }
            public float deferredTaxAmount { get; set; }
            public int term { get; set; }
            public bool amortizedTax { get; set; }
            public float taxAmount { get; set; }
            public float totalAmount { get; set; }
            public float amount { get; set; }
        }

        public class Residualvalue
        {
            public float taxAmount { get; set; }
            public float totalAmount { get; set; }
            public float amount { get; set; }
        }

        public class Downpayment1
        {
            public float taxAmountHST { get; set; }
            public float taxAmountGST { get; set; }
            public float taxAmountPST { get; set; }
            public float totalAmount { get; set; }
            public float amount { get; set; }
        }

        public class Sim
        {
            public string id { get; set; }
            public string code { get; set; }
            public string itemId { get; set; }
            public float? unitPrice { get; set; }
            public string description { get; set; }
            public object descriptionEn { get; set; }
            public object descriptionFr { get; set; }
        }

        public class Plan
        {
            public string description { get; set; }
            public object serviceTier { get; set; }
            public object promotionCode { get; set; }
            public object otherInfo { get; set; }
            public Commission commission { get; set; }
            public Adjustment adjustment { get; set; }
            public string code { get; set; }
            public string itemId { get; set; }
            public float? unitPrice { get; set; }
            public int term { get; set; }
            public string descriptionEn { get; set; }
            public string descriptionFr { get; set; }
        }

        public class Commission
        {
            public float associateAmount { get; set; }
            public float taxAmount { get; set; }
            public float totalAmount { get; set; }
            public float amount { get; set; }
        }

        public class Adjustment
        {
            public object itemId { get; set; }
            public object notes { get; set; }
            public float associateAmount { get; set; }
            public float taxAmount { get; set; }
            public float totalAmount { get; set; }
            public float amount { get; set; }
        }

        public class Service
        {
            public Commission1 commission { get; set; }
            public Adjustment1 adjustment { get; set; }
            public string code { get; set; }
            public string itemId { get; set; }
            public float? unitPrice { get; set; }
            public object quantity { get; set; }
            public object description { get; set; }
            public string descriptionEn { get; set; }
            public string descriptionFr { get; set; }
        }

        public class Commission1
        {
            public float associateAmount { get; set; }
            public float taxAmount { get; set; }
            public float totalAmount { get; set; }
            public float amount { get; set; }
        }

        public class Adjustment1
        {
            public object itemId { get; set; }
            public object notes { get; set; }
            public float associateAmount { get; set; }
            public float taxAmount { get; set; }
            public float totalAmount { get; set; }
            public float amount { get; set; }
        }
    }
}