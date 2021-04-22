using System.Text;
using X;
using UnityEngine;
using System.Collections.Generic;

namespace HP.Cmd
{
    internal class HPCmdToCreatePenPath : XLoggableCmd {

        // fields
        private List<Vector3> startPt = null;

        // private constructor
        private HPCmdToCreatePenPath(XApp app, List<Vector3> pt) : base(app) {
            this.startPt = pt;
        }

        public static bool execute(XApp app, List<Vector3> pt) {
            HPCmdToCreatePenPath cmd = new HPCmdToCreatePenPath(app,
                pt);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            HPApp app = (HPApp)this.mApp;
            HPPenPath path = new HPPenPath((HPApp)this.mApp, this.startPt);
            app.getPenPathMgr().addPenPath(path);
            app.getPenPathMgr().setCurPenPath(path);
            app.getPenHandleLine().update();
            return true;
        }

        protected override string createLog() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            return sb.ToString();
        }
    }
}