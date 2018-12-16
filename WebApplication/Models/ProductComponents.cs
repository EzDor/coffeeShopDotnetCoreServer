using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication.Models
{
    public partial class ProductComponents
    {
        [JsonIgnore] [ForeignKey("Products")] public int ProductId { get; set; }

        [JsonIgnore]
        [ForeignKey("Components")]
        public int ProductComponentsId { get; set; }

        [JsonIgnore] public Products Product { get; set; }

        [JsonProperty("productComponents")]
        public Components ProductComponentsNavigation { get; set; }
    }
}