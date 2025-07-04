using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Security
{
    public interface IPasswordHasher
    {
        public string Hash(string password);

        public bool Verify(string password, string hashed);
    }
}
