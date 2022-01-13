using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameOverScreen : MonoBehaviour
{
    public event UnityAction RestartButtonClick;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button button;
    [SerializeField] private Text result;

    public Text Result { get => result; set => result = value; }

    private void OnEnable()
    {
        button.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnRestartButtonClick);
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        button.interactable = true;
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        button.interactable = false;
    }

    protected void OnRestartButtonClick()
    {
        RestartButtonClick?.Invoke();
    }
}