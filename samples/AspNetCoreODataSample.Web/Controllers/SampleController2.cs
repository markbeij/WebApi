using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AspNetCoreODataSample.Web.Controllers
{
    /// <summary>
    /// Request with $top and $top-less request give different item at index 0
    /// GET /Sample2
    /// GET /Sample2?$top=1
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    public class Sample2Controller : ODataController
    {
        public class Item
        {
            public string Id { get; set; }
        }

        private readonly static IList<Item> Items;

        static Sample2Controller()
        {
            Items = new List<Item>();

            for (int i = 0; i < 1000; i++)
            {
                Items.Add(new Item { Id = Guid.NewGuid().ToString("D") });
            }
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(Items);
        }
    }
}