using System;
using RestSharp;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using APITestingProject.Utils;
using System.Text;

namespace APITestingProject.B2BBackofficeAPITest
{
    
    public class CreateConsumerMerchantSubMerchantTest : BaseClass
    {
        JsonData jsonData = new JsonData();
        string consumerPaycriticalId;
        string merchantPaycriticalId;
        string subMerchantPaycriticalId;

        [Test, Order(1)]
        public void CreateConsumer()
        {
            JObject jObjectbody = new JObject();
            JArray jArray = new JArray();
            jObjectbody=  jsonData.getConsumerJsonData();

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
            //Verify that status code 201 should be displayed after successful consumer creation
            Assert.AreEqual(StatusCodes.statusCode201, StatusCode, "Status code should be displayed 201");

            //parse the json response
            var jObject = JObject.Parse(restResponse.Content);
            consumerPaycriticalId = (string)jObject["processedEntities"][0]["paycriticalId"];
            Console.WriteLine("consumerPaycriticalId: " + consumerPaycriticalId);

        }


        [Test, Order(2)]
        public void CreateMerchant()
        {
            JObject jObjectbody = new JObject();
            jObjectbody = jsonData.getMerchantJsonData();

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
            //Verify that status code 201 should be displayed after successful merchant creation
            Assert.AreEqual(StatusCodes.statusCode201, StatusCode, "Status code should be displayed 201");

            //parse the json response
            var jObject = JObject.Parse(restResponse.Content);
            merchantPaycriticalId = (string)jObject["processedEntities"][0]["paycriticalId"];
            Console.WriteLine("merchantPaycriticalId: " + merchantPaycriticalId);

        }



        [Test,Order(3)]
        public void CreateSubMerchant()
        {
            JObject jObjectbody = new JObject();
            jObjectbody = jsonData.getSubMerchantJsonData(merchantPaycriticalId);

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
            subMerchantPaycriticalId = (string)jObject["processedEntities"][0]["paycriticalId"];
            Console.WriteLine("subMerchantPaycriticalId: " + subMerchantPaycriticalId);

        }



        [Test, Order(4)]
        public void Deposit()
        {
            JObject jObjectbody = new JObject();
            jObjectbody = jsonData.getDepositJsonData(consumerPaycriticalId);

            //Creating Client connection 
            RestClient restClient = new RestClient(b2BBackofficeAPIBaseURL);


            //Creating request to get data from server
            RestRequest restRequest = new RestRequest(Resources.addDepositResource, Method.POST);

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

            //parse the json+ response
            var jObject = JObject.Parse(restResponse.Content);
            Console.WriteLine("response: " + jObject.ToString());
            
            string processedDepositsValue = jObject.GetValue("processedDeposits").ToString();
            //Verify that deposit process successfuly
            Assert.AreEqual(processedDepositsValue, "1");



        }

    }
}
