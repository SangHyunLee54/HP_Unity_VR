using UnityEngine;
using HP.AppObject;
using System.Collections.Generic;
using System.Linq;

namespace HP {
    public class HPPenPath : HPAppNoGeom3D{
        // constant
        public enum CurveState {LINEAR, QUADRATIC_START, QUADRATIC_END, CUBIC};

        // field
        private HPApp mApp = null;

        private List<HPControlPt> mStartPts = null;
        public List<HPControlPt> getStartPts() {
            return this.mStartPts;
        }

        private List<HPBezierCurve> mCurves = null;
        public List<HPBezierCurve> getCurves() {
            return this.mCurves;
        }
        
        private List<CurveState> mCurveStates = null;
        public List<CurveState> getCurveStates() {
            return this.mCurveStates;
        }

        public HPPenPath(HPApp app, List<Vector3> startPts) : base("Pen") {
            this.mApp = app;
            this.mStartPts = new List<HPControlPt>();
            foreach (Vector3 v in startPts) {
                HPControlPt cPt = new HPControlPt(v);
                this.mStartPts.Add(cPt);
            }
            this.mCurves = new List<HPBezierCurve>();
            this.mCurveStates = new List<CurveState>();
        }

        public void addPoint(HPControlPt pt) {
            HPBezierCurve bc = new HPBezierCurve(this.mApp);

            if (this.mCurves.Count == 0) {
                if (this.mStartPts.Count == 1) {
                    bc.addControlPt(this.mStartPts[0]);
                    this.mCurveStates.Add(HPPenPath.CurveState.LINEAR);
                } else if (this.mStartPts.Count == 2) {
                    bc.addControlPt(this.mStartPts[0]);
                    bc.addControlPt(this.mStartPts[1]);
                    this.mCurveStates.Add(HPPenPath.CurveState.QUADRATIC_START);
                }
            } else {
                HPBezierCurve prevCurve = this.mCurves.Last();
                CurveState prevState = this.mCurveStates.Last();
                // make linear line
                if (prevState == HPPenPath.CurveState.LINEAR ||
                    prevState == HPPenPath.CurveState.QUADRATIC_START) {
                    // add Last point
                    bc.addControlPt(prevCurve.getContorlPts().Last());
                    // curve state is linear
                    this.mCurveStates.Add(HPPenPath.CurveState.LINEAR);
                } else {
                    // make cubic bezier
                    // add Last point
                    bc.addControlPt(prevCurve.getContorlPts().Last());

                    // add symmetric point of second of last
                    HPControlPt lastPt = prevCurve.getContorlPts().Last();
                    HPControlPt secondLastPt = prevCurve.
                        getContorlPts()[prevCurve.getContorlPts().Count - 2];
                    HPControlPt newPt = new HPControlPt(lastPt.getPos() * 2 - 
                        secondLastPt.getPos());
                    bc.addControlPt(newPt);
                    // curve state is quadratic start
                    this.mCurveStates.Add(HPPenPath.CurveState.QUADRATIC_START);
                }
            }

            // add new control Pt
            bc.addControlPt(pt);

            // add curve to mCurves
            this.mCurves.Add(bc);

            this.update();
        }

        public void addCurve(HPControlPt pt1, HPControlPt pt2) {
            HPBezierCurve bc = new HPBezierCurve(this.mApp);

            if (this.mCurves.Count == 0) {
                if (this.mStartPts.Count == 1) {
                    bc.addControlPt(this.mStartPts[0]);
                    this.mCurveStates.Add(HPPenPath.CurveState.QUADRATIC_END);
                } else if (this.mStartPts.Count == 2) {
                    bc.addControlPt(this.mStartPts[0]);
                    bc.addControlPt(this.mStartPts[1]);
                    this.mCurveStates.Add(HPPenPath.CurveState.CUBIC);
                }

            } else {
                HPBezierCurve prevCurve = this.mCurves.Last();
                CurveState prevState = this.mCurveStates.Last();
                // make cubic bezier curve
                if (prevState == HPPenPath.CurveState.LINEAR ||
                    prevState == HPPenPath.CurveState.QUADRATIC_START) {
                    // add Last point
                    bc.addControlPt(prevCurve.getContorlPts().Last());
                    // curve state is linear
                    this.mCurveStates.Add(HPPenPath.CurveState.QUADRATIC_END);
                } else {
                    // make cubic bezier
                    // add Last point
                    bc.addControlPt(prevCurve.getContorlPts().Last());

                    // add symmetric point of second of last
                    HPControlPt lastPt = prevCurve.getContorlPts().Last();
                    HPControlPt secondLastPt = prevCurve.
                        getContorlPts()[prevCurve.getContorlPts().Count - 2];
                    HPControlPt newPt = new HPControlPt(lastPt.getPos() * 2 - 
                        secondLastPt.getPos());
                    bc.addControlPt(newPt);
                    // curve state is quadratic start
                    this.mCurveStates.Add(HPPenPath.CurveState.CUBIC);
                }
                // add new control point
            }

            // add control pts
            bc.addControlPt(pt1);
            bc.addControlPt(pt2);
            // add curve to mCurves
            this.mCurves.Add(bc);

            this.update();
        }

        public void update() {
            foreach (HPBezierCurve bc in this.mCurves) {
                bc.update();
            }
        }
    }
}