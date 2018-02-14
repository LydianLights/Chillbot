using System;
using System.Threading.Tasks;

namespace ChillBot
{
    public class Program
    {
        static void Main(string[] args) => new Bot().StartupAsync().GetAwaiter().GetResult();
    }
}
