using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class WaypointManager : Singleton<WaypointManager>
{
    [Header("Radiant must be at the top going down to Dire")]
    [SerializeField]
    private Waypoint[] _topWaypointSet;

    [SerializeField]
    private Waypoint[] _midWaypointSet;

    [SerializeField]
    private Waypoint[] _botWaypointSet;

    public Waypoint[] topWaypointSet { get { return _topWaypointSet; } }
    public Waypoint[] midWaypointSet { get { return _midWaypointSet; } }
    public Waypoint[] botWaypointSet { get { return _botWaypointSet; } }

    /*
    [ContextMenu("Connect Waypoint Paths")]
    private void ConnectWaypointPaths()
    {
        SerializedObject curWaypoint;
        SerializedObject toRadiantWaypoint;
        SerializedObject toDireWaypoint;

        for (int i = 0; i < _topWaypointSet.Length; i++)
        {
            curWaypoint = new SerializedObject(_topWaypointSet[i]);

            if (i == _topWaypointSet.Length - 1 || i == 0)
            {
                curWaypoint.FindProperty("_lane").intValue = (int)Lane.Corner;
                curWaypoint.FindProperty("_toRadiant").objectReferenceValue = null;
                curWaypoint.FindProperty("_toDire").objectReferenceValue = null;
            } else
            {
                toRadiantWaypoint = new SerializedObject(_topWaypointSet[i - 1]);
                toDireWaypoint = new SerializedObject(_topWaypointSet[i + 1]);

                curWaypoint.FindProperty("_lane").intValue = (int)Lane.Top;
                curWaypoint.FindProperty("_toRadiant").objectReferenceValue = toRadiantWaypoint.targetObject;
                curWaypoint.FindProperty("_toDire").objectReferenceValue = toDireWaypoint.targetObject;
            }
            curWaypoint.ApplyModifiedProperties();
        }

        for (int i = 0; i < _midWaypointSet.Length; i++)
        {
            curWaypoint = new SerializedObject(_midWaypointSet[i]);

            if (i == _midWaypointSet.Length - 1 || i == 0)
            {
                curWaypoint.FindProperty("_lane").intValue = (int)Lane.Corner;
                curWaypoint.FindProperty("_toRadiant").objectReferenceValue = null;
                curWaypoint.FindProperty("_toDire").objectReferenceValue = null;
            } else
            {
                toRadiantWaypoint = new SerializedObject(_midWaypointSet[i - 1]);
                toDireWaypoint = new SerializedObject(_midWaypointSet[i + 1]);

                curWaypoint.FindProperty("_lane").intValue = (int)Lane.Middle;
                curWaypoint.FindProperty("_toRadiant").objectReferenceValue = toRadiantWaypoint.targetObject;
                curWaypoint.FindProperty("_toDire").objectReferenceValue = toDireWaypoint.targetObject;
            }
            curWaypoint.ApplyModifiedProperties();        
        }

        for (int i = 0; i < _botWaypointSet.Length; i++)
        {
            curWaypoint = new SerializedObject(_botWaypointSet[i]);

            if (i == _botWaypointSet.Length - 1 || i == 0)
            {
                curWaypoint.FindProperty("_lane").intValue = (int)Lane.Corner;
                curWaypoint.FindProperty("_toRadiant").objectReferenceValue = null;
                curWaypoint.FindProperty("_toDire").objectReferenceValue = null;
            } else
            {
                toRadiantWaypoint = new SerializedObject(_botWaypointSet[i - 1]);
                toDireWaypoint = new SerializedObject(_botWaypointSet[i + 1]);

                curWaypoint.FindProperty("_lane").intValue = (int)Lane.Bottom;
                curWaypoint.FindProperty("_toRadiant").objectReferenceValue = toRadiantWaypoint.targetObject;
                curWaypoint.FindProperty("_toDire").objectReferenceValue = toDireWaypoint.targetObject;
            }
            curWaypoint.ApplyModifiedProperties();
        }
        Debug.Log("Done connecting");
    }


    [ContextMenu("Test Waypoint Paths")]
    private void TestWaypointPaths()
    {
        for (int i = 0; i < _topWaypointSet.Length; i++)
        {
            if (_topWaypointSet[i].lane == Lane.Corner) continue;

            if (_topWaypointSet[i].toDire == null || _topWaypointSet[i].toRadiant == null)
            {
                Debug.LogError("Non Corner Waypoint missing a pointer.", gameObject);
                Debug.Break();
            }
        }

        for (int i = 0; i < _midWaypointSet.Length; i++)
        {
            if (_midWaypointSet[i].lane == Lane.Corner) continue;

            if (_midWaypointSet[i].toDire == null || _midWaypointSet[i].toRadiant == null)
            {
                Debug.LogError("Non Corner Waypoint missing a pointer.", gameObject);
                Debug.Break();
            }
        }

        for (int i = 0; i < _botWaypointSet.Length; i++)
        {
            if (_botWaypointSet[i].lane == Lane.Corner) continue;

            if (_botWaypointSet[i].toDire == null || _botWaypointSet[i].toRadiant == null)
            {
                Debug.LogError("Non Corner Waypoint missing a pointer.", gameObject);
                Debug.Break();
            }
        }

        Debug.Log("All clear.");
    }
    */
}
