using UnityEngine;
using HP.AppObject;
using System.Collections.Generic;

namespace HP {
    public class HPBezierSurface : HPAppNoGeom3D{
        // constants
        private static readonly float WIDTH = 0.005f;
        private static readonly Color COLOR = new Color(0.75f, 0.75f, 0.75f);
        private static readonly float MAX_DISTANCE_TO_RENDER = 0.000001f;
        private static readonly int RENDER_SECTION_NUM = 10;

        // field
        private HPApp mApp = null;
        private List<List<HPControlPt>> mControlPts= null;
        public List<List<HPControlPt>> getContorlPts() {
            return this.mControlPts;
        }

        public int getDegree() {
            return this.mControlPts[0].Count;
        }

        private Color mColor = Color.red;
        public void setColor(Color color) {
            this.mColor = color;
            this.update();
        }

        // constructor
        public HPBezierSurface(HPApp app) : 
            base("Bezier Surface") {
            this.mApp = app;
            this.mControlPts = new List<List<HPControlPt>>();
        }

        // add control points from curves
        public void addCurve(HPBezierCurve bc) {
            if (this.mControlPts.Count != 0 &&
                this.getDegree() < bc.getDegree()) {
                for (int i = 0; i < bc.getDegree() - this.getDegree(); i++) {
                    this.degreeElevate();
                }
            } else if (this.mControlPts.Count != 0 &&
                this.getDegree() > bc.getDegree()) {
                for (int i = 0; i < this.getDegree() - bc.getDegree(); i++) {
                    bc.degreeElevate();
                }
            }
            this.mControlPts.Add(bc.getContorlPts());
            this.mApp.getBezierCurveMgr().removeCurve(bc);
            this.update();
        }

        public void update() {
            HPAppSurface3D surface = new HPAppSurface3D("Bezier Surface",
                this.calcPts(HPBezierSurface.RENDER_SECTION_NUM),
                this.calcTriangles(HPBezierSurface.RENDER_SECTION_NUM),
                HPBezierSurface.COLOR);
            if (this.getChildren().Count != 0) {
                HPAppObject child = this.getChildren()[0];
                this.removeChild(child);
                child.destroyGameObject();
            }
            addChild(surface);
        }

        private Mesh calcMesh(int renderSectionNum) {
            Mesh mesh = new Mesh();
            mesh.vertices = this.calcPts(renderSectionNum).ToArray();
            mesh.triangles = this.calcTriangles(renderSectionNum).ToArray();
            return mesh;
        }

        private List<int> calcTriangles(int renderSectionNum) {
            List<int> triangles = new List<int>();
            for (int i = 0; i < renderSectionNum; i++) {
                for (int j = 0; j < renderSectionNum; j++) {
                    triangles.Add(i * (renderSectionNum + 1) + j);
                    triangles.Add(i * (renderSectionNum + 1) + j + 1);
                    triangles.Add((i + 1) * (renderSectionNum + 1) + j);
                    triangles.Add((i + 1) * (renderSectionNum + 1) + j + 1);
                    triangles.Add((i + 1) * (renderSectionNum + 1) + j);
                    triangles.Add(i * (renderSectionNum + 1) + j + 1);
                }
            }
            return triangles;
        }

        private List<Vector3> calcPts(int renderSectionNum) {
            // convert List of HPControlPts to Vector3
            List<List<Vector3>> controlPts = new List<List<Vector3>>();
            for (int i = 0; i < this.mControlPts.Count; i++) {
                List<Vector3> ptRow = new List<Vector3>();
                for (int j = 0; j < this.mControlPts[i].Count; j++) {
                    ptRow.Add(this.mControlPts[i][j].getPos());
                }
                controlPts.Add(ptRow);
            }

            // result points list
            List<Vector3> pts = new List<Vector3>();

            // calculate iso-parametric curve
            // add the point list to result
            for (int section = 0; section <= renderSectionNum; section++) {
                // get control point of iso-parametric curve
                List<Vector3> isoControlPts = new List<Vector3>();
                for (int curvenum = 0; curvenum < controlPts.Count; curvenum++) {
                    // calculate with de-casteljau's algorithm
                    List<List<Vector3>> middlePts = new List<List<Vector3>>();
                    middlePts.Add(controlPts[curvenum]);

                    for (int i = 1; i < controlPts[curvenum].Count; i++) {
                        List<Vector3> mPts = new List<Vector3>();
                        for (int j = 1; j < controlPts[curvenum].Count - i + 1; j++) {
                            mPts.Add(
                                ((renderSectionNum - section) * middlePts[i - 1][j] + 
                                    section * middlePts[i - 1][j - 1]) 
                                / renderSectionNum);
                        }
                        middlePts.Add(mPts);
                    }
                    isoControlPts.Add(middlePts[controlPts[curvenum].Count - 1][0]);
                }
                pts.AddRange(this.calcCurvePts(isoControlPts, renderSectionNum));
            }

            return pts;
        }

        private List<Vector3> calcCurvePts(List<Vector3> cPts,
            int renderSectionNum) {
            List<Vector3> pts = new List<Vector3>();

            // get points from de casteljau's algorithm.

            for (int section = 0; section <= renderSectionNum; section++) {
                List<List<Vector3>> middlePts = new List<List<Vector3>>();
                middlePts.Add(cPts);

                for (int i = 1; i < cPts.Count; i++) {
                    List<Vector3> mPts = new List<Vector3>();
                    for (int j = 1; j < cPts.Count - i + 1; j ++) {
                        mPts.Add(
                            ((renderSectionNum - section) * middlePts[i - 1][j] + 
                                section * middlePts[i - 1][j - 1]) 
                            / renderSectionNum);
                    }
                    middlePts.Add(mPts);
                }
                pts.Add(middlePts[cPts.Count - 1][0]);
            }

            return pts;
        }

        private bool isEnoughToRender(List<Vector3> pts) {
            List<Vector3> meshDirVectors = new List<Vector3>();
            int x_num = mControlPts.Count;
            int y_num = mControlPts[0].Count;

            for (int i = 0; i < x_num - 1; i++) {
                for (int j = 0; j < y_num - 1; j++) {
                    Vector3 vec1 = this.mControlPts[i + 1][j].getPos()
                        - this.mControlPts[i][j].getPos();
                    Vector3 vec2 = this.mControlPts[i][j + 1].getPos()
                        - this.mControlPts[i][j].getPos();
                    Vector3 vec3 = this.mControlPts[i][j + 1].getPos()
                        - this.mControlPts[i + 1][j + 1].getPos();
                    Vector3 vec4 = this.mControlPts[i + 1][j].getPos()
                        - this.mControlPts[i + 1][j + 1].getPos();
                    meshDirVectors.Add(
                        Vector3.Normalize(Vector3.Cross(vec1, vec2)));
                    meshDirVectors.Add(
                        Vector3.Normalize(Vector3.Cross(vec3, vec4)));
                }
            }
            return true;
        }

        // degree elevation
        public void degreeElevate() {
            List<List<HPControlPt>> elevatedList = 
                new List<List<HPControlPt>>();
            int n = this.mControlPts[0].Count - 1;
            for (int j = 0; j < this.mControlPts.Count; j++) {
                List<HPControlPt> elevatedRow = new List<HPControlPt>();
                elevatedRow.Add(this.mControlPts[j][0]);
                for (int i = 1; i <= n; i++) {
                    Vector3 newPos = Vector3.zero;
                    newPos += this.mControlPts[j][i - 1].
                        getPos() * (i / (n + 1));
                    newPos += this.mControlPts[j][i].
                        getPos() * (1 - (i / (n + 1)));
                    HPControlPt newCPt = new HPControlPt(newPos);
                    elevatedRow.Add(newCPt);
                }
                elevatedRow.Add(mControlPts[j][n]);
            }

            for (int i = 0; i < this.mControlPts.Count; i++) {
                for (int j = 0; j < this.mControlPts[i].Count; j++)
                {
                    this.mControlPts[i][j].destroyGameObject();
                    this.mApp.getControlPtMgr().getControlPts().
                        Remove(this.mControlPts[i][j]);
                }
            }
            this.mControlPts = elevatedList;
            this.update();
        }
    }
}