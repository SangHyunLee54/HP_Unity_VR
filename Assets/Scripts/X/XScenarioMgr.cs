using System.Collections.Generic;

namespace X {
    public abstract class XScenarioMgr {
        //fields
        protected XApp mApp = null;
        // no arraylist in c#, there is list
        protected List<XScenario> mScenarios = null;
        protected XScene mCurScene = null;
        public XScene getCurScene() {
            return this.mCurScene;
        }
        
        public void setCurScene(XScene scene) {
            if (this.mCurScene != null) {
                this.mCurScene.wrapUp();
            }
            scene.getReady();
            this.mCurScene = scene;
        }
        
        //constructor
        protected XScenarioMgr(XApp app) {
            this.mApp = app;
            this.mScenarios = new List<XScenario>();
            this.addScenarios();
            this.setInitCurScene();
        }
        
        //abstract methods
        protected abstract void addScenarios();
        protected abstract void setInitCurScene();
        
        //concrete methods
        protected void addScenario(XScenario scenario) {
            //method of C# starts with capital letter
            this.mScenarios.Add(scenario);
        }
    }
}