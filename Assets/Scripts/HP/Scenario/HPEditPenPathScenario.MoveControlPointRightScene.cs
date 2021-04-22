using X;
using UnityEngine;
using HP.Cmd;
using System.Collections.Generic;

namespace HP.Scenario {
    public partial class HPEditPenPathScenario {
        public class MoveControlPointRightScene : HPScene {
            // fields
            private HPControlPt movePt = null;

            private static MoveControlPointRightScene mSingleton = null;
            public static MoveControlPointRightScene getSingleton() {
                Debug.Assert(MoveControlPointRightScene.mSingleton != null);
                return MoveControlPointRightScene.mSingleton;
            }
            
            public static MoveControlPointRightScene createSingleton(
                XScenario scenario) {
                Debug.Assert(MoveControlPointRightScene.mSingleton == null);
                MoveControlPointRightScene.mSingleton = 
                    new MoveControlPointRightScene(scenario);
                return MoveControlPointRightScene.mSingleton;
            }

            // constructor
            private MoveControlPointRightScene(XScenario scenario) : base(scenario) {
            }

            public override void handleLeftPinchStart() {
                Debug.Log("Left Pinch Start");
            }

            public override void handleLeftPinchEnd() {
                Debug.Log("Left Pinch End");
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
                            HPEditPenPathScenario.EditPenPathReadyScene.
                            getSingleton(), this);
            }

            public override void handleRightGrabStart() {
                Debug.Log("Right Grab Start");
            }

            public override void handleRightGrabEnd() {
                Debug.Log("Right Grab End");
            }

            public override void handleHandsMove() {
                if (this.movePt == null) {
                    return;
                }
                HPApp app = (HPApp)this.mScenario.getApp();
                HPCmdToMovePenPoint.execute(app, app.getRightHand(), 
                    this.movePt);
            }

            public override void getReady() {
                setMovePt();
            }

            public override void wrapUp() {
            }

            // set a control point to move
            private void setMovePt() {
                HPApp app = (HPApp)this.mScenario.getApp();
                HPPenPath curPath = app.getPenPathMgr().getCurPenPath();
                HPControlPt closestPt = null;
                foreach (HPBezierCurve bc in curPath.getCurves()) {
                    List<HPControlPt> controlPts = bc.getContorlPts();
                    Vector3 pinchPos = app.getRightHand().calcPinchPos();
                    float sqrMinDistance = HPControlPt.MIN_DIST_PINCH;
                    
                    foreach(HPControlPt cpt in controlPts) {
                        Vector3 distance = cpt.getPos() - pinchPos;
                        float sm = Vector3.SqrMagnitude(distance);
                        if(sm < sqrMinDistance) {
                            sqrMinDistance = sm;
                            closestPt = cpt;
                        }
                    }
                }
                this.movePt = closestPt;
            }
        }
    }
    
}