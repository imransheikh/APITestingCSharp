using System;
using RestSharp;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using APITestingProject.Utils;
using System.Threading;

namespace APITestingProject.MyTicketAPITest
{
    [TestFixture]
    public class Scenario2_AddGetCancelPaymentPollingTrue : BaseClass
    {
        int amount = 100;
        string orderReference = Guid.NewGuid().ToString();
        string phoneNumber = "+351919999999";
        string changeId = "C3651892-DCB6-454A-B275-F251BD3A4F7F";
        //channel ID
        Boolean longPolling = true;

        static string paymentId;

        
        [Test, Order(1)]
        public void AddPayment()
        {
            //Creating Json object
            JObject jObjectbody = new JObject();
            jObjectbody.Add("amount", amount);
            jObjectbody.Add("orderRef", orderReference);
            jObjectbody.Add("phoneNumber", phoneNumber);
            jObjectbody.Add("channelId", changeId);
            jObjectbody.Add("longPolling", longPolling);


            //Creating Client connection 
            RestClient restClient = new RestClient(myTicketAPIBaseURL);


            //Creating request to get data from server
            RestRequest restRequest = new RestRequest(Resources.getPaymentResource, Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", myTicketAPIAuthorizationCode);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);
            //Thread.Sleep(62000);


            //get status code from received response
            int StatusCode = (int)restResponse.StatusCode;
            //Verify that status code should be displayed 200 after successful
            Assert.AreEqual(StatusCodes.statusCode200, StatusCode, "Status code should be displayed 200");

            //parse the json response
            var jObject = JObject.Parse(restResponse.Content);

            //Verify status complete message
            Console.WriteLine("Add payment Response: " + restResponse.Content);
            paymentId = jObject.GetValue("paymentId").ToString();
            string statusMessage = jObject.GetValue("status").ToString();
            Console.WriteLine("Payment id: " + paymentId);
            Assert.AreEqual("Expired",statusMessage,"Status message should be displayed 'expired'");


        }



        [Test, Order(2)]
        public void getPayment()
        {
          
            //Creating Client connection 
            RestClient restClient = new RestClient(myTicketAPIBaseURL);

            //Creating request to get data from server
            RestRequest restRequest = new RestRequest(Resources.getPaymentResource + paymentId, Method.GET);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", myTicketAPIAuthorizationCode);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);

            // get status code from received response
            int StatusCode = (int)restResponse.StatusCode;

            //Verify that status code should be displayed 200 after successful
            Assert.AreEqual(StatusCodes.statusCode200, StatusCode, "Status code should be displayed 200");

            //parse the json response
            var jObject = JObject.Parse(restResponse.Content);

            Console.WriteLine("Response: " + restResponse.Content);

            //Verify status complete message
            string statusMessage = jObject.GetValue("status").ToString();
            Assert.AreEqual("Expired", statusMessage, "Status message should be displayed 'Completed'");
        }


        [Test, Order(3)]
        public void cancelPayment()
        {
            //Creating Json object
            JObject jObjectbody = new JObject();
            jObjectbody.Add("paymentId", paymentId);


            //Creating Client connection 
            RestClient restClient = new RestClient(myTicketAPIBaseURL);


            //Creating request to get data from server
            RestRequest restRequest = new RestRequest(Resources.cancelPaymentResource, Method.PUT);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", myTicketAPIAuthorizationCode);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);

            Console.WriteLine("Cancel Response: " + restResponse.Content + " :"+ (int)restResponse.StatusCode);

            // Extracting output data from received response
            string response = restResponse.Content;
            int StatusCode = (int)restResponse.StatusCode;

            //Verify that status code should be displayed 403. Beucause you can not 
            // cancel the payment which is already expired
            Assert.AreEqual(StatusCodes.statusCode403, StatusCode, "Status code should be displayed 200");


            //parse the json response
            var jObject = JObject.Parse(restResponse.Content);

            Console.WriteLine("Response: " + restResponse.Content);

            //Verify the message
            string message = jObject.GetValue("message").ToString();
            Assert.IsTrue( message.Contains("The payment could not be canceled. Current state Expired"));

            
        }



    }

}
