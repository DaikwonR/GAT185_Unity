using UnityEngine;

public class Game_manager : MonoBehaviour
{
    static Game_manager instance;
    public static Game_manager Instance { get { return instance; } }

    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject loseUI;

    enum eState
    {
        TITLE,
        GAME,
        WIN,
        LOSE
    }

    eState state = eState.TITLE;
    float timer = 0;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case eState.TITLE:
                titleUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    OnStartGame();
                }
                break;
            case eState.GAME:
                break;
            case eState.WIN:
                print("win!");
                break;
            case eState.LOSE:
                break;
            default:
                break;
        }

    }

    public void OnStartGame()
    {
        titleUI.SetActive(false);
        state = eState.GAME;
    }

    public void SetGameOver()
    {
        state = eState.WIN;
    }
}
