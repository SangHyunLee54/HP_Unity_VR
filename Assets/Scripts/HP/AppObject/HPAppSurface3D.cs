using System.Collections.Generic;
using HP.Geom;
using UnityEngine;

namespace HP.AppObject {
    public class HPAppSurface3D : HPAppGeom3D {
        //fields
        private Color mColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }
        public void setGeom(List<Vector3> pts, List<int> triangles) {
            this.mGeom = new HPSurface3D(pts, triangles);
            this.refreshAtGeomChange();
        }

        //constructor

        public HPAppSurface3D(string name, List<Vector3> pts,
            List<int> triangles, Color color) :  base($"{ name }/Polyline3D") {

            this.mGeom = new HPSurface3D(pts, triangles);

            this.refreshAtGeomChange();
        }

        protected override void addComponents() {
            this.mGameObject.AddComponent<MeshFilter>();
            this.mGameObject.AddComponent<MeshRenderer>();
            this.mGameObject.AddComponent<MeshCollider>();
        }

        protected override void refreshRenderer() {
            HPSurface3D surface = (HPSurface3D) this.mGeom;
            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = surface.calcMesh();
            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            mr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            // mr.material = new Material(Shader. 
            //    Find("Custom/BezierSurfaceShader"));
            mr.material.color = this.mColor;
        }

        protected override void refreshCollider() {
        }
    }

}