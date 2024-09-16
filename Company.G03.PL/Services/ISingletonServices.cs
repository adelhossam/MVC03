namespace Company.G03.PL.Services
{
    public interface ISingletonServices
    {
        public Guid guid { get; set; }
        public string GetGuid();
    }
}
