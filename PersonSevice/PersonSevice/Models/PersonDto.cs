namespace PersonSevice.Models
{
    public class PersonDto : Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Coche Coche { get; set; }
    }
}