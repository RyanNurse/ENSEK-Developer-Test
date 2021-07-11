using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace Energy_Meter_API.Controllers
{
    [Route("[controller]")]
    public class MeterController : ApiController
    {
        [HttpPost]
        [Route("/api/meter/meter-reading-uploads")]
        public IHttpActionResult UploadReadings(IFormFile csvFile)
        {
            //store CSV data in temp file, then read it back in
            var filePath = Path.GetTempFileName();
            var csvData = new List<string>();

            using (var stream = File.Create(filePath))
            {
                csvFile.CopyTo(stream);

                csvData = File.ReadAllLines(filePath).ToList();
            }

            //convert CSV lines to readings
            var readings = new List<MeterReading>();
            foreach(var line in csvData)
            {
                var reading = MeterReading.CreateFromCsv(line);
                if (reading != null)
                {
                    readings.Add(reading);
                }
            }

            int successfulAdds = 0;

            //send readings to DB and count how many were successful
            foreach(var reading in readings)
            {
                if (IMeterReadingRepository.Insert(reading))
                {
                    successfulAdds++;
                }
            }

            int unsuccessfulAdds = csvData.Count - successfulAdds;

            return Ok($"{successfulAdds} readings were added successfully.\n{unsuccessfulAdds} readings were invalid or duplicated and were not added.");
        }
    }
}
