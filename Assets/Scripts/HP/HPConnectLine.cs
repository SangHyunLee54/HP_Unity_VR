using UnityEngine;
using HP.AppObject;
using System.Collections.Generic;

namespace HP {
    public class HPConnectLine : HPAppNoGeom3D{
        // constants
        private static readonly float WIDTH = 0.005f;
        private static readonly Color COLOR = new Color(0.00f, 0.00f, 0.75f);

        // field
        HPApp mApp = null;

        public HPConnectLine(HPApp app) : base("Connect Line") {
            this.mApp = app;
        }
        
        public void update() {
            List<Vector3> pts = new List<Vector3>();
            
            if (this.mApp.getBezierCurveMgr().getCurCurve() == null) {
                if(this.getChildren().Count != 0) {
                    HPAppObject child = this.getChildren()[0];
                    this.removeChild(child);
                    child.destroyGameObject();
                }
                return;
            }

            List<HPControlPt> cPts = this.mApp.getBezierCurveMgr().
                getCurCurve().getContorlPts();
            
            if (cPts.Count == 0) {
                return;
            }

            foreach(HPControlPt cPt in cPts) {
                pts.Add(cPt.getPos());
            }
            if (this.getChildren().Count != 0) {
                HPAppObject child = this.getChildren()[0];
                this.removeChild(child);
                child.destroyGameObject();
            }
            HPAppPolyline3D line = new HPAppPolyline3D("Connect Line", pts,
                HPConnectLine.WIDTH, HPConnectLine.COLOR);
            addChild(line);
        }
    }
}