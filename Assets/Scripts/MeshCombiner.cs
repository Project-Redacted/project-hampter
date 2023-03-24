using UnityEngine;
using System.Collections.Generic;

public class MeshCombiner : MonoBehaviour
{
    public string tagToCombine;
    public bool destroyAfterCombine;

    private void Start()
    {
        Invoke("CombineMeshes", 0.5f);
        //CombineMeshes();
    }

    

    private void CombineMeshes()
    {

        Debug.Log("started");
        // Find all game objects with the specified tag
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Wall");
        /*
        List<GameObject> taggedObjectsList = new List<GameObject>();
        foreach(GameObject g in upperTaggedObjects)
        {
            foreach(MeshFilter mr in g.GetComponents<MeshFilter>())
            {
                taggedObjectsList.Add(mr.gameObject);
                Debug.Log("Added");
            }
        }
        taggedObjectsList.TrimExcess();
        GameObject[] taggedObjects = taggedObjectsList.ToArray();
        */
        Debug.Log(taggedObjects.Length);
        // Combine all the meshes into a single mesh
        Mesh combinedMesh = new Mesh();
        CombineInstance[] combineInstances = new CombineInstance[taggedObjects.Length];
        for (int i = 0; i < taggedObjects.Length; i++)
        {
            combineInstances[i].mesh = taggedObjects[i].GetComponent<MeshFilter>().sharedMesh;
            combineInstances[i].transform = taggedObjects[i].transform.localToWorldMatrix;
        }
        combinedMesh.CombineMeshes(combineInstances, true);

        // Create a new game object to hold the combined mesh
        GameObject combinedObject = new GameObject(tagToCombine + "_Combined");
        combinedObject.transform.position = Vector3.zero;
        combinedObject.transform.rotation = Quaternion.identity;

        // Add a mesh filter and renderer to the combined game object
        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();
        meshFilter.mesh = combinedMesh;
        meshRenderer.material = taggedObjects[0].GetComponent<Renderer>().sharedMaterial;

        // Destroy the original game objects if specified
        if (destroyAfterCombine)
        {
            foreach (GameObject obj in taggedObjects)
            {
                Destroy(obj);
            }
        }
    }
}
