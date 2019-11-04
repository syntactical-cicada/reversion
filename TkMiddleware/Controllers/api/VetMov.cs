using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using TkMiddleware.DataObjects;
using TkMiddleware.Helpers;
using TkMiddleware.Outbound;

namespace TkMiddleware.Controllers.api
{    
    [Route(VetMov.url)]
    public class VetMovController : ApiController
    {
        private const string entityName = "VetMov";
        private EntityHelper entityHelper = new EntityHelper();
        private HClient hClient = new HClient();

        public VetMovController()
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
                return Ok(entityHelper.GetEntityInstance(entityTypeName)); // data contract for object
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult Post(int version, dynamic json)
        {
            string entityTypeName = entityHelper.GetEntityTypeName(version, entityName, false);
            if (!string.IsNullOrEmpty(entityTypeName))
            {
                if (json.data != null && json.data.Count > 0)
                {
                    List<dynamic> records = new List<dynamic>();
                    foreach (var item in json.data)
                    {
                        records.Add(entityHelper.MapJsonToObject(item, entityTypeName));
                    }
                    TkWrapper outboundJsonObject = new TkWrapper() { version = version, data = records };

                    return hClient.Submit(version, entityName, Request.Headers, outboundJsonObject);
                }
                else
                {
                    return BadRequest("source: TkMiddleware");
                }
            }
            else
            {
                return StatusCode(System.Net.HttpStatusCode.NotImplemented);
            }
        }
    }
}