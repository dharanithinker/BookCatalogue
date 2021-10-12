using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookCatalogue.Models
{
    [DataContract]
    public class Book
    {        
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Authors { get; set; }
        [Required]
        [RegularExpression("[0-9]*[-| ][0-9]*[-| ][0-9]*[-| ][0-9]*[-| ][0-9]*", ErrorMessage = "Invalid ISBN Number")]
        public string ISBN { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public int? ModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }
    }
}
