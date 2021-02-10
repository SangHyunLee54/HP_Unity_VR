namespace HP.AppObject {
    public abstract class HPAppObject3D : HPAppObject {
        public HPAppObject3D(string name) : base(name) {
            this.mGameObject.layer = 0; //default layer
        }
    }
}