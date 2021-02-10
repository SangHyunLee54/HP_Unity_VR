using HP.AppObject;
using UnityEngine;

namespace HP {
    public class HPControlPt : HPAppNoGeom3D {
        // field
        GameObject mCtrlSphere = null;

        private Vector3 mPos = Vector3.zero;
        public Vector3 getPos() {
            return this.mPos;
        }

        // constructor
        public HPControlPt(Vector3 pos) : base("Control Point") {
            // destroy default game object and replace it with prefab
            GameObject.Destroy(this.mGameObject);
            this.mGameObject.name = "Control Point";
            this.mPos = pos;

            mCtrlSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mCtrlSphere.name = "controlSphere";
            mCtrlSphere.transform.position = pos;
            mCtrlSphere.transform.localScale = 0.01f * Vector3.one;
            mCtrlSphere.GetComponent<MeshRenderer>().material.color 
                = Color.red;
        }

        // methods
        protected override void addComponents() {}

        public void move(Vector3 pos) {
            this.mPos = pos;
            this.mCtrlSphere.transform.position = pos;
        }
    }
}