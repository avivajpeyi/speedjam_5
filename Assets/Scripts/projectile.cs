using UnityEngine;

public class ProjectilePath : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody2D playerRigidbody;
    public int resolution = 10;

    void Start()
    {
        DrawPath();
    }

    void DrawPath()
    {
        lineRenderer.positionCount = resolution + 1;
        Vector3[] points = new Vector3[resolution + 1];

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            points[i] = CalculatePoint(t);
        }

        lineRenderer.SetPositions(points);
    }

    Vector3 CalculatePoint(float t)
    {
        float velocity = playerRigidbody.velocity.magnitude;
        float angle = Vector2.Angle(Vector2.right, playerRigidbody.velocity) * Mathf.Deg2Rad;
        float g = Physics2D.gravity.magnitude * playerRigidbody.gravityScale;
        float x = t;
        float y = x * Mathf.Tan(angle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(angle) * Mathf.Cos(angle)));
        return new Vector3(x, y);
    }
}
