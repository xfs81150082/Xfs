using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xfs;

namespace XfsServer
{
    class XfsBookerMysql : XfsSoulerdbMysql
    {
        public override void XfsAwake()
        {
            DatabaseFormName = "bookeritem";
            Console.WriteLine("TmBookerMysql-13:" + DatabaseFormName);

        }
        public XfsBookerMysql()
        {
            GetSoulerDBs();
        }
        bool isYes = false;
        private void GetSoulerDBs()
        {
            Dictionary<int, TmSoulerDB> dbs = GetTmSoulerDBsDict();

            Console.WriteLine("24:" + dbs.Count);

            if (dbs.Count > 0 && !isYes)
            {
                //XfsObjects.Bookers = dbs;
                isYes = true;
                //Console.WriteLine(XfsTimerTool.CurrentTime() + " TmBookerMysql-Bookers: " + TmObjects.Bookers.Count);
            }
            else
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " 没有角色");
            }
        }
             
    }
}