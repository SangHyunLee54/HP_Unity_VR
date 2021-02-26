using X;
using UnityEngine;
using HP.Cmd;
using System.Collections.Generic;

namespace HP.Scenario {
    public partial class HPMoveScenario {
        public class MoveControlPointLeftScene : HPScene {
            // fields
            private HPControlPt movePt = null;

            private static MoveControlPointLeftScene mSingleton = null;
            public static MoveControlPointLeftScene getSingleton() {
                Debug.Assert(MoveControlPointLeftScene.mSingleton != null);
                return MoveControlPointLeftScene.mSingleton;
            }
            
            public static MoveControlPointLeftScene createSingleton(
                XScenario scenario) {
                Debug.Assert(MoveControlPointLeftScene.mSingleton == null);
                MoveControlPointLeftScene.mSingleton = 
                    new MoveControlPointLeftScene(scenario);
                return MoveControlPointLeftScene.mSingleton;
            }

            // constructor
            private MoveControlPointLeftScene(XScenario scenario) : base(scenario) {
            }

            public override void handleLeftPinchStart() {
                Debug.Log("Left Pinch Start");
            }

            public override void handleLeftPinchEnd() {
                Debug.Log("Left Pinch End");
                HPApp app = (HPApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app,
                            HPDefaultScenario.ReadyScene.getSingleton(),
                            this);
            }

            public override void handleLeftGrabStart() {
                Debug.Log("Left Grab Start");
            }

            public override void handleLeftGrabEnd() {
                Debug.Log("Left Grab End");
            }

            public override void handleRightPinchStart() {
                Debug.Log("Right Pinch Start");
                HPApp app = (HPApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app,
                            HPMoveScenario.MoveControlPointBothScene.
                            getSingleton(), this);
            }

            public override void handleRightPinchEnd() {
                Debug.Log("Right Pinch End");
            }

            public override void handleRightGrabStart() {
                Debug.Log("Right Grab Start");
            }

            public override void handleRightGrabEnd() {
                Debug.Log("Right Grab End");
                HPApp app = (HPApp)this.mScenario.getApp();
                HPCmdToCreateControlPoint.execute(app, app.getRightHand());
            }

            public override void handleHandsMove() {
                if (this.movePt == null) {
                    return;
                }
                HPApp app = (HPApp)this.mScenario.getApp();
                HPCmdToMoveControlPoint.execute(app, app.getLeftHand(), 
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
                List<HPControlPt> controlPts = app.getControlPtMgr().
                    getControlPts();
                Vector3 pinchPos = app.getLeftHand().calcPinchPos();
                float sqrMinDistance = 0.1f;
                HPControlPt closestPt = null;
                foreach(HPControlPt cpt in controlPts) {
                    Vector3 distance = cpt.getPos() - pinchPos;
                    float sm = Vector3.SqrMagnitude(distance);
                    if(sm < sqrMinDistance) {
                        sqrMinDistance = sm;
                        closestPt = cpt;
                    }
                }
                this.movePt = closestPt;
            }
            
        }
    }
    
}