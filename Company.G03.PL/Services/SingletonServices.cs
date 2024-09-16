namespace Company.G03.PL.Services
{
    public class SingletonServices : ISingletonServices
    {
        public Guid guid { get; set; }

        public SingletonServices()
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
