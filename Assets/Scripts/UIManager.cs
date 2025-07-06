using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject clearScreenPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowClearScreen()
    {
        clearScreenPanel.SetActive(true);
    }

    public void HideClearScreen()
    {
        clearScreenPanel.SetActive(false);
    }

    public void OnNextLevelButtonClicked()
    {
        GameManager.Instance.LoadNextLevel();
    }
}
