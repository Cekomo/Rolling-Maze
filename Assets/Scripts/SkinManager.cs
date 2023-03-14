using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public const int SCORE_MULTIPLIER = 3;
    public static int GamePoint;
    public static int LevelPoint;

    [SerializeField] private GameObject ball;
    private int currentBallIndex;
    [HideInInspector] public Material currentBallSkin;
    [SerializeField] private Material[] ballSkins;

    private void Awake()
    {
        SetBallSkin();
    }

    private void SetBallSkin()
    {
        currentBallIndex = PlayerPrefs.GetInt("CurrentSkinIndex");
        currentBallSkin = ballSkins[currentBallIndex];
        ball.GetComponent<MeshRenderer>().material = currentBallSkin;
    }

    public void ChangeBallSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("CurrentSkinIndex", skinIndex);
        SetBallSkin();
    }
}
