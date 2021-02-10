using HP.Geom;
using UnityEngine;

namespace HP.AppObject {
    public abstract class HPAppGeom3D : HPAppObject3D {
        //fields
        protected HPGeom3D mGeom = null;
        public HPGeom3D getGeom3D() {
            return this.mGeom;
        }
        public void setGeom(HPGeom3D geom) {
            this.mGeom = geom;
            this.refreshAtGeomChange();
        }
        public Collider getCollider() {
            Collider collider = this.mGameObject.GetComponent<Collider>();
            return collider;
        }

        //constructor
        public HPAppGeom3D(string name) : base(name) {

        }

        //methods
        public void refreshAtGeomChange() {
            this.refreshRenderer();
            this.refreshCollider();
        }

        protected abstract void refreshRenderer();
        protected abstract void refreshCollider();
    }
}