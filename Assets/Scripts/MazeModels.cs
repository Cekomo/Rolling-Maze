using System.Collections.Generic;
using UnityEngine;

public class MazeModels : MonoBehaviour
{
    private const int SMALLEST_SCALE = 5;
    
    public static int MaximumLevel;
    public List<GameObject> mazeModelList;
    public static int[] MazeScaleList;

    public static Dictionary<Colors, int> MazeColorList;

    private void Awake()
     { 
        PlayerPrefs.SetInt("LevelIndex", 18);
        MaximumLevel = mazeModelList.Count - 1;
        MazeScaleList = new int[MaximumLevel + 1];

        MazeColorList = new Dictionary<Colors, int> // () ?
        {
            { Colors.Red, 0xFF0000 }, { Colors.Green, 0x00FF00 }, { Colors.Blue, 0x0000FF }, 
            { Colors.Yellow, 0xFFFF00 }, { Colors.Orange, 0xFFA500 }, { Colors.Magenta, 0xFF00FF }, 
            { Colors.Pink, 0xFFC0CB }, { Colors.Purple, 0x800080 }, { Colors.Cyan, 0x00FFFF }, 
            { Colors.Gray, 0x808080 }, { Colors.LightBlue, 0xADD8E6 }, { Colors.LightYellow, 0xFFFFE0 }, 
            { Colors.DarkGray, 0xA9A9A9 }, { Colors.DarkGreen, 0x006400 } 
        };

        // levelCount+2 is seems good for initial camera distance
        MazeScaleList = new[] { 5, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9};
        
        for (var i = 0; i < MazeScaleList.Length; i++)
            if (MazeScaleList[i] == 0)
                MazeScaleList[i] = SMALLEST_SCALE;
        
        var theMazeScale = MazeScaleList[LevelLoader.GetLevel()];
        CameraMovementController.PausedOffset = new Vector3(0, theMazeScale * 5, theMazeScale * -4f);
    }
}
