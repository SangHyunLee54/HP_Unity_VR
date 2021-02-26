using System.Collections.Generic;
using UnityEngine;
namespace HP.Geom {
    public class HPSurface3D : HPGeom3D {
        // fields
        private List<Vector3> mPts = null;
        public List<Vector3> getPts() {
            return this.mPts;
        }
        
        private List<int> mTriangles = null;
        public List<int> getTriangles() {
            return this.mTriangles;
        }

        // constructor
        public HPSurface3D(List<Vector3> pts, List<int> triangles) {
            this.mPts = pts;
            this.mTriangles = triangles;
        }
        // methods
        public Mesh calcMesh() {
            Mesh mesh = new Mesh();
            mesh.vertices = this.mPts.ToArray();
            mesh.triangles = this.mTriangles.ToArray();
            return mesh;
        }
    }
}