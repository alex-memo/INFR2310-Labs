using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
/// <summary>
/// @alex-memo 2023
/// </summary>
[CustomEditor(typeof(PathManager))]
public class PathManagerEditor : Editor
{
    private PathManager pathManager;
    private List<Waypoint> path;
    private Waypoint selectedPoint;

    private void OnEnable()
    {
        pathManager = target as PathManager;
    }
    private void OnSceneGUI()
    {
        drawPath(pathManager.GetPath());
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        path = pathManager.GetPath();

        base.OnInspectorGUI();
        drawPathInEditor();
        SceneView.RepaintAll();
    }
    private void drawPathInEditor()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Path");
        drawGUIForPoints();
        if (GUILayout.Button("Add Point to Path"))
        {
            pathManager.CreateAddPoint();
        }
        EditorGUILayout.EndVertical();
    }
    private void drawGUIForPoints()
    {
        if (path == null || path.Count == 0) return;

        for (int _i = path.Count - 1; _i >= 0; _i--)
        {
            Waypoint _point = path[_i];
            EditorGUILayout.BeginHorizontal();

            Color _originalColor = GUI.color;
            GUI.color = selectedPoint == _point ? Color.green : _originalColor;

            Vector3 _newPos = EditorGUILayout.Vector3Field("", _point.Position);
            if (EditorGUI.EndChangeCheck())
            {
                _point.Position = _newPos;
            }

            GUI.color = _originalColor;  // Reset color

            if (GUILayout.Button("-", GUILayout.Width(25)))
            {
                path.RemoveAt(_i);
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    private void drawPath(List<Waypoint> _waypoints)
    {
        if (_waypoints == null) return;

        for (int i = 0; i < _waypoints.Count; i++)
        {
            if (drawPoint(_waypoints[i]))
            {
                Repaint();
            }
            drawPathLine(_waypoints[i], _waypoints[(i + 1) % _waypoints.Count]);
        }
    }
    private void drawPathLine(Waypoint _pointA, Waypoint _pointB)
    {
        Handles.color = Color.gray;
        Handles.DrawLine(_pointA.Position, _pointB.Position);
    }

    private bool drawPoint(Waypoint _waypoint)
    {
        bool _isChanged = false;
        float _handleSize = HandleUtility.GetHandleSize(_waypoint.Position);

        Color _originalColor = Handles.color;

        if (selectedPoint == _waypoint)
        {
            Handles.color = Color.green;
            EditorGUI.BeginChangeCheck();
            Vector3 _newPos = Handles.PositionHandle(_waypoint.Position, Quaternion.identity);
            Handles.SphereHandleCap(-1, _newPos, Quaternion.identity, 0.25f * _handleSize, EventType.Repaint);

            if (EditorGUI.EndChangeCheck())
            {
                _waypoint.Position = _newPos;
            }
        }
        else if (Handles.Button(_waypoint.Position, Quaternion.identity, 0.25f * _handleSize, 0.25f * _handleSize, Handles.SphereHandleCap))
        {
            _isChanged = true;
            selectedPoint = _waypoint;
        }

        Handles.color = _originalColor;
        return _isChanged;
    }
}
