using MarketplaceWebServiceOrders;
using MarketplaceWebServiceOrders.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace MwsOrders
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get Order List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListOrders_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = CommonValue.strServiceURL;
            MarketplaceWebServiceOrders.MarketplaceWebServiceOrders client = new MarketplaceWebServiceOrdersClient(
                                                                                    AccessKeyId,
                                                                                    SecretKeyId,
                                                                                    ApplicationName,
                                                                                    ApplicationVersion,
                                                                                    config);
            ListOrdersRequest request = new ListOrdersRequest();
            request.SellerId = SellerId;
            request.CreatedAfter = DateTime.Now.AddDays(-5);
            List<string> lstMarketplace = new List<string>();
            lstMarketplace.Add(MarketplaceId);
            request.MarketplaceId = lstMarketplace;
            request.MWSAuthToken = MWSAuthToken;

            ListOrdersResponse response = client.ListOrders(request);
            if (response.IsSetListOrdersResult())
            {
                ListOrdersResult listOrdersResult = response.ListOrdersResult;
                if (listOrdersResult.IsSetOrders())
                {
                    List<Order> orders = listOrdersResult.Orders;
                    foreach (Order order in orders)
                    {
                        strbuff += order.AmazonOrderId + System.Environment.NewLine;
                    }
                }
                txtListOrders.Text = strbuff;
            }
        }

        /// <summary>
        /// Get Next Order List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListOrderByNextToken_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = CommonValue.strServiceURL;
            MarketplaceWebServiceOrders.MarketplaceWebServiceOrders client = new MarketplaceWebServiceOrdersClient(
                                                                                    AccessKeyId,
                                                                                    SecretKeyId,
                                                                                    ApplicationName,
                                                                                    ApplicationVersion,
                                                                                    config);
            ListOrdersRequest request = new ListOrdersRequest();
            request.SellerId = SellerId;
            request.CreatedAfter = DateTime.Now.AddDays(-300);
            List<string> lstMarketplace = new List<string>();
            lstMarketplace.Add(MarketplaceId);
            request.MarketplaceId = lstMarketplace;
            request.MaxResultsPerPage = 20;

            ListOrdersResponse response = client.ListOrders(request);
            if (response.IsSetListOrdersResult())
            {
                ListOrdersResult listOrdersResult = response.ListOrdersResult;
                if (listOrdersResult.IsSetOrders())
                {
                    if (listOrdersResult.NextToken != null)
                    {
                        ListOrdersByNextTokenRequest request1 = new ListOrdersByNextTokenRequest();

                        request1.SellerId = SellerId;
                        request1.MWSAuthToken = MWSAuthToken;
                        request1.NextToken = listOrdersResult.NextToken;
                        ListOrdersByNextTokenResponse response1 = client.ListOrdersByNextToken(request1);
                        if (response1.IsSetListOrdersByNextTokenResult())
                        {
                            ListOrdersByNextTokenResult listOrdersByNextResult = response1.ListOrdersByNextTokenResult;
                            if (listOrdersByNextResult.IsSetOrders())
                            {
                                List<Order> orders = listOrdersByNextResult.Orders;
                                foreach (Order order in orders)
                                {
                                    strbuff += order.AmazonOrderId + System.Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                txtListOrderByNextToken.Text = strbuff;
            }

        }

        /// <summary>
        ///  Get Order Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetOrder_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = CommonValue.strServiceURL;
            MarketplaceWebServiceOrders.MarketplaceWebServiceOrders client = new MarketplaceWebServiceOrdersClient(
                                                                                    AccessKeyId,
                                                                                    SecretKeyId,
                                                                                    ApplicationName,
                                                                                    ApplicationVersion,
                                                                                    config);
            GetOrderRequest request = new GetOrderRequest();
            request.SellerId = SellerId;
            request.MWSAuthToken = MWSAuthToken;
            List<string> amazonorderId = new List<string>();
            amazonorderId.Add("Set OrderId");
            request.AmazonOrderId = amazonorderId;

            GetOrderResponse response = client.GetOrder(request);
            if (response.IsSetGetOrderResult())
            {
                List<Order> orders = response.GetOrderResult.Orders;
                foreach (Order order in orders)
                {
                    strbuff += "購入者：" + order.BuyerName + System.Environment.NewLine;
                }
            }
            txtGetOrder.Text = strbuff;
        }

        /// <summary>
        /// Get Order Item List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLIstOrderItems_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = CommonValue.strServiceURL;
            MarketplaceWebServiceOrders.MarketplaceWebServiceOrders client = new MarketplaceWebServiceOrdersClient(
                                                                                    AccessKeyId,
                                                                                    SecretKeyId,
                                                                                    ApplicationName,
                                                                                    ApplicationVersion,
                                                                                    config);
            ListOrderItemsRequest request = new ListOrderItemsRequest();
            request.SellerId = SellerId;
            request.AmazonOrderId = "Set Order ID";
            request.MWSAuthToken = MWSAuthToken;
            ListOrderItemsResponse response = client.ListOrderItems(request);
            if (response.IsSetListOrderItemsResult())
            {
                ListOrderItemsResult listOrderItemsResult = response.ListOrderItemsResult;
                if (listOrderItemsResult.IsSetOrderItems())
                {
                    List<OrderItem> orderItems = listOrderItemsResult.OrderItems;
                    foreach (OrderItem orderItem in orderItems)
                    {
                        strbuff += "商品名：" + orderItem.Title + System.Environment.NewLine;
                    }
                }
            }
            txtLIstOrderItems.Text = strbuff;
        }

        /// <summary>
        /// Get Next Order Item List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListOrderItemsByNextToken_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = CommonValue.strServiceURL;
            MarketplaceWebServiceOrders.MarketplaceWebServiceOrders client = new MarketplaceWebServiceOrdersClient(
                                                                                    AccessKeyId,
                                                                                    SecretKeyId,
                                                                                    ApplicationName,
                                                                                    ApplicationVersion,
                                                                                    config);
            ListOrderItemsRequest request = new ListOrderItemsRequest();
            request.SellerId = SellerId;
            request.AmazonOrderId = "Set OrderID";
            request.MWSAuthToken = MWSAuthToken;
            ListOrderItemsResponse response = client.ListOrderItems(request);
            if (response.IsSetListOrderItemsResult())
            {
                ListOrderItemsResult listOrderItemsResult = response.ListOrderItemsResult;
                if (listOrderItemsResult.NextToken != null)
                {
                    ListOrderItemsByNextTokenRequest request1 = new ListOrderItemsByNextTokenRequest();
                    request1.SellerId = SellerId;
                    request1.MWSAuthToken = MWSAuthToken;
                    request1.NextToken = listOrderItemsResult.NextToken;

                    ListOrderItemsByNextTokenResponse response1 = client.ListOrderItemsByNextToken(request1);
                    if (response1.IsSetListOrderItemsByNextTokenResult())
                    {
                        ListOrderItemsByNextTokenResult listOrderByNextItemsResult = response1.ListOrderItemsByNextTokenResult;
                        if (listOrderByNextItemsResult.IsSetOrderItems())
                        {
                            List<OrderItem> orderItems = listOrderItemsResult.OrderItems;
                            foreach (OrderItem orderItem in orderItems)
                            {
                                if (orderItem.IsSetOrderItemId())
                                {
                                    strbuff += "商品名：" + orderItem.Title + System.Environment.NewLine;
                                }
                            }
                        }
                    }
                }
            }
            txtLIstOrderItems.Text = strbuff;
        }

        /// <summary>
        /// Get Service Status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetServiceStatus_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = CommonValue.strServiceURL;
            MarketplaceWebServiceOrders.MarketplaceWebServiceOrders client = new MarketplaceWebServiceOrdersClient(
                                                                                    AccessKeyId,
                                                                                    SecretKeyId,
                                                                                    ApplicationName,
                                                                                    ApplicationVersion,
                                                                                    config);
            GetServiceStatusRequest request = new GetServiceStatusRequest();
            request.SellerId = SellerId;
            request.MWSAuthToken = MWSAuthToken;

            GetServiceStatusResponse response = client.GetServiceStatus(request);
            if (response.IsSetGetServiceStatusResult())
            {
                strbuff = "処理状況：" + response.GetServiceStatusResult.Status;
            }
            txtGetServiceStatus.Text = strbuff;
        }
    }
}
