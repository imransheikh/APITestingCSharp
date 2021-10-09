using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using APITestingProject.Utils;
using NUnit.Framework;

namespace APITestingProject.Utils
{
    class JsonData
    {

        CommonClass commonClass = new CommonClass();

        string ticketId = Guid.NewGuid().ToString();
        string ConsumerEntityType = "Consumer";
        string phoneNumber = "+3519199";

        string MerchantEntityType = "Merchant";
        string name = "APITest";
        string vatNumber;

        string SubMerchantEntityType = "SubMerchant";
        string iban = "ibanTest";
        int vatRate = 10;
        string postalCode = "3000";
        string merchantPaycriticalId;
        int transactionFee = 1;
        int settlementDays = 1;

        int amount = 10;
        int orderRef;
        string serialNumber;





        public JObject getConsumerJsonData()
        {

            int randomNumber= commonClass.getRandomNumber(5);
            string phoneNumber1 = phoneNumber + randomNumber.ToString();
            vatNumber = randomNumber.ToString();
            //Creating Json array
            JArray jsonArray = new JArray();
            //Creating Json object
            JObject jObjectbody = new JObject();
            jObjectbody.Add("ticketId", ticketId);
            jObjectbody.Add("vatNumber", vatNumber);
            jObjectbody.Add("entityType", ConsumerEntityType);
            jObjectbody.Add("phoneNumber", phoneNumber1);
            jsonArray.Add(jObjectbody);

            JObject jObjectbodyN = new JObject();
            jObjectbodyN["entities"] = jsonArray;
            Console.WriteLine(jObjectbodyN);

            return jObjectbodyN;
        }



        public JObject getMerchantJsonData()
        {

            int randomNumber = commonClass.getRandomNumber(5);
            string phoneNumber1 = phoneNumber + randomNumber.ToString();
            vatNumber = randomNumber.ToString();

            //Creating Json array
            JArray jsonArray = new JArray();
            //Creating Json object
            JObject jObjectbody = new JObject();
            jObjectbody.Add("ticketId", ticketId);
            jObjectbody.Add("entityType", MerchantEntityType);
            jObjectbody.Add("name", name);
            jObjectbody.Add("vatNumber", vatNumber);
           
            jsonArray.Add(jObjectbody);

            JObject jObjectbodyN = new JObject();
            jObjectbodyN["entities"] = jsonArray;
            Console.WriteLine(jObjectbodyN);

            return jObjectbodyN;
        }



        public JObject getMerchantJsonDataForSubMerchantAndTerminals(String vatNumber)
        {
            //Creating Json array
            JArray jsonArray = new JArray();
            //Creating Json object
            JObject jObjectbody = new JObject();
            jObjectbody.Add("ticketId", ticketId);
            jObjectbody.Add("entityType", MerchantEntityType);
            jObjectbody.Add("name", name);
            jObjectbody.Add("vatNumber", vatNumber);

            jsonArray.Add(jObjectbody);

            JObject jObjectbodyN = new JObject();
            jObjectbodyN["entities"] = jsonArray;
            Console.WriteLine(jObjectbodyN);

            return jObjectbodyN;
        }

        public JObject getSubMerchantJsonData(string merchantPayId)
        {
            merchantPaycriticalId = merchantPayId;
            int randomNumber = commonClass.getRandomNumber(5);
            string ticketIdForSubMerchant = randomNumber.ToString();
            vatNumber = randomNumber.ToString();

            //Creating Json array
            JArray jsonArray = new JArray();
            //Creating Json object
            JObject jObjectbody = new JObject();
            jObjectbody.Add("ticketId", ticketIdForSubMerchant);
            jObjectbody.Add("entityType", SubMerchantEntityType);
            jObjectbody.Add("name", name);
            jObjectbody.Add("iban", iban);
            jObjectbody.Add("vatRate", vatRate);
            jObjectbody.Add("postalCode", postalCode);
            jObjectbody.Add("merchantPaycriticalId", merchantPaycriticalId);
            jObjectbody.Add("transactionFee", transactionFee);
            jObjectbody.Add("settlementDays", settlementDays);


            jsonArray.Add(jObjectbody);

            JObject jObjectbodyN = new JObject();
            jObjectbodyN["entities"] = jsonArray;
            Console.WriteLine(jObjectbodyN);

            return jObjectbodyN;
        }



        public JObject addTerminalJsonData(string subMerchantId)
        {
            int randomNumber = commonClass.getRandomNumber(5);
            serialNumber = randomNumber.ToString();
            
            //Creating Json object
            JObject jObjectbody = new JObject();
            jObjectbody.Add("subMerchantId", subMerchantId);
            jObjectbody.Add("serialNumber", serialNumber);
 
            return jObjectbody;
        }


        public JObject getDepositJsonData(string consumerPayId)
        {
            string consumerPaycriticalId = consumerPayId;
            int randomNumber = commonClass.getRandomNumber(5);
            orderRef = randomNumber;

            //Creating Json array
            JArray jsonArray = new JArray();
            //Creating Json object
            JObject jObjectbody = new JObject();
            jObjectbody.Add("paycriticalId", consumerPaycriticalId);
            jObjectbody.Add("orderRef", orderRef);
            jObjectbody.Add("amount", amount);


            jsonArray.Add(jObjectbody);

            JObject jObjectbodyN = new JObject();
            jObjectbodyN["deposits"] = jsonArray;
            Console.WriteLine(jObjectbodyN);

            return jObjectbodyN;
        }

    }
}
