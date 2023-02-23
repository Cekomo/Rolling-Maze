using System.Collections.Generic;
using UnityEngine;

public class MazeModels : MonoBehaviour
{
    private const int SMALLEST_SCALE = 5;
    
    public static int MaximumLevel;
    public List<GameObject> mazeModelList;
    public static int[] MazeScaleList;
    
    private void Awake()
    { // PlayerPrefs.SetInt("LevelIndex", 0);
        MaximumLevel = mazeModelList.Count - 1;
        MazeScaleList = new int[MaximumLevel + 1];

        MazeScaleList = new[] { 6, 6, 7, 7, 7, 8, 8};
        
        for (var i = 0; i < MazeScaleList.Length; i++)
            if (MazeScaleList[i] == 0)
                MazeScaleList[i] = SMALLEST_SCALE;
        
        var theMazeScale = MazeScaleList[LevelLoader.GetLevel()];
        CameraMovementController.PausedOffset = new Vector3(0, theMazeScale * 5, theMazeScale * -4);
    }
}
