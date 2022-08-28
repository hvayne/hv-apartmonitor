using Newtonsoft.Json;

namespace ApartmentCrawler.Entities
{
    public class User
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("gender_id")]
        public string GenderId { get; set; }

        [JsonProperty("personal_data_agreement")]
        public string PersonalDataAgreement { get; set; }

        [JsonProperty("AgreeTBCTerms")]
        public string AgreeTBCTerms { get; set; }
    }   
}
