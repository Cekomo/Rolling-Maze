using System.Collections.Generic;
using UnityEngine;

public class MazeModels : MonoBehaviour
{
    private const int SMALLEST_SCALE = 5;
    
    public static int MaximumLevel;
    public List<GameObject> mazeModelList;
    public static int[] MazeScaleList;

    public static Dictionary<Colors, Color> ColorDict;

    private void Awake()
     {
         MaximumLevel = mazeModelList.Count - 1;
        MazeScaleList = new int[MaximumLevel + 1];

        ColorDict = new Dictionary<Colors, Color>() // possible to use it inside another class
        {
            { Colors.Red, new Color(1f, 0f, 0f, 1f) },
            { Colors.Green, new Color(0f, 1f, 0f, 1f) },
            { Colors.Blue, new Color(0f, 0f, 1f, 1f) },
            { Colors.Yellow, new Color(1f, 1f, 0f, 1f) },
            { Colors.Orange, new Color(1f, 0.85f, 0.7f, 1f) },
            { Colors.Magenta, new Color(1f, 0f, 1f, 1f) },
            { Colors.Pink, new Color(1f, 0.75f, 0.8f, 1f) },
            { Colors.Purple, new Color(0.5f, 0f, 0.5f, 1f) },
            { Colors.Cyan, new Color(0f, 1f, 1f, 1f) },
            { Colors.Black, new Color(0.0f, 0.0f, 0.0f, 1.0f)},
            { Colors.Gray, new Color(0.5f, 0.5f, 0.5f, 1f) },
            { Colors.LightBlue, new Color(0.68f, 0.85f, 0.9f, 1f) },
            { Colors.LightYellow, new Color(1f, 1f, 0.88f, 1f) },
            { Colors.DarkGray, new Color(0.5f, 0.5f, 0.5f, 1f) },
            { Colors.DarkGreen, new Color(0.13f, 0.37f, 0.31f, 1f) },
            { Colors.Navy, new Color(0.0f, 0.0f, 0.5f, 1.0f)},
            { Colors.Teal, new Color(0.0f, 0.5f, 0.5f, 1.0f)},
            { Colors.Maroon, new Color(0.5f, 0.0f, 0.0f, 1.0f)},
            { Colors.Peach, new Color(1.0f, 0.9f, 0.7f, 1.0f)},
            { Colors.Mustard, new Color(1f, 0.86f, 0.35f, 1f)},
            { Colors.Mint, new Color(0.62f, 0.8f, 0.67f, 1f)},
            { Colors.Scarlet, new Color(0.8f, 0.1f, 0.1f, 1f)},
            { Colors.LightGray, new Color(0.8f, 0.8f, 0.8f, 1f)},
            { Colors.Salmon, new Color(0.94f, 0.48f, 0.43f, 1f)},
            { Colors.Cream, new Color(1f, 1f, 0.9f, 1f)},
            { Colors.LightPink, new Color(1f, 0.8f, 0.8f, 1f)},
            { Colors.Sand, new Color(0.95f, 0.9f, 0.7f, 1f)},
            { Colors.LightBrown, new Color(0.8f, 0.7f, 0.55f, 1.0f)}
        };

        // levelCount+2 is seems good for initial camera distance
        MazeScaleList = new[] { 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
            8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9};
        // MazeScaleList = new[] { 5, 6, 7, 8, 9}; // MVP edition
        
        for (var i = 0; i < MazeScaleList.Length; i++)
            if (MazeScaleList[i] == 0)
                MazeScaleList[i] = SMALLEST_SCALE;
        
        var theMazeScale = MazeScaleList[LevelLoader.GetLevel()]; // * 10 MVP
        CameraMovementController.PausedOffset = new Vector3(0, theMazeScale * 5, theMazeScale * -4f);
    }
}
