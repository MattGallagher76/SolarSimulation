using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineTest : MonoBehaviour
{
    public Material material;
    public Vector3[] testingList;

    public float dist;
    public Vector3[] circleParts;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    //Test

    // Start is called before the first frame update
    void Start()//Normals are based on the vertex not the faces. 
    {
        //Debug.Log(dist);
        testingList = new Vector3[]
        {
            new Vector3(1.5f, 0, 0),
            new Vector3(1, 0, 1),
            new Vector3(0, 0, 1.5f),
            new Vector3(-1, 0, 1),
            new Vector3(-1.5f, 0, 0),
            new Vector3(-1, 0, -1.5f),
            new Vector3(0, 0, -2.5f),
            new Vector3(2, 0, -2)
        };

        circleParts = new Vector3[testingList.Length * 2];
        for (int i = 0; i < testingList.Length; i ++)
        {
            circleParts[i * 2] = testingList[i] * (1 + dist/2);
            circleParts[i * 2 + 1] = testingList[i] * (1 - dist/2);
            //Debug.Log(testingList[i] + " High: " + circleParts[i * 2] + " Low: " + circleParts[i * 2 + 1]);
        }
        triangles = new int[circleParts.Length * 6];
        int index = 0;
        Debug.Log(triangles.Length);
        for(int i = 0; i < circleParts.Length - 2; i += 2)
        {
            triangles[index] = i;
            triangles[index + 1] = i + 1;
            triangles[index + 2] = i + 3;
            triangles[index + 3] = i + 3;
            triangles[index + 4] = i + 2;
            triangles[index + 5] = i;
            //Debug.Log(triangles[index] + " " + triangles[index+1] + " " + triangles[index+2] + " " + triangles[index+3] + " " + triangles[index+4] + " " + triangles[index+5]);
            //Debug.Log(index + " " + i);
            index += 6;
        }

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.Clear();
        mesh.vertices = circleParts;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void sharpCircleTesting()
    {
        testingList = new Vector3[]
        {
            new Vector3(0, 0, 0),

            new Vector3(1.5f, 0, 0),
            new Vector3(1, 0.1f, 1),
            new Vector3(0, 0.2f, 1.5f),
            new Vector3(-1, 0.3f, 1),
            new Vector3(-1.5f, 0.4f, 0),
            new Vector3(-1, 0.5f, -1),
            new Vector3(0, 0.6f, -1.5f),
            new Vector3(1, 0.7f, -1),

            new Vector3(1.5f, 0, 0),
            new Vector3(1, 0.1f, 1),
            new Vector3(0, 0.2f, 1.5f),
            new Vector3(-1, 0.3f, 1),
            new Vector3(-1.5f, 0.4f, 0),
            new Vector3(-1, 0.5f, -1),
            new Vector3(0, 0.6f, -1.5f),
            new Vector3(1, 0.7f, -1)
        };

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        vertices = testingList;

        triangles = new int[]
        {
            0, 10, 1,
            0, 11, 2,
            0, 12, 3,
            0, 13, 4,
            0, 14, 5,
            0, 15, 6,
            0, 16, 7,
            0, 9, 8
        };


        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void smoothCircleTesting()
    {
        testingList = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1.5f, 0, 0),
            new Vector3(1, 0.1f, 1),
            new Vector3(0, 0.2f, 1.5f),
            new Vector3(-1, 0.3f, 1),
            new Vector3(-1.5f, 0.4f, 0),
            new Vector3(-1, 0.5f, -1),
            new Vector3(0, 0.6f, -1.5f),
            new Vector3(1, 0.7f, -1)
        };

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        vertices = testingList;

        triangles = new int[]
        {
            0, 2, 1,
            0, 3, 2,
            0, 4, 3,
            0, 5, 4,
            0, 6, 5,
            0, 7, 6,
            0, 8, 7,
            0, 1, 8
        };


        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void createShape()
    {
        vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0),
            new Vector3(1, 1, 0)
        };

        triangles = new int[]
        {
            0, 1, 2,
            3, 5, 4
        };
    }

    private void updateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }


}
