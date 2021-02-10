using HP.AppObject;
using UnityEngine;

namespace HP {
    public class HPEye : HPAppObject3D {
        // constructor
        public HPEye(GameObject eyePrefab) : base("Eye") {
            // destroy default game object and replace it with prefab
            GameObject.Destroy(this.mGameObject);
            this.mGameObject = eyePrefab;
            this.mGameObject.name = "Eye";
        }

        // methods
        protected override void addComponents() {}
    }
}