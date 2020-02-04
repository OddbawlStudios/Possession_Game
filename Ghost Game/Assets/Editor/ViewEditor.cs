using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyBehaviour))]
public class ViewEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyBehaviour eb = (EnemyBehaviour)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(eb.transform.position, Vector3.forward, Vector3.up, 360, eb.radius);
        Vector3 viewAngleA = eb.DirfromAngle(-eb.fieldOfView / 2,false);
        Vector3 viewAngleB = eb.DirfromAngle(eb.fieldOfView / 2, false);

        Handles.DrawLine(eb.transform.position,eb.transform.position + viewAngleA * eb.radius);
        Handles.DrawLine(eb.transform.position, eb.transform.position + viewAngleB * eb.radius);

        Handles.DrawLine(eb.transform.position, eb.target.position);
    }
}
