using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image checkboxImage;

    [Header("Sprites")]
    [SerializeField] private Sprite uncheckedSprite;
    [SerializeField] private Sprite checkedSprite;

    private TasksScriptable currentTask;

    public void Setup(TasksScriptable task, bool isComplete)
    {
        currentTask = task;
        descriptionText.text = task.description;
        UpdateStatus(isComplete);
    }

    public void UpdateStatus(bool isComplete)
    {
        if (isComplete)
        {
            descriptionText.fontStyle = FontStyles.Strikethrough;
            descriptionText.color = Color.gray;
            checkboxImage.sprite = checkedSprite;
        }
        else
        {
            descriptionText.fontStyle = FontStyles.Normal;
            descriptionText.color = Color.black;
            checkboxImage.sprite = uncheckedSprite;
        }
    }

    public void UpdateProgress(int current, int required)
    {
        descriptionText.text = $"{currentTask.description} ({current}/{required})";
    }
}