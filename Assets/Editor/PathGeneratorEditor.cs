using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PathGenerator))]
public class PathGeneratorEditor : Editor
{

    void OnSceneGUI()
    {
        PathGenerator path = (PathGenerator)target;
        Handles.color = Color.white;
        if (path.Positions.Count > 0)
        {
            Vector3 previousPos = Vector3.zero;
            if (path.Positions[0] != null)
            {
                previousPos = path.Positions[0].position;
            }

            for (int i = 0; i < path.Positions.Count; i++)
            {
                if (path.Positions[i] != null)
                {
                    Handles.DrawLine(previousPos, path.Positions[i].position);
                    previousPos = path.Positions[i].position;
                }
                else
                {
                    path.Positions.RemoveAt(i);
                }

            }
        }

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PathGenerator path = (PathGenerator)target;
        if (GUILayout.Button("Add Position"))
        {
            GameObject go = new GameObject();
            go.transform.SetParent(path.transform);
            go.transform.position = path.transform.position;
            go.name = "Position " + path.Positions.Count;
            path.Positions.Add(go.transform);
            EditorUtility.SetDirty(path.GetComponent<PathGenerator>());
        }
    }

}