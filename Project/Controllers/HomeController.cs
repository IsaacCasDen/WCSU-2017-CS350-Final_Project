using Newtonsoft.Json;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        const String apiDomain = "http://words.bighugelabs.com/";
        String apiQuery { get { return "api/2/" + ConfigurationManager.AppSettings["APIKey"] + "/{0}/json"; } }
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [ActionName("Search")]
        public String getSynonyms(String Word)
        {
            //List<Synonym> values = new List<Synonym>();
            var data = getWebData(apiDomain + string.Format(apiQuery, Word));
            var dataObject = parseWebString(data);
            var items = parseDataObject(dataObject);

            string outputValue = string.Join<ThesaurusItem>(",<br />", items.ToArray());
            return outputValue;
        }

        [NonAction]
        public List<ThesaurusItem> parseDataObject(JsonModel.Rootobject dataObject)
        {
            List<ThesaurusItem> values = new List<ThesaurusItem>();

            if (dataObject!=null)
            {
                values.AddRange(parseDataObjectComponents(dataObject.adjective));
                values.AddRange(parseDataObjectComponents(dataObject.adverb));
                values.AddRange(parseDataObjectComponents(dataObject.noun));
                values.AddRange(parseDataObjectComponents(dataObject.verb));
            }

            return values;
        }
        private List<ThesaurusItem> parseDataObjectComponents(SpeechComponent component)
        {
            List<ThesaurusItem> values = new List<ThesaurusItem>();

            if (component!=null)
            {
                values.AddRange(parseComponentItems(component.syn, component.SpeechPart, WordRel.Synonym));
                values.AddRange(parseComponentItems(component.sim, component.SpeechPart, WordRel.Similar));
                values.AddRange(parseComponentItems(component.rel, component.SpeechPart, WordRel.Related));
                values.AddRange(parseComponentItems(component.ant, component.SpeechPart, WordRel.Antonym));
            }

            return values;
        }
        private List<ThesaurusItem> parseComponentItems(String[] words, SpeechPart speechPart, WordRel wordRel)
        {
            List<ThesaurusItem> values = new List<ThesaurusItem>();

            if (words != null)
            {
                foreach (String word in words)
                {
                    values.Add(new ThesaurusItem(word, speechPart, wordRel));
                }
            }

            return values;
        }


        [NonAction]
        public JsonModel.Rootobject parseWebString(String data)
        {
            JsonModel.Rootobject value = null;

            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    value = JsonConvert.DeserializeObject<JsonModel.Rootobject>(data);
                }
                catch (Exception ex)
                {
                    if (ex!=null)
                    {

                    }
                }
            }

            return value;
        }

        [NonAction]
        public String getWebData(String url)
        {
            String value = String.Empty;

            using (var w = new WebClient())
            {
                try
                {
                    value = w.DownloadString(url);
                }
                catch (Exception ex) {}
            }

            return value;
        }

        //private T Test2<T>(String url) where T : new()
        //{
        //    using (var w = new WebClient())
        //    {
        //        var value = String.Empty;
        //        try
        //        {
        //            value = w.DownloadString(url);
        //        } catch (Exception ex)
        //        {
        //        }

        //        return !string.IsNullOrEmpty(value) ? JsonConvert.DeserializeObject<T>(value) : new T();
        //    }
        //}
        //private Object readFile()
        //{
        //    JsonModel value=null;
        //    var path = Server.MapPath("~/Content/data.json");

        //    using (StreamReader sr = new StreamReader(path))
        //    {
        //        value = JsonConvert.DeserializeObject<JsonModel>(sr.ReadToEnd());
        //    }

        //    return value;
        //}

        //[NonAction]
        //public ActionResult getSynonymsAsync(String word)
        //{
        //    var data = GetData(word).Result;
        //    return data;
        //}

        //[HttpGet]
        //public async Task<ActionResult> Test2(String word)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(apiDomain);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        String finalQuery = string.Format(apiQuery, word);

        //        HttpResponseMessage response = await client.GetAsync(finalQuery);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var apiData = await response.Content.ReadAsStringAsync();
        //            var jsonData = Json(new { data = apiData }, JsonRequestBehavior.AllowGet);
        //            return jsonData;
        //        }
        //    }

        //    return null;
        //}

        //public ActionResult Test()
        //{
        //    using (var client = new System.Net.Http.HttpClient())
        //    {
        //        client.BaseAddress = apiUri;
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        var response = client.GetAsync("").Result;
        //        String res = "";
        //        using (HttpContent content=response.Content)
        //        {
        //            Task<String> result = content.ReadAsStringAsync();
        //            res = result.Result;
        //        }
        //    }
        //}
    }
}