using IDSProject.core.Models;
using IDSProject.Models;

namespace DemoAPI.Controllers
{
    internal class DatabaseFirstContext
    {
        public object? Users { get; internal set; }
        public object? Members { get; internal set; }
        public object Events { get; internal set; }
        public object LookUps { get; internal set; }
        public object Guides { get; internal set; }
        public static implicit operator DatabaseFirstContext(DatabaseServerContext v)
        {
            throw new NotImplementedException();
        }
    }
}