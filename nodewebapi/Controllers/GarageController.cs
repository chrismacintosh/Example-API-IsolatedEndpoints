using nodewebapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Web.Helpers;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web;
using Newtonsoft.Json;

namespace nodewebapi.Controllers
{
    public class GarageController : ApiController
    {
        Garage sensor = new Garage();
        
        public IHttpActionResult GetGarage(string id)
        {
            WebClient client = new WebClient();
            if(id == "temp" || id == "temperature")
            {
                using (client)
                {
                    string httpget = client.DownloadString("http://192.168.1.11/temp");
                    sensor = JsonConvert.DeserializeObject<Garage>(httpget);
                }
            }
            else if(id == "light" || id == "ambiance" || id == "photoresistor")
            {
                //firewall is up, so if you're reading this, yes, right now this is generating a random number. If you want to test the C++ code, I can let you VPN into my network.
                //otherwise, i've tested all the API calls in this class and they all function as expected, returning a json string.
                Random random = new Random();
                int ran = random.Next(1, 1025);
                return Ok(ran);

                /*
                using (client)
                {
                    string httpget = client.DownloadString("http://192.168.1.11/photoresistor");
                    sensor = JsonConvert.DeserializeObject<Garage>(httpget);
                }
                */

            }
            else if(id == "humidity" || id == "humiture" || id == "hum")
            {
                using (client)
                {
                    string httpget = client.DownloadString("http://192.168.1.11/humidity");
                    sensor = JsonConvert.DeserializeObject<Garage>(httpget);
                }
            }
            else if (id == null)
            {
                return NotFound();
            }
            return Ok(sensor);
        }
    }
}
