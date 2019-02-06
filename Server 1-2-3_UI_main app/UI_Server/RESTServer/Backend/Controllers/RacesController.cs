using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/Races")]
    public class RacesController : Controller
    {
        public RacesController()
        {
        }

        // GET: api/Races
        [HttpGet]
        public IEnumerable<Race> GetRace()
        {
            var client = new WebClient();
            client.Headers.Add("Accept", "application/json");

            var result = client.DownloadString
                ("http://localhost:47356/RaceService.svc/GetRaces");

            var serializer = new DataContractJsonSerializer(typeof(List<Race>));

            List<Race> Races;
            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                Races = (List<Race>)serializer.ReadObject(stream);
            }

            return Races;
        }

        // GET: api/Races/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRace([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = new WebClient();
            client.Headers.Add("Accept", "application/json");

            var result = client.DownloadString
                ("http://localhost:47356/RaceService.svc/GetRaces");

            var serializer = new DataContractJsonSerializer(typeof(List<Race>));

            List<Race> Races;
            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(result)))
            {
                Races = (List<Race>)serializer.ReadObject(stream);
            }

            return Ok(Races);
        }

        // PUT: api/Races/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRace([FromRoute] int id, [FromBody] Race race)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != race.Id)
            {
                return BadRequest();
            }

            var RaceFromServer = SendDataToServer(
                "http://localhost:47356/RaceService.svc/Race/"+id.ToString(),
                "PUT", race);

            return NoContent();
        }

        // POST: api/Races
        [HttpPost]
        public async Task<IActionResult> PostRace([FromBody] Race race)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var RaceFromServer = SendDataToServer(
                "http://localhost:47356/RaceService.svc/CreateRace",
                "POST", race);

            return CreatedAtAction("GetRace", new { id = RaceFromServer.Id }, race);
        }

        // DELETE: api/Races/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRace([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var RaceFromServer = SendDataToServer(
                "http://localhost:47354/RaceService.svc/DeleteRace",
                "DELETE", id);

            return Ok(RaceFromServer);
        }

        private T SendDataToServer<T>(string endpoint, string method, T Race)
        {
            var request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Accept = "application/json";
            request.ContentType = "application/json";

            request.Method = method;
         
            var serializer = new DataContractJsonSerializer(typeof(T));
            var requestStream = request.GetRequestStream();
            serializer.WriteObject(requestStream, Race);
            requestStream.Close();

            var response = request.GetResponse();
            if (response.ContentLength == 0)
            {
                response.Close();
                return default(T);
            }

            var responseStream = response.GetResponseStream();
            var responseObject = (T)serializer.ReadObject(responseStream);

            responseStream.Close();

            return responseObject;
        }
    }
}