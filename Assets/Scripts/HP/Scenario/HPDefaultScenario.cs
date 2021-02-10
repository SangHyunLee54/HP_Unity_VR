using X;
using UnityEngine;

namespace HP.Scenario {
    //for each class, we can create several files: partical class
    public partial class HPDefaultScenario : XScenario {
        private static HPDefaultScenario mSingleton = null;
        public static HPDefaultScenario getSingleton() {
            Debug.Assert(HPDefaultScenario.mSingleton != null);
            return HPDefaultScenario.mSingleton;
        }
        
        public static HPDefaultScenario createSingleton(XApp app) {
            Debug.Assert(HPDefaultScenario.mSingleton == null);
            HPDefaultScenario.mSingleton = new HPDefaultScenario(app);
            return HPDefaultScenario.mSingleton;
        }

        private HPDefaultScenario (XApp app) : base(app) {
        }
        
        protected override void addScenes() {
            this.addScene(HPDefaultScenario.ReadyScene.createSingleton(this));
        }
    }
}