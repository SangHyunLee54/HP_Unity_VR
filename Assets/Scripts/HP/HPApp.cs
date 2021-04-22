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

        private HPBezierCurveMgr mBezierCurveMgr = null;
        public HPBezierCurveMgr getBezierCurveMgr() {
            return this.mBezierCurveMgr;
        }
        
        private HPBezierSurfaceMgr mBezierSurfaceMgr = null;
        public HPBezierSurfaceMgr getBezierSurfaceMgr() {
            return this.mBezierSurfaceMgr;
        }

        private HPPenPathMgr mPenPathMgr = null;
        public HPPenPathMgr getPenPathMgr() {
            return this.mPenPathMgr;
        }

        private HPConnectLine mConnectLine = null;
        public HPConnectLine GetConnectLine() {
            return this.mConnectLine;
        }

        private HPPenHandleLine mPenHandleLine = null;
        public HPPenHandleLine getPenHandleLine() {
            return this.mPenHandleLine;
        }

 

        void Start() {
            this.mEye = new HPEye(this.mEyePrefab);
            this.mLeftHand = new HPHand(this.mLeftHandPrefab);
            this.mRightHand = new HPHand(this.mRightHandPrefab);
            this.mHandEventSource = new HPHandEventSource(this);
            this.mEventListener = new HPEventListener(this);
            this.mHandEventSource.setListener(this.mEventListener);
            
            
            this.mControlPtMgr = new HPControlPtMgr();
            this.mBezierCurveMgr = new HPBezierCurveMgr(this);
            this.mBezierSurfaceMgr = new HPBezierSurfaceMgr(this);
            this.mPenPathMgr = new HPPenPathMgr(this);
            
            
            this.mScenarioMgr = new HPScenarioMgr(this);
            this.mLogMgr = new XLogMgr();
            this.mLogMgr.setPrintOn(true);


            this.mConnectLine = new HPConnectLine(this);
            this.mPenHandleLine = new HPPenHandleLine(this);
        }
        void Update() {
            this.mHandEventSource.update();
            //this.mConnectLine.update();
            //this.mPenHandleLine.update();
        }
    }
}