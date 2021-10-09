using System;
using System;
using RestSharp;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using APITestingProject.Utils;

namespace APITestingProject.B2BPOSAPITest
{

   
    public class GetSubMerchantAndAddTerminal : BaseClass
    {
        JsonData jsonData = new JsonData();
        CommonClass commonClass = new CommonClass();
        String testVatNumber;
        string merchantPaycriticalIdTest;
        string subMerchantPaycriticalIdTest;
        string getSubMerchantId;
        string channelID;

        [Test, Order(1)]
        public void CreateMerchant()
        {
            int randomNumber = commonClass.getRandomNumber(5);
            testVatNumber = randomNumber.ToString();

            JObject jObjectbody = new JObject();
            jObjectbody = jsonData.getMerchantJsonDataForSubMerchantAndTerminals(testVatNumber);

            //Creating Client connection 
            RestClient restClient = new RestClient(b2BBackofficeAPIBaseURL);


            //Creating request to get data from server
            RestRequest restRequest = new RestRequest(Resources.addEntitiesResource, Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", b2BBackofficeAPIAuthorizationCode);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);

            Console.WriteLine("response: " + restResponse.Content);
            Console.WriteLine("Vat Number: " + testVatNumber);

            //get status code from received response
            int StatusCode = (int)restResponse.StatusCode;
            //Verify that status code 201 should be displayed after successful merchant creation
            Assert.AreEqual(StatusCodes.statusCode201, StatusCode, "Status code should be displayed 201");

            //parse the json response
            var jObject = JObject.Parse(restResponse.Content);
            merchantPaycriticalIdTest = (string)jObject["processedEntities"][0]["paycriticalId"];
            Console.WriteLine("merchantPaycriticalId: " + merchantPaycriticalIdTest);


        }




        [Test, Order(2)]
        public void CreateSubMerchant()
        {
            JObject jObjectbody = new JObject();
            jObjectbody = jsonData.getSubMerchantJsonData(merchantPaycriticalIdTest);

            //Creating Client connection 
            RestClient restClient = new RestClient(b2BBackofficeAPIBaseURL);


            //Creating request to get data from server
            RestRequest restRequest = new RestRequest(Resources.addEntitiesResource, Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", b2BBackofficeAPIAuthorizationCode);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);

            //get status code from received response
            int StatusCode = (int)restResponse.StatusCode;
            //Verify that status code 201 should be displayed after successful sub merchant creation
            Assert.AreEqual(StatusCodes.statusCode201, StatusCode, "Status code should be displayed 201");

            //parse the json response
            var jObject = JObject.Parse(restResponse.Content);
            Console.WriteLine("response: " + jObject.ToString());
            subMerchantPaycriticalIdTest = (string)jObject["processedEntities"][0]["paycriticalId"];
            Console.WriteLine("subMerchantPaycriticalId: " + subMerchantPaycriticalIdTest);

        }


        [Test, Order(3)]
        public void GetSubMerchant()
        {
            //Creating Client connection 
            RestClient restClient = new RestClient(b2BPOSAPIBaseURL);


            //Creating request to get data from server
            RestRequest restRequest = new RestRequest(Resources.getSubMerchant, Method.GET);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", b2BPOSAPIAuthorizationCode);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("vatNumber", testVatNumber);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);

            Console.WriteLine("response: " + restResponse.Content);
            Console.WriteLine("Vat Number1: " + testVatNumber);

            //get status code from received response
            int StatusCode = (int)restResponse.StatusCode;

            Console.WriteLine("get submerchant status code:" + StatusCode);

            // Verify that status code 200 should be displayed after successful getting sub merchant
            Assert.AreEqual(StatusCodes.statusCode200, StatusCode);


            //parse the json response
            var jObject = JObject.Parse(restResponse.Content);
            getSubMerchantId = (string)jObject["subMerchants"][0]["subMerchantId"];
            Assert.IsNotNull(getSubMerchantId, "SubMerchant ID should not be null in respose");
           
        }



        [Test, Order(4)]
        public void AddTerminal()
        {
            JObject jObjectbody = new JObject();
            jObjectbody = jsonData.addTerminalJsonData(getSubMerchantId);

            //Creating Client connection 
            RestClient restClient = new RestClient(b2BPOSAPIBaseURL);


            //Creating request to get data from server
            RestRequest restRequest = new RestRequest(Resources.addTerminal, Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", b2BPOSAPIAuthorizationCode);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);

            Console.WriteLine("response: " + restResponse.Content);

            //get status code from received response
            int StatusCode = (int)restResponse.StatusCode;

            Console.WriteLine("get submerchant status code:" + StatusCode);

            // Verify that status code 201 should be displayed after adding terminal
            Assert.AreEqual(StatusCodes.statusCode201, StatusCode);


            //parse the json response
            var jObject = JObject.Parse(restResponse.Content);
            channelID = jObject.GetValue("paycriticalId").ToString();
            Assert.IsNotNull(channelID, "channnel ID should not be null in respose");

        }



    }
}
