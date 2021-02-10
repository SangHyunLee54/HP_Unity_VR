namespace X {
    // column instead of implements
    public abstract class XLoggableCmd : XExecutable {
        //fields
        protected XApp mApp = null;
        
        //constructor
        protected XLoggableCmd(XApp app) {
            this.mApp = app;
        }
        
        public bool execute() {
            if (this.defineCmd()) {
                this.mApp.getLogMgr().addLog(this.createLog());
                return true;
            } else {
                return false;
            }
        }
        
        //abstract methos
        protected abstract bool defineCmd();
        protected abstract string createLog();
    }
}