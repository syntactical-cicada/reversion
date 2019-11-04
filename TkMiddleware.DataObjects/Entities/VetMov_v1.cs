using System;

namespace TkMiddleware.DataObjects
{
    public static class VetMov
    {
        public const string url = "api/v{version}/VetMov";
    }

    public class VetMov_v1
    {
        public int version = 1;
        public string url
        {
            get
            {
                return string.Format(VetMov.url.Replace("version", "0"), version);
            }
        }

        public DateTime Date { get; set; } 
        public int Headcount { get; set; }
        public decimal RefOrTemp { get; set; }
        public decimal Facility { get; set; }
        public int Weight { get; set; }

        // string parameters require special handling because we support data contract violations
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
        private string _fromPen = string.Empty;
        public string FromPen
        {
            get
            {
                return _fromPen;
            }
            set
            {
                if (value != null) _fromPen = value;
            }
        }
        private string _homePen = string.Empty;
        public string HomePen
        {
            get
            {
                return _homePen;
            }
            set
            {
                if (value != null) _homePen = value;
            }
        }
        private string _toPen = string.Empty;
        public string ToPen
        {
            get
            {
                return _toPen;
            }
            set
            {
                if (value != null) _toPen = value;
            }
        }
        private string _tag = string.Empty;
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                if (value != null) _tag = value;
            }
        }
        private string _altTag = string.Empty;
        public string AltTag
        {
            get
            {
                return _altTag;
            }
            set
            {
                if (value != null) _altTag = value;
            }
        }
        private string _deathDescription = string.Empty;
        public string DeathDescription
        {
            get
            {
                return _deathDescription;
            }
            set
            {
                if (value != null) _deathDescription = value;
            }
        }
    }
}