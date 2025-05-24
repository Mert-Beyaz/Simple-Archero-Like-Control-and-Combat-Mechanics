using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    [Header("Settings")]
    public int resolution = 30; // Nokta sayısı
    public float simulationTime = 2f; // Simülasyon süresi

    [Header("References")]
    public LineRenderer lineRenderer;

    public void DrawTrajectory(Vector3 start, Vector3 velocity, float gravity)
    {
        lineRenderer.positionCount = resolution;
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < resolution; i++)
        {
            float t = i * simulationTime / resolution;

            Vector3 point = start + velocity * t + 0.5f * Vector3.down * gravity * t * t;
            points.Add(point);
        }

        lineRenderer.SetPositions(points.ToArray());
    }

    public void HideTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}