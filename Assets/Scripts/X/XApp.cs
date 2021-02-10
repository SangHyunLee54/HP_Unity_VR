// using is similar to import
using UnityEngine;

// no package in C#, instead we use namespace
namespace X {
    public abstract class XApp : MonoBehaviour {
        public abstract XScenarioMgr getScenarioMgr();
        public abstract XLogMgr getLogMgr();
    }
}