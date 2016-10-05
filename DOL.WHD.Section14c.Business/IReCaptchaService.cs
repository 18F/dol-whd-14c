namespace DOL.WHD.Section14c.Business
{
    public enum ReCaptchaValidationResult
    {
        Disabled,
        Success,
        InvalidResponse
    }
    public interface IReCaptchaService
    {
        ReCaptchaValidationResult ValidateResponse(string reCaptchaSecretKey, string clientReponse, string remoteIpAddress);
    }
}
