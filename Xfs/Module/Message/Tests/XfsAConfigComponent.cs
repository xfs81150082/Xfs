using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public interface ISerializeToEntity
    {
    }
    public abstract class XfsAConfigComponent : XfsComponent, ISerializeToEntity
    {
    }
}
