using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();

        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshCollider>().convex = true;
    }
    void CreateShape()
    {
        vertices = new Vector3[]
        {
            new Vector3 (0f, 0f, 0f), //0
            new Vector3 (1f, 0f, 0f), //1
            new Vector3 (0f, 0f, 1f), //2
            new Vector3 (1f, 0f, 1f), //3

            new Vector3 (0.5f, 3f, 0.5f), //4
            new Vector3 (0.5f, -1f, 0.5f), //5

            new Vector3 (0f, 2f, 0f), //6
            new Vector3 (1f, 2f, 0f), //7
            new Vector3 (0f, 2f, 1f), //8
            new Vector3 (1f, 2f, 1f), //9
        };

        triangles = new int[]
        {
            4, 7, 6,
            4, 9, 7, 
            6, 8, 4,
            8, 9, 4,

            0, 1, 5,
            1, 3, 5,
            5, 2, 0,
            5, 3, 2,

            6, 1, 0,
            1, 6, 7,
            7, 3, 1,
            3, 7, 9,
            9, 2, 3,
            2, 9, 8,
            8, 0, 2,
            0, 8, 6,
        };
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
