namespace Company.G03.PL.Services
{
    public interface ITransientServices
    {
        public Guid guid { get; set; }
        public string GetGuid();
    }
}
