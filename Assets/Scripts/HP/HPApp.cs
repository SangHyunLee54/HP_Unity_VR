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

        private HPConnectLine mConnectLine = null;
        public HPConnectLine GetConnectLine() {
            return this.mConnectLine;
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
            
            
            this.mScenarioMgr = new HPScenarioMgr(this);
            this.mLogMgr = new XLogMgr();
            this.mLogMgr.setPrintOn(false);


            this.mConnectLine = new HPConnectLine(this);

            // bezier surface test
            // List<List<HPControlPt>> cpts = new List<List<HPControlPt>>();
            // for (int i = 0; i < 5; i++) {
            //     List<HPControlPt> cpt = new List<HPControlPt>();
            //     for (int j = 0; j < 5; j++) {
            //         Vector3 vec = new Vector3((float)i / 10.0f, (float)j / 10.0f, (float)i / (((float)j + 1.0f) * 5.0f));
            //         HPControlPt controlPt = new HPControlPt(vec);
            //         cpt.Add(controlPt);
            //     }
            //     cpts.Add(cpt);
            // }
            // this.mSurface = new HPBezierSurface(this);
            // this.mSurface.update();
        }
        void Update() {
            this.mHandEventSource.update();
            this.mConnectLine.update();
        }
    }
}