using System.Collections.Generic;

namespace TkMiddleware.DataObjects
{
    public class TkWrapper
    {
        public int version { get; set; }
        public List<dynamic> data { get; set; }
    }
}
