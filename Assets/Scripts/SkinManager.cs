using UnityEngine;

public class SkinManager : MonoBehaviour
{
    // ReSharper disable once InconsistentNaming
    public const int SCORE_MULTIPLIER = 3;
    public static int GamePoint;
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
        
        // print(PlayerPrefs.GetString("UnlockedSkins"));
        // PlayerPrefs.SetInt("CurrentSkinIndex", 0);
        // if (PlayerPrefs.GetInt("GamePoint") < 2000)
        //     PlayerPrefs.SetInt("GamePoint", 15000);
        BallSkinCosts = new int[20];
        BallSkinCosts = new[] { 0, 200, 300, 400, 500, 510, 500, 500, 510, 503, 
            500, 520, 505, 500, 504, 500, 500, 509, 503, 502};
        
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
