using System.Text;
using X;
using UnityEngine;
using System.Collections.Generic;

namespace HP.Cmd
{
    internal class HPCmdToSetCurCurve : XLoggableCmd {
        // field
        HPHand mHand = null;        

        // private constructor
        private HPCmdToSetCurCurve(XApp app, HPHand hand) : base(app) {
            this.mHand = hand;
        }

        public static bool execute(XApp app, HPHand hand) {
            HPCmdToSetCurCurve cmd = new HPCmdToSetCurCurve(app, hand);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            HPApp app = (HPApp)this.mApp;
            List<HPControlPt> controlPts = app.getControlPtMgr().
                getControlPts();

            // find nearest control point
            Vector3 pinchPos = this.mHand.calcPinchPos();
            float sqrMinDistance = HPControlPt.MIN_DIST_PINCH;
            HPControlPt closestPt = null;
            foreach(HPControlPt cpt in controlPts) {
                Vector3 distance = cpt.getPos() - pinchPos;
                float sm = Vector3.SqrMagnitude(distance);
                if(sm < sqrMinDistance) {
                    sqrMinDistance = sm;
                    closestPt = cpt;
                }
            }
            
            // there is no near control point.
            if (closestPt == null) {
                return false;
            }
            
            // find curve has the closest point
            List<HPBezierCurve> bcs = app.getBezierCurveMgr().getCurves();
            foreach(HPBezierCurve bc in bcs) {
                if (bc.getContorlPts().Contains(closestPt)) {
                    app.getBezierCurveMgr().setCurCurve(bc);
                    return true;
                }
            }
            return false;
        }

        protected override string createLog() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            return sb.ToString();
        }
    }
}