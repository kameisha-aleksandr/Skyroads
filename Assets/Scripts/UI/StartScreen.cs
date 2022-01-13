using UnityEngine;
using UnityEngine.Events;

public class StartScreen : MonoBehaviour
{
    public event UnityAction PlayButtonClick;

    [SerializeField] private CanvasGroup canvasGroup;

    private bool isRunning = false; //the game is running or not

    private void Update()
    {
        //start the game by pressing any key if the game is not running
        if (!isRunning && Input.anyKey)
        {
            PlayButtonClick?.Invoke();
        }
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        isRunning = false;
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        isRunning = true;
    }  
}
