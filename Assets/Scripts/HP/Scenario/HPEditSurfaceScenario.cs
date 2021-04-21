using X;
using UnityEngine;

namespace HP.Scenario {
    //for each class, we can create several files: partical class
    public partial class HPEditSurfaceScenario : XScenario {
        // constants
        float MIN_DIST_PINCH = 0.0025f;

        private static HPEditSurfaceScenario mSingleton = null;
        public static HPEditSurfaceScenario getSingleton() {
            Debug.Assert(HPEditSurfaceScenario.mSingleton != null);
            return HPEditSurfaceScenario.mSingleton;
        }
        
        public static HPEditSurfaceScenario createSingleton(XApp app) {
            Debug.Assert(HPEditSurfaceScenario.mSingleton == null);
            HPEditSurfaceScenario.mSingleton = new HPEditSurfaceScenario(app);
            return HPEditSurfaceScenario.mSingleton;
        }

        private HPEditSurfaceScenario (XApp app) : base(app) {
        }
        
        protected override void addScenes() {
            this.addScene(HPEditSurfaceScenario.EditSurfaceReadyScene.
                createSingleton(this));
        }
    }
}