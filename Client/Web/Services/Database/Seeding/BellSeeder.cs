using CapitalClue.Common.Models.Enums;
using CapitalClue.Common.Models;

namespace CapitalClue.Frontend.Web.Services.Database;

public class BellSeeder
{
    public string[] bellstring = null;
    public List<BellSourceDto> BellSourceToSeed { get; set; } = new List<BellSourceDto>();

    public BellSeeder()
    {
        string bellFile = "1,0,1259,,99729049254,,7/10/2021,Ailene Moltz,Wireline,Home Internet,Commission,False,,,;2,0,-193,,31099874525,,5/3/2023,Adelle Pedder,Wireline,IoT,Commission,FALSE,,,;3,0,670,,60680284522,,11/14/2022,Blossom Frisinger,Wireline,Home Internet,Commission,False,,,;4,0,418,,85797142565,,2/10/2022,Ocie Brennick,Wireline,IoT,Commission,FALSE,,,;5,0,688,,44626367438,,3/13/2022,Clemencia Normandeau,Wireline,IoT,Commission,FALSE,,,;6,85511774439,1441,,29497815413,3207700812,10/19/2020,Jaime Opitz,Wireless,Wireless,Commission,FALSE,,,;7,85511774439,232,,29497815413,2370174045,10/19/2020,Jaime Opitz,Wireless,Wireless,Down Payment,FALSE,,,;8,85511774439,268,,29497815413,5467361369,10/19/2020,Jaime Opitz,Wireless,Wireless,Tax Rebate,FALSE,,,;9,0,862,,50549767516,,7/30/2021,Lucille Borlace,Wireline,Home Internet,Commission,False,,,;10,80389827948,1006,,70264069748,6670943362,6/14/2022,Emmett Suero,Wireless,Wireless,Commission,FALSE,,,;11,80389827948,492,,70264069748,1822321436,6/14/2022,Lauryn Hermon,Wireless,Wireless,Down Payment,FALSE,,,;12,80389827948,1185,,70264069748,4015646066,6/14/2022,Lauryn Hermon,Wireless,Wireless,Tax Rebate,FALSE,,,;13,0,1058,,89892130120,,11/23/2020,Darell Collopy,Wireline,IoT,Commission,FALSE,,,;14,0,-287,,63434461717,,9/29/2021,Matthew Rippy,Wireless,IoT,Commission,FALSE,,,;15,95987946589,390,,30024913239,6431339165,12/11/2020,Jimmie Tindal,Wireless,Wireless,Commission,FALSE,,,;16,95987946589,1315,,30024913239,6431339165,12/11/2020,Jimmie Tindal,Wireless,Wireless,Down Payment,FALSE,,,;17,95987946589,840,,30024913239,6431339165,12/11/2020,Jimmie Tindal,Wireless,Wireless,Tax Rebate,FALSE,,,;18,0,1178,,51095509031,,10/9/2021,Alita Sport,Wireline,IoT,Commission,FALSE,,,;19,0,1343,,37468123947,,5/22/2021,Mozelle Hailstone,Wireline,IoT,Commission,FALSE,,,;20,0,-378,,32188463385,,10/24/2020,An Twyman,Wireless,IoT,Commission,FALSE,,,;21,0,-395,,71879547131,,1/3/2022,Edmond Adessa,Wireline,IoT,Commission,FALSE,,,;25,0,-278,,84377748307,,4/11/2021,Shawnna Evartt,Wireline,IoT,Commission,FALSE,,,;26,84658796579,900,,53617640053,4678324088,5/11/2022,Nada Steinhorst,Wireless,Wireless,Commission,FALSE,,,;27,84658796579,963,,53617640053,6735892354,5/11/2022,Nada Steinhorst,Wireless,Wireless,Down Payment,FALSE,,,;28,84658796579,833,,53617640053,9362677889,5/11/2022,Nada Steinhorst,Wireless,Wireless,Tax Rebate,FALSE,,,;29,0,-196,,75479679299,,2/19/2023,Von Mcglocklin,Wireline,IoT,Commission,FALSE,,,;30,0,-236,,38230219070,,11/7/2022,Sarina Mattia,Wireline,IoT,Commission,FALSE,,,;31,18707324631,766,,69096048049,4424571635,11/23/2021,Rochell Magathan,Wireless,Wireless,Commission,FALSE,,,;32,18707324631,894,,69096048049,4424571635,11/23/2021,Rochell Magathan,Wireless,Wireless,Down Payment,FALSE,,,;33,18707324631,442,,69096048049,4424571635,11/23/2021,Rochell Magathan,Wireless,Wireless,Tax Rebate,FALSE,,,;34,0,907,,46022206545,,6/22/2021,Mabel Kitchens,Wireless,IoT,Commission,FALSE,,,;35,0,139,,98265001180,,11/20/2022,Twila Betterton,Wireline,IoT,Commission,FALSE,,,;36,0,712,,99239912158,,6/10/2021,Marla Purslow,Wireless,IoT,Commission,FALSE,,,;37,0,1009,,54222281877,,1/29/2021,Francoise Oien,Wireline,IoT,Commission,FALSE,,,;38,0,1025,,38106848042,,9/30/2022,Nakisha Matsuno,Wireline,IoT,Commission,FALSE,,,;40,0,296,,75465550379,,10/31/2020,Rodney Okuniewski,Wireline,IoT,Commission,FALSE,,,;41,0,-163,,77308921276,,10/7/2020,Callie Pesqueira,Wireless,IoT,Commission,FALSE,,,;42,0,1064,,20038780986,,8/11/2022,Dominga Gerten,Wireless,IoT,Commission,FALSE,,,;43,65846570806,898,,83397444703,7528621010,11/9/2022,Rashad Mamudoski,Wireless,Wireless,Commission,FALSE,,,;44,65846570806,697,,83397444703,7528621010,11/9/2022,Rashad Mamudoski,Wireless,Wireless,Down Payment,FALSE,,,;45,65846570806,776,,83397444703,7528621010,11/9/2022,Rashad Mamudoski,Wireless,Wireless,Tax Rebate,FALSE,,,;46,0,-121,,24160681387,,4/26/2022,Alison Edmison,Wireless,IoT,Commission,FALSE,,,;47,0,1288,,45882765182,,11/10/2020,Gregg Skahan,Wireline,IoT,Commission,FALSE,,,;48,0,919,,70629209069,,6/26/2022,Danilo Perry,Wireless,IoT,Commission,FALSE,,,;49,0,654,,24232064765,,6/28/2021,Alison Vinroe,Wireless,IoT,Commission,False,,,;50,0,914,,43285824169,,6/8/2021,Johna Normandeau,Wireline,IoT,Commission,FALSE,,,;51,0,-452,,41417332520,,4/18/2022,Jerilyn Brake,Wireline,IoT,Commission,FALSE,,,;52,0,1489,,25790971081,,4/16/2023,Kaycee Foulds,Wireline,IoT,Commission,FALSE,,,;53,0,609,,17378538153,,11/20/2022,Shala Bastain,Wireless,IoT,Commission,FALSE,,,;54,0,264,,21352946984,,2/16/2023,Maxima Beliles,Wireline,IoT,Commission,FALSE,,,;55,14249503197,1312,,26327422074,7612774980,7/4/2022,Jarod Foulds,Wireless,Wireless,Commission,FALSE,,,;56,14249503197,880,,26327422074,7612774980,7/4/2022,Jarod Foulds,Wireless,Wireless,Down Payment,FALSE,,,;57,14249503197,948,,26327422074,7612774980,7/4/2022,Jarod Foulds,Wireless,Wireless,Tax Rebate,FALSE,,,;58,0,1020,,98094445217,,12/29/2020,Maxima Russi,Wireline,IoT,Commission,FALSE,,,;59,27008959760,1375,,28145598339,4398339490,6/9/2021,Catina Mckeon,Wireless,Wireless,Commission,FALSE,,,;60,27008959760,144,,28145598339,4398339490,6/9/2021,Catina Mckeon,Wireless,Wireless,Down Payment,FALSE,,,;61,27008959760,225,,28145598339,4398339490,6/9/2021,Catina Mckeon,Wireless,Wireless,Tax Rebate,FALSE,,,;62,97499214072,1162,,89693501216,1548103150,11/5/2020,Kendra Stimac,Wireless,Wireless,Commission,FALSE,,,;63,97499214072,578,,89693501216,1548103150,11/5/2020,Kendra Stimac,Wireless,Wireless,Down Payment,FALSE,,,;64,97499214072,987,,89693501216,1548103150,11/5/2020,Kendra Stimac,Wireless,Wireless,Tax Rebate,FALSE,,,;65,0,869,,28169873692,,7/17/2022,Nan Echegoyen,Wireless,IoT,Commission,FALSE,,,;66,0,585,,72870550038,,8/23/2022,Kimi Geberth,Wireless,IoT,Commission,FALSE,,,;71,0,921,,68589551747,,4/17/2022,Bernadine Eisert,Wireline,Home Internet,Commission,False,,,;72,0,1144,,83029636434,,7/29/2022,Lashaun Prothro,Wireline,IoT,Commission,FALSE,,,;73,0,-202,,69354498861,,12/25/2020,Kathrine Rand,Wireless,IoT,Commission,False,,,;74,0,-395,,60670096197,,2/22/2023,Shasta Vanhuss,Wireline,Home Internet,Commission,False,,,;75,0,-54,,95282786600,,11/21/2021,Kassandra Bonventre,Wireless,IoT,Commission,FALSE,,,;76,79434900961,1312,,38301557601,6397456769,11/13/2022,Margot Pennel,Wireless,Wireless,Commission,FALSE,,,;77,79434900961,861,,38301557601,6397456769,11/13/2022,Margot Pennel,Wireless,Wireless,Down Payment,FALSE,,,;78,79434900961,1152,,38301557601,6397456769,11/13/2022,Margot Pennel,Wireless,Wireless,Tax Rebate,FALSE,,,;79,0,-499,,37884524771,,2/19/2021,Freeman Shufelt,Wireline,IoT,Commission,FALSE,,,;81,0,819,,69770452491,,7/2/2022,An Bubrig,Wireline,IoT,Commission,FALSE,,,;82,0,509,,34797370111,,11/7/2020,Ricki Boerboom,Wireline,Home Internet,Commission,False,,,;83,95767318469,358,,70985038883,6868725940,10/13/2021,Doretha Eggers,Wireless,Wireless,Commission,FALSE,,,;84,95767318469,315,,70985038883,6868725940,10/13/2021,Doretha Eggers,Wireless,Wireless,Down Payment,FALSE,,,;85,95767318469,997,,70985038883,6868725940,10/13/2021,Doretha Eggers,Wireless,Wireless,Tax Rebate,FALSE,,,;86,0,-442,,12217617329,,9/15/2021,Florinda Klukken,Wireline,IoT,Commission,FALSE,,,;87,0,500,,57470332279,,2/22/2022,Beata Weekly,Wireline,IoT,Commission,FALSE,,,;88,0,1346,,98533377854,,4/30/2022,Caren Pritt,Wireline,IoT,Commission,FALSE,,,;89,0,1148,,52193497305,,10/3/2022,Callie Oppy,Wireline,IoT,Commission,FALSE,,,;90,0,482,,16907596280,,10/20/2020,Chuck Kana,Wireless,IoT,Commission,FALSE,,,;94,0,987,,89841417486,,3/5/2022,Madonna Elmquist,Wireline,IoT,Commission,FALSE,,,;95,0,938,,50804394623,,4/17/2022,Paola Jobst,Wireline,IoT,Commission,FALSE,,,;96,0,-465,,15318666558,,6/27/2022,Yong Keomuangtai,Wireline,IoT,Commission,FALSE,,,;97,96336825968,1161,,22913012616,3736149606,11/14/2021,Jenifer Doscher,Wireless,Wireless,Commission,FALSE,,,;98,96336825968,192,,22913012616,3736149606,11/14/2021,Jenifer Doscher,Wireless,Wireless,Down Payment,FALSE,,,;99,96336825968,1153,,22913012616,3736149606,11/14/2021,Jenifer Doscher,Wireless,Wireless,Tax Rebate,FALSE,,,;100,0,511,,49097195998,,12/6/2021,Bev Bub,Wireline,IoT,Commission,FALSE,,,;103,43339724204,1305,,99504246957,8689237623,8/10/2021,Danilo Kratt,Wireless,Wireless,Commission,FALSE,,,;104,43339724204,265,,99504246957,8689237623,8/10/2021,Danilo Kratt,Wireless,Wireless,Down Payment,FALSE,,,;105,43339724204,852,,99504246957,8689237623,8/10/2021,Danilo Kratt,Wireless,Wireless,Tax Rebate,FALSE,,,;106,0,692,,59025731926,,2/22/2021,Aimee Griepentrog,Wireline,IoT,Commission,FALSE,,,;107,23948417678,1433,,33826819786,9036069147,7/9/2022,Alberto Matsuno,Wireless,Wireless,Commission,FALSE,,,;108,23948417678,403,,33826819786,9036069147,7/9/2022,Alberto Matsuno,Wireless,Wireless,Down Payment,FALSE,,,;109,23948417678,1390,,33826819786,9036069147,7/9/2022,Alberto Matsuno,Wireless,Wireless,Tax Rebate,FALSE,,,;";
        bellstring = bellFile.Split(';');
        foreach (string bell in bellstring)
        {
            if (string.IsNullOrEmpty(bell))
            { continue; }
            var bellres = FromCsv(bell);
            if (bellres.Id == 0)
            {
                continue;
            }
            BellSourceToSeed.Add(bellres);
        }
    }

    public static BellSourceDto FromCsv(string csvLine)
    {
        if (csvLine.Length == 0)
        {
            return new BellSourceDto() { Id = 0 };
        }

        BellSourceDto blsc = new BellSourceDto();
        try
        {
            string[] values = csvLine.Split(',');
            blsc.Id = Convert.ToInt32(values[0]);
            blsc.Phone = Convert.ToInt64(values[1]);
            blsc.Amount = Convert.ToDecimal(values[2]);
            blsc.Comments = Convert.ToString(values[3]);
            blsc.OrderNumber = values[4];
            blsc.Imei = Convert.ToString(values[5]);
            blsc.TransactionDate = Convert.ToDateTime(values[6]);
            blsc.CustomerName = Convert.ToString(values[7]);
            blsc.Lob = Convert.ToString(values[8]);
            //blsc.SubLob = Convert.ToString(values[9]);
            blsc.RebateType = Convert.ToString(values[10]);
        }
        catch (Exception ex)
        {
            throw;
        }
        return blsc;
    }
}