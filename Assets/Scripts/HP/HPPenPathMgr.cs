using HP.AppObject;
using UnityEngine;
using System.Collections.Generic;

namespace HP {
    public class HPPenPathMgr {
        // fields
        private HPApp mApp = null;

        private List<HPPenPath> mPenPaths = null;
        public List<HPPenPath> getPens() {
            return this.mPenPaths;
        }

        private HPPenPath mCurPenPath = null;
        public HPPenPath getCurPenPath() {
            return this.mCurPenPath;
        }
        public void setCurPenPath(HPPenPath pen) {
            this.mCurPenPath = pen;
            this.updatePointColor();
        }

        // constructor
        public HPPenPathMgr(HPApp app) {
            this.mApp = app;
            this.mPenPaths = new List<HPPenPath>();
        }

        // methods
        public void addPenPath(HPPenPath pen) {
            this.mPenPaths.Add(pen);
        }

        public void removeCurve(HPPenPath pen) {
            this.mPenPaths.Remove(pen);
        }

        // get BezierCurve has input ControlPt.
        public HPPenPath getPen(HPControlPt cp) {
            foreach(HPPenPath pen in this.mPenPaths) {
                foreach(HPBezierCurve bc in pen.getCurves()) {
                    if (bc.getContorlPts().Contains(cp)) {
                        return pen;
                    }
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
            if (this.getCurPenPath() != null) {
                foreach(HPBezierCurve bc in this.getCurPenPath().getCurves()) {
                    foreach(HPControlPt cpt in bc.getContorlPts()) {
                        cpt.getCtrlSphere().GetComponent<MeshRenderer>().
                        material.color = Color.blue;
                    }   
                }
            }
        }
    }
}