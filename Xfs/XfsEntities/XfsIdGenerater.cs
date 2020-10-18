using System;

namespace Xfs
{
    public static class XfsIdGenerater
    {
        static int idCount = 1400;
        public static string GetId()
        {
            string tmId = "";
            idCount += 1;
            if (idCount > 4000)
            {
                idCount = 1400;
            }
            tmId = XfsTimerTool.IdCurrentTime() + idCount.ToString();
            return tmId;
        }
    }
}
