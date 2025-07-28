using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Exceptions
{
    public class BrandNameAlreadyUsedException : Exception
    {
        public BrandNameAlreadyUsedException() : base("Le nom de la marque est déjà utilisé") { }
    }
}
