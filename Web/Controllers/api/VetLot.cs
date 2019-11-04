using System.Net.Http;
using System.Web.Http;
using TkMiddleware.DataObjects;
using TkMiddleware.Helpers;
using TkMiddleware.Outbound;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TkMiddleware.Controllers.api
{
    [Route(VetLot.url)]
    public class VetLotController : ApiController
    {
        private const string entityName = "VetLot";
        private EntityHelper entityHelper = new EntityHelper();
        private HClient hClient = new HClient();

        public VetLotController()
        {
            hClient.Request = new HttpRequestMessage();
            hClient.Configuration = new HttpConfiguration();
        }

        [HttpGet]
        public IHttpActionResult Get(int version)
        {
            string entityTypeName = entityHelper.GetEntityTypeName(version, entityName);
            if (!string.IsNullOrEmpty(entityTypeName))
            {
                //return Ok(entityHelper.GetEntityInstance(entityTypeName)); // data contract for object
                var raw = hClient.Submit(version, entityName, Request.Headers) as dynamic;
                if(raw.GetType().GetProperty("Response") != null)
                {
                    if (raw.Response.IsSuccessStatusCode)
                    {
                        var rawObjs = JsonConvert.DeserializeObject(raw.Content.Result);
                        var records = new List<dynamic>();
                        foreach (var rec in rawObjs)
                        {
                            records.Add(entityHelper.MapJsonToObject(rec, entityTypeName));
                        }
                        return Ok(records);
                    }
                    else
                    {
                        return raw;
                    }
                }
                else
                {
                    return raw;
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}