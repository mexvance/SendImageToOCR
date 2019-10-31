using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SendImageToOCR
{
    public static class Function1
    {
        //static string subscriptionkey = Environment.GetEnvironmentVariable("30f3c8a9825e44b0bc19cbc71141ee78");
        static string subscriptionkey = "30f3c8a9825e44b0bc19cbc71141ee78";
        static string endpoint = Environment.GetEnvironmentVariable("https://imagereaderocr.cognitiveservices.azure.com/");
        // the analyze method endpoint
        //static string uribase = "https://imagereaderocr.cognitiveservices.azure.com/" + "vision/v2.1/ocr";
        static string uribase = "https://imagereaderocr.cognitiveservices.azure.com/" + "vision/v2.1/describe";
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            setpath();
            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
        // add your computer vision subscription key and endpoint to your environment variables.
      

        static void setpath()
        {
            // get the path and filename to process from the user.
            // console.writeline("analyze an image:");
            // console.write(
            //"enter the path to the image you wish to analyze: ");
            string imagefilepath = "https://images.pexels.com/photos/67636/rose-blue-flower-rose-blooms-67636.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500";

            if (File.Exists(imagefilepath))
            {
                // call the rest api method.
                // console.writeline("\nwait a moment for the results to appear.\n");
            }
            else
            {
                MakeAnalysisRequest(imagefilepath);
                //console.writeline("\ninvalid file path");
            }
            //console.writeline("\npress enter to exit...");
            //console.readline();
        }
        static async void MakeAnalysisRequest(string imageFilePath)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(imageFilePath);
            //var words = new TextContent();
            // Request headers. Replace the second parameter with a valid subscription key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "30f3c8a9825e44b0bc19cbc71141ee78");

            // Request parameters. You can change "landmarks" to "celebrities" on requestParameters and uri to use the Celebrities model.
            var uri = uribase;
            
            Console.WriteLine(uri);

            HttpResponseMessage response;

            // Request body. Try this sample with a locally stored JPEG image.
            //byte[] byteData = GetImageAsByteArray(imageFilePath);
            byte[] byteData = GetImageAsByteArray("C:/Users/mexva/Pictures/Moved Test Images/RIBBONSHEADER.gif");
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                var resp = await response.Content.ReadAsStringAsync();
                var region = JObject.Parse(resp)["description"]["captions"];
                var captionList = new List<CaptionInfo>();
                var captionInfo = new CaptionInfo();
                foreach(var item in region)
                {
                    //var val = item.Value;
                    //captionInfo.text = region.Value;
                    //captionInfo.accuracy = region.
                    captionInfo = JsonConvert.DeserializeObject<CaptionInfo>(item.ToString());
                    captionList.Add(captionInfo);
                }
            }
        }
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }

    public class CaptionInfo
    {
        public string text;
        public double confidence;
    }
}
