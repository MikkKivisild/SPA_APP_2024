using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class People
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Age {  get; set; }
    }
}
