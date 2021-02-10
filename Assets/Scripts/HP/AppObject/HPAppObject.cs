using System.Collections.Generic;
using UnityEngine;

namespace HP.AppObject {
    public abstract class HPAppObject {
        //fields
        protected GameObject mGameObject = null;
        public GameObject getGameObject() {
            return this.mGameObject;
        }

        protected List<HPAppObject> mChildren = null;
        public List<HPAppObject> getChildren() {
            return this.mChildren;
        }

        //constructor
        public HPAppObject(string name) {
            this.mGameObject = new GameObject(name);
            this.mChildren = new List<HPAppObject>();
            this.addComponents();
        }

        //methods
        protected abstract void addComponents();

        public void addChild(HPAppObject child) {
            this.mChildren.Add(child);
            GameObject childGameObject = child.getGameObject();

            Vector3 localPos = childGameObject.transform.localPosition;
            Quaternion localRot = childGameObject.transform.localRotation;
            Vector3 localScale = childGameObject.transform.localScale;

            childGameObject.transform.parent = this.mGameObject.transform;

            childGameObject.transform.localPosition = localPos;
            childGameObject.transform.localRotation = localRot;
            childGameObject.transform.localScale = localScale;
        }

        public void removeChild(HPAppObject child) {
            this.mChildren.Remove(child);
            GameObject childGameObject = child.getGameObject();

            Vector3 localPos = childGameObject.transform.localPosition;
            Quaternion localRot = childGameObject.transform.localRotation;
            Vector3 localScale = childGameObject.transform.localScale;

            childGameObject.transform.parent = null;

            childGameObject.transform.localPosition = localPos;
            childGameObject.transform.localRotation = localRot;
            childGameObject.transform.localScale = localScale;
        }

        public void destroyGameObject() {
            GameObject.Destroy(this.mGameObject);
            foreach (HPAppObject child in this.mChildren) {
                child.destroyGameObject();
            }
        }
    }

}