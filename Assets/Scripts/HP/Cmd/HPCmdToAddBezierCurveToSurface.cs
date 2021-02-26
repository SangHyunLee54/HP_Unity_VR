using System.Text;
using X;
using UnityEngine;

namespace HP.Cmd
{
    internal class HPCmdToAddBezierCurveToSurface : XLoggableCmd {

        // fields
        private HPBezierSurface mSurface = null;
        private HPBezierCurve mCurve = null;

        // private constructor
        private HPCmdToAddBezierCurveToSurface(XApp app, HPBezierSurface bs,
            HPBezierCurve bc) : base(app) {
            this.mSurface = bs;
            this.mCurve = bc;
        }

        public static bool execute(XApp app, HPBezierSurface bs,
            HPBezierCurve bc) {
            HPCmdToAddBezierCurveToSurface cmd = 
                new HPCmdToAddBezierCurveToSurface(app, bs, bc);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            this.mSurface.addCurve(this.mCurve);
            return true;
        }

        protected override string createLog() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            return sb.ToString();
        }
    }
}