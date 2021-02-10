using UnityEngine;
using X;

namespace HP {
    public abstract class HPScene : XScene {
        // constructor
        protected HPScene(XScenario scenario) : base(scenario) {
        }

        // abstract methods
        public abstract void handleLeftPinchStart();
        public abstract void handleLeftPinchEnd();

        public abstract void handleLeftGrabStart();
        public abstract void handleLeftGrabEnd();

        public abstract void handleRightPinchStart();
        public abstract void handleRightPinchEnd();

        public abstract void handleRightGrabStart();
        public abstract void handleRightGrabEnd();

        public abstract void handleHandsMove();
        }
}