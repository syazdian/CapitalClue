using Bell.Reconciliation.Web.Server.Data.Sqlserver;

using Mapster;

namespace Bell.Reconciliation.Web.Server.Services
{
    public class DatabaseGenerator
    {
        public DatabaseGenerator()
        {
        }

        private List<BellSource> bellBuld = new List<BellSource>();
        private List<StaplesSource> stapleBuld = new List<StaplesSource>();

        private string[] Brands = { "Bell", "Virgin", "Lucky Mobile" };
        private string[] Lobs = { "Wireless", "Wireline" };
        private string[] WirelessSubLOBs = { "Wireless", "IoT", "Mobile Data" };
        private string[] WiredSubLOBs = { "Home Internet", "Home Internet", "Home Television", "Home Phone", "SMB Internet" };
        private string[] Locations = { "Toronto", "Ottawa", "London", "Kingston", "Windsor", "Hamilton", "Barrie", "Kitchener", "Mississagua" };
        private string[] RebateTypes = { "Commission", "Down Payment", "Tax Rebate" };

        private string[] Products = { "iPhone 13 128GB Red", "iPhone 14 128GB Black", "iPhone 13 256GB Gold","2-Year NAC WRLS 35+",
"2-Year NAC WRLS 55+","iPhone 13 128GB Gold","iPhone 14 256GB Gold","iPhone 14 256GB Black"};

        private List<string> salesPersons = new();
        private Random rand = new Random();
        private BellRecContext db = new BellRecContext();
       // private Data.Sqlserver.StapleContext db = new StapleContext();

        public string DBGenerator(int recordNom, string deleteOldRecords, int differenceRate)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                    salesPersons.Add(GetSampleName(db, rand));

                if (deleteOldRecords == "y" || deleteOldRecords == "Y")
                {
                    db.BellSources.RemoveRange(db.BellSources);
                    db.StaplesSources.RemoveRange(db.StaplesSources);
                    db.SaveChanges();
                }

                for (int i = 1; i < recordNom; i += 1)
                {
                    var randNom = rand.Next(0, 100);
                    if (randNom <= 3 * differenceRate) // has difference
                    {
                        if (randNom <= differenceRate) //only bell
                        {
                            if (rand.Next(0, 100) <= 50) //wireless LOB
                            {
                                if (rand.Next(0, 100) <= 50) //wireless sublob - mobile with line
                                {
                                    var bellsource = GetBellSource(id: i, wirelessLob: true, wirelessSubLob: true);

                                    var bellSource2 = bellsource.Adapt<BellSource>();
                                    var bellSource3 = bellsource.Adapt<BellSource>();

                                    bellSource2.RebateType = RebateTypes[1];
                                    bellSource3.RebateType = RebateTypes[2];

                                    bellSource2.Phone = bellsource.Phone;
                                    bellSource3.Phone = bellsource.Phone;

                                    bellSource2.Imei = bellsource.Imei;
                                    bellSource3.Imei = bellsource.Imei;

                                    bellSource2.Amount = rand.Next(100, 1500);
                                    bellSource3.Amount = rand.Next(100, 1500);

                                    bellSource2.Id = i + 1;
                                    bellSource3.Id = i + 2;

                                    bellBuld.Add(bellsource);
                                    bellBuld.Add(bellSource2);
                                    bellBuld.Add(bellSource3);

                                    i += 2;
                                }
                                else//lob:wireliss - sublub:non-wireless
                                {
                                    var bellsource = GetBellSource(id: i, wirelessLob: true, wirelessSubLob: false);
                                    bellBuld.Add(bellsource);
                                }
                            }
                            else//Wired lob
                            {
                                var bellsource = GetBellSource(id: i, wirelessLob: false);
                                bellBuld.Add(bellsource);
                            }
                        }
                        else if (randNom <= 2 * differenceRate) // only in staple
                        {
                            if (rand.Next(0, 100) <= 50) //wireless LOB
                            {
                                if (rand.Next(0, 100) <= 50) //wireless sublob - mobile with line
                                {
                                    var staplesSource = GetStapleSource(id: i, wirelessLob: true, wirelessSubLob: true);

                                    var staplesSource2 = staplesSource.Adapt<StaplesSource>();
                                    var staplesSource3 = staplesSource.Adapt<StaplesSource>();

                                    staplesSource2.RebateType = RebateTypes[1];
                                    staplesSource3.RebateType = RebateTypes[2];

                                    staplesSource2.Phone = staplesSource.Phone;
                                    staplesSource3.Phone = staplesSource.Phone;

                                    staplesSource2.Imei = staplesSource.Imei;
                                    staplesSource3.Imei = staplesSource.Imei;

                                    staplesSource2.Amount = rand.Next(100, 1500);
                                    staplesSource3.Amount = rand.Next(100, 1500);

                                    staplesSource2.Id = i + 1;
                                    staplesSource3.Id = i + 2;

                                    stapleBuld.Add(staplesSource);
                                    stapleBuld.Add(staplesSource2);
                                    stapleBuld.Add(staplesSource3);

                                    i += 2;
                                }
                                else //lob:wireliss - sublub:non-wireless
                                {
                                    var stapeSource = GetStapleSource(id: i, wirelessLob: true, wirelessSubLob: false);

                                    stapleBuld.Add(stapeSource);
                                }
                            }
                            else//Wired lob
                            {
                                var stapeSource = GetStapleSource(id: i, wirelessLob: false);

                                stapleBuld.Add(stapeSource);
                            }
                        }
                        else //if(randNom <= 3*DifferenceRate) //both with difference
                        {
                            if (rand.Next(0, 100) <= 50) //wireless LOB
                            {
                                if (rand.Next(0, 100) <= 50) //wireless sublob - mobile with line
                                {
                                    #region staple

                                    var staplesSource = GetStapleSource(id: i, wirelessLob: true, wirelessSubLob: true);

                                    var staplesSource2 = staplesSource.Adapt<StaplesSource>();
                                    var staplesSource3 = staplesSource.Adapt<StaplesSource>();

                                    staplesSource2.RebateType = RebateTypes[1];
                                    staplesSource3.RebateType = RebateTypes[2];

                                    staplesSource2.Phone = staplesSource.Phone;
                                    staplesSource3.Phone = staplesSource.Phone;

                                    staplesSource2.Imei = staplesSource.Imei;
                                    staplesSource3.Imei = staplesSource.Imei;

                                    staplesSource2.Amount = rand.Next(100, 1500);
                                    staplesSource3.Amount = rand.Next(100, 1500);

                                    staplesSource2.Id = i + 1;
                                    staplesSource3.Id = i + 2;

                                    #endregion staple

                                    #region bell

                                    var bellsource = GetBellSource(id: i, wirelessLob: true, wirelessSubLob: true);

                                    var bellSource2 = bellsource.Adapt<BellSource>();
                                    var bellSource3 = bellsource.Adapt<BellSource>();

                                    bellSource2.RebateType = RebateTypes[1];
                                    bellSource3.RebateType = RebateTypes[2];

                                    bellSource2.Phone = bellsource.Phone;
                                    bellSource3.Phone = bellsource.Phone;

                                    bellSource2.Imei = bellsource.Imei;
                                    bellSource3.Imei = bellsource.Imei;

                                    bellSource2.Amount = rand.Next(100, 1500);
                                    bellSource3.Amount = rand.Next(100, 1500);

                                    bellSource2.Id = i + 1;
                                    bellSource3.Id = i + 2;

                                    #endregion bell

                                    MakeSameThenDifferentObjects(ref bellsource, ref staplesSource);
                                    MakeSameThenDifferentObjects(ref bellSource2, ref staplesSource2);
                                    MakeSameThenDifferentObjects(ref bellSource3, ref staplesSource3);

                                    stapleBuld.Add(staplesSource);
                                    stapleBuld.Add(staplesSource2);
                                    stapleBuld.Add(staplesSource3);

                                    bellBuld.Add(bellsource);
                                    bellBuld.Add(bellSource2);
                                    bellBuld.Add(bellSource3);
                                    i += 2;
                                }
                                else //lob:wireliss - sublub:non-wireless
                                {
                                    var stapeSource = GetStapleSource(id: i, wirelessLob: true, wirelessSubLob: false);
                                    var bellSource = GetBellSource(id: i, wirelessLob: true, wirelessSubLob: false);
                                    MakeSameThenDifferentObjects(ref bellSource, ref stapeSource);

                                    stapleBuld.Add(stapeSource);
                                    bellBuld.Add(bellSource);
                                }
                            }
                            else//Wired lob
                            {
                                var stapeSource = GetStapleSource(id: i, wirelessLob: false);
                                var bellSource = GetBellSource(id: i, wirelessLob: false);
                                MakeSameThenDifferentObjects(ref bellSource, ref stapeSource);

                                stapleBuld.Add(stapeSource);
                                bellBuld.Add(bellSource);
                            }
                        }
                    }
                    else //nodifference
                    {
                        if (rand.Next(0, 100) <= 50) //wireless LOB
                        {
                            if (rand.Next(0, 100) <= 50) //wireless sublob - mobile with line
                            {
                                #region staple

                                var staplesSource = GetStapleSource(id: i, wirelessLob: true, wirelessSubLob: true);

                                var staplesSource2 = staplesSource.Adapt<StaplesSource>();
                                var staplesSource3 = staplesSource.Adapt<StaplesSource>();

                                staplesSource2.RebateType = RebateTypes[1];
                                staplesSource3.RebateType = RebateTypes[2];

                                staplesSource2.Phone = staplesSource.Phone;
                                staplesSource3.Phone = staplesSource.Phone;

                                staplesSource2.Imei = staplesSource.Imei;
                                staplesSource3.Imei = staplesSource.Imei;

                                staplesSource2.Amount = rand.Next(100, 1500);
                                staplesSource3.Amount = rand.Next(100, 1500);

                                staplesSource2.Id = i + 1;
                                staplesSource3.Id = i + 2;

                                #endregion staple

                                #region bell

                                var bellsource = GetBellSource(id: i, wirelessLob: true, wirelessSubLob: true);

                                var bellSource2 = bellsource.Adapt<BellSource>();
                                var bellSource3 = bellsource.Adapt<BellSource>();

                                bellSource2.RebateType = RebateTypes[1];
                                bellSource3.RebateType = RebateTypes[2];

                                bellSource2.Phone = bellsource.Phone;
                                bellSource3.Phone = bellsource.Phone;

                                bellSource2.Imei = bellsource.Imei;
                                bellSource3.Imei = bellsource.Imei;

                                bellSource2.Amount = rand.Next(100, 1500);
                                bellSource3.Amount = rand.Next(100, 1500);

                                bellSource2.Id = i + 1;
                                bellSource3.Id = i + 2;

                                #endregion bell

                                i += 2;

                                MakeSameObjects(ref bellsource, ref staplesSource);
                                MakeSameObjects(ref bellSource2, ref staplesSource2);
                                MakeSameObjects(ref bellSource3, ref staplesSource3);

                                stapleBuld.Add(staplesSource);
                                stapleBuld.Add(staplesSource2);
                                stapleBuld.Add(staplesSource3);

                                bellBuld.Add(bellsource);
                                bellBuld.Add(bellSource2);
                                bellBuld.Add(bellSource3);
                            }
                            else //lob:wireliss - sublub:non-wireless
                            {
                                var stapeSource = GetStapleSource(id: i, wirelessLob: true, wirelessSubLob: false);
                                var bellSource = GetBellSource(id: i, wirelessLob: true, wirelessSubLob: false);
                                MakeSameObjects(ref bellSource, ref stapeSource);

                                stapleBuld.Add(stapeSource);
                                bellBuld.Add(bellSource);
                            }
                        }
                        else//Wired lob
                        {
                            var stapeSource = GetStapleSource(id: i, wirelessLob: false);
                            var bellSource = GetBellSource(id: i, wirelessLob: false);
                            MakeSameObjects(ref bellSource, ref stapeSource);

                            stapleBuld.Add(stapeSource);
                            bellBuld.Add(bellSource);
                        }
                    }

                    if (bellBuld.Count + stapleBuld.Count > 990 || i >= recordNom - 20)
                    {
                        db.StaplesSources.AddRange(stapleBuld);
                        stapleBuld = new List<StaplesSource>();

                        db.BellSources.AddRange(bellBuld);
                        bellBuld = new List<BellSource>();

                        db.SaveChanges();
                    }
                }
                return "Done";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetSampleName(BellRecContext db, Random rand)
       // private string GetSampleName(StapleContext db, Random rand)
        {
            var count = db.SampleNames.Count();
            var inx = rand.Next(0, count - 2);
            var firstname = db.SampleNames.OrderBy(c => c.FirstName).Skip(inx).Take(1).First().FirstName;

            inx = rand.Next(0, count - 2);
            var lastname = db.SampleNames.OrderBy(c => c.FirstName).Skip(inx).Take(1).First().LastName;

            return firstname + " " + lastname;
        }

        private BellSource GetBellSource(int id, bool wirelessLob = false, bool wirelessSubLob = false)
        {
            BellSource bellSource = new BellSource();

            bellSource.Id = id;
            bellSource.Phone = 0;// rand.NextInt64(11234567890, 99999999999);
            bellSource.Amount = rand.Next(-500, 1500);
            bellSource.Comment = string.Empty;
            // bellSource.CommissionDetails = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            bellSource.CustomerName = GetSampleName(db, rand);
            bellSource.OrderNumber = rand.NextInt64(11234567890, 99999999999);
            bellSource.TransactionDate = DateTime.Now.AddDays(new Random().Next(-1000, 0));
            bellSource.Reconciled = "False";

            if (wirelessLob == false)
            {
                bellSource.Lob = Lobs[1];
                bellSource.RebateType = RebateTypes[0];
                bellSource.SubLob = WiredSubLOBs[rand.Next(WiredSubLOBs.Length - 1)];
            }
            else
            {
                bellSource.Lob = Lobs[0];
                if (wirelessSubLob == false)
                {
                    bellSource.RebateType = RebateTypes[0];
                    bellSource.SubLob = WirelessSubLOBs[rand.Next(1, 2)];
                }
                else // WirelessLob is true and WirelessSubLob is true too
                {
                    bellSource.SubLob = WirelessSubLOBs[0];
                    bellSource.RebateType = RebateTypes[0];
                    bellSource.Phone = rand.NextInt64(11234567890, 99999999999);
                    bellSource.Imei = rand.NextInt64(1234567890, 9876543210);
                    bellSource.Amount = rand.Next(100, 1500);
                }
            }

            return bellSource;
        }

        private StaplesSource GetStapleSource(int id, bool wirelessLob = false, bool wirelessSubLob = false)
        {
            var staplesSource = new StaplesSource();

            staplesSource.Phone = 0;// bellSource.Phone;

            staplesSource.Id = id;
            staplesSource.Comment = string.Empty;
            staplesSource.Brand = Brands[rand.Next(0, Brands.Length - 1)];
            staplesSource.DeviceCo = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            staplesSource.Location = Locations[rand.Next(0, Locations.Length - 1)];
            staplesSource.Msf = rand.Next(5, 1400);
            staplesSource.Product = Products[rand.Next(0, Products.Length - 1)];
            staplesSource.Rec = "FALSE";
            staplesSource.SalesPerson = salesPersons[rand.Next(0, salesPersons.Count - 1)];
            staplesSource.TaxCode = rand.NextInt64(11234567890, 99999999999);
            staplesSource.TransactionDate = DateTime.Now.AddDays(rand.Next(-1000, 0));
            staplesSource.CustomerName = GetSampleName(db, rand);
            staplesSource.Amount = rand.Next(-500, 1500);
            staplesSource.OrderNumber = rand.NextInt64(11234567890, 99999999999);
            staplesSource.Reconciled = "FALSE";

            if (wirelessLob == false)
            {
                staplesSource.Lob = Lobs[1];
                staplesSource.RebateType = RebateTypes[0];

                var r = rand.Next(1, 2);
                staplesSource.SubLob = WirelessSubLOBs[r];
            }
            else
            {
                staplesSource.Lob = Lobs[0];
                if (wirelessSubLob == false)
                {
                    staplesSource.RebateType = RebateTypes[0];
                    var r = rand.Next(1, 2);
                    staplesSource.SubLob = WirelessSubLOBs[r];
                }
                else // WirelessLob is true and WirelessSubLob is true too
                {
                    staplesSource.SubLob = WirelessSubLOBs[0];
                    staplesSource.RebateType = RebateTypes[0];
                    staplesSource.Phone = rand.NextInt64(11234567890, 99999999999);
                    staplesSource.Imei = rand.NextInt64(1234567890, 9876543210);
                    staplesSource.Amount = rand.Next(100, 1500);
                }
            }

            return staplesSource;
        }

        private void MakeSameObjects(ref BellSource bell,ref StaplesSource staple)
        {
            var reservedId = bell.Id;
            bell = staple.Adapt<BellSource>();
            bell.Id = reservedId;
        }

        private void MakeSameThenDifferentObjects(ref BellSource bell,ref StaplesSource staple)
        {
            var reservedId = bell.Id;
            bell = staple.Adapt<BellSource>();
            bell.Id = reservedId;

            var rnd = rand.Next(100);
            if (rnd < 100)
                bell.Amount = bell.Amount + rnd;

            if (bell.Imei != null )
            {
                if (rnd < 30)
                    bell.Imei = rand.NextInt64(1234567890, 9876543210);
                else if (rnd > 80)
                    bell.Phone = rand.NextInt64(11234567890, 99999999999);
            }

            if (rnd < 25)
                bell.CustomerName = GetSampleName(db, rand);
        }
    }
}