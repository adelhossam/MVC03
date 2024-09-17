namespace Company.G03.PL.Services
{
    public interface IScopedServices
    {
        public Guid guid { get; set; }
        public string GetGuid();
    }
}
