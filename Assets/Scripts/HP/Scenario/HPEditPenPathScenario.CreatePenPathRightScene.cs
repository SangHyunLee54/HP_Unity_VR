using X;
using UnityEngine;
using HP.Cmd;
using System.Collections.Generic;

namespace HP.Scenario {
    public partial class HPEditPenPathScenario {
        public class CreatePenPathRightScene : HPScene {
            // fields
            private HPControlPt movePt = null;

            private static CreatePenPathRightScene mSingleton = null;
            public static CreatePenPathRightScene getSingleton() {
                Debug.Assert(CreatePenPathRightScene.mSingleton != null);
                return CreatePenPathRightScene.mSingleton;
            }
            
            public static CreatePenPathRightScene createSingleton(
                XScenario scenario) {
                Debug.Assert(CreatePenPathRightScene.mSingleton == null);
                CreatePenPathRightScene.mSingleton = 
                    new CreatePenPathRightScene(scenario);
                return CreatePenPathRightScene.mSingleton;
            }

            // constructor
            private CreatePenPathRightScene(XScenario scenario) : base(scenario) {
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
            }

            public override void handleRightGrabStart() {
                Debug.Log("Right Grab Start");
            }

            public override void handleRightGrabEnd() {
                HPApp app = (HPApp)this.mScenario.getApp();
                HPEditPenPathScenario scenario = 
                    (HPEditPenPathScenario) this.mScenario;
                Vector3 startPos = scenario.getStartPt();
                Vector3 endPos = app.getRightHand().calcPalmPos();
                List<Vector3> startPts = new List<Vector3>();
                if (Vector3.Distance(startPos, endPos) < 0.05) {
                    startPts.Add(startPos);
                    HPCmdToCreatePenPath.execute(app, startPts);
                } else {
                    startPts.Add(startPos);
                    startPts.Add(endPos);
                    HPCmdToCreatePenPath.execute(app, startPts);
                }
                XCmdToChangeScene.execute(app,
                    HPEditPenPathScenario.EditPenPathReadyScene.
                    getSingleton(), this);
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