﻿using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 10;
    public int zSize = 10;

	//This for initialization
	void Start () {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();

        PipeServer.SartDuplexPipe("Pipe");

    }

    private void Update()
    {
        UpdateMesh();
    }
   
    public void CreateShape()
    {
        //ADTReader adtReader = new ADTReader();
		//adtReader.LoadADT("world/maps/2235/2235_60_03.adt");
        
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
       
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 1f; 
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;

                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

            }
            vert++;
        }
        
    }

    /*   private void OnDrawGizmos()
       {
           if (vertices == null)
               return;

           for (int i = 0; i < vertices.Length; i++)
           {
               Gizmos.DrawSphere(vertices[i], .1f);
           }
       }
	*/
	
    // Update is called once per frame
    void UpdateMesh()
    {
        mesh.Clear();   

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

}
