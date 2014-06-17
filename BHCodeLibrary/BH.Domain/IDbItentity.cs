using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Domain
{
    /// <summary>
    /// Interface that allows access to the identity property for database objects
    /// </summary>
    public interface IDbItentity
    {
        int Id { get; }
    }
}
