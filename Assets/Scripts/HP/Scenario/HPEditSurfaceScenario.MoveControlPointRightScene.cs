using X;
using UnityEngine;
using HP.Cmd;
using System.Collections.Generic;

namespace HP.Scenario {
    public partial class HPEditSurfaceScenario {
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