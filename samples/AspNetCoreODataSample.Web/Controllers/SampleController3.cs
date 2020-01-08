using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AspNetCoreODataSample.Web.Controllers
{
    /// <summary>
    /// Request with $top and $top-less request give same item at index 0
    /// GET /Sample3
    /// GET /Sample3?$top=1
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    public class Sample3Controller : ODataController
    {
        private readonly static IList<string> Items;

        static Sample3Controller()
        {
            Items = new List<string>();

            for (int i = 0; i < 1000; i++)
            {
                Items.Add(Guid.NewGuid().ToString("D"));
            }
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(Items);
        }
    }
}