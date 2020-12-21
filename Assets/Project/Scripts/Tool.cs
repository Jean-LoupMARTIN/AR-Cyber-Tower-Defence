using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public static class Tool
{
    // INPUT
    public static bool PointInRT(Vector2 pos, RectTransform rt) {
        return RectTransformUtility.RectangleContainsScreenPoint(rt, pos);
        /*
        Camera cam = ProjectMan.inst.cam;
        Vector3 rtPos = cam.WorldToScreenPoint(rt.position);
        float
            w = rt.rect.width / 1600 * Screen.width,
            h = rt.rect.height / 1600 * Screen.width,
            l = rtPos.x - w * rt.pivot.x,
            d = rtPos.y - h * rt.pivot.y,
            r = l + w,
            t = d + h;

        return pos.x > l && pos.x < r && pos.y > d && pos.y < t;
        */
    }

    public static bool MouseInRT(RectTransform rt) {
        return PointInRT(Input.mousePosition, rt);
    }

    public static bool Click(RectTransform rt = null, bool on = true) {
        return Input.GetMouseButtonDown(0) && (!rt || MouseInRT(rt) == on);
    }

    public static bool MouseHit(Camera cam, out Vector3 pos, int layer = -1)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if ((layer == -1 && Physics.Raycast(ray, out hit, 1000)) ||
            (layer != -1 && Physics.Raycast(ray, out hit, layer, 1000))) {

            pos = hit.point;
            return true;
        }

        pos = Vector3.zero;
        return false;
    }


    public static bool ScreenCenterHitAR(Camera cam, ARRaycastManager arRaycastManager, out Vector3 pos)
    {
        Vector3 center = cam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (arRaycastManager.Raycast(center, hits, TrackableType.All))
        {
            pos = hits[0].pose.position;
            return true;
        }

        pos = Vector3.zero;
        return false;
    }



    // MATH
    public static float Progress(float t, float max) { return t / max; }

    public static T Last<T>(T[] list) { return list[list.Length - 1]; }
    public static T Last<T>(List<T> list) { return list[list.Count - 1]; }

    // RANDOM
    public static int   Rand(int   max) { return Random.Range(0, max); }
    public static float Rand(float max) { return Random.Range(0, max); }

    public static T Rand<T>(T[]     list) { return list[Rand(list.Length)]; }
    public static T Rand<T>(List<T> list) { return list[Rand(list.Count)]; }

    public static bool Percent(float proba) { return Rand(100f) < proba; }



    // DIRECTION 
    public static float Dist(MonoBehaviour a, MonoBehaviour b) { return Dist(a.transform, b.transform); }
    public static float Dist(Vector3       a, MonoBehaviour b) { return Dist(a,           b.transform); }
    public static float Dist(MonoBehaviour a, Vector3       b) { return Dist(a.transform, b          ); }
    public static float Dist(Transform     a, MonoBehaviour b) { return Dist(a,           b.transform); }
    public static float Dist(MonoBehaviour a, Transform     b) { return Dist(a.transform, b          ); }
    public static float Dist(Transform     a, Transform     b) { return Dist(a.position,  b.position ); }
    public static float Dist(Vector3       a, Transform     b) { return Dist(a,           b.position ); }
    public static float Dist(Transform     a, Vector3       b) { return Dist(a.position,  b          ); }
    public static float Dist(Vector3       a, Vector3       b) { return (a - b).magnitude; }

    public static Vector3 Dir(MonoBehaviour a, MonoBehaviour b, bool normalized = false) { return Dir(a.transform, b.transform, normalized); }
    public static Vector3 Dir(Vector3       a, MonoBehaviour b, bool normalized = false) { return Dir(a,           b.transform, normalized); }
    public static Vector3 Dir(MonoBehaviour a, Vector3       b, bool normalized = false) { return Dir(a.transform, b,           normalized); }
    public static Vector3 Dir(Transform     a, MonoBehaviour b, bool normalized = false) { return Dir(a,           b.transform, normalized); }
    public static Vector3 Dir(MonoBehaviour a, Transform     b, bool normalized = false) { return Dir(a.transform, b,           normalized); }
    public static Vector3 Dir(Transform     a, Transform     b, bool normalized = false) { return Dir(a.position,  b.position,  normalized); }
    public static Vector3 Dir(Vector3       a, Transform     b, bool normalized = false) { return Dir(a,           b.position,  normalized); }
    public static Vector3 Dir(Transform     a, Vector3       b, bool normalized = false) { return Dir(a.position,  b,           normalized); }
    public static Vector3 Dir(Vector3       a, Vector3       b, bool normalized = false) {
        if (normalized) return (b - a).normalized;
        return b - a;
    }

    public static void LookPoint(MonoBehaviour mono, Vector3 point) => LookPoint(mono.transform, point);
    public static void LookPoint(Transform     trans,Vector3 point) => LookDir(trans, Dir(trans, point, false));
    public static void LookDir(MonoBehaviour mono,  Vector3 dir) => LookDir(mono.transform, dir);
    public static void LookDir(Transform     trans, Vector3 dir) => trans.rotation = Quaternion.LookRotation(dir);




    // ANIMATION
    public static bool AnimIs(Animator animator, string name) {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
