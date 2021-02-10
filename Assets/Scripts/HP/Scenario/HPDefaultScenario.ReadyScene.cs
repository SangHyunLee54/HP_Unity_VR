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