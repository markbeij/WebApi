using AspNetCoreODataSample.Web.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AspNetCoreODataSample.Web.Controllers
{
    /// <summary>
    /// Request with $top and $top-less request give different item at index 0
    /// GET /Sample
    /// GET /Sample?$top=1
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    public class SampleController : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            var result = GetObjectFromAssembly<IEnumerable<SampleEntity>>(typeof(SampleController).Assembly, "items.json");

            var list = result.ToList();

            return Ok(list);
        }

        private T GetObjectFromAssembly<T>(Assembly asm, string filename)
        {
            var resource = asm.GetManifestResourceNames().Single(name => name.EndsWith($"{filename}"));

            using (var stream = asm.GetManifestResourceStream(resource))
            {
                if (stream != null)
                {
                    var reader = new StreamReader(stream);
                    var jsonString = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<T>(jsonString, _jsonSerializerSettings);
                }
            }

            throw new FileNotFoundException(resource);
        }

        private JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.None,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };
    }
}