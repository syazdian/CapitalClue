using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace CapitalClue.Common.Models;

public class BellCsv
{
    public string ENTITYCODE { get; set; }
    public string PAIDDEALER { get; set; }
    public string DEALER { get; set; }
    public string PRODUCERCODE { get; set; }
    public string DEALERNAME { get; set; }
    public string TRANSACTIONDATE { get; set; }
    public string INITIALACTIVATIONDATE { get; set; }
    public string EFFECTIVEDATE { get; set; }
    public string DAYSINSERVICE { get; set; }
    public string ACCOUNTDESCRIPTION { get; set; }
    public string NAGCODE { get; set; }
    public string NAGCLASSIFICATION { get; set; }
    public string ACCOUNTNUMBER { get; set; }
    public string SUBSCRIBERID { get; set; }
    public string CTN { get; set; }
    public string CTNOLD { get; set; }
    public string SURNAME { get; set; }
    public string GIVENNAME { get; set; }
    public string TRANSACTIONTYPE { get; set; }
    public string TRANSACTIONREASONCODE { get; set; }
    public string TRANSACTIONREASONDESCRIPTION { get; set; }
    public string PRODUCTCODE { get; set; }
    public string PRODUCTDESCRIPTION { get; set; }
    public string PRODUCTTERM { get; set; }
    public string MSF { get; set; }
    public string NETMSF { get; set; }
    public string FEATUREMIGRATIONPRODUCT1 { get; set; }
    public string FEATUREMIGRATIONPRODUCT1DESC { get; set; }
    public string FEATUREMIGRATIONPRODUCT2 { get; set; }
    public string FEATUREMIGRATIONPRODUCT2DESC { get; set; }
    public string ESN_IMEI { get; set; }
    public string ESN_IMEIOLD { get; set; }
    public string HARDWAREINDICATOR { get; set; }
    public string MODELNUMBER { get; set; }
    public string MODELDESCRIPTION { get; set; }
    public string HANDSETSRP { get; set; }
    public string NETWORKTYPE { get; set; }
    public string SIMCARDNUMBER { get; set; }
    public string SIMCARDNUMBEROLD { get; set; }
    public string PROMOID { get; set; }
    public string CERTIFICATENUMBER { get; set; }
    public string TRANSACTIONSEQUENCENUMBER { get; set; }
    public string ADJUSTMENTINDICATOR { get; set; }
    public string ADJUSTMENTNUMBER { get; set; }
    public string QUANTITY { get; set; }
    public string COMMISSIONAMOUNT { get; set; }
    public string COMMISSIONGROUP { get; set; }
    public string COMMISSIONTYPE { get; set; }
    public string COMMISSIONDESCRIPTION { get; set; }
    public string SIM_PROFILE { get; set; }
    public string PRODUCTSEQUENCENUMBER { get; set; }
    public string BENEFITTYPE { get; set; }
    public string BENEFITCODE { get; set; }
    public string BENEFITNAME { get; set; }
}