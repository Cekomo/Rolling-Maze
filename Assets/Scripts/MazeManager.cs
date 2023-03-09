    using UnityEngine;

    public class MazeManager : MonoBehaviour
    {
        public MazeModels mazeModels;
        public Transform mazeContainer;
        
        public static GameObject CurrentMaze;
        
        private void Awake()
        {  
             // PlayerPrefs.SetInt("LevelIndex", 1);
            LevelLoader.PauseGame(true);
            InstantiateNewMaze();
            DecideMazeColor();
        }

        public void InstantiateNewMaze()
        {
            if (CurrentMaze != null) Destroy(CurrentMaze);
            CurrentMaze = Instantiate(mazeModels.mazeModelList[LevelLoader.GetLevel()], mazeContainer); // * 5 MVP
            
            CurrentMaze.transform.position = new Vector3(0, -4, 0);
            CurrentMaze.transform.localScale = new Vector3(2, 2, 2);
        }

        public static void DecideMazeColor()
        {
            var currentLevel = PlayerPrefs.GetInt("LevelIndex") % 20; // * 5 for MVP
            
            switch (currentLevel)
            {
                case < 3 and >= 0:
                    PaintTheMaze(MazeModels.ColorDict[Colors.DarkGray], MazeModels.ColorDict[Colors.Gray]);
                    // PaintTheMaze(MazeModels.ColorDict[Colors.Orange], MazeModels.ColorDict[Colors.Blue]);
                    break;
                case < 8 and >= 3:
                    PaintTheMaze(MazeModels.ColorDict[Colors.Teal], MazeModels.ColorDict[Colors.Navy]);
                    break;
                case < 18 and >= 8:
                    PaintTheMaze(MazeModels.ColorDict[Colors.Mustard], MazeModels.ColorDict[Colors.DarkGreen]);
                    break;
                case < 28 and >= 18:
                    PaintTheMaze(MazeModels.ColorDict[Colors.Maroon], MazeModels.ColorDict[Colors.Peach]);
                    break;
                case < 38 and >= 28:
                    PaintTheMaze(MazeModels.ColorDict[Colors.Orange], MazeModels.ColorDict[Colors.Navy] );
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

        // not used since mobile does not detect collision in run-time collision addition
        public static void PrepareTheMaze()
        {
            AddColliderToChildren();
            TagChildren();
        }
        
        private static void AddColliderToChildren()
        {
            var meshFilters = CurrentMaze.GetComponentsInChildren<MeshFilter>();
            foreach (var filter in meshFilters)
            {
                var meshCollider = filter.gameObject.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = filter.sharedMesh;
            }
        }
        
        private static void TagChildren()
        {
            var mazeTransform = CurrentMaze.transform;
            for (var i = 0; i < mazeTransform.childCount; i++)
                mazeTransform.GetChild(i).tag = "Wall";
            
            mazeTransform.GetChild(mazeTransform.childCount - 1).tag = "Floor";
        }
    }
