using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Split_Trays.Classes;

namespace Split_Trays
{
    public partial class MainWindow : Window
    {
        private void Timer_Tick3(object sender, EventArgs e)
        {
            if (t3.IsCompleted)
            {

                Timer3.Stop();
                if (MergeList.Count > 0)
                {
                    TotalSkidsTxt3.Content = cnt.ToString();
                    OptionalMrgesTxt3.Content = tmerges.ToString();
                    WHTxt3.Text = WH;
                    PLTxt3.Content = PL;
                    Grid3.ItemsSource = MergeList;
                    PBar3.Visibility = Visibility.Collapsed;
                    StartBtn3.IsEnabled = true;
                    Grid3.Visibility = Visibility.Visible;
                    if (stat)
                    {
                        TraceTxt3.Content = "Yes";
                        SortByPLTxt3.Content = "MFG Lot";
                    }
                    else
                    {
                        TraceTxt3.Content = "No";
                        SortByPLTxt3.Content = "Date Code";
                    }
                }
                else
                {
                    PBar3.Visibility = Visibility.Collapsed;
                    StartBtn3.IsEnabled = true;
                    Grid3.ItemsSource = null;
                    Grid3.Visibility = Visibility.Hidden;
                    //MessageBox.Show("No Items", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Go_Click3(object sender, RoutedEventArgs e)
        {
            Grid3.Visibility = Visibility.Hidden;
            CheckMerges();
        }

        private void WH_KeyDown3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Grid3.Visibility = Visibility.Hidden;
                CheckMerges();
            }
        }

        private void CheckMerges()
        {
            WH = WHTxt3.Text.ToUpper();
            PL = "";
            if (WH != "")
            {
                PBar3.Visibility = Visibility.Visible;
                StartBtn3.IsEnabled = false;
                Grid3.ItemsSource = null;
                cnt = 0;
                tmerges = 1;
                MergeList.Clear();

                try
                {
                    t3 = Task.Factory.StartNew(() =>
                    {
                        PL = GetPL(WH);
                        if (PL == "") return;

                        CheckTraceability(WH);
                        GetRegularData();
                    });
                    Timer3.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public void GetRegularData()
        {
            int mergID = 1;
            string key = "";
            string bcolor = "White";

            try
            {
                SQLClass sql = new SQLClass("172.20.20.2", "SiplacePro", "sa", "$Sq2010");
                string sqlStr = string.Format(@"SELECT 
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
                                           ttiitm001400.t_dscc
                                           FROM baandb.ttdilc101400 ttdilc101400 
                                           LEFT JOIN baandb.ttdltc001400 ttdltc001400 ON ttdltc001400.t_clot = ttdilc101400.t_clot
                                           LEFT JOIN baandb.ttdinv001400 ttdinv001400 on ttdinv001400.t_cwar = ttdilc101400.t_cwar and ttdinv001400.t_item = ttdilc101400.t_item
                                           LEFT JOIN baandb.ttiitm001400 ttiitm001400 ON ttiitm001400.t_item = ttdilc101400.t_item
                                           LEFT JOIN baandb.ttcmcs950400 ttcmcs950400 ON ttcmcs950400.t_mnum = ttdltc001400.t_mnum
                                           WHERE ttdilc101400.t_cwar='{0}' AND ttdltc001400.t_pack_c='4'
                                            ORDER BY ttdilc101400.t_item, ttdltc001400.t_daco, ttdltc001400.t_mitm, ttdltc001400.t_lsup, ttdilc101400.t_strs ASC", WH);
                MergeList = sql.SelectBaanData(sqlStr);

                foreach (var r in MergeList)
                {
                    cnt++;
                    r.ID = mergID.ToString().Trim();
                    r.WH = WH;
                    r.Tray = GetTray(r.Item);
                    r.TrayQty = GetTrayQty(r.Tray);
                    r.Location = GetLocation(r.Tray);
                    string TempKey = stat == false ? r.Item + r.DateCode + r.MPN : r.Item + r.DateCode + r.MPN + r.MLot;

                    if (key != "")
                    {
                        if (key != TempKey)
                        {
                            bcolor = bcolor == "White" ? "LightGray" : "White";
                            tmerges++;
                            mergID++;
                            r.ID = mergID.ToString();
                        }
                    }
                    r.RowColor = bcolor;
                    key = TempKey;
                }

                foreach (var item in MergeList)
                {
                    List<Row> TempList = MergeList.Where(x => x.ID == item.ID).OrderBy(y => y.Qty).ToList();
                    if (TempList.Count >= 2)
                    {
                        int TotalSum = TempList[0].Qty;
                        for (int i = 0; i < TempList.Count - 1; i++)
                        {
                            if (TotalSum <= TempList[i].TrayQty * 0.25)
                            {
                                if (TotalSum + TempList[i + 1].Qty <= TempList[i + 1].TrayQty)
                                {
                                    MergeList.FirstOrDefault(x => x.Lot == TempList[i].Lot).Remarks = "Merge";
                                    MergeList.FirstOrDefault(x => x.Lot == TempList[i + 1].Lot).Remarks = "Merge";
                                    TotalSum += TempList[i].Qty + TempList[i + 1].Qty;
                                    continue;
                                }
                            }
                        }
                    }

                }

                //for (int i = 0; i < MergeList.Count; i++)
                //{
                //    if (!MergeList.Any(x => x.Lot != MergeList[i].Lot && x.ID == MergeList[i].ID)) //Remove Single Reels
                //    {
                //        MergeList.RemoveAt(i);
                //        i--;
                //    }
                //}


                //After Remove Reels, Orgenize Again.
                key = "";
                bcolor = "White";
                cnt = 0;
                tmerges = 1;
                mergID = 1;
                foreach (var r in MergeList)
                {
                    cnt++;
                    r.ID = mergID.ToString().Trim();
                    string TempKey = stat == false ? r.Item + r.DateCode + r.MPN : r.Item + r.DateCode + r.MPN + r.MLot;

                    if (key != "")
                    {
                        if (key != TempKey)
                        {
                            bcolor = bcolor == "White" ? "LightGray" : "White";
                            tmerges++;
                            mergID++;
                            r.ID = mergID.ToString();
                        }
                    }
                    r.RowColor = bcolor;
                    key = TempKey;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
