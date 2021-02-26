using X;
using UnityEngine;
using HP.Cmd;
using System.Collections.Generic;

namespace HP.Scenario {
    public partial class HPEditCurveScenario {
        public class MoveControlPointRightScene : HPScene {
            // fields
            private HPControlPt movePt = null;
            // to create surface
            private HPControlPt surfacePt = null;

            private static MoveControlPointRightScene mSingleton = null;
            public static MoveControlPointRightScene getSingleton() {
                Debug.Assert(MoveControlPointRightScene.mSingleton != null);
                return MoveControlPointRightScene.mSingleton;
            }
            
            public static MoveControlPointRightScene createSingleton(
                XScenario scenario) {
                Debug.Assert(MoveControlPointRightScene.mSingleton == null);
                MoveControlPointRightScene.mSingleton = 
                    new MoveControlPointRightScene(scenario);
                return MoveControlPointRightScene.mSingleton;
            }

            // constructor
            private MoveControlPointRightScene(XScenario scenario) : base(scenario) {
            }

            public override void handleLeftPinchStart() {
                Debug.Log("Left Pinch Start");
                HPApp app = (HPApp)this.mScenario.getApp();
                // if pinch other curve's point
                if (isOtherCurveControlPt(app.getLeftHand())) {
                    // create bezier surface from 2 bezier curves.
                    HPBezierCurve curve1 = null;
                    HPBezierCurve curve2 = null;
                    foreach (HPBezierCurve bc in 
                        app.getBezierCurveMgr().getCurves()) {
                        if(bc.getContorlPts().Contains(this.movePt)) {
                            curve2 = bc;
                        } else if(bc.getContorlPts().Contains(this.surfacePt)) {
                            curve1 = bc;
                        }
                    }
                    HPCmdToCreateBezierSurface.execute(app);
                    HPBezierSurface bs = app.getBezierSurfaceMgr().
                        getCurSurface();
                    Debug.Log(curve1.getContorlPts().Count);
                    Debug.Log(curve2.getContorlPts().Count);
                    HPCmdToAddBezierCurveToSurface.execute(app, bs, curve1);
                    HPCmdToAddBezierCurveToSurface.execute(app, bs, curve2);

                } else {
                    XCmdToChangeScene.execute(app,
                        HPEditCurveScenario.MoveControlPointBothScene.
                        getSingleton(), this);
                }
            }

            public override void handleLeftPinchEnd() {
                Debug.Log("Left Pinch End");
            }

            public override void handleLeftGrabStart() {
                Debug.Log("Left Grab Start");
            }

            public override void handleLeftGrabEnd() {
                HPApp app = (HPApp)this.mScenario.getApp();
                HPCmdToCreateControlPoint.execute(app, app.getLeftHand());
                Debug.Log("Left Grab End");
            }

            public override void handleRightPinchStart() {
                Debug.Log("Right Pinch Start");
            }

            public override void handleRightPinchEnd() {
                Debug.Log("Right Pinch End");
                HPApp app = (HPApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app,
                            HPEditCurveScenario.EditCurveReadyScene.
                            getSingleton(), this);
            }

            public override void handleRightGrabStart() {
                Debug.Log("Right Grab Start");
            }

            public override void handleRightGrabEnd() {
                Debug.Log("Right Grab End");
            }

            public override void handleHandsMove() {
                if (this.movePt == null) {
                    return;
                }
                HPApp app = (HPApp)this.mScenario.getApp();
                HPCmdToMoveControlPoint.execute(app, app.getRightHand(), 
                    this.movePt);
                
            }

            public override void getReady() {
                setMovePt();
            }

            public override void wrapUp() {
            }

            // set a control point to move
            private void setMovePt() {
                HPApp app = (HPApp)this.mScenario.getApp();
                List<HPControlPt> controlPts = app.getBezierCurveMgr().
                    getCurCurve().getContorlPts();
                Vector3 pinchPos = app.getRightHand().calcPinchPos();
                float sqrMinDistance = HPControlPt.MIN_DIST_PINCH;
                HPControlPt closestPt = null;
                foreach(HPControlPt cpt in controlPts) {
                    Vector3 distance = cpt.getPos() - pinchPos;
                    float sm = Vector3.SqrMagnitude(distance);
                    if(sm < sqrMinDistance) {
                        sqrMinDistance = sm;
                        closestPt = cpt;
                    }
                }
                this.movePt = closestPt;
            }

            // whether pinch other bezier curve's point
            private bool isOtherCurveControlPt(HPHand hand) {
                HPApp app = (HPApp)this.mScenario.getApp();
                List<HPControlPt> controlPts = app.getControlPtMgr().
                    getControlPts();
                Vector3 pinchPos = hand.calcPinchPos();
                float sqrMinDistance = HPControlPt.MIN_DIST_PINCH;
                HPControlPt closestPt = null;
                foreach(HPControlPt cpt in controlPts) {
                    Vector3 distance = cpt.getPos() - pinchPos;
                    float sm = Vector3.SqrMagnitude(distance);
                    if(sm < sqrMinDistance) {
                        sqrMinDistance = sm;
                        closestPt = cpt;
                    }
                }

                if (closestPt == null) {
                    return false;
                }

                // closest Point is in Current Curve.
                if (app.getBezierCurveMgr().getCurCurve().getContorlPts().
                    Contains(closestPt)) {
                        return false;
                } else {
                    this.surfacePt = closestPt;
                    return true;
                }
            }
        }
    }
    
}