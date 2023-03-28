using UnityEngine;

public class SkinManager : MonoBehaviour
{
    // ReSharper disable once InconsistentNaming
    public static int LevelPoint;
    
    [SerializeField] private GameObject ball;
    [HideInInspector] public Material currentBallSkin;
    [SerializeField] private Material[] ballSkins;
    
    private int _currentBallIndex;
    public static int[] BallSkinCosts;
    
    private string _unlockedSkinsArray;
    public static int SelectedSkinValue;
    public static int SelectedSkinIndex;

    private void Awake()
    {
        if (PlayerPrefs.GetString("UnlockedSkins") == "")
            PlayerPrefs.SetString("UnlockedSkins", "1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
        
        // PlayerPrefs.SetInt("CurrentSkinIndex", 0);
        // if (PlayerPrefs.GetInt("GamePoint") < 2000)
        //     PlayerPrefs.SetInt("GamePoint", 35000);
        
        BallSkinCosts = new int[20];
        BallSkinCosts = new[]
        { 0, 1500, 2000, 2000, 2500, 2500, 3000, 3000, 3500, 3500, 
            3500, 3500, 4000, 4500, 4500, 4500, 5000, 5000, 5500, 6000 };
        
        // 1000 (0-4), 1500 (5-14), 2000 (15-24), 2500 (25-34), 3000 (35-44)
        
        SelectBallSkin(PlayerPrefs.GetInt("CurrentSkinIndex"));
    }

    private void SetBallSkin()
    {
        _currentBallIndex = PlayerPrefs.GetInt("CurrentSkinIndex");
        currentBallSkin = ballSkins[_currentBallIndex];
        ball.GetComponent<MeshRenderer>().material = currentBallSkin;
    }

    public void SelectBallSkin(int skinIndex)
    {
        _unlockedSkinsArray = PlayerPrefs.GetString("UnlockedSkins");
        SelectedSkinValue = int.Parse(_unlockedSkinsArray[skinIndex * 2].ToString());
        SelectedSkinIndex = skinIndex;

        if (SelectedSkinValue == 1)
            ChangeBallSkin();
    }

    private void ChangeBallSkin()
    {
        PlayerPrefs.SetInt("CurrentSkinIndex", SelectedSkinIndex);
        SetBallSkin();
    }

    public void BuyBallSkin()
    {
        if (SelectedSkinValue == 1) return;
        if (PlayerPrefs.GetInt("GamePoint") < BallSkinCosts[SelectedSkinIndex]) return;

        var newGamePoint = PlayerPrefs.GetInt("GamePoint") - BallSkinCosts[SelectedSkinIndex];
        PlayerPrefs.SetInt("GamePoint", newGamePoint);

        UnlockBallSkin();
        ChangeBallSkin();
    }
    
    private void UnlockBallSkin()
    {
        var newUnlockedSkins = "";
        for (var i = 0; i < _unlockedSkinsArray.Length; i++)
        {
            if (i == SelectedSkinIndex * 2)
            {
                newUnlockedSkins += "1,";
                i++;
                continue;
            }
            newUnlockedSkins += _unlockedSkinsArray[i];
        }

        PlayerPrefs.SetString("UnlockedSkins", newUnlockedSkins);
        SelectedSkinValue = 1;
    }
}
