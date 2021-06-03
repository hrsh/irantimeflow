namespace Irantimeline.Helpers
{
    public static class AccountProviderExtensions
    {
        public static string ToProviderName(this string provider) =>
            provider switch
            {
                "Google" => "گوگل",
                "Microsoft" => "مایکروسافت",
                _ => ""
            };
    }
}
