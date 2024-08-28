using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace CarSelling.Services
{
    public class PayPalService
    {
        private readonly PayPalHttpClient _client;

        public PayPalService(IConfiguration configuration)
        {
            var clientId = configuration["PayPal:ClientId"];
            var clientSecret = configuration["PayPal:ClientSecret"];
            var mode = configuration["PayPal:Mode"];

            PayPalEnvironment environment = null;

            if (mode == "live")
            {
                environment = new LiveEnvironment(clientId, clientSecret);
            }
            else if (mode == "sandbox")
            {
                environment = new SandboxEnvironment(clientId, clientSecret);
            }

            _client = new PayPalHttpClient(environment);
        }

        public async Task<string> CreateOrder(decimal amount, string returnUrl, string cancelUrl)
        {
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(new OrderRequest
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest
                    {
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                            CurrencyCode = "EUR",
                            Value = amount.ToString("F2")
                        }
                    }
                },
                ApplicationContext = new ApplicationContext
                {
                    ReturnUrl = returnUrl,
                    CancelUrl = cancelUrl
                }
            });

            var response = await _client.Execute(request);
            var result = response.Result<Order>();

            return result.Id;
        }

        public async Task<Order> GetOrderDetails(string orderId)
        {
            var request = new OrdersGetRequest(orderId);
            var response = await _client.Execute(request);
            return response.Result<Order>();
        }
    }
}
