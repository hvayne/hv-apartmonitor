using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartmentCrawler.Entities
{
    public class Product
    {
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("makler_id")]
        public string MaklerId { get; set; }

        [JsonProperty("has_logo")]
        public string HasLogo { get; set; }

        [JsonProperty("makler_name")]
        public string MaklerName { get; set; }

        [JsonProperty("loc_id")]
        public string LocId { get; set; }

        [JsonProperty("street_address")]
        public string StreetAddress { get; set; }

        [JsonProperty("yard_size")]
        public string YardSize { get; set; }

        [JsonProperty("yard_size_type_id")]
        public string YardSizeTypeId { get; set; }

        [JsonProperty("submission_id")]
        public string SubmissionId { get; set; }

        [JsonProperty("adtype_id")]
        public string AdtypeId { get; set; }

        [JsonProperty("product_type_id")]
        public string ProductTypeId { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("photo_ver")]
        public string PhotoVer { get; set; }

        [JsonProperty("photos_count")]
        public string PhotosCount { get; set; }

        [JsonProperty("area_size_value")]
        public string AreaSizeValue { get; set; }

        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("order_date")]
        public string OrderDate { get; set; }

        [JsonProperty("price_type_id")]
        public string PriceTypeId { get; set; }

        [JsonProperty("vip")]
        public string Vip { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("estate_type_id")]
        public string EstateTypeId { get; set; }

        [JsonProperty("area_size")]
        public string AreaSize { get; set; }

        [JsonProperty("area_size_type_id")]
        public string AreaSizeTypeId { get; set; }

        [JsonProperty("comment")]
        [NotMapped]
        public object Comment { get; set; }

        [JsonProperty("map_lat")]
        public string MapLat { get; set; }

        [JsonProperty("map_lon")]
        public string MapLon { get; set; }

        [JsonProperty("l_living")]
        public string LLiving { get; set; }

        [JsonProperty("special_persons")]
        public string SpecialPersons { get; set; }

        [JsonProperty("rooms")]
        public string Rooms { get; set; }

        [JsonProperty("bedrooms")]
        public string Bedrooms { get; set; }

        [JsonProperty("floor")]
        public string Floor { get; set; }

        [JsonProperty("parking_id")]
        public string ParkingId { get; set; }

        [JsonProperty("canalization")]
        public string Canalization { get; set; }

        [JsonProperty("water")]
        public string Water { get; set; }

        [JsonProperty("road")]
        public string Road { get; set; }

        [JsonProperty("electricity")]
        public string Electricity { get; set; }

        [JsonProperty("owner_type_id")]
        public string OwnerTypeId { get; set; }

        [JsonProperty("osm_id")]
        public string OsmId { get; set; }

        [JsonProperty("name_json")]
        public string NameJson { get; set; }

        [JsonProperty("pathway_json")]
        public string PathwayJson { get; set; }
        public string HomeSelfie { get; set; }

        [JsonProperty("seo_title_json")]
        public string SeoTitleJson { get; set; }

        [JsonProperty("seo_name_json")]
        public string SeoNameJson { get; set; }
    }
}
