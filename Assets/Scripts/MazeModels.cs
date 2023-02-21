using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeModels : MonoBehaviour
{
    public static int MaximumLevel;
    public List<GameObject> mazeModelList;

    private void Start()
    {
        MaximumLevel = mazeModelList.Count - 1;
    }
}
