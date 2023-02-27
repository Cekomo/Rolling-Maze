    using UnityEngine;

    public class MazeManager : MonoBehaviour
    {
        public MazeModels mazeModels;
        public Transform mazeContainer;
        
        public static GameObject CurrentMaze;
        
        private void Awake()
        {  
            // PlayerPrefs.SetInt("LevelIndex", 3);
            LevelLoader.PauseGame(true);
            InstantiateNewMaze();
            AddColliderToChildren();
            TagChildren();
        }

        public void InstantiateNewMaze()
        {
            if (CurrentMaze != null) Destroy(CurrentMaze);
            CurrentMaze = Instantiate(mazeModels.mazeModelList[LevelLoader.GetLevel()], mazeContainer);
            
            CurrentMaze.transform.position = new Vector3(0, -4, 0);
            CurrentMaze.transform.localScale = new Vector3(2, 2, 2);
            // CurrentMaze.transform.Rotate(0, 180, 0);
        }

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
