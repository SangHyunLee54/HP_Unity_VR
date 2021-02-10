using System.Collections.Generic;
using UnityEngine;

namespace X {
    public class XLogMgr {
        //fields
        // vector => list
        // in java String is a class
        // in c# it's primitive
        private List<string> mLogs = null;
        public List<string> getLogs() {
            return this.mLogs;
        }
        private bool mPrintOn = false;
        public bool isPrintOn() {
            return this.mPrintOn;
        }
        public void setPrintOn(bool isPrintOn) {
            this.mPrintOn = isPrintOn;
        }
        
        //constructor
        public XLogMgr() {
            this.mLogs = new List<string>();
        }
        
        public void addLog(string log) {
            this.mLogs.Add(log);
            if (this.mPrintOn) {
                // System.out.println(log);
                // unity
                Debug.Log(log);
            }
        }
    }
}