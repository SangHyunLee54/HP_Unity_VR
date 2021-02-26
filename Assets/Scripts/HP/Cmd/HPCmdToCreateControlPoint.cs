using System.Text;
using X;
using UnityEngine;

namespace HP.Cmd
{
    internal class HPCmdToCreateControlPoint : XLoggableCmd {
        // fields
        HPHand mHand = null;

        // private constructor
        private HPCmdToCreateControlPoint(XApp app, HPHand hand) : base(app) {
            this.mHand = hand;
        }

        public static bool execute(XApp app, HPHand hand) {
            HPCmdToCreateControlPoint cmd = 
                new HPCmdToCreateControlPoint(app, hand);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            HPApp app = (HPApp)this.mApp;
            HPControlPt cp = new HPControlPt(mHand.calcPalmPos());
            app.getControlPtMgr().getControlPts().Add(cp);
            app.getBezierCurveMgr().getCurCurve().addControlPt(cp);
            //app.getConnectLine().update();
            app.getBezierCurveMgr().getCurCurve().update();
            app.getBezierCurveMgr().updatePointColor();
            return true;
        }

        protected override string createLog() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            return sb.ToString();
        }
    }
}