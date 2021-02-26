using System.Text;
using X;
using UnityEngine;

namespace HP.Cmd
{
    internal class HPCmdToCreateBezierCurve : XLoggableCmd {

        // private constructor
        private HPCmdToCreateBezierCurve(XApp app) : base(app) {
        }

        public static bool execute(XApp app) {
            HPCmdToCreateBezierCurve cmd = new HPCmdToCreateBezierCurve(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            HPApp app = (HPApp)this.mApp;
            HPBezierCurve curve = new HPBezierCurve((HPApp)this.mApp);
            app.getBezierCurveMgr().addCurve(curve);
            app.getBezierCurveMgr().setCurCurve(curve);
            return true;
        }

        protected override string createLog() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            return sb.ToString();
        }
    }
}