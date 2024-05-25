using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;
using UnityEngine.Events;

public class DragManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float maxLength = 5f; // Set your desired maximum length
    public Color startColor = Color.green;
    public Color endColor = Color.red;
    private Material LrMaterial; 
    private Camera cam;

    
    public UnityEvent OnMouseRelease;
    
    private Vector3 startPoint;
    private Vector3 endPoint;
    private bool isDragging = false;

    public Vector2 DragVector
    {
        get
        {
            return (endPoint - startPoint).normalized;
        }
    }

    
    public float Percent
    {
        get
        {
            return Vector2.Distance(startPoint, endPoint) / maxLength;
        }
    }


    private void OnDrawGizmos()
    {
        // Draw the drag vector
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPoint, endPoint);
        
    }


    void Start()
    {
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = startColor;
        LrMaterial = lineRenderer.material;
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        else if (Input.GetMouseButton(0) )
        {
            ContinueDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
            OnMouseRelease.Invoke();
        }
    }

    public void StartDrawing()
    {
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        startPoint.z = 0;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, startPoint);
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = startColor;
        isDragging = true;
    }

    void ContinueDrawing()
    {
        if (isDragging)
        {
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 0;

            // Limit the length of the line
            float length = Vector2.Distance(startPoint, endPoint);
            if (length > maxLength)
            {
                endPoint = startPoint + (endPoint - startPoint).normalized * maxLength;
            }

            
            // Update the line renderer positions and color
            lineRenderer.SetPosition(1, endPoint);
            float t = length / maxLength; // Interpolate color based on the length
            lineRenderer.endColor = Color.Lerp(startColor, endColor, t);
        }
    }

    
    
    
    public void StopDrawing()
    {
        isDragging = false;

        // Clear the line when releasing the mouse button
        lineRenderer.positionCount = 0;
        
        Debug.Log("DragVector: " + DragVector + ", Percent: " + Percent); 

    }

    public void Hide()
    {
        Color c = LrMaterial.color;
        c.a = 0; 
        lineRenderer.material.color = c;
    }
    
    public void Show()
    {
        Color c = LrMaterial.color;
        c.a = 1; 
        lineRenderer.material.color = c;
    }
}
