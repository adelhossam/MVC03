
namespace Company.G03.PL.Services
{
    public class ScopedServices : IScopedServices
    {
        public Guid guid { get ; set; }

        public ScopedServices()
        {
            guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return guid.ToString();
        }
        public override string ToString()
        {
            return guid.ToString();
        }
    }
}
