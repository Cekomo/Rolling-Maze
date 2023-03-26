    using UnityEngine;

    public class MazeManager : MonoBehaviour
    {
        public MazeModels mazeModels;
        public Transform mazeContainer;
        
        public static GameObject CurrentMaze;
        
        private void Awake()
        {
            // if (PlayerPrefs.GetInt("LevelIndex") > 25) // GetInt("LevelIndex") * 10 > 40 MVP edition
            //      PlayerPrefs.SetInt("LevelIndex", 0);
            LevelLoader.PauseGame(true);
            InstantiateNewMaze();
            DecideMazeColor();
        }

        private void InstantiateNewMaze()
        {
            if (CurrentMaze != null) Destroy(CurrentMaze);
            CurrentMaze = Instantiate(mazeModels.mazeModelList[LevelLoader.GetLevel()], mazeContainer); // * 10 MVP
            
            CurrentMaze.transform.position = new Vector3(0, -4, 0);
            CurrentMaze.transform.localScale = new Vector3(2, 2, 2);
        }

        private static void DecideMazeColor()
        {
            var currentLevel = PlayerPrefs.GetInt("LevelIndex"); // * 5 for MVP
            
            switch (currentLevel)
            {
                case < 5 and >= 0:
                    PaintTheMaze(MazeModels.ColorDict[Colors.DarkGray], MazeModels.ColorDict[Colors.Gray]);
                    SkinManager.LevelPoint = 100;
                    break;
                case < 15 and >= 5:
                    PaintTheMaze(MazeModels.ColorDict[Colors.Teal], MazeModels.ColorDict[Colors.Navy]);
                    SkinManager.LevelPoint = 150;
                    break;
                case < 25 and >= 15:
                    PaintTheMaze(MazeModels.ColorDict[Colors.Mustard], MazeModels.ColorDict[Colors.DarkGreen]);
                    SkinManager.LevelPoint = 200;
                    break;
                case < 35 and >= 25:
                    PaintTheMaze(MazeModels.ColorDict[Colors.Maroon], MazeModels.ColorDict[Colors.Peach]);
                    SkinManager.LevelPoint = 250;
                    break;
                case < 45 and >= 35:
                    PaintTheMaze(MazeModels.ColorDict[Colors.Orange], MazeModels.ColorDict[Colors.Navy]);
                    SkinManager.LevelPoint = 300;
                    break;
            }
        }

        private static void PaintTheMaze(Color floorColor, Color wallColor)
        {
            var mazeTransform = CurrentMaze.transform;

            mazeTransform.GetChild(0).GetComponent<Renderer>().material.color = floorColor;

            mazeTransform.GetChild(1).GetComponent<Renderer>().material.color = wallColor;
            mazeTransform.GetChild(2).GetComponent<Renderer>().material.color = wallColor;
        }
    }
