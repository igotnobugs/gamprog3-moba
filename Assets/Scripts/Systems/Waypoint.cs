using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private Lane _lane;

    [SerializeField]
    private Waypoint _toRadiant;

    [SerializeField]
    private Waypoint _toDire;

    public Lane lane { get { return _lane; } }
    public Waypoint toRadiant { get { return _toRadiant; } }
    public Waypoint toDire { get { return _toDire; } }
    public Vector3 groundPosition { private set; get; } 

    private float offSet = 1.0f;

    private void Awake()
    {
        Vector3 skyAbove = transform.position + (Vector3.up * 2000);
        if (Physics.Raycast(skyAbove, Vector3.down, out RaycastHit hit, 90000.0f, 1 << 10))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                groundPosition = hit.point + (Vector3.up * offSet);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 dir;
        Vector3 per;

        if (_toDire != null)
        {
            dir = _toDire.transform.position - transform.position;
            per = Vector3.Cross(dir.normalized, Vector3.down);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(
                transform.position + (per * 2.0f), 
                _toDire.transform.position + (per * 2.0f)
                );
        }

        if (_toRadiant != null)
        {
            dir = _toRadiant.transform.position - transform.position;
            per = Vector3.Cross(dir.normalized, Vector3.down);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                transform.position + (per * 2.0f), 
                _toRadiant.transform.position + (per * 2.0f)
                );
        }

        Gizmos.color = new Color((int)_lane / 1, (int)_lane / 2, (int)_lane / 3, 1);
        Gizmos.DrawSphere(
            transform.position + new Vector3(0, 5, 0), 2.0f
            );

        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            transform.position + new Vector3(0, 5, 0),
            transform.position + new Vector3(0, -20, 0)
            );


        Vector3 skyAbove = transform.position + (Vector3.up * 2000);
        if (Physics.Raycast(skyAbove, Vector3.down, out RaycastHit hit, 90000.0f, 1 << 10))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                Gizmos.DrawSphere(hit.point + (Vector3.up * offSet), 2.0f);
            }
        }
    }

}

public enum Lane
{
    Top, Middle, Bottom, Corner
}
