using X;
using UnityEngine;
using HP.Cmd;
using System.Collections.Generic;

namespace HP.Scenario {
    public partial class HPEditCurveScenario {
        public class MoveControlPointBothScene : HPScene {
            // fields
            private HPControlPt movePtL = null;
            private HPControlPt movePtR = null;

            private static MoveControlPointBothScene mSingleton = null;
            public static MoveControlPointBothScene getSingleton() {
                Debug.Assert(MoveControlPointBothScene.mSingleton != null);
                return MoveControlPointBothScene.mSingleton;
            }
            
            public static MoveControlPointBothScene createSingleton(
                XScenario scenario) {
                Debug.Assert(MoveControlPointBothScene.mSingleton == null);
                MoveControlPointBothScene.mSingleton = 
                    new MoveControlPointBothScene(scenario);
                return MoveControlPointBothScene.mSingleton;
            }

            // constructor
            private MoveControlPointBothScene(XScenario scenario) : base(scenario) {
            }

            public override void handleLeftPinchStart() {
                Debug.Log("Left Pinch Start");
            }

            public override void handleLeftPinchEnd() {
                Debug.Log("Left Pinch End");
                HPApp app = (HPApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app,
                            HPEditCurveScenario.MoveControlPointRightScene.
                            getSingleton(), this);
            }

            public override void handleLeftGrabStart() {
                Debug.Log("Left Grab Start");
            }

            public override void handleLeftGrabEnd() {
                Debug.Log("Left Grab End");
            }

            public override void handleRightPinchStart() {
                Debug.Log("Right Pinch Start");
            }

            public override void handleRightPinchEnd() {
                Debug.Log("Right Pinch End");
                HPApp app = (HPApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app,
                            HPEditCurveScenario.MoveControlPointLeftScene.
                            getSingleton(), this);
            }

            public override void handleRightGrabStart() {
                Debug.Log("Right Grab Start");
            }

            public override void handleRightGrabEnd() {
                Debug.Log("Right Grab End");
            }

            public override void handleHandsMove() {
                HPApp app = (HPApp)this.mScenario.getApp();
                if (this.movePtL != null) {
                    HPCmdToMoveControlPoint.execute(app, app.getLeftHand(), 
                    this.movePtL);
                }
                if (this.movePtR != null) {
                    HPCmdToMoveControlPoint.execute(app, app.getRightHand(), 
                    this.movePtR);
                }
            }

            public override void getReady() {
                setMovePtLeft();
                setMovePtRight();
            }

            public override void wrapUp() {
            }

            // set a control point to move
            private void setMovePtLeft() {
                HPApp app = (HPApp)this.mScenario.getApp();
                List<HPControlPt> controlPts = app.getBezierCurveMgr().
                    getCurCurve().getContorlPts();
                Vector3 pinchPos = app.getLeftHand().calcPinchPos();
                float sqrMinDistance = HPControlPt.MIN_DIST_PINCH;
                HPControlPt closestPt = null;
                foreach(HPControlPt cpt in controlPts) {
                    Vector3 distance = cpt.getPos() - pinchPos;
                    float sm = Vector3.SqrMagnitude(distance);
                    if(sm < sqrMinDistance) {
                        sqrMinDistance = sm;
                        closestPt = cpt;
                    }
                }
                this.movePtL = closestPt;
            }

            private void setMovePtRight() {
                HPApp app = (HPApp)this.mScenario.getApp();
                List<HPControlPt> controlPts = app.getBezierCurveMgr().
                    getCurCurve().getContorlPts();
                Vector3 pinchPos = app.getRightHand().calcPinchPos();
                float sqrMinDistance = HPControlPt.MIN_DIST_PINCH;
                HPControlPt closestPt = null;
                foreach(HPControlPt cpt in controlPts) {
                    Vector3 distance = cpt.getPos() - pinchPos;
                    float sm = Vector3.SqrMagnitude(distance);
                    if(sm < sqrMinDistance) {
                        sqrMinDistance = sm;
                        closestPt = cpt;
                    }
                }
                this.movePtR = closestPt;
            }
            
        }
    }
    
}