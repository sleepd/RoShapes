using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LayerMask shapeLayer;
    private LevelManager _levelManager;
    private UIManager _UIManager;
    private AudioSource audioSource;
    public AudioClip rotateSFX;
    public AudioClip winSFX;

    private bool _canClick = true;
    void Awake()
    {
        Instance = this;
        _levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        _UIManager = GameObject.Find("Ingame UI Canvas").GetComponent<UIManager>();
            
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_canClick && Mouse.current.leftButton.isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D hit = Physics2D.OverlapPoint(mousePos, shapeLayer);

            if (hit != null)
            {
                hit.GetComponent<RoShape>().RotateShape(true);
                _canClick = false;
                audioSource.PlayOneShot(rotateSFX);
            }
        }
    }

    public void OnRotateFinished()
    {
        if (_levelManager.IsEqual())
        {
            audioSource.PlayOneShot(winSFX);
            _UIManager.ShowClearScreen();
        }
        else _canClick = true;
    }

    public void LoadNextLevel()
    {
        string levelName = _levelManager.nextLevel.ToString();
        if (levelName != "0") SceneManager.LoadScene(levelName);
    }
}
