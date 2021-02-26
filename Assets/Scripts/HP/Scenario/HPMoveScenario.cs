using X;
using UnityEngine;

namespace HP.Scenario {
    //for each class, we can create several files: partical class
    public partial class HPMoveScenario : XScenario {
        // constants
        float MIN_DIST_PINCH = 0.0025f;

        private static HPMoveScenario mSingleton = null;
        public static HPMoveScenario getSingleton() {
            Debug.Assert(HPMoveScenario.mSingleton != null);
            return HPMoveScenario.mSingleton;
        }
        
        public static HPMoveScenario createSingleton(XApp app) {
            Debug.Assert(HPMoveScenario.mSingleton == null);
            HPMoveScenario.mSingleton = new HPMoveScenario(app);
            return HPMoveScenario.mSingleton;
        }

        private HPMoveScenario (XApp app) : base(app) {
        }
        
        protected override void addScenes() {
            this.addScene(HPMoveScenario.MoveControlPointLeftScene.
                createSingleton(this));
            this.addScene(HPMoveScenario.MoveControlPointRightScene.
                createSingleton(this));
            this.addScene(HPMoveScenario.MoveControlPointBothScene.
                createSingleton(this));
        }
    }
}