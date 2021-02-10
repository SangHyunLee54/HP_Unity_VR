using HP.AppObject;
using UnityEngine;

/*
(Left hand)

pt    rt    mt    it

p3    r3    m3    i3

p2    r2    m2    i2

p1    r1    m1    i1    tht

                        th3

p0                      th2

                  th1
                
                th0

          s

          fs

fs: forearm stub
s: start
th: thumb
i: index
m: middle
r: ring
p: pinky
t: tip
*/
namespace HP {
    public class HPHand : HPAppObject3D {
        // constants
        public enum Handedness { LEFT, RIGHT};
        public readonly static float GRAB_THRESHHOLD = 0.03f;

        // fields
        private Handedness mHandedness;
        public Handedness getHandedness() {
            return this.mHandedness;
        }
        private OVRHand mHand;
        private OVRSkeleton mSkeleton;
        public OVRSkeleton getSkeleton() {
            return this.mSkeleton;
        }
        private HPAppNoGeom3D mAnchor;
        public HPAppNoGeom3D getAnchor() {
            return this.mAnchor;
        }
        // reference forwar dir in local hand coord
        // that the anchor shoul try to look toward
        private Vector3 mAnchorForward;
        public Vector3 getAnchorForward() {
            return this.mAnchorForward;
        }
        public void setAnchorForward(Vector3 anchorForward) {
            this.mAnchorForward = anchorForward;
        }

        // constructor
        public HPHand(GameObject handPrefab) : base("Hand") {
            
            // destroy default game object and replace it with prefab
            // this game object's pos and rot is that of 'start'
            GameObject.Destroy(this.mGameObject);
            this.mGameObject = handPrefab;

            // hand
            this.mHand = this.mGameObject.GetComponent<OVRHand>();
            this.mSkeleton = this.mGameObject.GetComponent<OVRSkeleton>();
            
            if (this.mSkeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.
                HandLeft) {
                this.mHandedness = HPHand.Handedness.LEFT;
                this.mGameObject.name = "LeftHand";
            } else if (this.mSkeleton.GetSkeletonType() == OVRSkeleton.
                SkeletonType.HandRight) {
                this.mHandedness = HPHand.Handedness.RIGHT;
                this.mGameObject.name = "RightHand";
            }

            // anchor
            this.mAnchor = new HPAppNoGeom3D("Anchor");
        }

        protected override void addComponents() {}

        // methods
        public OVRBone getBone(OVRSkeleton.BoneId boneId) {
            foreach (OVRBone bone in this.mSkeleton.Bones) {
                if (bone.Id == boneId) {
                    return bone;
                }
            }
            return null;
        }

        public OVRBoneCapsule getBoneCapsule(OVRSkeleton.BoneId boneId) {
            foreach (OVRBoneCapsule boneCapsule in this.mSkeleton.Capsules) {
                if (boneCapsule.BoneIndex == (short) boneId) {
                    return boneCapsule;
                }
            }
            return null;
        }

        public Vector3 calcPalmPos() {
            if (this.getBone(OVRSkeleton.BoneId.Hand_Thumb1) == null ||
                this.getBone(OVRSkeleton.BoneId.Hand_Index1) == null ||
                this.getBone(OVRSkeleton.BoneId.Hand_Pinky1) == null) {
                    return Vector3.zero;
                }
            Vector3 thumb1Pos = this.getBone(OVRSkeleton.BoneId.Hand_Thumb1).
                Transform.position;
            Vector3 index1Pos = this.getBone(OVRSkeleton.BoneId.Hand_Index1).
                Transform.position;
            Vector3 pinky1Pos = this.getBone(OVRSkeleton.BoneId.Hand_Pinky1).
                Transform.position;
            return (thumb1Pos + index1Pos + pinky1Pos) / 3f;
        }

        public Vector3 calcPalmUpDir() {
            if (this.mHandedness == HPHand.Handedness.LEFT) {
                return this.getGameObject().transform.up;
            } else {
                return -this.getGameObject().transform.up;
            }
        }

        public Vector3 calcPinchPos() {
            Vector3 indexTipPos = this.getBone(OVRSkeleton.BoneId.
                Hand_IndexTip).Transform.position;
            Vector3 thumbTipPos = this.getBone(OVRSkeleton.BoneId.
                Hand_ThumbTip).Transform.position;
            return (indexTipPos + thumbTipPos) / 2f;
        }

        public Transform getPointerPose() {
            return this.mHand.PointerPose;
        }

        public bool isPinching() {
            return this.mHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        }

        public bool isGrabbing() {
            Vector3 palmPos = this.calcPalmPos();
            Vector3 middleTipPos = this.getBone(OVRSkeleton.BoneId.
                Hand_MiddleTip).Transform.position;
            Vector3 ringTipPos = this.getBone(OVRSkeleton.BoneId.
                Hand_RingTip).Transform.position;
            return Vector3.Distance(palmPos, middleTipPos) <
                HPHand.GRAB_THRESHHOLD &&
                Vector3.Distance(palmPos, ringTipPos) <
                HPHand.GRAB_THRESHHOLD;
        }
    }
}