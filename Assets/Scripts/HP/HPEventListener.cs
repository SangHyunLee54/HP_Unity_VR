using UnityEngine;

namespace HP {
    public class HPEventListener {
        private HPApp mApp;

        public HPEventListener(HPApp app) {
            this.mApp = app;
        }

        // left hand
        public void leftPinchStart() {
            HPScene curScene = (HPScene)this.mApp.getScenarioMgr().
                getCurScene();
            curScene.handleLeftPinchStart();
        }

        public void leftPinchEnd() {
            HPScene curScene = (HPScene)this.mApp.getScenarioMgr().
                getCurScene();
            curScene.handleLeftPinchEnd();
        }

        public void leftGrabStart() {
            HPScene curScene = (HPScene)this.mApp.getScenarioMgr().
                getCurScene();
            curScene.handleLeftGrabStart();
        }

        public void leftGrabEnd() {
            HPScene curScene = (HPScene)this.mApp.getScenarioMgr().
                getCurScene();
            curScene.handleLeftGrabEnd();
        }

        // right hand
        // left hand
        public void rightPinchStart() {
            HPScene curScene = (HPScene)this.mApp.getScenarioMgr().
                getCurScene();
            curScene.handleRightPinchStart();
        }

        public void rightPinchEnd() {
            HPScene curScene = (HPScene)this.mApp.getScenarioMgr().
                getCurScene();
            curScene.handleRightPinchEnd();
        }

        public void rightGrabStart() {
            HPScene curScene = (HPScene)this.mApp.getScenarioMgr().
                getCurScene();
            curScene.handleRightGrabStart();
        }

        public void rightGrabEnd() {
            HPScene curScene = (HPScene)this.mApp.getScenarioMgr().
                getCurScene();
            curScene.handleRightGrabEnd();
        }

        // both hands
        public void handsMove() {
            HPScene curScene = (HPScene)this.mApp.getScenarioMgr().
                getCurScene();
            curScene.handleHandsMove();
        }
    }
    
}