using X;
using UnityEngine;
using HP.Cmd;
using System.Collections.Generic;

namespace HP.Scenario {
    public partial class HPEditCurveScenario {
        public class EditCurveReadyScene : HPScene {
            // fields
            private HPControlPt movePt = null;

            private static EditCurveReadyScene mSingleton = null;
            public static EditCurveReadyScene getSingleton() {
                Debug.Assert(EditCurveReadyScene.mSingleton != null);
                return EditCurveReadyScene.mSingleton;
            }
            
            public static EditCurveReadyScene createSingleton(
                XScenario scenario) {
                Debug.Assert(EditCurveReadyScene.mSingleton == null);
                EditCurveReadyScene.mSingleton = 
                    new EditCurveReadyScene(scenario);
                return EditCurveReadyScene.mSingleton;
            }

            // constructor
            private EditCurveReadyScene(XScenario scenario) : base(scenario) {
            }

            public override void handleLeftPinchStart() {
                Debug.Log("Left Pinch Start");

                HPApp app = (HPApp)this.mScenario.getApp();
                if(HPCmdToSetCurCurve.execute(app,app.getLeftHand())) {
                    XCmdToChangeScene.execute(app,
                        HPEditCurveScenario.MoveControlPointLeftScene.
                        getSingleton(), this);
                } else {
                    XCmdToChangeScene.execute(app,
                        HPDefaultScenario.ReadyScene.
                        getSingleton(), null);
                }
            }

            public override void handleLeftPinchEnd() {
                Debug.Log("Left Pinch End");
            }

            public override void handleLeftGrabStart() {
                Debug.Log("Left Grab Start");
            }

            public override void handleLeftGrabEnd() {
                HPApp app = (HPApp)this.mScenario.getApp();
                HPCmdToCreateControlPoint.execute(app, app.getLeftHand());
                Debug.Log("Left Grab End");
            }

            public override void handleRightPinchStart() {
                Debug.Log("Right Pinch Start");

                HPApp app = (HPApp)this.mScenario.getApp();
                if(HPCmdToSetCurCurve.execute(app,app.getRightHand())) {
                    XCmdToChangeScene.execute(app,
                        HPEditCurveScenario.MoveControlPointRightScene.
                        getSingleton(), this);
                } else {
                    XCmdToChangeScene.execute(app,
                        HPDefaultScenario.ReadyScene.
                        getSingleton(), null);
                }
            }

            public override void handleRightPinchEnd() {
                Debug.Log("Right Pinch End");
            }

            public override void handleRightGrabStart() {
                Debug.Log("Right Grab Start");
            }

            public override void handleRightGrabEnd() {
                HPApp app = (HPApp)this.mScenario.getApp();
                HPCmdToCreateControlPoint.execute(app, app.getRightHand());
                Debug.Log("Right Grab End");
            }

            public override void handleHandsMove() {
            }

            public override void getReady() {
            }

            public override void wrapUp() {
            }
        }
    }
    
}