using System;

namespace TkMiddleware.DataObjects
{
    public class VetLot
    {
        public const string url = "api/v{version}/VetLot";
    }
    public class VetLot_v1
    {
        public int version = 1;
        public string url
        {
            get
            {
                return string.Format(VetLot.url.Replace("version", "0"), version);
            }
        }
        public string LOT { get; set; }
        public string PEN { get; set; }
        public string PVP { get; set; }
        public int HED { get; set; }
        public int SHP { get; set; }
        public int DED { get; set; }
        public int HOS { get; set; }
        public int BUL { get; set; }
        public DateTime DTI { get; set; }
        public DateTime DTO { get; set; }
        public int GAN { get; set; }
        public string NAM { get; set; }
        public string BUY { get; set; }
        public DateTime ADI { get; set; }
        public int TRK { get; set; }
        public float CUR { get; set; }
        public string ORI { get; set; }
        public string SEX { get; set; }
        public int PAY { get; set; }
        public int WTO { get; set; }
    }
}