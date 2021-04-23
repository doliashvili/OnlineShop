namespace OnlineShop.Domain.CommonModels.Identity
{
    public class SignInResultCustom
    {
        public bool RequiresTwoFactor { get; set; }
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
    }
}