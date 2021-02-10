using HP.AppObject;
using UnityEngine;
using System.Collections.Generic;

namespace HP {
    public class HPControlPtMgr {
        // fields
        private List<HPControlPt> mControlPts = null;
        public List<HPControlPt> getControlPts() {
            return this.mControlPts;
        }

        // constructor
        public HPControlPtMgr() {
            this.mControlPts = new List<HPControlPt>();
        }
    }
}