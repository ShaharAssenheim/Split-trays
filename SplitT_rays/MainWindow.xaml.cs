using Split_Trays.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Split_Trays
{
    public partial class MainWindow : Window
    {
        List<Row> Alllist = new List<Row>();
        List<Row> Splitlist = new List<Row>();
        List<Row> PBRSplitlist = new List<Row>();
        List<Row> MergeList = new List<Row>();
        public DispatcherTimer Timer = new DispatcherTimer();
        public DispatcherTimer Timer2 = new DispatcherTimer();
        public DispatcherTimer Timer3 = new DispatcherTimer();
        Task t;
        Task t2;
        Task t3;
        int tSplit = 0;
        int tShort = 0;
        int cnt = 0;
        string WH = "";
        string PL = "";
        bool stat = false;
        int tmerges = 1;
        bool PBRNvidia = false;

        public MainWindow()
        {
            InitializeComponent();
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer2.Tick += Timer_Tick2;
            Timer2.Interval = new TimeSpan(0, 0, 1);
            Timer3.Tick += Timer_Tick3;
            Timer3.Interval = new TimeSpan(0, 0, 1);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (t.IsCompleted)
            {
                Timer.Stop();
                if (Splitlist.Count > 0)
                {
                    TotalSkidsTxt.Content = cnt.ToString();
                    Total_SplitsTxt.Content = (tSplit).ToString();
                    Total_Shortages.Content = (tShort).ToString();
                    WHTxt.Text = WH;
                    PLTxt.Content = PL;
                    Grid1.ItemsSource = Splitlist;
                    Grid4.ItemsSource = PBRSplitlist;
                    PBar.Visibility = Visibility.Collapsed;
                    StartBtn.IsEnabled = true;
                    if (stat)
                        TraceTxt.Content = "Yes";
                    else
                        TraceTxt.Content = "No";

                    MenuItems.Visibility = Visibility.Visible;
                    Grid1.Visibility = Visibility.Visible;
                }
                else
                {
                    PBar.Visibility = Visibility.Collapsed;
                    StartBtn.IsEnabled = true;
                    Grid1.ItemsSource = null;
                    Grid1.Visibility = Visibility.Hidden;
                    MessageBox.Show("No Items", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            MenuItems.Visibility = Visibility.Hidden;
            Grid1.Visibility = Visibility.Hidden;
            StickBox.Visibility = Visibility.Hidden;
            CheckSplits();
            CheckMerges();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void WH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MenuItems.Visibility = Visibility.Hidden;
                Grid1.Visibility = Visibility.Hidden;
                StickBox.Visibility = Visibility.Hidden;
                CheckSplits();
                CheckMerges();
            }
        }

        private void CheckSplits()
        {
            WH = WHTxt.Text.ToUpper();
            WHTxt3.Text = WHTxt.Text.ToUpper();
            WHTxt4.Text = WHTxt.Text.ToUpper();

            PL = "";
            if (WH != "")
            {
                PBar.Visibility = Visibility.Visible;
                StartBtn.IsEnabled = false;
                Grid1.ItemsSource = null;
                tSplit = 0;
                tShort = 0;
                cnt = 0;
                Splitlist.Clear();
                Alllist.Clear();
                try
                {
                    t = Task.Factory.StartNew(() =>
                    {
                        PL = GetPL(WH);
                        if (PL == "") return;

                        CheckTraceability(WH);
                        GetData();
                    });
                    Timer.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void GetData()
        {
            int mergID = 1;
            string key = "";
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
                                           ttiitm001400.t_dscc
                                           FROM baandb.ttdilc101400 ttdilc101400 
                                           LEFT JOIN baandb.ttdltc001400 ttdltc001400 ON ttdltc001400.t_clot = ttdilc101400.t_clot
                                           LEFT JOIN baandb.ttdinv001400 ttdinv001400 on ttdinv001400.t_cwar = ttdilc101400.t_cwar and ttdinv001400.t_item = ttdilc101400.t_item
                                           LEFT JOIN baandb.ttiitm001400 ttiitm001400 ON ttiitm001400.t_item = ttdilc101400.t_item
                                           LEFT JOIN baandb.ttcmcs950400 ttcmcs950400 ON ttcmcs950400.t_mnum = ttdltc001400.t_mnum
                                           WHERE ttdilc101400.t_cwar='{0}' AND ttdltc001400.t_pack_c NOT IN (20,19,14) AND  ttiitm001400.t_csgs NOT IN (140003, 500000)
                                           ORDER BY ttdilc101400.t_item, ttdltc001400.t_daco, ttdilc101400.t_strs ASC", WH);
                Alllist = sql.SelectBaanData(q);
                Splitlist = Alllist.Where(x => (x.Size != "1" && x.Size != "2" && x.Size != "3" && x.Size != "18") || x.Item == "MLELE000072").ToList();
                Alllist = Alllist.Where(x => (x.Size == "1" || x.Size == "2" || x.Size == "3" || x.Size == "18") && Splitlist.Exists(y => y.Item == x.Item && y.Lot != x.Lot)).ToList();

                Splitlist.AddRange(Alllist);
                Splitlist = Splitlist.OrderBy(x => x.Item).ThenBy(x => x.Year).ThenBy(x => x.Month).ThenBy(x => x.Qty).ThenBy(x => x.Lot).ToList();

                foreach (var r in Splitlist)
                {
                    cnt++;
                    r.ID = mergID.ToString().Trim();
                    r.WH = WH;
                    r.Tray = GetTray(r.Item);
                    r.TrayQty = GetTrayQty(r.Tray);
                    r.Location = GetLocation(r.Tray);
                    Get_Shape(r.Tray, out var Shape);
                    r.Shape = Shape;
                    if (r.Description == "BGA" && r.Customer.ToLower().StartsWith("mell"))
                    {
                        r.Description = Check_PbrDemand(PL, r.Item);
                        r.Last_Location = GetLastLoacation(PL, r.Item, r.Lot);
                        PBRNvidia = true;
                    }

                    else r.Description = "";

                    if (key != "")
                    {
                        if (key != r.Item)
                        {
                            mergID++;
                            r.ID = mergID.ToString();
                        }
                        else
                        {
                            r.LineDemand = (Convert.ToInt32(Splitlist[i - 1].LineDemand) - Splitlist[i - 1].Qty).ToString();
                        }
                    }
                    key = r.Item;
                    i++;
                }

                if (PBRNvidia)
                {
                    CalculatePBRNvidia();
                }

                for (i = 0; i < Splitlist.Count; i++)//set Excess to the last skid in any group.
                {
                    if (i + 1 < Splitlist.Count && Splitlist[i].ID == Splitlist[i + 1].ID)
                    {
                        Splitlist[i].Excess = "";
                        Splitlist[i].Location = "";
                        Splitlist[i].Tray = "";
                    }
                    else
                    {
                        int n = Splitlist[i].Qty - Convert.ToInt32(Splitlist[i].LineDemand);
                        Splitlist[i].Excess = n.ToString();

                        if (Splitlist[i].KitDemand == "0")
                        {
                            Splitlist[i].Remarks = "Check Allocation";
                            Splitlist[i].RowColor = "LightGoldenrodYellow";
                        }
                        else if (n > 0)
                        {
                            tSplit++;
                            Splitlist[i].Remarks = "Split(" + Splitlist[i].LineDemand + ")";
                            Splitlist[i].RowColor = "LightGoldenrodYellow";
                        }
                        else if (n < 0)
                        {
                            tShort++;
                            Splitlist[i].Remarks = "Shortage";
                            Splitlist[i].RowColor = "Salmon";
                        }
                        else if (n == 0)
                        {
                            Splitlist[i].Remarks = "Done";
                            Splitlist[i].RowColor = "LightGreen";
                        }

                        if (Splitlist.Any(x => x.ID == Splitlist[i].ID && x.Size != Splitlist[i].Size))
                            Splitlist[i].Remarks = "Double Package";

                    }

                    if (Convert.ToInt32(Splitlist[i].LineDemand) < 0)
                    {
                        Splitlist[i].Remarks = "Excess In PN";
                        Splitlist[i].RowColor = "LightGoldenrodYellow";
                        Splitlist[i].Excess = "";
                    }

                    if (i + 1 == Splitlist.Count - 1)
                    {
                        int n = Splitlist[i + 1].Qty - Convert.ToInt32(Splitlist[i + 1].LineDemand);
                        Splitlist[i + 1].Excess = n.ToString();

                        if (Splitlist[i].KitDemand == "0")
                        {
                            Splitlist[i].Remarks = "Check Allocation";
                            Splitlist[i].RowColor = "LightGoldenrodYellow";
                        }
                        else if (n > 0)
                        {
                            tSplit++;
                            Splitlist[i + 1].Remarks = "Split(" + Splitlist[i + 1].LineDemand + ")";
                            Splitlist[i + 1].RowColor = "LightGoldenrodYellow";
                        }
                        else if (n < 0)
                        {
                            tShort++;
                            Splitlist[i + 1].Remarks = "Shortage";
                            Splitlist[i + 1].RowColor = "Salmon";
                        }
                        else if (n == 0)
                        {
                            Splitlist[i + 1].Remarks = "Done";
                            Splitlist[i + 1].RowColor = "LightGreen";
                        }

                        if (Splitlist.Any(x => x.ID == Splitlist[i + 1].ID && x.Size != Splitlist[i + 1].Size))
                            Splitlist[i + 1].Remarks = "Double Package";

                        if (Convert.ToInt32(Splitlist[i + 1].LineDemand) < 0)
                        {
                            Splitlist[i + 1].Remarks = "Excess In PN";
                            Splitlist[i + 1].RowColor = "LightGoldenrodYellow";
                            Splitlist[i + 1].Excess = "";
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                Application.Current.MainWindow.Height = 770;
                Application.Current.MainWindow.Width = 1320;
            }
        }

        private string GetTray(string PN)
        {
            SQLClass sql = new SQLClass("172.20.20.2", "SiplacePro", "sa", "$Sq2010");
            string Tray;
            string qry = string.Format(@"SELECT Top 1 dbo.AliasName.ObjectName AS PN, AliasName_1.ObjectName AS TRAY
                                        FROM dbo.CComponent INNER JOIN
                                        dbo.AliasName ON dbo.CComponent.OID = dbo.AliasName.PID INNER JOIN
                                        dbo.CComponentShape ON dbo.CComponent.spComponentShapeRef = dbo.CComponentShape.OID INNER JOIN
                                        dbo.AliasName AS AliasName_2 ON dbo.CComponentShape.OID = AliasName_2.PID INNER JOIN
                                        dbo.CFeederOffset ON dbo.CComponent.OID = dbo.CFeederOffset.PID INNER JOIN
                                        dbo.AliasName AS AliasName_1 ON dbo.CFeederOffset.CRTCollection_CReserveType = AliasName_1.PID
                                        WHERE(dbo.AliasName.ObjectName = N'{0}') AND (AliasName_2.IsDefault = 1)", PN);
            Tray = sql.SelectString(qry);
            if (Tray == "")
            {
                qry = string.Format(@"SELECT Top 1 AliasName_2.ObjectName AS PN, dbo.AliasName.ObjectName AS TRAY, dbo.AliasName.FolderID
                                  FROM dbo.AliasName AS AliasName_2 INNER JOIN
                                  dbo.CComponent ON AliasName_2.PID = dbo.CComponent.OID INNER JOIN
                                  dbo.CComponentShape INNER JOIN
                                  dbo.CFeederOffset ON dbo.CComponentShape.OID = dbo.CFeederOffset.PID INNER JOIN
                                  dbo.AliasName ON dbo.CFeederOffset.CRTCollection_CReserveType = dbo.AliasName.PID INNER JOIN
                                  dbo.AliasName AS AliasName_1 ON dbo.CComponentShape.OID = AliasName_1.PID ON dbo.CComponent.spComponentShapeRef = dbo.CComponentShape.OID
                                  WHERE (AliasName_2.ObjectName = N'{0}') AND (AliasName_1.IsDefault = 1)", PN);
                Tray = sql.SelectString(qry);
            }
            return Tray;
        }

        private int GetTrayQty(string Tray)
        {
            SQLClass sql = new SQLClass("172.20.20.2", "SiplacePro", "sa", "$Sq2010");

            string qry = string.Format(@"SELECT TOP 1 AliasName.ObjectName, CReceptacleMatrix.lColumns, CReceptacleMatrix.lRows
                                  FROM dbo.CReceptacleMatrix INNER JOIN
                                  dbo.CTrayReserve INNER JOIN
                                  dbo.CTrayReserveType ON dbo.CTrayReserve.spTypeRef = dbo.CTrayReserveType.OID INNER JOIN
                                  dbo.AliasName ON dbo.CTrayReserveType.OID = dbo.AliasName.PID ON dbo.CReceptacleMatrix.PID = dbo.CTrayReserveType.OID INNER JOIN
                                  dbo.CReceptacle ON dbo.CReceptacleMatrix.OID = dbo.CReceptacle.PID
						          WHERE AliasName.ObjectName='{0}'", Tray);
            int Dem = sql.SelectDemension(qry);
            return Dem;
        }

        private void Get_Shape(string pn, out string Shape)
        {
            Shape = "";
            SQLClass sql = new SQLClass("172.20.20.2", "SiplacePro", "ez", "$Flex2016");

            string qry = string.Format(@"SELECT  dbo.AliasName.ObjectName AS PN, AliasName_1.ObjectName AS Shape, dbo.Root.UserText AS Comment, dbo.CComponentShape.lPitch AS Shape_Pitch
                                         FROM dbo.AliasName INNER JOIN
                                         dbo.CComponent INNER JOIN
                                         dbo.CDIPF INNER JOIN
                                         dbo.CComponentShape ON dbo.CDIPF.PID = dbo.CComponentShape.OID ON dbo.CComponent.spComponentShapeRef = dbo.CComponentShape.OID ON dbo.AliasName.PID = dbo.CComponent.OID INNER JOIN
                                         dbo.Root ON dbo.CComponentShape.OID = dbo.Root.OID LEFT OUTER JOIN
                                         dbo.Root AS Root_1 ON dbo.CComponentShape.spDefaultReserveTypeRef = Root_1.OID LEFT OUTER JOIN
                                         dbo.Root AS Root_2 ON dbo.CComponent.spDefaultReserveTypeRef = Root_2.OID LEFT OUTER JOIN
                                         dbo.AliasName AS AliasName_1 ON dbo.CComponentShape.OID = AliasName_1.PID LEFT OUTER JOIN
                                         dbo.CBodyVCyl ON dbo.CDIPF.OID = dbo.CBodyVCyl.PID LEFT OUTER JOIN
                                         dbo.CBodyHCyl ON dbo.CDIPF.OID = dbo.CBodyHCyl.PID LEFT OUTER JOIN
                                         dbo.CBodyRect ON dbo.CDIPF.OID = dbo.CBodyRect.PID
                                         WHERE AliasName_1.IsDefault = 1 and dbo.AliasName.ObjectName='{0}'", pn);
            DataTable dt = sql.SelectDB(qry);
            if (dt.Rows.Count > 0)
            {
                Shape = dt.Rows[0]["Shape"].ToString().Trim();
            }
        }

        public string GetPL(string wh)
        {
            string pl = "";
            try
            {
                OdbcConnection DbConnection = new OdbcConnection("DSN=Baan");
                DbConnection.Open();
                OdbcCommand DbCommand = DbConnection.CreateCommand();
                DbCommand.CommandTimeout = 180;
                string sqlStr = @"SELECT FIRST 1 tticst910400.t_pino
                                FROM 
                                    baandb.tticst910400 tticst910400
                                    ,baandb.ttisfc010400 ttisfc010400
                                WHERE 
                                    ttisfc010400.t_pdno = tticst910400.t_pdno 
                                    AND ttisfc010400.t_opno = tticst910400.t_opno 
                                    AND ((tticst910400.t_twar='" + wh + @"'))
                                ORDER BY tticst910400.t_rdat DESC";

                DbCommand.CommandText = sqlStr;
                OdbcDataReader DbReader = DbCommand.ExecuteReader();
                if (DbReader.Read())
                {
                    pl = DbReader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return pl;
            }
            return pl;
        }

        private string GetLocation(string Tray)
        {
            SQLClass sql = new SQLClass("MIGSQLCLU4\\SMT", "Tray_Search", "aoi", "$Flex2016");
            string qry = string.Format(@"SELECT Index1, Index2, Index3 FROM Tray_Search WHERE Tray_Name='{0}'", Tray);

            string Loc = sql.SelectLocation(qry);
            return Loc != "" ? Loc : "Not Exist";
        }

        private void CheckTraceability(string wh)
        {
            stat = false;
            try
            {
                OdbcConnection DbConnection = new OdbcConnection("DSN=Baan");
                DbConnection.Open();
                OdbcCommand DbCommand = DbConnection.CreateCommand();
                DbCommand.CommandTimeout = 180;
                string sqlStr = "SELECT FIRST 1 t3.t_fval ";
                sqlStr += "FROM baandb.ttdilc101400 t1 ";
                sqlStr += "LEFT JOIN baandb.ttiitm200400 t2 ON t1.t_item=t2.t_item ";
                sqlStr += "LEFT JOIN baandb.ttdltc605400 t3 ON t2.t_cuno=t3.t_cuno ";
                sqlStr += "WHERE t1.t_cwar=lpad('" + wh + "',3,' ')  ";
                DbCommand.CommandText = sqlStr;
                OdbcDataReader DbReader = DbCommand.ExecuteReader();
                if (DbReader.Read())
                {
                    if (!DbReader.IsDBNull(0))
                    {
                        int v = Convert.ToInt32(DbReader.GetValue(0));
                        if (v == 1)
                        {
                            stat = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MyTab.SelectedIndex == 0)
                Header.Content = "Split Trays";
            else if (MyTab.SelectedIndex == 1)
                Header.Content = "Merge Trays";
            else if (MyTab.SelectedIndex == 2)
                Header.Content = "Poly-Cap";
        }

        private void Grid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Row r = (Row)Grid1.SelectedItem;
            if (r != null)
            {
                StickBox.Visibility = Visibility.Visible;
                StickHeader.Text = r.Item;
                StickQry(r.WH, r.Item);
                GLL_Exist(r.Item, r.Shape);
            }
        }

        private void GLL_Exist(string Item, string Shape)
        {
            SQLClass sql = new SQLClass("MIGSQLCLU4\\SMT", "Warehouse", "aoi", "$Flex2016");
            string qry;
            if (Shape != "")
                qry = string.Format(@"SELECT COUNT(Item) AS Count FROM GLL WHERE Item='{0}' OR Shape='{1}'", Item, Shape);
            else
                qry = string.Format(@"SELECT COUNT(Item) AS Count FROM GLL WHERE Item='{0}'", Item);

            int n = sql.SelectInt(qry);
            if (n > 0)
            {
                Gll_Note.Content = "Reel Exist";
                Gll_Note.Background = Brushes.LightGreen;
            }
            else
            {
                Gll_Note.Content = "No Reel Recorded";
                Gll_Note.Background = Brushes.LightGoldenrodYellow;
            }
            Gll_Note.Visibility = Visibility.Visible;
        }

        private void StickQry(string WH, string Item)
        {
            OdbcConnection DbConnection = new OdbcConnection("DSN=Baan");
            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();
            DbCommand.CommandTimeout = 180;

            int n = 16 - Item.Length;
            string Fix = "";
            while (n > 0)
            {
                Fix += " ";
                n--;
            }
            Fix += Item;

            string sqlStr = string.Format(@"SELECT ttdilc101400.t_item, ttdltc001400.t_clot, ttdilc101400.t_cwar, ttdilc101400.t_strs, ttcmcs830400.t_ptyp, ttdinv001400.t_allo
                                           FROM baandb.ttccom010400 ttccom010400, baandb.ttccom020400 ttccom020400, baandb.ttcmcs830400 ttcmcs830400, baandb.ttcmcs950400 ttcmcs950400,
                                           baandb.ttdilc101400 ttdilc101400, baandb.ttdltc001400 ttdltc001400, baandb.ttiitm200400 ttiitm200400, baandb.ttdinv001400 ttdinv001400
                                           WHERE ttdltc001400.t_clot = ttdilc101400.t_clot AND
                                           ttiitm200400.t_item = ttdilc101400.t_item AND
                                           ttccom010400.t_cuno = ttiitm200400.t_cuno AND
                                           ttcmcs950400.t_mnum = ttdltc001400.t_mnum AND
                                           ttccom020400.t_suno = ttdltc001400.t_suno AND
                                           ttcmcs830400.t_pcod = ttdltc001400.t_pack_c AND
                                           ttdinv001400.t_cwar = ttdilc101400.t_cwar AND
                                           ttdinv001400.t_item = ttdilc101400.t_item AND
                                           (ttdilc101400.t_cwar=' MH' OR ttdilc101400.t_cwar='RWH' OR ttdilc101400.t_cwar='AW1' OR ttdilc101400.t_cwar='AW2' OR ttdilc101400.t_cwar='NEF' OR ttdilc101400.t_cwar='SEP')
                                           AND ttdilc101400.t_item='{1}'", WH, Fix);

            DbCommand.CommandText = sqlStr;
            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            List<StickRow> list = new List<StickRow>();
            while (DbReader.Read())
            {
                StickRow r = new StickRow();
                r.Lot = DbReader.IsDBNull(1) ? "" : DbReader.GetString(1).Trim();
                r.WH = DbReader.IsDBNull(2) ? "" : DbReader.GetString(2).Trim();
                r.Qty = DbReader.IsDBNull(3) ? 0 : Convert.ToInt32(DbReader.GetValue(3));
                r.Packaging = DbReader.IsDBNull(4) ? "" : DbReader.GetString(4).Trim();
                list.Add(r);
            }

            DbReader.Close();
            DbCommand.Dispose();
            DbConnection.Close();
            GridStick.ItemsSource = list;
        }

        private void SendStickMail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder msg = new StringBuilder();
                string mailTo = "Shahar.Assenheim@flex.com, Ori.Reuven@flex.com, yizhar.metzger@flex.com, igor.panchenko@flex.com, yaakov.lusky@flex.com";
                //string mailTo = "Shahar.Assenheim@flex.com";

                string mailCC = "Shahar.Assenheim@flex.com";
                string mailSubject = PL + @" / " + WH + "-Sticks on Kit";

                foreach (var row in Splitlist)
                {
                    if ((row.Size == "9" || row.Size == "4" || row.Item == "MLELE000072") && row.Location == "Not Exist" && row.Tray == "")
                    {
                        msg.Append(@"<h5>Item: " + row.Item + "</h5>");
                        msg.Append(@"<h5>Package Type: " + row.Size + "</h5>");
                        msg.Append(@"<h5>Lot: " + row.Lot + "</h5>");
                        msg.Append(@"<h5>Kit Demand : " + row.KitDemand + "</h5>");
                        msg.Append(@"<h5>Remarks: " + row.Remarks + "</h5>");
                        msg.Append(@"<h5>Tray Qty: " + row.TrayQty + "</h5>");
                        msg.Append(@"<h5>Tray Name: " + row.Tray + "</h5>");
                        msg.Append(@"<h5>Location: " + row.Location + "</h5>");
                        msg.Append(@"<h5>--------------------------------------------</h5>");
                        SaveAlertToDB(PL, WH, row);
                    }
                }
                if (msg.ToString() == "") return;

                string mailBody = msg.ToString();
                WebClient client = new WebClient();
                string URI = "http://mignt100/Service/HtmlMail.ashx";

                string filename = @"\\mignt002\public\smt1\Projects\Failed_Action_Reports\FirstOff_Imgs\FirstOffMails.txt";
                StreamWriter fileMail = new StreamWriter(filename, false);

                fileMail.WriteLine(string.Format("TO={0}" +
                    Environment.NewLine + "CC={1}" +
                    Environment.NewLine + "Subject={2}" +
                    Environment.NewLine + "Message={3}"
                    , mailTo
                    , mailCC
                    , mailSubject
                    , mailBody));

                fileMail.Close();
                byte[] bret = client.UploadFile(URI, filename);
                string resp = System.Text.Encoding.ASCII.GetString(bret);
                client.Dispose();
                MailSent m = new MailSent();
                m.ShowDialog();
            }
            catch (WebException)
            {
                MessageBox.Show("Failed Sending Mail");
            }
        }

        private void SaveAlertToDB(string pl, string wh, Row row)
        {
            SQLClass sql = new SQLClass("MIGSQLCLU4\\SMT", "Warehouse", "aoi", "$Flex2016");
            DateTime dt = DateTime.Now;
            string qry = string.Format(@"INSERT INTO Split_Trays_Stick_Alerts (PL, WH, Item, Size, Lot, Kit_Demand, Remarks, TrayQty, Tray, Location, Time)
                                       VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                                       pl, wh, row.Item, row.Size, row.Lot, row.KitDemand, row.Remarks, row.TrayQty, row.Tray, row.Location, dt.ToString("yyyy-MM-dd HH:mm:ss"));
            sql.InsertNonQuery(qry);
        }

        private string Check_PbrDemand(string PL, string PN)
        {
            int qty1 = 0;
            int qty2 = 0;
            try
            {
                int n = 16 - PN.Length;
                while (n > 0)
                {
                    PN = " " + PN;
                    n--;
                }

                OdbcConnection DbConnection = new OdbcConnection("DSN=Baan");
                DbConnection.Open();
                OdbcCommand DbCommand = DbConnection.CreateCommand();
                DbCommand.CommandTimeout = 180;

                string sql = string.Format(@"SELECT 
                                            tticst910400.t_pino ,ttisfc001400.t_pdno, ttisfc001400.t_mitm,tticst001400.t_sitm,
                                            tticst001400.t_qucs+tticst001400.t_issu+tticst001400.t_subd as Estimate_Qty, ttisfc001400.t_npif
                                            FROM tticst910400
                                            LEFT JOIN  ttisfc001400 ON tticst910400.t_pdno=ttisfc001400.t_pdno
                                            LEFT JOIN  tticst001400 ON tticst910400.t_pdno=tticst001400.t_pdno
                                            WHERE tticst910400.t_pino='{0}' AND tticst001400.t_opno=10  AND tticst001400.t_sitm='{1}'", PL, PN);
                DbCommand.CommandText = sql;
                OdbcDataReader DbReader = DbCommand.ExecuteReader();



                while (DbReader.Read())
                {
                    int q = DbReader.IsDBNull(4) ? 0 : Convert.ToInt32(DbReader.GetValue(4));
                    int npif = DbReader.IsDBNull(5) ? 0 : Convert.ToInt32(DbReader.GetValue(5));
                    if (npif == 1 || npif == 10)
                        qty1 += q;
                    else
                        qty2 += q;
                }
                DbReader.Close();
                DbCommand.Dispose();
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return "PBR: " + qty1.ToString() + " | " + qty2.ToString();
        }

        private string GetLastLoacation(string PL, string PN, string Lot)
        {
            string Last_Loc = "";
            try
            {
                int n = 16 - PN.Length;
                while (n > 0)
                {
                    PN = " " + PN;
                    n--;
                }

                SQLClass sql = new SQLClass("10.229.1.144\\SMT", "FirstOff", "aoi", "$Flex2016");
                string qry = string.Format(@"SELECT CreationTime
                                          FROM WH_Kit
                                          WHERE PL='{0}'", PL);
                DataTable dt = sql.SelectDB(qry);
                DateTime PLDate;
                if (dt.Rows.Count > 0)
                    PLDate = Convert.ToDateTime(dt.Rows[0]["CreationTime"]);
                else
                    return Last_Loc;

                OdbcConnection DbConnection = new OdbcConnection("DSN=Baan");
                DbConnection.Open();
                OdbcCommand DbCommand = DbConnection.CreateCommand();
                DbCommand.CommandTimeout = 180;

                while (Last_Loc == "")
                {
                    List<Transuction> list = new List<Transuction>();
                    qry = string.Format(@"SELECT ttdilc301400.t_item,
                                    ttdilc301400.t_clot,
                                    ttdilc301400.t_date,
                                    ttdilc301400.t_cwar,
                                    ttdilc301400.t_loca,
                                    ttdilc301400.t_trdt,
                                    ttdilc301400.t_trtm/86400,
                                    ttdilc301400.t_sern,
                                    ttdilc301400.t_koor,
                                    ttdilc301400.t_kost,
                                    ttdilc301400.t_orno,
                                    ttdltc001400.t_ltor_c
                                    FROM baandb.ttdilc301400 ttdilc301400
                                    LEFT JOIN baandb.ttdltc001400 ttdltc001400 ON ttdltc001400.t_clot= ttdilc301400.t_clot AND ttdltc001400.t_item = ttdilc301400.t_item
                                    WHERE ttdilc301400.t_item='{0}' AND
                                    ttdilc301400.t_clot like '%{1}%' AND
                                    ttdilc301400.t_trdt>= DATE('{2}') AND 
                                    ttdilc301400.t_sern=0 AND
                                    ttdilc301400.t_cwar!='BAW'
                                    ORDER BY ttdilc301400.t_trdt asc, ttdilc301400.t_trtm/86400 asc", PN, Lot, PLDate.ToString("MM-dd-yyyy"));
                    DbCommand.CommandText = qry;
                    OdbcDataReader DbReader = DbCommand.ExecuteReader();

                    while (DbReader.Read())
                    {
                        Transuction T = new Transuction();
                        T.FromWH = DbReader.IsDBNull(3) ? "" : DbReader.GetValue(3).ToString().Trim();
                        T.ToWH = DbReader.IsDBNull(4) ? "" : DbReader.GetValue(4).ToString().Trim();
                        T.OriginalLot = DbReader.IsDBNull(11) ? "" : DbReader.GetValue(11).ToString().Trim();
                        list.Add(T);
                    }

                    Transuction Temp = new Transuction();
                    if (list.Count == 1)
                        Temp = list[0];
                    else
                    {
                        int i = list.FindLastIndex(x => x.FromWH != WH);
                        if (i == -1)
                            return "";
                        Temp = list[i];
                    }

                    if (Temp.FromWH != WH)
                    {
                        Last_Loc = Temp.FromWH + "->" + Temp.ToWH;
                    }
                    else if (Lot != Temp.OriginalLot)
                        Lot = Temp.OriginalLot;
                    else
                        return Last_Loc;

                    DbReader.Close();
                }

                DbCommand.Dispose();
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return Last_Loc;
        }

        private void CalculatePBRNvidia()
        {
            PBRSplitlist.Clear();
            int mergID = 1;
            string key = "";
            int i = 0;
            foreach (var item in Splitlist)
            {
                if (item.Description != "")
                {
                    Row r = new Row();
                    r.MPN = item.MPN;
                    r.Customer = item.Customer;
                    r.WH = item.WH;
                    r.Remarks = item.Remarks;
                    r.Qty = item.Qty;
                    r.Item = item.Item;
                    r.Date = item.Date;
                    r.TrayQty = item.TrayQty;
                    r.Excess = item.Excess;
                    r.Description = item.Description;
                    r.Last_Location = item.Last_Location;
                    r.DateCode = item.DateCode;
                    r.Size = item.Size;
                    r.Lot = item.Lot;
                    r.KitDemand = item.KitDemand;
                    r.Tray = item.Tray;
                    r.Location = item.Location;
                    PBRSplitlist.Add(r);
                }
            }

            PBRSplitlist = PBRSplitlist.OrderBy(x => x.Item).ThenBy(x => x.Last_Location).ToList();
            foreach (var r in PBRSplitlist)
            {
                r.ID = mergID.ToString();
                if (key != "")
                {
                    if (key != r.Item + r.Last_Location)
                    {
                        mergID++;
                        r.ID = mergID.ToString();
                    }
                }
                key = r.Item + r.Last_Location;
                i++;
            }

            for (i = 0; i < PBRSplitlist.Count; i++)//set Excess to the last skid in any group.
            {
                bool IsTowLastLocatiob = false;
                List<Row> temp = PBRSplitlist.Where(x => x.Item == PBRSplitlist[i].Item).ToList();
                if (temp.Exists(x => x.Last_Location != PBRSplitlist[i].Last_Location))
                    IsTowLastLocatiob = true;

                if (i + 1 < PBRSplitlist.Count && PBRSplitlist[i].ID == PBRSplitlist[i + 1].ID)
                {
                    PBRSplitlist[i].Excess = "";
                    PBRSplitlist[i].Location = "";
                    PBRSplitlist[i].Tray = "";
                }
                else
                {
                    int n = 0;
                    string PBR = PBRSplitlist[i].Description;
                    if (PBRSplitlist[i].Last_Location.Contains("AUTOWH"))
                        n = Convert.ToInt32(PBR.Substring(PBR.LastIndexOf('|') + 1));
                    else if (PBRSplitlist[i].Last_Location.Contains("SAF"))
                        n = Convert.ToInt32(PBR.Substring(PBR.LastIndexOf(':') + 1, PBR.Length - PBR.LastIndexOf('|')));

                    PBRSplitlist[i].Excess = (PBRSplitlist[i].Qty - n).ToString();
                    if (PBRSplitlist[i].KitDemand == "0")
                    {
                        PBRSplitlist[i].Remarks = "Check Allocation";
                        PBRSplitlist[i].RowColor = "LightGoldenrodYellow";
                    }
                    else if (n > 0)
                    {
                        tSplit++;
                        if (!IsTowLastLocatiob)
                        {
                            PBRSplitlist[i].Remarks = "Split(" + n + ") of ARIBA & Shortage of " + PBR.Substring(0, PBR.LastIndexOf('|'));
                            PBRSplitlist[i].RowColor = "Salmon";
                        }
                        else
                        {
                            PBRSplitlist[i].Remarks = "Split(" + n + ")";
                            PBRSplitlist[i].RowColor = "LightGoldenrodYellow";
                        }
                    }
                    else if (n < 0)
                    {
                        tShort++;
                        PBRSplitlist[i].Remarks = "Shortage";
                        PBRSplitlist[i].RowColor = "Salmon";
                    }
                    else if (n == 0)
                    {
                        if (!IsTowLastLocatiob)
                        {
                            PBRSplitlist[i].Remarks = "Done ARIBA & Shortage of " + PBR.Substring(0, PBR.LastIndexOf('|'));
                            PBRSplitlist[i].RowColor = "Salmon";
                        }
                        else
                        {
                            PBRSplitlist[i].Remarks = "Split(" + n + ")";
                            PBRSplitlist[i].RowColor = "LightGoldenrodYellow";
                        }
                    }
                }

                if (Convert.ToInt32(PBRSplitlist[i].LineDemand) < 0)
                {
                    PBRSplitlist[i].Remarks = "Excess In PN";
                    PBRSplitlist[i].RowColor = "LightGoldenrodYellow";
                    PBRSplitlist[i].Excess = "";
                }

                if (i + 1 == PBRSplitlist.Count - 1)
                {
                    int n = 0;
                    string PBR = PBRSplitlist[i + 1].Description;
                    if (PBRSplitlist[i + 1].Last_Location.Contains("AUTOWH"))
                        n = Convert.ToInt32(PBR.Substring(PBR.LastIndexOf('|') + 1));
                    else if (PBRSplitlist[i + 1].Last_Location.Contains("SAF"))
                        n = Convert.ToInt32(PBR.Substring(PBR.LastIndexOf(':') + 1, PBR.Length - PBR.LastIndexOf('|')));


                    PBRSplitlist[i + 1].Excess = (PBRSplitlist[i + 1].Qty - n).ToString();
                    if (PBRSplitlist[i + 1].KitDemand == "0")
                    {
                        PBRSplitlist[i + 1].Remarks = "Check Allocation";
                        PBRSplitlist[i + 1].RowColor = "LightGoldenrodYellow";
                    }
                    else if (n > 0)
                    {
                        tSplit++;
                        if (!IsTowLastLocatiob)
                        {
                            PBRSplitlist[i + 1].Remarks = "Split(" + n + ") of ARIBA & Shortage of " + PBR.Substring(0, PBR.LastIndexOf('|'));
                            PBRSplitlist[i + 1].RowColor = "Salmon";
                        }
                        else
                        {
                            PBRSplitlist[i + 1].Remarks = "Split(" + n + ")";
                            PBRSplitlist[i + 1].RowColor = "LightGoldenrodYellow";
                        }
                    }
                    else if (n < 0)
                    {
                        tShort++;
                        PBRSplitlist[i + 1].Remarks = "Shortage";
                        PBRSplitlist[i + 1].RowColor = "Salmon";
                    }
                    else if (n == 0)
                    {
                        if (!IsTowLastLocatiob)
                        {
                            PBRSplitlist[i + 1].Remarks = "Done ARIBA & Shortage of " + PBR.Substring(0, PBR.LastIndexOf('|'));
                            PBRSplitlist[i + 1].RowColor = "Salmon";
                        }
                        else
                        {
                            PBRSplitlist[i + 1].Remarks = "Split(" + n + ")";
                            PBRSplitlist[i + 1].RowColor = "LightGoldenrodYellow";
                        }
                    }
                    break;
                }
            }
        }
    }
}
