using X;
using UnityEngine;
using HP.Cmd;
using System.Collections.Generic;

namespace HP.Scenario {
    public partial class HPEditSurfaceScenario {
        public class EditSurfaceReadyScene : HPScene {
            // fields
            private HPControlPt movePt = null;

            private static EditSurfaceReadyScene mSingleton = null;
            public static EditSurfaceReadyScene getSingleton() {
                Debug.Assert(EditSurfaceReadyScene.mSingleton != null);
                return EditSurfaceReadyScene.mSingleton;
            }
            
            public static EditSurfaceReadyScene createSingleton(
                XScenario scenario) {
                Debug.Assert(EditSurfaceReadyScene.mSingleton == null);
                EditSurfaceReadyScene.mSingleton = 
                    new EditSurfaceReadyScene(scenario);
                return EditSurfaceReadyScene.mSingleton;
            }

            // constructor
            private EditSurfaceReadyScene(XScenario scenario) : base(scenario) {
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