using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public const int SCORE_MULTIPLIER = 3;
    public static int GamePoint;
    public static int LevelPoint;

    [SerializeField] private GameObject ball;
    private int _currentBallIndex;
    [HideInInspector] public Material currentBallSkin;
    [SerializeField] private Material[] ballSkins;
    private int[] _ballSkinCosts;
    // private bool[] _purchasedBallSkins;

    private void Awake()
    {
        // PlayerPrefs.SetInt("CurrentSkinIndex", 0);
        // PlayerPrefs.SetInt("GamePoint", 250);
        _ballSkinCosts = new int[5];
        _ballSkinCosts = new[] { 0, 200, 300, 400, 500 };
        
        // var unlockedSkins = "1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
        print(PlayerPrefs.GetString("UnlockedSkins"));
        SetBallSkin();
    }

    private void SetBallSkin()
    {
        _currentBallIndex = PlayerPrefs.GetInt("CurrentSkinIndex");
        currentBallSkin = ballSkins[_currentBallIndex];
        ball.GetComponent<MeshRenderer>().material = currentBallSkin;
    }

    public void ChangeBallSkin(int skinIndex)
    {
        var unlockedSkinsArray = PlayerPrefs.GetString("UnlockedSkins");
        
        if (unlockedSkinsArray[skinIndex * 2] == 1)
            SetBallSkin();
        else 
            UnlockBallSkin(skinIndex, unlockedSkinsArray);
    }

    private void UnlockBallSkin(int skinIndex, string unlockedSkins)
    {
        if (GamePoint < _ballSkinCosts[skinIndex]) return;
        GamePoint -= _ballSkinCosts[skinIndex];
        var newUnlockedSkins = "";
        for (var i = 0; i < unlockedSkins.Length; i++)
        {
            if (i == skinIndex * 2)
            {
                newUnlockedSkins += "1,";
                i++;
                continue;
            }
            newUnlockedSkins += unlockedSkins[i];
        }
        print("zort");
        PlayerPrefs.SetString("UnlockedSkins", newUnlockedSkins);
    }
}
