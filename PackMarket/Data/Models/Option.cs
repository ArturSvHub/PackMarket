using System.ComponentModel.DataAnnotations;

namespace PackMarket.Data.Models
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public List<OptionList>? OptionLists { get; set; }
    }
}
