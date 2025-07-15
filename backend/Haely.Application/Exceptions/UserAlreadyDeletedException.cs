using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Exceptions
{
    public class UserAlreadyDeletedException : Exception
    {
        public UserAlreadyDeletedException() : base("Ce compte utilisateur a déjà été supprimé.") { }
    }
}
