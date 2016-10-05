using RestSharp;

namespace DOL.WHD.Section14c.Business.Services
{
    public class ReCaptchaService : IReCaptchaService
    {
        private readonly IRestClient _restClient;

        public ReCaptchaService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public ReCaptchaValidationResult ValidateResponse(string reCaptchaSecretKey, string clientReponse, string remoteIpAddress)
        {
            if(string.IsNullOrEmpty(reCaptchaSecretKey))
                return ReCaptchaValidationResult.Disabled;

            var request = new RestRequest();
            request.AddParameter("secret", reCaptchaSecretKey);
            request.AddParameter("response", clientReponse);
            request.AddParameter("remoteip", remoteIpAddress);

            var response = _restClient.Execute(request);

            if (string.IsNullOrEmpty(response?.Content))
                return ReCaptchaValidationResult.InvalidResponse;

            dynamic json = Newtonsoft.Json.Linq.JObject.Parse(response.Content);

            return json.success.Value ? ReCaptchaValidationResult.Success : ReCaptchaValidationResult.InvalidResponse;
            
        }
    }
}
