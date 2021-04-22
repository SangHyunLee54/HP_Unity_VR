using UnityEngine;
using HP.AppObject;
using System.Collections.Generic;

namespace HP {
    public class HPPenHandleLine : HPAppNoGeom3D{
        // constants
        private static readonly float WIDTH = 0.005f;
        private static readonly Color COLOR = new Color(0.00f, 0.00f, 0.75f);

        // field
        HPApp mApp = null;

        public HPPenHandleLine(HPApp app) : base("Pen Handle Line") {
            this.mApp = app;
        }
        
        public void update() {
            while (this.getChildren().Count != 0) {
                HPAppObject child = this.getChildren()[0];
                this.removeChild(child);
                child.destroyGameObject();
            }

            HPPenPath curPath = mApp.getPenPathMgr().getCurPenPath();
            for (int i = 0; i < curPath.getCurves().Count; i++) {
                List<Vector3> pts1 = new List<Vector3>();
                List<Vector3> pts2 = new List<Vector3>();
                HPBezierCurve curve = curPath.getCurves()[i];
                HPPenPath.CurveState state = curPath.getCurveStates()[i];
                switch (state) {
                    case HPPenPath.CurveState.LINEAR:
                        break;

                    case HPPenPath.CurveState.QUADRATIC_START:
                        pts1.Add(curve.getContorlPts()[0].getPos());
                        pts1.Add(curve.getContorlPts()[1].getPos());
                        HPAppPolyline3D lineQuadStart = new HPAppPolyline3D(
                            "Connect Line", pts1, HPPenHandleLine.WIDTH,
                            HPPenHandleLine.COLOR);
                        addChild(lineQuadStart);
                        break;

                    case HPPenPath.CurveState.QUADRATIC_END:
                        pts1.Add(curve.getContorlPts()[1].getPos());
                        pts1.Add(curve.getContorlPts()[2].getPos());
                        HPAppPolyline3D lineQuadEnd = new HPAppPolyline3D(
                            "Connect Line", pts1, HPPenHandleLine.WIDTH,
                            HPPenHandleLine.COLOR);
                        addChild(lineQuadEnd);
                        break;

                    case HPPenPath.CurveState.CUBIC:
                        pts1.Add(curve.getContorlPts()[0].getPos());
                        pts1.Add(curve.getContorlPts()[1].getPos());
                        pts2.Add(curve.getContorlPts()[2].getPos());
                        pts2.Add(curve.getContorlPts()[3].getPos());
                        HPAppPolyline3D lineCubic1 = new HPAppPolyline3D(
                            "Connect Line", pts1, HPPenHandleLine.WIDTH,
                            HPPenHandleLine.COLOR);
                        HPAppPolyline3D lineCubic2 = new HPAppPolyline3D(
                            "Connect Line", pts2, HPPenHandleLine.WIDTH,
                            HPPenHandleLine.COLOR);
                        addChild(lineCubic1);
                        addChild(lineCubic2);
                        break;
                }
            }
        }
    }
}