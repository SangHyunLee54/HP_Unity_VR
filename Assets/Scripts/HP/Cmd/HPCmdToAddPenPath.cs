using System.Text;
using X;
using UnityEngine;
using System.Collections.Generic;

namespace HP.Cmd
{
    internal class HPCmdToAddPenPath : XLoggableCmd {

        // fields
        private List<HPControlPt> newPts = null;

        // private constructor
        private HPCmdToAddPenPath(XApp app, List<HPControlPt> pts) : base(app) {
            this.newPts = pts;
        }

        public static bool execute(XApp app, List<HPControlPt> pt) {
            HPCmdToAddPenPath cmd = new HPCmdToAddPenPath(app,
                pt);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            HPApp app = (HPApp)this.mApp;
            HPPenPath path = app.getPenPathMgr().getCurPenPath();
            if (newPts.Count == 1) {
                path.addPoint(newPts[0]);
                app.getPenHandleLine().update();
                return true;
            } else if (newPts.Count == 2) {
                path.addCurve(newPts[0], newPts[1]);
                app.getPenHandleLine().update();
                return true;
            }
            app.getPenHandleLine().update();
            return false;
        }

        protected override string createLog() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            return sb.ToString();
        }
    }
}