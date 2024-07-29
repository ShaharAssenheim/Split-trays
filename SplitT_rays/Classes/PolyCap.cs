using Split_Trays.Classes;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Split_Trays
{
    public partial class MainWindow : Window
    {
        private void Timer_Tick2(object sender, EventArgs e)
        {
            if (t2.IsCompleted)
            {
                Timer2.Stop();
                if (Splitlist.Count > 0)
                {
                    TotalSkidsTxt2.Content = cnt.ToString();
                    Total_SplitsTxt2.Content = (tSplit).ToString();
                    Total_Shortages2.Content = (tShort).ToString();
                    WHTxt2.Text = WH;
                    PLTxt2.Content = PL;
                    Grid2.ItemsSource = Splitlist;
                    PBar2.Visibility = Visibility.Collapsed;
                    StartBtn2.IsEnabled = true;
                    if (stat)
                        TraceTxt2.Content = "Yes";
                    else
                        TraceTxt2.Content = "No";

                    MenuItems2.Visibility = Visibility.Visible;
                    Grid2.Visibility = Visibility.Visible;
                }
                else
                {
                    PBar2.Visibility = Visibility.Collapsed;
                    StartBtn2.IsEnabled = true;
                    Grid2.ItemsSource = null;
                    Grid2.Visibility = Visibility.Hidden;
                    MessageBox.Show("No Items", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void StartBtn_Click2(object sender, RoutedEventArgs e)
        {
            MenuItems2.Visibility = Visibility.Hidden;
            Grid2.Visibility = Visibility.Hidden;
            CheckSplits2();
        }


        private void WH_KeyDown2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MenuItems2.Visibility = Visibility.Hidden;
                Grid2.Visibility = Visibility.Hidden;
                CheckSplits2();
            }
        }

        private void CheckSplits2()
        {
            WH = WHTxt2.Text.ToUpper();
            PL = "";
            if (WH != "")
            {
                PBar2.Visibility = Visibility.Visible;
                StartBtn2.IsEnabled = false;
                Grid2.ItemsSource = null;
                tSplit = 0;
                tShort = 0;
                cnt = 0;
                Splitlist.Clear();
                try
                {
                    t2 = Task.Factory.StartNew(() =>
                    {
                        PL = GetPL(WH);
                        if (PL == "") return;

                        CheckTraceability(WH);
                        GetData2();
                    });
                    Timer2.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void GetData2()
        {
            int mergID = 1;
            string key = "";
            string bcolor = "White";
            int i = 0;
            SQLClass sql = new SQLClass("172.20.20.2", "SiplacePro", "sa", "$Sq2010");
            try
            {
                string q = string.Format(@"SELECT
                                           ttdilc101400.t_clot,
                                           ttdilc101400.t_item,
                                           ttiitm001400.t_copr,
                                           ttdltc001400.t_mnum,
                                           ttdltc001400.t_mitm,
                                           ttdltc001400.t_daco,
                                           ttdltc001400.t_lsup,
                                           ttdltc001400.t_oudt,
                                           ttdltc001400.t_pack_c,
                                           ttdilc101400.t_strs,
                                           ttdinv001400.t_allo,
                                           ttcmcs950400.t_dsca,
                                           ''
                                           FROM baandb.ttdilc101400 ttdilc101400
                                           LEFT JOIN baandb.ttdltc001400 ttdltc001400 ON ttdltc001400.t_clot = ttdilc101400.t_clot
                                           LEFT JOIN baandb.ttdinv001400 ttdinv001400 on ttdinv001400.t_cwar = ttdilc101400.t_cwar and ttdinv001400.t_item = ttdilc101400.t_item
                                           LEFT JOIN baandb.ttiitm001400 ttiitm001400 ON ttiitm001400.t_item = ttdilc101400.t_item
                                           LEFT JOIN baandb.ttcmcs950400 ttcmcs950400 ON ttcmcs950400.t_mnum = ttdltc001400.t_mnum
                                           WHERE ttdilc101400.t_cwar= '{0}'
                                           AND ttdilc101400.t_item IN ('          126506','          126816','          128076','          128472',
                                           '     MLCAP000128','     MLCAP000129','     MLCAP000250','     MLCAP000251','     MLCAP000288','     MLCAP000511',
                                           '     MLCAP000576','     MLCAP000785','     MLCAP001472','     MLCAP001478', '          126049', 
                                           '     MLCAP001954', '     MLCAP001956', '     MLCAP001958', '     MLCAP002008')
                                           ORDER BY ttdilc101400.t_item, ttdltc001400.t_daco, ttdilc101400.t_strs ASC", WH);
                Splitlist = sql.SelectBaanData(q);

                foreach (var r in Splitlist)
                {
                    cnt++;
                    r.ID = mergID.ToString().Trim();
                    r.WH = WH;
                    r.Tray = GetTray(r.Item);
                    r.TrayQty = GetTrayQty(r.Tray);
                    r.Location = GetLocation(r.Tray);
                    if (r.Lot == "2245013240")
                        r.Lot = "2245013240";
                    if (key != "")
                    {
                        if (key != r.Item)
                        {
                            mergID++;
                            r.ID = mergID.ToString();
                        }
                        else
                        {
                            if (Convert.ToInt32(Splitlist[i - 1].LineDemand_Poly) - Convert.ToInt32(Splitlist[i - 1].Qty) > 0)
                            {
                                r.LineDemand_Poly = (Convert.ToInt32(Splitlist[i - 1].LineDemand_Poly) - Convert.ToInt32(Splitlist[i - 1].Qty)).ToString();
                                r.LineDemand = (Convert.ToInt32(Splitlist[i - 1].LineDemand) - Convert.ToInt32(Splitlist[i - 1].Qty)).ToString();
                            }
                            else
                            {
                                r.LineDemand_Poly = r.Qty.ToString();
                                r.LineDemand = r.Qty.ToString();
                            }
                        }
                    }
                    r.RowColor = bcolor;
                    key = r.Item;
                    i++;
                }

                List<string> RemoveList = new List<string>();

                for (i = 0; i < Splitlist.Count - 1; i++)//set Excess to the last skid in any group.
                {
                    int n = Convert.ToInt32(Splitlist[i].Qty) - Convert.ToInt32(Splitlist[i].LineDemand_Poly);
                    int n2 = Convert.ToInt32(Splitlist[i].Qty) - Convert.ToInt32(Splitlist[i].LineDemand);
                    Splitlist[i].Excess = n.ToString();
                    Splitlist[i].RealExcess = n2.ToString();

                    if (Splitlist[i].ID == Splitlist[i + 1].ID)
                    {
                        Splitlist[i].Location = "";
                        Splitlist[i].Tray = "";
                    }
                    else
                    {
                        if (n2 >= 100)
                        {
                            tSplit++;
                            Splitlist[i].Remarks = "Split(" + Splitlist[i].LineDemand_Poly + ")";
                            Splitlist[i].RowColor = "LightGoldenrodYellow";
                        }
                        else if (n2 >= 0 && n2 < 100)
                        {
                            tShort++;
                            Splitlist[i].Remarks = "Done";
                            Splitlist[i].RowColor = "LightGreen";
                        }
                        else if (n2 < 0)
                        {
                            tShort++;
                            Splitlist[i].Remarks = "Shortage";
                            Splitlist[i].RowColor = "Salmon";
                        }
                    }

                    if (!Splitlist.Exists(x => x.Customer == "PANASONIC" && x.ID == Splitlist[i].ID))
                        RemoveList.Add(Splitlist[i].ID);


                    if (i + 1 == Splitlist.Count - 1)
                    {
                        n = Convert.ToInt32(Splitlist[i + 1].Qty) - Convert.ToInt32(Splitlist[i + 1].LineDemand_Poly);
                        n2 = Convert.ToInt32(Splitlist[i + 1].Qty) - Convert.ToInt32(Splitlist[i + 1].LineDemand);
                        Splitlist[i + 1].Excess = n.ToString();
                        Splitlist[i + 1].RealExcess = n2.ToString();

                        if (n2 >= 100)
                        {
                            tSplit++;
                            //Splitlist[i + 1].Remarks = "Split(" + (Convert.ToInt32(Splitlist[i + 1].Qty) + n).ToString() + ")";
                            Splitlist[i + 1].Remarks = "Split(" + Splitlist[i + 1].LineDemand_Poly + ")";
                            Splitlist[i + 1].RowColor = "LightGoldenrodYellow";
                        }
                        else if (n2 >= 0 && n2 < 100)
                        {
                            tShort++;
                            Splitlist[i + 1].Remarks = "Done";
                            Splitlist[i + 1].RowColor = "LightGreen";
                        }
                        else if (n2 < 0)
                        {
                            tShort++;
                            Splitlist[i + 1].Remarks = "Shortage";
                            Splitlist[i + 1].RowColor = "Salmon";
                        }

                        if (!Splitlist.Exists(x => x.Customer == "PANASONIC" && x.ID == Splitlist[i + 1].ID))
                            RemoveList.Add(Splitlist[i + 1].ID);
                    }
                }

                foreach (var id in RemoveList)
                {
                    Splitlist.RemoveAll(x => x.ID == id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
