using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipNormals : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;

        // flip the normals
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -1 * normals[i];

        mesh.normals = normals;

        // fllip the vertex order, for each tri, so they are counter clockwise
        for (int i = 0; i < mesh.subMeshCount; i++) {
            int[] tris = mesh.GetTriangles(i);
            for (int j = 0; j < tris.Length; j+=3) {
                // swap order of tri vertices
                int temp = tris[j];
                tris[j] = tris[j + 1];
                tris[j + 1] = temp;
            }

            mesh.SetTriangles(tris, i);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
