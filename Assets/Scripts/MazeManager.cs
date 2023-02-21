using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public MazeModels mazeModels;
    public static GameObject CurrentMaze;
    
    private void Awake()
    {
        LevelLoader.LoadLevel();
    }
}
