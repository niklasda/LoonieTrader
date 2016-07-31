using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.RestRequesters
{
    public class OrdersRequester : RequesterBase, IOrdersRequester
    {
        public OrdersRequester(ISettings settings) : base(settings)
        {
        }

        public AccountOrdersResponse GetOrders(string accountId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/orders/");

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var aor = JSON.Deserialize<AccountOrdersResponse>(input);
                return aor;
            }
        }

        public AccountPendingOrdersResponse GetPendingOrders(string accountId)
        {
            string urlPendingAccountOrders = base.GetRestUrl("accounts/{0}/pendingOrders/");

            WebClient wc = new WebClient();
            wc.Headers.Add("Authorization", base.BearerApiKey);

            var responseBytes = wc.DownloadData(string.Format(urlPendingAccountOrders, accountId));

            var responseString = Encoding.UTF8.GetString(responseBytes);

            using (var input = new StringReader(responseString))
            {
                var aor = JSON.Deserialize<AccountPendingOrdersResponse>(input);
                return aor;
            }
        }
    }
}