namespace Api.Dto.Get
{
    public class ProductDtoGet
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public int CategoryID { get; set; }
        public string CategoryDescription { get; set; }
    }
}
