using CapitalClue.Common.Utilities;
using CapitalClue.Web.Server.Data.Sqlserver;
using CapitalClue.Web.Server.Model;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Nodes;
using static CapitalClue.Web.Server.Model.IntegrationJsonDestination;
using static CapitalClue.Web.Server.Model.IntegrationJsonSource;

namespace CapitalClue.Web.Server.Services
{
    public class JsonManipulating
    {
        public string JsonManipulatingService(string receivedJson)
        {
            var transactions = ConvertSnapOperToTransactions(receivedJson);
            if (transactions.IsNullOrEmpty())
            {
                return "{\"Error\" : \"There is no data to present\" } ";
            }
            var mappedJson = JsonConvertorting(transactions);
            return mappedJson;
        }

        public string ConvertSnapOperToTransactions(string res)
        {
            try
            {
                string result = string.Empty;
                if (res.IsNullOrEmpty())
                {
                    return null;
                }
                JsonNode forecastNode = JsonNode.Parse(res)!;
                var obj = forecastNode.AsObject();
                if (obj.ContainsKey("snapshots"))
                    obj.Remove("snapshots");
                if (obj.ContainsKey("operations"))
                {
                    var operationsValue = obj["operations"].ToJsonString();

                    var newobject = new JsonObject
                    {
                        ["transactions"] = JsonNode.Parse(operationsValue)
                    };
                    result = newobject.ToJsonString();
                }

                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string JsonConvertorting(string transactionsSource)
        {
            try
            {
                SourceJson jsonSource = new SourceJson();
                jsonSource = transactionsSource.JsonDeserialize<SourceJson>();

                IntegrationJsonDestination jsonDestination = new IntegrationJsonDestination();

                DestinationJson destinationJson = new DestinationJson();

                foreach (var transaction in jsonSource.transactions)
                {
                    IntegrationJsonDestination.Transaction mapping = new IntegrationJsonDestination.Transaction();
                    mapping.bellTransactionId = $"{transaction.masterId} - {transaction.version} - {transaction.status}";
                    mapping.posTransactionId = $"{transaction.pos?.dateTime.ToShortDateString()} - {transaction.pos?.store} - {transaction.pos?.register} - {transaction.pos?.transactionId}";
                    mapping.dateTime = $"{transaction.pos?.dateTime} | {transaction.vendor?.dateTime}";
                    mapping.user = $"{transaction.pos?.user} | {transaction.orders[0]?.soldBy?.userId} | {transaction.orders[0]?.soldBy?.employeeId}";
                    mapping.dealerCode = $"{transaction.orders[0]?.brandIndicatorType} | {transaction.vendor?.dealerCode}";
                    mapping.originalSale = $"{transaction.originalSale?.vendorTransactionId} - {transaction.originalSale?.posDateTime?.ToString("yyyyMMdd")} - {transaction.originalSale?.posStore} - {transaction.originalSale?.posRegister} - {transaction.originalSale?.posTransactionId}";
                    if (transaction.contacts?.Count() > 0)
                    {
                        mapping.customerName = $"{transaction.orders[0]?.account?.number} | {transaction.contacts[0]?.company} | {transaction.contacts[0]?.person?.firstName}   {transaction.contacts[0]?.person?.lastName}";
                        mapping.customerContact = $"{transaction.contacts[0]?.emails[0]?.address} | {transaction.contacts[0]?.phones[0]?.number} | {transaction.contacts[0]?.addresses[0]?.city} , {transaction.contacts[0]?.addresses[0]?.provinceCode}";
                    }
                    else
                    {
                        mapping.customerName = $"{transaction.orders[0]?.account?.number}";
                    }
                    mapping.taxExemption = $"{transaction.tax?.provinceCode} | F= {transaction.tax?.federalExempted} | P= {transaction.tax?.provincialExempted}";
                    mapping.taxTotal = $"Devices= {transaction.tax?.totalDevicesTax} | BellRebate= {transaction.tax?.totalTaxFromBell}";
                    mapping.downPayment = $"{transaction.downPayment?.amount} | {transaction.downPayment?.totalAmount}";
                    mapping.transactionType = $"{transaction.vendor?.transactionType}" ;
                    foreach (var tOrder in transaction.orders)
                    {
                        var order = new IntegrationJsonDestination.Order();
                        order.productType = tOrder.productType;
                        order.orderNumber = tOrder.orderNumber;
                        order.orderDate = tOrder.orderDate.ToString();
                        order.orderType = $"{tOrder.orderType} |  {tOrder.orderSubType} |  {tOrder.serviceType}  |  {tOrder.accountType} |  {tOrder.customerSubType}";

                        foreach (var tOrderSubscriber in tOrder.subscribers)
                        {
                            var orderSubscriber = new IntegrationJsonDestination.Subscriber();
                            orderSubscriber.saleType = tOrderSubscriber.saleType;
                            orderSubscriber.device = $"{tOrderSubscriber.item?.model} |  {tOrderSubscriber.item?.descriptionEn}";
                            orderSubscriber.deviceSKU = $"{tOrderSubscriber.item?.code} |  {tOrderSubscriber.item?.itemId}";
                            orderSubscriber.deviceSerial = $"{tOrderSubscriber.item?.serialNumber?.value} | {tOrderSubscriber.item?.upcCode?.ToString()}";
                            orderSubscriber.deviceUnitPrice = tOrderSubscriber.item?.unitPrice?.ToString();
                            orderSubscriber.deviceCredit = tOrderSubscriber.item?.upfrontDiscount.ToString();
                            orderSubscriber.deviceNetPrice = $"{tOrderSubscriber.item?.netPrice.totalAmount} | {tOrderSubscriber.item?.netPrice.amount} | {tOrderSubscriber.item?.netPrice.taxAmountHST} | {tOrderSubscriber.item?.netPrice.taxAmountGST} | {tOrderSubscriber.item?.netPrice.taxAmountPST}";
                            orderSubscriber.deviceInstallment = $"{tOrderSubscriber.item?.installment?.totalAmount} | {tOrderSubscriber.item?.installment?.amount} | {tOrderSubscriber.item?.installment?.taxAmount} | {tOrderSubscriber.item?.installment?.term}";
                            orderSubscriber.deviceDRO = $"{tOrderSubscriber.item?.residualValue?.totalAmount} | {tOrderSubscriber.item?.residualValue?.amount} | {tOrderSubscriber.item?.residualValue?.taxAmount}";
                            orderSubscriber.deviceDownPayment = $"{tOrderSubscriber.item?.downPayment?.totalAmount} | {tOrderSubscriber.item?.downPayment?.amount} | {tOrderSubscriber.item?.downPayment?.taxAmountHST} | {tOrderSubscriber.item?.downPayment?.taxAmountGST} | {tOrderSubscriber.item?.downPayment?.taxAmountPST}";
                            orderSubscriber.phoneNumber = tOrderSubscriber.phoneNumber;
                            orderSubscriber.sim = tOrderSubscriber.sim?.description;
                            orderSubscriber.simSKU = $"{tOrderSubscriber.sim?.code} | {tOrderSubscriber.sim?.itemId}";
                            orderSubscriber.simSerial = tOrderSubscriber.sim?.id;
                            orderSubscriber.simUnitPrice = tOrderSubscriber.sim?.unitPrice?.ToString();
                            orderSubscriber.plan = $"{tOrderSubscriber.plan?.description} | {tOrderSubscriber.plan?.term} | {tOrderSubscriber.plan?.unitPrice} | {tOrderSubscriber.plan?.descriptionEn} | {tOrderSubscriber.plan?.serviceTier}";
                            orderSubscriber.planSKU = $"{tOrderSubscriber.plan?.code} | {tOrderSubscriber.plan?.itemId}";
                            orderSubscriber.planCommission = $"{tOrderSubscriber.plan?.commission?.totalAmount} | {tOrderSubscriber.plan?.commission?.amount} | {tOrderSubscriber.plan?.commission?.taxAmount}";
                            orderSubscriber.planIncentive = tOrderSubscriber.plan?.commission?.associateAmount.ToString();
                            orderSubscriber._return = $"{tOrderSubscriber._return?.code} |  {tOrderSubscriber._return?.description}";
                            order.subscribers.Add(orderSubscriber);
                        }
                        mapping.orders.Add(order);
                    }

                    destinationJson.Transactions.Add(mapping);
                }

                return destinationJson.ToJson();
            }
            catch (Exception)
            {
                return transactionsSource;
            }
        }
    }
}