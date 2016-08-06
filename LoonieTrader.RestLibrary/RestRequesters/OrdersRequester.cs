﻿using System;
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

        public OrdersResponse GetOrders(string accountId)
        {
            string urlOrders = base.GetRestUrl("accounts/{0}/orders/");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
             //   wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlOrders, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var aor = JSON.Deserialize<OrdersResponse>(input);
                    return aor;
                }
            }
        }

        public OrdersPendingResponse GetPendingOrders(string accountId)
        {
            string urlPendingOrders = base.GetRestUrl("accounts/{0}/pendingOrders/");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
               // wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlPendingOrders, accountId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var aor = JSON.Deserialize<OrdersPendingResponse>(input);
                    return aor;
                }
            }
        }


        public OrderDetailsResponse GetOrderDetails(string accountId, string orderId)
        {
            string urlOrderDetails = base.GetRestUrl("accounts/{0}/orders/{1}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
              //  wc.Headers.Add("Authorization", base.BearerApiKey);

                var responseBytes = wc.DownloadData(string.Format(urlOrderDetails, accountId, orderId));

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var aor = JSON.Deserialize<OrderDetailsResponse>(input);
                    return aor;
                }
            }
        }

        public OrderCreateResponse PostCreateOrder(string accountId, OrderCreateResponse.OrderDefinition order)
        {
            string urlCreateOrder = base.GetRestUrl("accounts/{0}/orders/");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
              //  wc.Headers.Add("Content-Type", "application/json");
              //  wc.Headers.Add("Authorization", base.BearerApiKey);

                var orderJson = JSON.Serialize(order, new Options(excludeNulls:true));
                var orderBytes = Encoding.UTF8.GetBytes(orderJson);

                var responseBytes = wc.UploadData(string.Format(urlCreateOrder, accountId), "POST", orderBytes);

                var responseString = Encoding.UTF8.GetString(responseBytes);

                using (var input = new StringReader(responseString))
                {
                    var aor = JSON.Deserialize<OrderCreateResponse>(input);
                    return aor;
                }
            }
        }

        public OrderCreateResponse PutCancelOrder(string accountId, string orderId)
        {
            string urlCancelOrder = base.GetRestUrl("accounts/{0}/orders/{1}/cancel");
            throw new NotImplementedException();
        }
    }
}