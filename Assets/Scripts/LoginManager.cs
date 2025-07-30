using UnityEngine;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject nameInputObject;
    [SerializeField] private TextMeshProUGUI welcomeText;
    [SerializeField] private GameObject welcomeTextObject;

    private void Start()
    {
        // When the login screen starts, check if the prologue is already done.
        if (PlayerData.Instance.isPrologueComplete)
        {
            nameInputObject.SetActive(false);
            welcomeTextObject.SetActive(true);
            welcomeText.text = $"Welcome, Caretaker {PlayerData.Instance.playerName}";
        }
        else
        {
            nameInputObject.SetActive(true);
            welcomeTextObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (PlayerData.Instance.isPrologueComplete)
        {
            nameInputObject.SetActive(false);
            welcomeTextObject.SetActive(true);
            welcomeText.text = $"Welcome, Caretaker {PlayerData.Instance.playerName}";
        }
    }

    public void OnLoginButtonClicked()
    {
        // Check if this is the first-time login (prologue)
        if (!PlayerData.Instance.isPrologueComplete && !PlayerData.Instance.isNameConfirmed)
        {
            if (!string.IsNullOrEmpty(nameInputField.text))
            {
                PlayerData.Instance.playerName = nameInputField.text;

                DialogueManager.Instance.StartDialogue("Prologue", "PROLOGUE_02_Greeting");

                ViewManager.Instance.GoToDesktopView();
            }
            else
            {
                Debug.LogWarning("Player name cannot be empty!");
            }
        }
        else
        {
            ViewManager.Instance.GoToDesktopView();
        }
    }
}