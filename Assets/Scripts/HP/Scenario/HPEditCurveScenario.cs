using X;
using UnityEngine;

namespace HP.Scenario {
    //for each class, we can create several files: partical class
    public partial class HPEditCurveScenario : XScenario {
        // constants
        float MIN_DIST_PINCH = 0.0025f;

        private static HPEditCurveScenario mSingleton = null;
        public static HPEditCurveScenario getSingleton() {
            Debug.Assert(HPEditCurveScenario.mSingleton != null);
            return HPEditCurveScenario.mSingleton;
        }
        
        public static HPEditCurveScenario createSingleton(XApp app) {
            Debug.Assert(HPEditCurveScenario.mSingleton == null);
            HPEditCurveScenario.mSingleton = new HPEditCurveScenario(app);
            return HPEditCurveScenario.mSingleton;
        }

        private HPEditCurveScenario (XApp app) : base(app) {
        }
        
        protected override void addScenes() {
            this.addScene(HPEditCurveScenario.EditCurveReadyScene.
                createSingleton(this));
            this.addScene(HPEditCurveScenario.MoveControlPointLeftScene.
                createSingleton(this));
            this.addScene(HPEditCurveScenario.MoveControlPointRightScene.
                createSingleton(this));
            this.addScene(HPEditCurveScenario.MoveControlPointBothScene.
                createSingleton(this));
        }
    }
}