using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    [XfsMessage((ushort)XfsSenceType.Gate)]
    public class G4C_Test1Handler : XfsAMHandler<T2M_CreateUnit>
    {
        protected override void Run(XfsSession session, T2M_CreateUnit message)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " Test1Handler: " + message.RolerId);
        }
    }
}
