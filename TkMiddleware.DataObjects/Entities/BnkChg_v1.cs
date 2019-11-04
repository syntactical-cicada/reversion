using System;

namespace TkMiddleware.DataObjects
{
    public static class BnkChg
    {
        public const string url = "api/v{version}/BnkChg";
    }

    public class BnkChg_v1
    {
        public int version = 1;
        public string url
        {
            get
            {
                return string.Format(BnkChg.url.Replace("version", "0"), version);
            }
        }

        public DateTime Date { get; set; }
        public int Feeding { get; set; }
        public int Fed { get; set; }
        public int PenHdct { get; set; }
        public int LotHdct { get; set; }
        public decimal Price { get; set; }
        public int Called { get; set; }

        // string parameters require special handling because we support data contract violations
        private string _lot = string.Empty;
        public string Lot
        {
            get
            {
                return _lot;
            }
            set
            {
                if (value != null)
                {
                    _lot = value;
                }
            }
        }
        private string _ration = string.Empty;
        public string Ration
        {
            get
            {
                return _ration;
            }
            set
            {
                if (value != null)
                {
                    _ration = value;
                }
            }
        }
        private string _pen = string.Empty;
        public string Pen
        {
            get
            {
                return _pen;
            }
            set
            {
                if (value != null)
                {
                    _pen = value;
                }
            }
        }
        private string _microRationCode = string.Empty;
        public string MicroRationCode
        {
            get
            {
                return _microRationCode;
            }
            set
            {
                if (value != null)
                {
                    _microRationCode = value;
                }
            }
        }
    }
}