using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestingProject.Utils
{
    public static class Resources
    {
        // My Ticket API Resources
        public static string getPaymentResource = "/api/payment/";
        public static string addPaymentResource = "/api/payment/";
        public static string cancelPaymentResource = "/api/payment/cancel";

       

        //B2B Backoffice API Resources
        public static string addEntitiesResource = "/api/entities";
        public static string addDepositResource = "/api/deposit";

        //B2B POS API Resources
        public static string getSubMerchant = "/api/submerchants";
        public static string addTerminal = "/api/terminal";




    }
}
