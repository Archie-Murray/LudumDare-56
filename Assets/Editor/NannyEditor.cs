using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Nanny))]
public class NannyEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if (GUILayout.Button("Spawn Wave")) {
            Nanny nanny = target as Nanny;
            nanny.StartWave();
        }
    }
}