﻿//using System;
//using System.Collections.Generic;
//using System.Text;
//using Newtonsoft.Json.Linq;
//using System;
//using System.IO;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;

//namespace SendImageToOCR.Services
//{
//    public class ImageStuff
//    {
//        / <param name="imageFilePath">The image file to analyze.</param>
//        static async Task MakeAnalysisRequest(string imageFilePath)
//        {
//            try
//            {
//                HttpClient client = new HttpClient();

//                 Request headers.
//                client.DefaultRequestHeaders.Add(
//                    "Ocp-Apim-Subscription-Key", subscriptionKey);

//                 Request parameters. A third optional parameter is "details".
//                 The Analyze Image method returns information about the following
//                 visual features:
//                 Categories:  categorizes image content according to a
//                              taxonomy defined in documentation.
//                 Description: describes the image content with a complete
//                              sentence in supported languages.
//                 Color:       determines the accent color, dominant color,
//                              and whether an image is black & white.
//                string requestParameters =
//                    "visualFeatures=Categories,Description,Color";

//                 Assemble the URI for the REST API method.
//                string uri = uriBase + "?" + requestParameters;

//                HttpResponseMessage response;

//                 Read the contents of the specified local image
//                 into a byte array.
//                byte[] byteData = GetImageAsByteArray(imageFilePath);

//                 Add the byte array as an octet stream to the request body.
//                using (ByteArrayContent content = new ByteArrayContent(byteData))
//                {
//                     This example uses the "application/octet-stream" content type.
//                     The other content types you can use are "application/json"
//                     and "multipart/form-data".
//                    content.Headers.ContentType =
//                        new MediaTypeHeaderValue("application/octet-stream");

//                     Asynchronously call the REST API method.
//                    response = await client.PostAsync(uri, content);
//                }

//                 Asynchronously get the JSON response.
//                string contentString = await response.Content.ReadAsStringAsync();

//                 Display the JSON response.
//                Console.WriteLine("\nResponse:\n\n{0}\n",
//                    JToken.Parse(contentString).ToString());
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("\n" + e.Message);
//            }
//        }

//        / <summary>
//        / Returns the contents of the specified file as a byte array.
//        / </summary>
//        / <param name="imageFilePath">The image file to read.</param>
//        / <returns>The byte array of the image data.</returns>
//        static byte[] GetImageAsByteArray(string imageFilePath)
//        {
//             Open a read-only file stream for the specified file.
//            using (FileStream fileStream =
//                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
//            {
//                 Read the file's contents into a byte array.
//                BinaryReader binaryReader = new BinaryReader(fileStream);
//                return binaryReader.ReadBytes((int)fileStream.Length);
//            }
//        }
//    }
//}
