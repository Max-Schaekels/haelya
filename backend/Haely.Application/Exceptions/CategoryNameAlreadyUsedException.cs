using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Exceptions
{
    public class CategoryNameAlreadyUsedException : Exception
    {
        public CategoryNameAlreadyUsedException() : base("Le nom de la catégorie est déjà utilisée.") { }
    }
}
