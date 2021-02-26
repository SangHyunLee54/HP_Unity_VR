using HP.AppObject;
using UnityEngine;
using System.Collections.Generic;

namespace HP {
    public class HPBezierSurfaceMgr {
        // fields
        private HPApp mApp = null;

        private List<HPBezierSurface> mSurfaces = null;
        public List<HPBezierSurface> getSurfaces() {
            return this.mSurfaces;
        }

        private HPBezierSurface mCurSurface = null;
        public HPBezierSurface getCurSurface() {
            return this.mCurSurface;
        }
        public void setCurSurface(HPBezierSurface surface) {
            this.mCurSurface = surface;
            this.updatePointColor();
        }

        // constructor
        public HPBezierSurfaceMgr(HPApp app) {
            this.mApp = app;
            this.mSurfaces = new List<HPBezierSurface>();
        }

        // methods
        public void addSurface(HPBezierSurface surface) {
            this.mSurfaces.Add(surface);
        }

        // get BezierSurface has input ControlPt.
        public HPBezierSurface getCurve(HPControlPt cp) {
            foreach(HPBezierSurface bs in this.mSurfaces) {
                foreach(List<HPControlPt> cpts in bs.getContorlPts()) {
                    if (cpts.Contains(cp)) {
                        return bs;
                    }
                }
            }
            return null;
        }

        // show current bezier curve with blue point
        public void updatePointColor() {
            foreach(HPBezierSurface bs in this.mSurfaces) {
                foreach(List<HPControlPt> cpts in bs.getContorlPts()) {
                    foreach(HPControlPt cpt in cpts) {
                        cpt.getCtrlSphere().GetComponent<MeshRenderer>().
                        material.color = Color.red;
                    }
                }
            }
            if (this.getCurSurface() != null) {
                foreach(List<HPControlPt> cpts in this.getCurSurface().
                    getContorlPts()) {
                    foreach(HPControlPt cpt in cpts) {
                        cpt.getCtrlSphere().GetComponent<MeshRenderer>().
                        material.color = Color.blue;
                    }
                }
            }
        }
    }
}