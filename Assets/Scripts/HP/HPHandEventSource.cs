using UnityEngine;

namespace HP {
    public class HPHandEventSource {
        // constants
        
        // properties
        private HPApp mApp;
        private HPEventListener mEventListener;
        public void setListener(HPEventListener el) {
            this.mEventListener = el;
        }
        private bool mWasLeftHandPinching = false;
        private bool mWasLeftHandGrabbing = false;
        private bool mWasRightHandPinching = false;
        private bool mWasRightHandGrabbing = false;

        // constructor
        public HPHandEventSource(HPApp app) {
            this.mApp = app;
        }

        public void update() {
            // recognize hand gestures
            HPHand leftHand = this.mApp.getLeftHand();
            HPHand rightHand = this.mApp.getRightHand();

            if (leftHand.calcPalmPos() == Vector3.zero || rightHand.
                calcPalmPos() == Vector3.zero) {
                return;
            }

            bool isLeftHandPinching = leftHand.isPinching();
            bool isLeftHandGrabbing = leftHand.isGrabbing();

            bool isRightHandPinching = rightHand.isPinching();
            bool isRightHandGrabbing = rightHand.isGrabbing();

            // create events
            // lefthand
            if (!this.mWasLeftHandPinching && isLeftHandPinching) {
                this.mEventListener.leftPinchStart();
            }
            if (this.mWasLeftHandPinching && !isLeftHandPinching) {
                this.mEventListener.leftPinchEnd();
            }
            if (!this.mWasLeftHandGrabbing && isLeftHandGrabbing) {
                this.mEventListener.leftGrabStart();
            }
            if (this.mWasLeftHandGrabbing && !isLeftHandGrabbing) {
                this.mEventListener.leftGrabEnd();
            }

            // righthand
            if (!this.mWasRightHandPinching && isRightHandPinching) {
                this.mEventListener.rightPinchStart();
            }
            if (this.mWasRightHandPinching && !isRightHandPinching) {
                this.mEventListener.rightPinchEnd();
            }
            if (!this.mWasRightHandGrabbing && isRightHandGrabbing) {
                this.mEventListener.rightGrabStart();
            }
            if (this.mWasRightHandGrabbing && !isRightHandGrabbing) {
                this.mEventListener.rightGrabEnd();
            }

            // both hands
            this.mEventListener.handsMove();

            // update cache values
            this.mWasLeftHandPinching = isLeftHandPinching;
            this.mWasLeftHandGrabbing = isLeftHandGrabbing;
            this.mWasRightHandPinching = isRightHandPinching;
            this.mWasRightHandGrabbing = isRightHandGrabbing;
        }
        
    }
}