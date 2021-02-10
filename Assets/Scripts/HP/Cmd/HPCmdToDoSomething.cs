using System.Text;
using X;
using UnityEngine;

namespace HP.Cmd
{
    internal class HPCmdToDoSomething : XLoggableCmd {

        // private constructor
        private HPCmdToDoSomething(XApp app) : base(app) {
            HPApp HP = (HPApp)this.mApp;
        }

        public static bool execute(XApp app) {
            HPCmdToDoSomething cmd = new HPCmdToDoSomething(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            throw new System.NotImplementedException();
        }

        protected override string createLog() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            return sb.ToString();
        }
    }
}