using System.Text;
using X;
using UnityEngine;

namespace HP.Cmd
{
    internal class HPCmdToMovePenPoint : XLoggableCmd {
        // fields
        HPHand mHand = null;
        HPControlPt mPt = null;

        // private constructor
        private HPCmdToMovePenPoint(XApp app, HPHand hand, HPControlPt pt) :
            base(app) {
            this.mHand = hand;
            this.mPt = pt;
        }

        public static bool execute(XApp app, HPHand hand, HPControlPt pt) {
            HPCmdToMovePenPoint cmd = 
                new HPCmdToMovePenPoint(app, hand, pt);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            HPApp app = (HPApp)this.mApp;
            this.mPt.move(this.mHand.calcPinchPos());
            app.getPenPathMgr().getCurPenPath().update();
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