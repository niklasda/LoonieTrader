﻿using System;
using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.RestRequesters.v3
{
    public class OrdersRequester : RequesterBase, IOrdersRequester
    {
        public OrdersRequester(ISettings settings) : base(settings)
        {
        }

        public AccountOrdersResponse GetOrders(string accountId)
        {
            string urlAccountOrders = base.GetRestUrl("accounts/{0}/orders/");

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlAccountOrders, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var aor = JSON.Deserialize<AccountOrdersResponse>(input);
                    return aor;
                }
            }
        }

        public AccountPendingOrdersResponse GetPendingOrders(string accountId)
        {
            string urlPendingAccountOrders = base.GetRestUrl("accounts/{0}/pendingOrders/");

            using (WebClient wc = new WebClient())
            {
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


        public AccountPendingOrdersResponse GetOrderDetails(string accountId, string orderId)
        {
            string urlPendingAccountOrders = base.GetRestUrl("accounts/{0}/orders/{1}");

            throw new NotImplementedException();
        }

        public AccountCreateOrdersResponse PostCreateOrder(string accountId, AccountCreateOrdersResponse.OrderDefinition order)
        {
            string urlPendingAccountOrders = base.GetRestUrl("accounts/{0}/orders/");

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("Authorization", base.BearerApiKey);

                var orderJson = JSON.Serialize(order);
                var orderBytes = Encoding.UTF8.GetBytes(orderJson);
                //var reqparm = new System.Collections.Specialized.NameValueCollection();
                //reqparm.Add("order", orderJson);

                var responseBytes = wc.UploadData(string.Format(urlPendingAccountOrders, accountId), "POST", orderBytes);

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var aor = JSON.Deserialize<AccountCreateOrdersResponse>(input);
                    return aor;
                }
            }
        }

        public AccountCreateOrdersResponse PutCancelOrder(string accountId, string orderId)
        {
            string urlPendingAccountOrders = base.GetRestUrl("accounts/{0}/orders/{1}/cancel");
            throw new NotImplementedException();
        }
    }
}