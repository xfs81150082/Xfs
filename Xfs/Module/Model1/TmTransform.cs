using System;
using System.Collections.Generic;

namespace Xfs
{
    [Serializable]
    public class TmTransform : XfsComponent
    {
        public double px { get; set; } = 0;
        public double py { get; set; } = 0;
        public double pz { get; set; } = 0;
        public double ax { get; set; } = 0;
        public double ay { get; set; } = 0;
        public double az { get; set; } = 0;
        public TmTransform() { }
        public TmTransform(double px, double py, double pz)
        {
            this.px = px;
            this.py = py;
            this.pz = pz;
        }
        public TmTransform(double px, double py, double pz, double ay)
        {
            this.px = px;
            this.py = py;
            this.pz = pz;
            this.ay = ay;
        }
        public TmTransform(double px, double py, double pz, double ax, double ay, double az)
        {
            this.px = px;
            this.py = py;
            this.pz = pz;
            this.ax = ax;
            this.ay = ay;
            this.az = az;
        }
    }
}
