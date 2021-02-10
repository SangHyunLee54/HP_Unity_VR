using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;

    namespace HP{
    public class HPApp : XApp{
        // editor fields
        public GameObject mEyePrefab;
        public GameObject mLeftHandPrefab;
        public GameObject mRightHandPrefab;
        public GameObject mLeftControllerPrefab;
        public GameObject mRightControllerPrefab;

        // fields
        private HPEye mEye = null;
        public HPEye GetEye() {
            return this.mEye;
        }
        private HPHand mLeftHand = null;
        public HPHand getLeftHand() {
            return this.mLeftHand;
        }
        private HPHand mRightHand = null;
        public HPHand getRightHand() {
            return this.mRightHand;
        }
        private HPHandEventSource mHandEventSource = null;
        private HPEventListener mEventListener = null;

        private XScenarioMgr mScenarioMgr = null;
        public override XScenarioMgr getScenarioMgr() {
            return this.mScenarioMgr;
        }

        private XLogMgr mLogMgr = null;
        public override XLogMgr getLogMgr() {
            return this.mLogMgr;
        }

        private HPControlPtMgr mControlPtMgr = null;
        public HPControlPtMgr getControlPtMgr() {
            return this.mControlPtMgr;
        }

        private HPConnectLine mConnectLine = null;
        public HPConnectLine getConnectLine() {
            return this.mConnectLine;
        }
        
        private HPBezierCurve mBezierCurve = null;
        public HPBezierCurve getBezierCurve() {
            return this.mBezierCurve;
        }

        void Start() {
            this.mEye = new HPEye(this.mEyePrefab);
            this.mLeftHand = new HPHand(this.mLeftHandPrefab);
            this.mRightHand = new HPHand(this.mRightHandPrefab);
            this.mHandEventSource = new HPHandEventSource(this);
            this.mEventListener = new HPEventListener(this);
            this.mHandEventSource.setListener(this.mEventListener);
            this.mScenarioMgr = new HPScenarioMgr(this);
            this.mLogMgr = new XLogMgr();
            this.mLogMgr.setPrintOn(true);
            this.mControlPtMgr = new HPControlPtMgr();

            this.mConnectLine = new HPConnectLine(this);
            this.mBezierCurve = new HPBezierCurve(this);
            
        }
        void Update() {
            this.mHandEventSource.update();        
            //this.mConnectLine.update();    
        }
    }
}