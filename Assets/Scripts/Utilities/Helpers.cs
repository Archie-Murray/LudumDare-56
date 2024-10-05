using UnityEngine;

namespace Utilities {
public class Helpers : Singleton<Helpers> {
    protected override void Awake() {
        base.Awake();
        MainCamera = Camera.main;
    }
    public Camera MainCamera;
    public static Vector2 FromRadians(float radians) {
        return new Vector2(Mathf.Sin(radians), Mathf.Cos(radians));
    }
}
}