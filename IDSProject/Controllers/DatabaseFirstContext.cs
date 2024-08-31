using DemoAPI.Models;

namespace DemoAPI.Controllers
{
    internal class DatabaseFirstContext
    {
        public object Users { get; internal set; }

        public static implicit operator DatabaseFirstContext(DatabaseServerContext v)
        {
            throw new NotImplementedException();
        }
    }
}