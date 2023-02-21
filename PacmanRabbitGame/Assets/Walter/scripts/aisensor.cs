/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aisensor : MonoBehaviour
{
   public float distance = 10;
   public float anhgle = 30;
   publicfloat height = 1.0f;
   public Color meshColor=Color.red;
   Mesh mesh;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Mesh CreateWedgeMesh()
    {
        Mesh mesh=new Mesh();
        int numtriangles=8;
        int numvertices=numtriangles*3;
        Vector3[]vertices = new Vector3[numvertices];
        int[]triangles=new int[numvertices];
Vector3 bottomCenter=Vector3.zero;
Vector3 bottomLeft=Quaternion.Euler(0,-angle,0)*Vector3.forward*distance;
Vector3 bottomRight=Quaternion.Euler(0,angle,0)*Vector3.forward*distance;
Vector3 topCenter=bottomCenter+Vector3.up*height;
Vector3 topRight=bottomRight+Vector3.up*height;
Vector3 topLeft=bottomLeft+Vector3.up*height;

int vert=0;


        return mesh;
            }
}
*/