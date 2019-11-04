using System;

namespace TkMiddleware.DataObjects
{   
    public class BnkMov_v2
    {
        public int version = 2;
        public string url
        {
            get
            {
                return string.Format(BnkMov.url.Replace("version", "0"), version);
            }
        }

        public int Headcount { get; set; }
        public DateTime Date { get; set; }
        public int WeightOrPenCount { get; set; }
        public decimal PercentHistory { get; set; }

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
                if (value != null)  _type = value;
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
        private string _fromLot = string.Empty;
        public string FromLot
        {
            get
            {
                return _fromLot;
            }
            set
            {
                if (value != null) _fromLot = value;
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
        private string _toLot = string.Empty;
        public string ToLot
        {
            get
            {
                return _toLot;
            }
            set
            {
                if (value != null) _toLot = value;
            }
        }
        private string _sexCode = string.Empty;
        public string SexCode
        {
            get
            {
                return _sexCode;
            }
            set
            {
                if (value != null) _sexCode = value;
            }
        }
        private string _fullMove = string.Empty;
        public string FullMove
        {
            get
            {
                return _fullMove;
            }
            set
            {
                if (value != null) _fullMove = value;
            }
        }
        private string _newField = string.Empty;
        public string NewField
        {
            get
            {
                return _newField;
            }
            set
            {
                if (value != null) _newField = value;
            }
        }
    }
}