  
using UnityEngine;

namespace HP {
    public static class HPUtil {
        public static readonly Vector2 VECTOR2_NAN = new Vector2(
                float.NaN, float.NaN);
        public static void createDebugSphere(Vector3 pt) {
            GameObject debugSphere = GameObject.CreatePrimitive(
                PrimitiveType.Sphere);
            debugSphere.name = "DebugSphere";
            debugSphere.transform.position = pt;
            debugSphere.transform.localScale = 0.05f * Vector3.one;
            debugSphere.GetComponent<MeshRenderer>().material.color 
                = Color.red;
        }
    }
}