using System.Text;

namespace X {
    // extends => :
    public class XCmdToChangeScene : XLoggableCmd {
        //fieds
        private XScene mFromScene = null;
        private XScene mToScene = null;
        private XScene mReturnScene = null;
        
        //private constructor
        //base instead of super, location also changes
        private XCmdToChangeScene(XApp app, XScene toScene, XScene returnScene) :
            base(app) {
            this.mFromScene = app.getScenarioMgr().getCurScene();
            this.mToScene = toScene;
            this.mReturnScene = returnScene;
        }
        
        //static method to construct and execute this command
        public static bool execute(
            XApp app, XScene toScene, XScene returnScene) {
            XCmdToChangeScene cmd = 
                new XCmdToChangeScene(app, toScene, returnScene);
            return cmd.execute();
        }
        
        // insert override in between
        protected override bool defineCmd() {
            this.mToScene.setReturnScene(this.mReturnScene);
            this.mApp.getScenarioMgr().setCurScene(this.mToScene);
            return true;
        }

        protected override string createLog() {
            //getClass().getSimpleName() => GetType().Name
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            sb.Append(this.mFromScene.GetType().Name).Append("\t");
            XScene curScene = this.mApp.getScenarioMgr().getCurScene();
            sb.Append(curScene.GetType().Name).Append(" \t");
            if(this.mReturnScene == null) {
                sb.Append("null");
            } else {
                sb.Append(curScene.getReturnScene().GetType().Name);
            }
            return sb.ToString();
        }
    }
}