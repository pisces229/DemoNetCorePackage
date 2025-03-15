namespace DemoFluentValidation.Requests
{
    public class CustomerRequest
    {
        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Forename { get; set; } = null!;
        public decimal Discount { get; set; }
        public string Address { get; set; } = null!;
    }
}
