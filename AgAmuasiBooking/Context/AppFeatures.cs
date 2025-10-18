namespace AgAmuasiBooking.Context
{
    public interface IAppFeatures
    {
        public string AppName { get; set; }

        public string Key { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public DateTime Expiry { get; set; }

        public byte Hours { get; set; }
    }

    public class AppFeatures : IAppFeatures
    {
        public string AppName { get; set; }

        public string Key { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public DateTime Expiry { get; set; }

        public byte Hours { get; set; }

        public AppFeatures(IConfiguration config)
        {
            var con = config.GetSection("AppFeatures").Get<AppModel>();
            if (con is not null)
            {
                var date = DateTime.UtcNow;
                AppName = con.AppName;
                Key = con.Key;
                Audience = con.Audience;
                Issuer = con.Issuer;
                Expiry = date.AddHours(Hours);
                Hours = con.Hours;
            }
            else throw new Exception("Application features were not found in the store");
        }
    }

    public class AppModel
    {
        public required string AppName { get; set; }

        public required string Key { get; set; }

        public required string Audience { get; set; }

        public required string Issuer { get; set; }

        public required byte Hours { get; set; }
    }
}
