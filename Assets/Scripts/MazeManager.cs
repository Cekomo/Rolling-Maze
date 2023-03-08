    using UnityEngine;

    public class MazeManager : MonoBehaviour
    {
        public MazeModels mazeModels;
        public Transform mazeContainer;
        
        public static GameObject CurrentMaze;
        
        private void Awake()
        {  
            // PlayerPrefs.SetInt("LevelIndex", 0);
            LevelLoader.PauseGame(true);
            InstantiateNewMaze();
        }

        public void InstantiateNewMaze()
        {
            if (CurrentMaze != null) Destroy(CurrentMaze);
            CurrentMaze = Instantiate(mazeModels.mazeModelList[LevelLoader.GetLevel()], mazeContainer);
            
            CurrentMaze.transform.position = new Vector3(0, -4, 0);
            CurrentMaze.transform.localScale = new Vector3(2, 2, 2);
        }

        public static void DecideMazeColor()
        {
            var currentLevel = PlayerPrefs.GetInt("LevelIndex") % 20; // % 20 => temporary
            
            switch (currentLevel)
            {
                case 0:
                    PaintTheMaze(Colors.Orange , Colors.Blue);
                    break;
                case <= 5 and > 0:
                    PaintTheMaze(Colors.Green , Colors.Pink );
                    break;
                case <= 10 and > 5:
                    PaintTheMaze(Colors.Yellow , Colors.Purple );
                    break;
                case <= 15 and > 10:
                    PaintTheMaze(Colors.DarkGray , Colors.LightBlue );
                    break;
                case <= 20 and > 15:
                    PaintTheMaze(Colors.Gray, Colors.DarkGray);
                    break;
            }
        }

        private static void PaintTheMaze(Colors floorColor, Colors wallColor)
        {
            
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
