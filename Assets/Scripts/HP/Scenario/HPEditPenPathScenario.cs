using X;
using UnityEngine;

namespace HP.Scenario {
    //for each class, we can create several files: partical class
    public partial class HPEditPenPathScenario : XScenario {
        // constants
        float MIN_DIST_PINCH = 0.0025f;

        private static HPEditPenPathScenario mSingleton = null;
        public static HPEditPenPathScenario getSingleton() {
            Debug.Assert(HPEditPenPathScenario.mSingleton != null);
            return HPEditPenPathScenario.mSingleton;
        }
        
        public static HPEditPenPathScenario createSingleton(XApp app) {
            Debug.Assert(HPEditPenPathScenario.mSingleton == null);
            HPEditPenPathScenario.mSingleton = new HPEditPenPathScenario(app);
            return HPEditPenPathScenario.mSingleton;
        }

        private HPEditPenPathScenario (XApp app) : base(app) {
        }
        
        protected override void addScenes() {
            this.addScene(HPEditPenPathScenario.EditPenPathReadyScene.
                createSingleton(this));
            this.addScene(HPEditPenPathScenario.CreatePenPathRightScene.
                createSingleton(this));
            this.addScene(HPEditPenPathScenario.AddPenPathRightScene.
                createSingleton(this));
            this.addScene(HPEditPenPathScenario.MoveControlPointRightScene.
                createSingleton(this));
        }

        public Vector3 startPt = Vector3.zero;

        public Vector3 getStartPt() {
            return this.startPt;
        }

        public void setStartPt(Vector3 pt) {
            this.startPt = pt;
        }
    }
}