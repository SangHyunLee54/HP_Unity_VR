using X;
using HP.Scenario;

namespace HP {
    public class HPScenarioMgr : XScenarioMgr {
        // constructor
        public HPScenarioMgr(XApp app) : base(app) {
        }

        // methods
        protected override void addScenarios() {
            HPApp app = (HPApp) this.mApp;
            this.addScenario(HPDefaultScenario.createSingleton(app));
        }

        protected override void setInitCurScene() {
            this.setCurScene(HPDefaultScenario.ReadyScene.getSingleton());
        }
    }
}