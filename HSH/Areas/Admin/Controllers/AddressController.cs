using HSH.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace HSH.Areas.Admin.Controllers
{
    public class AddressController : Controller
    {
        [HttpGet]
        public ActionResult GetAddress(string postalCode)
        {
            // API/search key - MUST be supplied to unlock search, 
            // if this is blank a restricted key is used which limits search to NR147PZ
            string searchKey = "PCW5S-L9L9X-TQM9C-TZHCV";
            //PCWXP - L3KTM - MK7M3 - TNNSH
            // processing type - for other options see 
            string method = "address";

            // search string - MUST be supplied, if blank defaults to NR147PZ
            string searchTerm = postalCode.Replace(" ", ""); //remove spaces

            PropertyModel address = new PropertyModel(); // in case of null response or error, returns a blank AddressModel

            try
            {
                // format the url		
                string uri = String.Format("http://ws.postcoder.com/pcw/{0}/{1}/IE/{2}",
                                                searchKey,
                                                method,
                                                System.Uri.EscapeDataString(searchTerm)
                                                );

                // call the service
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                request.Accept = "application/xml"; // xml or json permitted

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    // check for call failure
                    if (response.StatusCode != HttpStatusCode.OK) // = HTTP 200 code (OK)
                    {
                        throw new ApplicationException(String.Format("Failed : HTTP error code : {0}", response.StatusCode));
                    }

                    // grab the response
                    var addresses = (Addresses)((new XmlSerializer(typeof(Addresses))).Deserialize(response.GetResponseStream()));
                    if (addresses.AddressList.Any())
                    {
                        address = addresses.AddressList.FirstOrDefault();
                    }

                    response.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return PartialView("_AddressPartialView", address);
        }
    }
}