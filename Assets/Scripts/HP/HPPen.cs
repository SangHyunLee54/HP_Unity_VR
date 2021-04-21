using UnityEngine;
using HP.AppObject;
using System.Collections.Generic;
using System.Linq;

namespace HP {
    public class HPPen : HPAppNoGeom3D{
        // constant
        public enum CurveState {LINEAR, QUADRATIC_START, QUADRATIC_END, CUBIC};

        // field
        private HPApp mApp = null;

        private HPControlPt mStartPt = null;
        public HPControlPt getStartPt() {
            return this.mStartPt;
        }

        private List<HPBezierCurve> mCurves = null;
        public List<HPBezierCurve> getCurves() {
            return this.mCurves;
        }
        
        private List<CurveState> mCurveStates = null;
        public List<CurveState> getCurveStates() {
            return this.mCurveStates;
        }

        public HPPen(HPApp app, Vector3 startPt) : base("Pen") {
            this.mApp = app;
            this.mStartPt = new HPControlPt(startPt);
            this.mCurves = new List<HPBezierCurve>();
            this.mCurveStates = new List<CurveState>();
        }

        public void addPoint(HPControlPt pt) {
            HPBezierCurve bc = new HPBezierCurve(this.mApp);

            if (this.mCurves.Count == 0) {
                bc.addControlPt(this.mStartPt);
                // curve state is linear
                this.mCurveStates.Add(HPPen.CurveState.LINEAR);
            } else {
                HPBezierCurve prevCurve = this.mCurves.Last();
                CurveState prevState = this.mCurveStates.Last();
                // make linear line
                if (prevState == HPPen.CurveState.LINEAR ||
                    prevState == HPPen.CurveState.QUADRATIC_START) {
                    // add Last point
                    bc.addControlPt(prevCurve.getContorlPts().Last());
                    // curve state is linear
                    this.mCurveStates.Add(HPPen.CurveState.LINEAR);
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
                    this.mCurveStates.Add(HPPen.CurveState.QUADRATIC_START);
                }
            }

            // add new control Pt
            bc.addControlPt(pt);

            // add curve to mCurves
            this.mCurves.Add(bc);
        }

        public void addCurve(HPControlPt pt1, HPControlPt pt2) {
            HPBezierCurve bc = new HPBezierCurve(this.mApp);

            if (this.mCurves.Count == 0) {
                bc.addControlPt(this.mStartPt);
                // curve state is quadratic end
                this.mCurveStates.Add(HPPen.CurveState.QUADRATIC_END);
            } else {
                HPBezierCurve prevCurve = this.mCurves.Last();
                CurveState prevState = this.mCurveStates.Last();
                // make cubic bezier curve
                if (prevState == HPPen.CurveState.LINEAR ||
                    prevState == HPPen.CurveState.QUADRATIC_START) {
                    // add Last point
                    bc.addControlPt(prevCurve.getContorlPts().Last());
                    // curve state is linear
                    this.mCurveStates.Add(HPPen.CurveState.QUADRATIC_END);
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
                    this.mCurveStates.Add(HPPen.CurveState.CUBIC);
                }
                // add new control point
            }

            // add control pts
            bc.addControlPt(pt1);
            bc.addControlPt(pt2);
            // add curve to mCurves
            this.mCurves.Add(bc);
        }
    }
}