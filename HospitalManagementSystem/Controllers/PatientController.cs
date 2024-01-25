using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public PatientController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpPost("submit")]
        public IActionResult SubmitRequest([FromBody] PatientRequest request)
        {
            var key = $"patient:{Guid.NewGuid()}";
            var cacheEntry = JsonConvert.SerializeObject(request);

            // Store the patient request in the distributed cache
            _cache.SetString(key, cacheEntry, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(180)
            });

            return Ok(new { Message = "Request submitted successfully", RequestId = key });
        }

        [HttpGet("get/{requestId}")]
        public IActionResult GetRequest(string requestId)
        {
            // Retrieve patient request from the distributed cache
            var cacheEntry = _cache.GetString(requestId);
            if (cacheEntry == null)
            {
                return NotFound();
            }

            var request = JsonConvert.DeserializeObject<PatientRequest>(cacheEntry);
            return Ok(request);
        }
    }

    public class PatientRequest
    {
        public string PatientName { get; set; }
        public string Disease { get; set; }
    }
}
