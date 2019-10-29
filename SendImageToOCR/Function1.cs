using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SendImageToOCR
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
    //static class Program
    //{
    //    // Add your Computer Vision subscription key and endpoint to your environment variables.
    //    static string subscriptionKey = Environment.GetEnvironmentVariable("COMPUTER_VISION_SUBSCRIPTION_KEY");

    //    static string endpoint = Environment.GetEnvironmentVariable("COMPUTER_VISION_ENDPOINT");

    //    // the Analyze method endpoint
    //    const string uriBase = endpoint + "vision/v2.1/analyze";

    //    static void Main()
    //    {
    //        // Get the path and filename to process from the user.
    //        Console.WriteLine("Analyze an image:");
    //        Console.Write(
    //            "Enter the path to the image you wish to analyze: ");
    //        string imageFilePath = Console.ReadLine();

    //        if (File.Exists(imageFilePath))
    //        {
    //            // Call the REST API method.
    //            Console.WriteLine("\nWait a moment for the results to appear.\n");
    //            MakeAnalysisRequest(imageFilePath).Wait();
    //        }
    //        else
    //        {
    //            Console.WriteLine("\nInvalid file path");
    //        }
    //        Console.WriteLine("\nPress Enter to exit...");
    //        Console.ReadLine();
    //    }
    }
