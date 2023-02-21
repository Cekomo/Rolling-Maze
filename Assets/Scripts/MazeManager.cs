using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public MazeModels mazeModels;
    public Transform mazeContainer;
    
    public static GameObject CurrentMaze;
    
    private void Awake()
    {
        CurrentMaze = Instantiate(mazeModels.mazeModelList[0], mazeContainer);
        CurrentMaze.transform.localScale = new Vector3(2, 2, 2);
        AddColliderToChildren();
        TagChildren();
    }

    private static void TagChildren()
    {
        var mazeTransform = CurrentMaze.transform;
        for (var i = 0; i < mazeTransform.childCount; i++)
        {
            mazeTransform.GetChild(i).tag = "Wall";
            if (mazeTransform.GetChild(i).name == "Cylinder") 
                mazeTransform.GetChild(i).tag = "Floor";
        }
    }

    private static void AddColliderToChildren()
    {
        MeshFilter[] meshFilters = CurrentMaze.GetComponentsInChildren<MeshFilter>();
        foreach (var filter in meshFilters)
        {
            var meshCollider = filter.gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = filter.sharedMesh;
        }
    }
}
