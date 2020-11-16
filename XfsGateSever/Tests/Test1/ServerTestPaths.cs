using System;
using System.Collections;
using System.Diagnostics;
using Xfs;

namespace XfsGateSever
{
    class ServerTestPaths : XfsComponent
    {
        XfsGrid[,] grids { get; set; }
        XfsGrid start { get; set; }
        XfsGrid goal { get; set; }
        int row = 100;
        int cilumn = 100;
        int time = 0;
        int resTime = 120;
        ArrayList Paths { get; set; } = new ArrayList();
        bool iscan = true;
        Stopwatch TmTime = new Stopwatch();
        //TmSoulerItem SoulerItem = new TmSoulerItem();

    }
}