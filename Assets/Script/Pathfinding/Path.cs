using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector3> controlPoints = new List<Vector3>();

    private void OnDrawGizmos()
    {
        for (int i = 0; i < controlPoints.Count; i++)
        {
            if (i == 0)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.white;
            }
            if (i == controlPoints.Count - 1)
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(controlPoints[i], 0.1f);

        }

        for (int i = 0; i < controlPoints.Count; i++)
        {
            Gizmos.color = Color.white;
            CatmullSpline.DisplayCatmullRomSpline(i, this); // Calc full spline length.
        }
    }
}
