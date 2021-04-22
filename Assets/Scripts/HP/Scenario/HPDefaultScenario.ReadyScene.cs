using X;
using UnityEngine;
using HP.Cmd;

namespace HP.Scenario {
    public partial class HPDefaultScenario {
        public class ReadyScene : HPScene {
            private static ReadyScene mSingleton = null;
            public static ReadyScene getSingleton() {
                Debug.Assert(ReadyScene.mSingleton != null);
                return ReadyScene.mSingleton;
            }
            
            public static ReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(ReadyScene.mSingleton == null);
                ReadyScene.mSingleton = new ReadyScene(scenario);
                return ReadyScene.mSingleton;
            }

            // constructor
            private ReadyScene(XScenario scenario) : base(scenario) {
            }

            public override void handleLeftPinchStart() {
                Debug.Log("Left Pinch Start");
                HPApp app = (HPApp)this.mScenario.getApp();
                if(HPCmdToSetCurCurve.execute(app,app.getLeftHand())) {
                    XCmdToChangeScene.execute(app,
                        HPEditCurveScenario.MoveControlPointLeftScene.
                        getSingleton(), this);
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
                HPCmdToCreateBezierCurve.execute(app);
                HPCmdToCreateControlPoint.execute(app, app.getLeftHand());
                XCmdToChangeScene.execute(app,
                    HPEditCurveScenario.EditCurveReadyScene.
                    getSingleton(), this);

                Debug.Log("Left Grab End");
            }

            public override void handleRightPinchStart() {
                Debug.Log("Right Pinch Start");
                HPApp app = (HPApp)this.mScenario.getApp();
                if (HPCmdToSetCurCurve.execute(app,app.getRightHand())) {
                    XCmdToChangeScene.execute(app,
                        HPEditCurveScenario.MoveControlPointRightScene.
                        getSingleton(), this);
                }
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
                    HPEditPenPathScenario.CreatePenPathRightScene.
                    getSingleton(), this);
                Debug.Log("Right Grab Start");
            }

            public override void handleRightGrabEnd() {
                HPApp app = (HPApp)this.mScenario.getApp();
                HPCmdToCreateBezierCurve.execute(app);
                HPCmdToCreateControlPoint.execute(app, app.getRightHand());
                XCmdToChangeScene.execute(app,
                    HPEditCurveScenario.EditCurveReadyScene.
                    getSingleton(), this);
                Debug.Log("Right Grab End");
            }

            public override void handleHandsMove() {
            }

            public override void getReady() {
                HPApp app = (HPApp)this.mScenario.getApp();
                app.getBezierCurveMgr().setCurCurve(null);
            }

            public override void wrapUp() {
            }
            
        }
    }
    
}