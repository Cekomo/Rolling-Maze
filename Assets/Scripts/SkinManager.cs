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
        // if (PlayerPrefs.GetString("UnlockedSkins") == "")
            // PlayerPrefs.SetString("UnlockedSkins", "1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
        
        // PlayerPrefs.SetInt("CurrentSkinIndex", 0);
        // PlayerPrefs.SetInt("GamePoint", 250);
        _ballSkinCosts = new int[5];
        _ballSkinCosts = new[] { 0, 200, 300, 400, 500 };
        
        SetBallSkin();
    }

    private void SetBallSkin()
    {
        _currentBallIndex = PlayerPrefs.GetInt("CurrentSkinIndex");
        currentBallSkin = ballSkins[_currentBallIndex];
        ball.GetComponent<MeshRenderer>().material = currentBallSkin;
    }

    public void ChangeOrBuySkin(int skinIndex)
    {
        var unlockedSkinsArray = PlayerPrefs.GetString("UnlockedSkins");
        var selectedSkin = int.Parse(unlockedSkinsArray[skinIndex * 2].ToString());

        if (selectedSkin == 1)
        {
            ChangeBallSkin(skinIndex);
        }
        else 
            UnlockBallSkin(skinIndex, unlockedSkinsArray);
    }

    private void ChangeBallSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("CurrentSkinIndex", skinIndex);
        SetBallSkin();
    }
    
    private void UnlockBallSkin(int skinIndex, string unlockedSkins)
    {
        if (PlayerPrefs.GetInt("GamePoint") < _ballSkinCosts[skinIndex]) return;

        var newGamePoint = PlayerPrefs.GetInt("GamePoint") - _ballSkinCosts[skinIndex];
        PlayerPrefs.SetInt("GamePoint", newGamePoint);
        
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

        PlayerPrefs.SetString("UnlockedSkins", newUnlockedSkins);
    }
}
