using UnityEngine;
using System.Collections.Generic;

public class ShapeGenerator
{
    public static Mesh CreatePolygon(int sides, float radius, out List<Vector3> edgeVertices)
    {
        Vector3[] vertices = new Vector3[sides + 1];
        int[] triangles = new int[sides * 3];

        vertices[0] = Vector3.zero; // 中心点

        // 通用修正角度公式
        float angleOffset = -90f + (180f / sides);
        edgeVertices = new List<Vector3>();

        // 生成顶点
        for (int i = 0; i < sides; i++)
        {
            float angle = Mathf.Deg2Rad * (360f * i / sides + angleOffset);
            Vector3 point = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
            vertices[i + 1] = point;
            edgeVertices.Add(point);
        }

        // 生成三角形
        for (int i = 0; i < sides; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = (i + 2 > sides) ? 1 : i + 2;
        }

        Mesh mesh = new();
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
