using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Split_Trays.Classes
{
    public class Row
    {
        public string ID { get; set; }
        public string Item { get; set; }
        public string Size { get; set; }
        public string WH { get; set; }
        public string Lot { get; set; }
        public string MLot { get; set; }
        public string KitDemand { get; set; }
        public int Qty { get; set; }
        public string LineDemand { get; set; }
        public string LineDemand_Poly { get; set; }
        public string Excess { get; set; }
        public string RealExcess { get; set; }
        public string Remarks { get; set; }
        public string t_mnum { get; set; }
        public string MPN { get; set; }
        public string DateCode { get; set; }
        public int TrayQty { get; set; }
        public DateTime Date { get; set; }
        public string RowColor { get; set; }
        public string Location { get; set; }
        public string Tray { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Customer { get; set; }
        public string Shape { get; set; }
        public string Description { get; set; }
        public string Last_Location { get; set; }
    }

    public class StickRow
    {
        public int Qty { get; set; }
        public string WH { get; set; }
        public string Lot { get; set; }
        public string Packaging { get; set; }
    }


    public class Transuction
    {
        public string FromWH  { get; set; }
        public string ToWH { get; set; }
        public string OriginalLot { get; set; }
    }
}
