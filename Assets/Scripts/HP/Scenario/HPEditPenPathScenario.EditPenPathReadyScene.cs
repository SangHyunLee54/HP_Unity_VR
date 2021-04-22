using X;
using UnityEngine;
using HP.Cmd;
using System.Collections.Generic;

namespace HP.Scenario {
    public partial class HPEditPenPathScenario {
        public class EditPenPathReadyScene : HPScene {
            // fields
            private HPControlPt movePt = null;

            private static EditPenPathReadyScene mSingleton = null;
            public static EditPenPathReadyScene getSingleton() {
                Debug.Assert(EditPenPathReadyScene.mSingleton != null);
                return EditPenPathReadyScene.mSingleton;
            }
            
            public static EditPenPathReadyScene createSingleton(
                XScenario scenario) {
                Debug.Assert(EditPenPathReadyScene.mSingleton == null);
                EditPenPathReadyScene.mSingleton = 
                    new EditPenPathReadyScene(scenario);
                return EditPenPathReadyScene.mSingleton;
            }

            // constructor
            private EditPenPathReadyScene(XScenario scenario) : base(scenario) {
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
                HPApp app = (HPApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app,
                    HPEditPenPathScenario.MoveControlPointRightScene.
                    getSingleton(), this);
            }

            public override void handleRightPinchEnd() {
                Debug.Log("Right Pinch End");
            }

            public override void handleRightGrabStart() {
                HPApp app = (HPApp)this.mScenario.getApp();
                HPEditPenPathScenario scenario = 
                    HPEditPenPathScenario.getSingleton();
                Vector3 pt = app.getRightHand().calcPalmPos();
                scenario.setStartPt(pt);
                
                XCmdToChangeScene.execute(app,
                    HPEditPenPathScenario.AddPenPathRightScene.
                    getSingleton(), this);
                Debug.Log("Right Grab Start");
            }

            public override void handleRightGrabEnd() {
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