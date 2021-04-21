using HP.AppObject;
using UnityEngine;
using System.Collections.Generic;

namespace HP {
    public class HPBezierCurveMgr {
        // fields
        private HPApp mApp = null;

        private List<HPBezierCurve> mCurves = null;
        public List<HPBezierCurve> getCurves() {
            return this.mCurves;
        }

        private HPBezierCurve mCurCurve = null;
        public HPBezierCurve getCurCurve() {
            return this.mCurCurve;
        }
        public void setCurCurve(HPBezierCurve curve) {
            this.mCurCurve = curve;
            this.updatePointColor();
        }

        // constructor
        public HPBezierCurveMgr(HPApp app) {
            this.mApp = app;
            this.mCurves = new List<HPBezierCurve>();
        }

        // methods
        public void addCurve(HPBezierCurve curve) {
            this.mCurves.Add(curve);
        }

        public void removeCurve(HPBezierCurve curve) {
            this.mCurves.Remove(curve);
        }

        // get BezierCurve has input ControlPt.
        public HPBezierCurve getCurve(HPControlPt cp) {
            foreach(HPBezierCurve bc in this.mCurves) {
                if (bc.getContorlPts().Contains(cp)) {
                    return bc;
                }
            }
            return null;
        }

        // show current bezier curve with blue point
        public void updatePointColor() {
            HPApp app = (HPApp)this.mApp;
            foreach(HPControlPt cpt in app.getControlPtMgr().getControlPts()) {
                cpt.getCtrlSphere().GetComponent<MeshRenderer>().
                material.color = Color.red;
            }
            if (this.getCurCurve() != null) {
                foreach(HPControlPt cpt in this.getCurCurve().getContorlPts()) {
                    cpt.getCtrlSphere().GetComponent<MeshRenderer>().
                    material.color = Color.blue;
                }
            }
        }
    }
}