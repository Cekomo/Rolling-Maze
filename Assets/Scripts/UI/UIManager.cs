using TMPro;
using UnityEngine;
using Maze;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject startPanelGuide;
        [SerializeField] private GameObject startPanelDefault;
        [SerializeField] private TMP_Text levelCount;

        private void Start()
        {
            SetLevelCounter();
            SetStartPanelStatus(true);
        }

        private void Update() // transfer this if you create a class like gameManager 
        {
            if (Input.touchCount == 0) return;
        
            LevelLoader.PauseGame(false);
            SetStartPanelStatus(false);
        }

        public void SetStartPanelStatus(bool isActive)
        {
            if (LevelLoader.GetLevel() != 0) 
                startPanelDefault.SetActive(isActive);
            else 
                startPanelGuide.SetActive(isActive);
        }

        public void SetLevelCounter()
        {
            levelCount.text = "Level " + (LevelLoader.GetLevel() + 1);
        }
    }

}