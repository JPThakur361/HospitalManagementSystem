using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public DoctorController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet("assign/{requestId}")]
        public IActionResult AssignDoctor(string requestId)
        {
           
            Console.WriteLine($"Received requestId: {requestId}");

            var cacheEntry = _cache.GetString(requestId);
            if (cacheEntry == null)
            {
                // Debugging: Log that the requestId was not found
                Console.WriteLine($"Request with ID {requestId} not found in cache");
                return NotFound();
            }

            var request = JsonConvert.DeserializeObject<PatientRequest>(cacheEntry);

            // Debugging: Log the patient details
            Console.WriteLine($"Patient details: {JsonConvert.SerializeObject(request)}");

            // Assign a doctor based on the disease (dummy logic for demonstration)
            string assignedDoctor = AssignDoctorLogic(request.Disease);

            // Debugging: Log the assigned doctor
            Console.WriteLine($"Assigned Doctor: {assignedDoctor}");

            return Ok(new { Message = "Doctor assigned successfully", AssignedDoctor = assignedDoctor });
        }

        private string AssignDoctorLogic(string disease)
        {
            // Dummy logic: Assign a doctor based on the disease
            switch (disease.ToLower())
            {
                case "heart":
                    return "Dr. Smith";
                case "orthopedic":
                    return "Dr. Johnson";
                default:
                    return "Dr. Unknown";
            }
        }
    }
}

