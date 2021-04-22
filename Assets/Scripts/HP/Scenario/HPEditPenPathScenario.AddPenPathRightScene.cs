using X;
using UnityEngine;
using HP.Cmd;
using System.Collections.Generic;

namespace HP.Scenario {
    public partial class HPEditPenPathScenario {
        public class AddPenPathRightScene : HPScene {
            // fields
            private HPControlPt movePt = null;

            private static AddPenPathRightScene mSingleton = null;
            public static AddPenPathRightScene getSingleton() {
                Debug.Assert(AddPenPathRightScene.mSingleton != null);
                return AddPenPathRightScene.mSingleton;
            }
            
            public static AddPenPathRightScene createSingleton(
                XScenario scenario) {
                Debug.Assert(AddPenPathRightScene.mSingleton == null);
                AddPenPathRightScene.mSingleton = 
                    new AddPenPathRightScene(scenario);
                return AddPenPathRightScene.mSingleton;
            }

            // constructor
            private AddPenPathRightScene(XScenario scenario) : base(scenario) {
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
                List<HPControlPt> cPts = new List<HPControlPt>();
                if (Vector3.Distance(startPos, endPos) < 0.05) {
                    HPControlPt cPt = new HPControlPt(startPos);
                    cPts.Add(cPt);
                    HPCmdToAddPenPath.execute(app, cPts);
                } else {
                    HPControlPt cPt1 = new HPControlPt(startPos);
                    HPControlPt cPt2 = new HPControlPt(endPos);
                    cPts.Add(cPt1);
                    cPts.Add(cPt2);
                    HPCmdToAddPenPath.execute(app, cPts);
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