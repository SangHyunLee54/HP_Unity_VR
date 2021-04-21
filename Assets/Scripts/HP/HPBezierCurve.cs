using UnityEngine;
using HP.AppObject;
using System.Collections.Generic;

namespace HP {
    public class HPBezierCurve : HPAppNoGeom3D{
        // constants
        private static readonly float WIDTH = 0.005f;
        private static readonly Color COLOR = new Color(0.75f, 0.75f, 0.75f);
        private static readonly float MAX_DISTANCE_TO_RENDER = 0.000001f;

        // field
        private HPApp mApp = null;
        private List<HPControlPt> mControlPts= null;
        public List<HPControlPt> getContorlPts() {
            return this.mControlPts;
        }
        public int getDegree() {
            return this.mControlPts.Count - 1;
        }

        private HPConnectLine mConnectLine = null;
        public HPConnectLine getConnectLine() {
            return this.mConnectLine;
        }

        public HPBezierCurve(HPApp app) : base("Bezier Curve") {
            this.mApp = app;
            this.mControlPts = new List<HPControlPt>();
            
        }

        public void addControlPt(HPControlPt cPt) {
            this.mControlPts.Add(cPt);
        }
        
        public void update() {
            List<Vector3> pts = new List<Vector3>();
            List<HPControlPt> cPts = this.mControlPts;
            
            if (cPts.Count < 3) {
                return;
            }

            foreach(HPControlPt cPt in cPts) {
                pts.Add(cPt.getPos());
            }
            List<Vector3> bPts = this.calcCurvePts(pts);
            
            if (this.getChildren().Count != 0) {
                HPAppObject child = this.getChildren()[0];
                this.removeChild(child);
                child.destroyGameObject();
            }
            HPAppPolyline3D line = new HPAppPolyline3D("Bezier Line", bPts,
                HPBezierCurve.WIDTH, HPBezierCurve.COLOR);
            addChild(line);
        }

        // methods for calculate distance btw point and line segment.
        // P is point, B is point on line, M is direction vector
        // B + t*M = closest point on line from P
        private float getT0(Vector3 P, Vector3 B, Vector3 M) {
            float t0 =  Vector3.Dot(M, P - B) / Vector3.Dot(M, M);
            return t0;
        }

        private float getSqauredDistance(Vector3 P, Vector3 B, Vector3 M) {
            Vector3 diff = P - B;
            float t = Vector3.Dot(M, diff);

            if (t > 0) {
                float dotMM = Vector3.Dot(M, M);

                if (t < dotMM) {
                    t = t/dotMM;
                    diff = diff - t * M;
                } else {
                    t = 1;
                    diff = diff - M;
                }
            } else {
                t = 0;
            }

            return Vector3.Dot(diff, diff);
        }

        private bool isEnoughToRender(List<Vector3> pts) {
            if (pts.Count < 3) {
                return true;
            }
            Vector3 B = pts[0];
            Vector3 M = pts[pts.Count - 1] - B;

            for (int i = 0; i < pts.Count; i++) {
                Vector3 P = pts[i];
                if (getSqauredDistance(P, B, M) > 
                    HPBezierCurve.MAX_DISTANCE_TO_RENDER) {
                    // Debug.Log(getSqauredDistance(P, B, M));
                    return false;
                }
            }
            return true;
        }

        private List<Vector3> calcCurvePts(List<Vector3> pts) {

            List<Vector3> result = new List<Vector3>();
            if (isEnoughToRender(pts)) {
                result.Add(pts[0]);
                result.Add(pts[pts.Count - 1]);
                return pts;
            }
            
            List<List<Vector3>> middlePts = new List<List<Vector3>>();
            middlePts.Add(pts);

            for (int i = 1; i < pts.Count; i++) {
                List<Vector3> mPts = new List<Vector3>();
                for (int j = 1; j < pts.Count - i + 1; j ++) {
                    mPts.Add(
                        (middlePts[i - 1][j] + middlePts[i - 1][j - 1]) / 2);
                }
                middlePts.Add(mPts);
            }

            List<Vector3> firstControlPts = new List<Vector3>();
            List<Vector3> secondControlPts = new List<Vector3>();
            for (int i = 0; i < pts.Count; i ++) {
                firstControlPts.Add(middlePts[i][0]);
                secondControlPts.Add(middlePts[pts.Count - 1 - i][i]);
            }

            firstControlPts = calcCurvePts(firstControlPts);
            secondControlPts = calcCurvePts(secondControlPts);

            secondControlPts.RemoveAt(0);



            result.AddRange(firstControlPts);
            result.AddRange(secondControlPts);

            return result;
        }

        // degree elevation
        public void degreeElevate() {
            List<HPControlPt> elevatedList = new List<HPControlPt>();
            elevatedList.Add(this.mControlPts[0]);
            int n = this.mControlPts.Count - 1;
            for (int i = 1; i <= n; i++) {
                Vector3 newPos = Vector3.zero;
                newPos += this.mControlPts[i - 1].getPos() * (i / (n + 1));
                newPos += this.mControlPts[i].getPos() * (1 - (i / (n + 1)));
                HPControlPt newCPt = new HPControlPt(newPos);
                elevatedList.Add(newCPt);
            }
            elevatedList.Add(mControlPts[n]);
            for (int i = 0; i < this.mControlPts.Count; i++) {
                this.mControlPts[i].destroyGameObject();
                this.mApp.getControlPtMgr().getControlPts().
                    Remove(this.mControlPts[i]);
            }
            this.mControlPts = elevatedList;
            this.update();
        }
    }
}