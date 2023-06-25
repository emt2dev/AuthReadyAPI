using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price_Normal { get; set; }
        public double Price_Sale { get; set; }
        public double Price_Current { get; set; }
        public string ImageURL { get; set; }
        public string Company { get; set; }
        public string Modifiers { get; set; }
    }
}
