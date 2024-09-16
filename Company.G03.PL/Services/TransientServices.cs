namespace Company.G03.PL.Services
{
    public class TransientServices : ITransientServices
    {
        public Guid guid { get; set; }

        public TransientServices()
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
