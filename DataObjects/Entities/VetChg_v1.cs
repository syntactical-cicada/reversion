using System;

namespace TkMiddleware.DataObjects
{
    public static class VetChg
    {
        public const string url = "api/v{version}/VetChg";
    }
    public class VetChg_v1
    {
        public int version = 1;
        public string url
        {
            get
            {
                return string.Format(VetChg.url.Replace("version", "0"), version);
            }
        }

        public DateTime Date { get; set; }
        public decimal Units { get; set; }
        public decimal Price { get; set; }
        public decimal Facility { get; set; }

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
                if (value != null) _lot = value;
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
                if (value != null) _pen = value;
            }
        }
        private string _item = string.Empty;
        public string Item
        {
            get
            {
                return _item;
            }
            set
            {
                if (value != null) _item = value;
            }
        }
        private string _type = string.Empty;
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (value != null) _type = value;
            }
        }
    }
}