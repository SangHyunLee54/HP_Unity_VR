using System.Text;
using X;
using UnityEngine;
using System.Collections.Generic;

namespace HP.Cmd
{
    internal class HPCmdToCreateBezierSurface : XLoggableCmd {

        // private constructor
        private HPCmdToCreateBezierSurface(XApp app) : base(app) {
            HPApp HP = (HPApp)this.mApp;
        }

        public static bool execute(XApp app) {
            HPCmdToCreateBezierSurface cmd = 
                new HPCmdToCreateBezierSurface(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            HPApp app = (HPApp)this.mApp;
            HPBezierSurface surface = new HPBezierSurface((HPApp)this.mApp);
            app.getBezierSurfaceMgr().addSurface(surface);
            app.getBezierSurfaceMgr().setCurSurface(surface);
            return true;
        }

        protected override string createLog() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            return sb.ToString();
        }
    }
}