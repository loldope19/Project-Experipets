using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region Properties
    [SerializeField] private DialogueView _dialogueView;
    /// <summary>
    /// The core database of dialogue, put all dialogue that is directly callable here
    /// </summary>
    [SerializeReference] private DialogueDatabase _coreDatabase;
    /// <summary>
    /// For organizing dialogue, you may place other databases of dialogue here
    /// </summary>
    [SerializeReference] private List<DialogueDatabase> _otherDatabases;
    #endregion

    #region Private Fields
    private Dictionary<string, DialogueDatabase> _databaseKeys = new();
    private class DialogueTracker
    {
        public DialogueTracker(DialogueScriptable Dialogue, int CurrentStepIndex) { this.Dialogue = Dialogue; this.CurrentStepIndex = CurrentStepIndex; }
        public DialogueScriptable Dialogue;
        public int CurrentStepIndex;
        public DialogueStep CurrentStep
        {
            get
            {
                if(CurrentStepIndex < 0) return null;
                if (CurrentStepIndex >= Dialogue.DialogueSteps.Count) return null;
                return Dialogue.DialogueSteps[CurrentStepIndex];
            }
        }
    }
    private List<DialogueTracker> _activeDialogueStack = new();
    #endregion

    #region Public Methods
    /// <summary>
    /// Start a dialogue from the 'core' database of dialogue. (ie. dialogue that is directly callable)
    /// </summary>
    /// <param name="dialogueTag">The tag of the dialogue to call</param>
    /// <param name="dialogueStep">The step of the dialogue to start at (default = 0, ie. the start)</param>
    /// <returns>Returns true if the dialogue starts properly</returns>
    public bool StartDialogue(string dialogueTag, int dialogueStep = 0)
    {
        return StartDialogue(_coreDatabase.DatabaseTag, dialogueTag, dialogueStep);
    }

    /// <summary>
    /// Start a random dialogue from a given database.
    /// </summary>
    /// <param name="databaseTag">The tag of the database to get dialogue from</param>
    /// <param name="dialogueStep">The step of the dialogue to start at (default = 0, ie. the start)</param>
    /// <returns>Returns true if the dialogue starts properly</returns>
    public bool StartRandomDialogue(string databaseTag, int dialogueStep = 0)
    {

        DialogueDatabase database = GetDatabase(databaseTag);
        if (database == null) return false;

        int choice = Random.Range(0, database.Dialogues.Count);
        Debug.Log("Rendom choice: " + choice);
        return StartDialogue(databaseTag, database.Dialogues[choice].name, dialogueStep);
    }

    /// <summary>
    /// Start a dialogue from a database.
    /// </summary>
    /// <param name="databaseTag">The tag of the database to get dialogue from</param>
    /// <param name="dialogueTag">The tag of the dialogue to call</param>
    /// <param name="dialogueStep">The step of the dialogue to start at (default = 0, ie. the start)</param>
    /// <returns>Returns true if the dialogue starts properly</returns>
    public bool StartDialogue(string databaseTag, string dialogueTag, int dialogueStep = 0)
    {
        DialogueDatabase database = GetDatabase(databaseTag);
        if (database == null) return false;

        DialogueScriptable dialogue = database.GetDialogue(dialogueTag);
        if (dialogue == null) return false;

        // Add selected dialogue to the dialogue stack, with starting step before the target step
        _activeDialogueStack.Add(new DialogueTracker(dialogue, dialogueStep - 1));

        // Enable the dialogue view and increment to the starting step
        _dialogueView.Activate();
        return NextDialogueStep();
    }

    /// <summary>
    /// Callback for proceeding to the next step of dialogue
    /// </summary>
    /// <returns>Returns false if the entire dialogue is to end</returns>
    public bool NextDialogueStep(int dialogueChoiceIndex = -1)
    {
        // Ends Dialogue if there is no current dialogue
        if (_activeDialogueStack.Count <= 0)
            return EndDialogue();

        // If current step exists, process step specific info
        if (CurrentDialogue.CurrentStep != null)
        {
            // Cancel call if a dialogue choice should be provided but none or an invalid one is given
            if (CurrentDialogue.CurrentStep.DialogueOptions.Count > 0 && (dialogueChoiceIndex < 0 || dialogueChoiceIndex >= CurrentDialogue.CurrentStep.DialogueOptions.Count))
                return true;

            // If there is a dialogue choice index provided, check to load a new dialogue
            if (dialogueChoiceIndex >= 0)
            {
                // Check if index provided is valid choice
                if (dialogueChoiceIndex < CurrentDialogue.CurrentStep.DialogueOptions.Count)
                {
                    // Get the new dialogue to load
                    DialogueScriptable newDialogue = CurrentDialogue.CurrentStep.DialogueOptions[dialogueChoiceIndex].DialogueBranch;

                    // Adds the new dialogue making it the new current dialogue
                    _activeDialogueStack.Add(new DialogueTracker(newDialogue, CurrentDialogue.CurrentStep.DialogueOptions[dialogueChoiceIndex].BranchStartStepIndex - 1));

                    // Continue to iterating the new branch's next step (ie. the starting step)
                }

                // Otherwise, continue as if no dialogue choice index was provided
            }
        }

        // Check if current active dialogue is at its last step
        if (CurrentDialogue.CurrentStepIndex >= CurrentDialogue.Dialogue.DialogueSteps.Count - 1)
        {
            // If there is no dialogue after ending, return false to say its ended
            bool result = EndDialogue();
            if (!result) return false;

            // Otherwise, continues to getting and progressing to the next step with the new last dialogue
        }

        // Increment current step, and get the new current step to display
        ++CurrentDialogue.CurrentStepIndex;
        DialogueStep nextStep = CurrentDialogue.CurrentStep;

        // If next step is null, end the dialogue
        if (nextStep == null)
            return EndDialogue();

        _dialogueView.DisplayStep(nextStep);

        return true;
    }
    #endregion

    #region Private and Helper Functions
    /// <summary>
    /// End the current dialogue
    /// </summary>
    /// <returns>Returns true if there still are dialogues active, if none, returns false.</returns>
    private bool EndDialogue()
    {
        // Cleanup if there is no dialogue
        if (_activeDialogueStack.Count <= 0)
        {
            CleanupDialogue();
            return false;
        }

        DialogueScriptable finishedDialogue = CurrentDialogue.Dialogue;

        // Remove last dialogue
        _activeDialogueStack.RemoveAt(_activeDialogueStack.Count - 1);

        if (CurrentDialogue != null && CurrentDialogue.CurrentStep != null)
        {
            foreach (var option in CurrentDialogue.CurrentStep.DialogueOptions)
            {
                if (option.DialogueBranch == finishedDialogue && option.ConvergenceDialogue != null)
                {
                    Debug.Log($"Branch ended. Converging to new dialogue: {option.ConvergenceDialogue.name}");
                    _activeDialogueStack.Add(new DialogueTracker(option.ConvergenceDialogue, -1));
                    return true;
                }
            }
        }

        if (finishedDialogue != null && !string.IsNullOrEmpty(finishedDialogue.eventToTriggerOnEnd))
        {
            GameEventManager.Instance.TriggerEvent(finishedDialogue.eventToTriggerOnEnd);
        }

        // Returns true if there is still a dialogue
        if (_activeDialogueStack.Count >= 1)
            return true;

        // Otherwise, cleanup and return false
        CleanupDialogue();
        return false;
    }

    private void CleanupDialogue()
    {
        _dialogueView.Deactivate();
        _activeDialogueStack.Clear();
    }

    public static DialogueManager Instance { get; private set; }
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        if (_dialogueView == null) Debug.LogWarning($"[WARN]({this}): No Dialogue View assigned in Dialogue Manager.");
        if (_coreDatabase == null) Debug.LogWarning($"[WARN]({this}): No Core Dialogue Database assigned in Dialogue Manager.");

        _dialogueView.Deactivate();
        LoadDatabaseKeys();
    }

    private void LoadDatabaseKeys()
    {
        _databaseKeys.Clear();

        if (_coreDatabase != null)
        {
            _databaseKeys.Add(_coreDatabase.DatabaseTag, _coreDatabase);
            _coreDatabase.Initialize();
        }

        foreach (DialogueDatabase database in _otherDatabases)
        {
            _databaseKeys.Add(database.DatabaseTag, database);
            database.Initialize();
        }

    }

    private DialogueDatabase GetDatabase(string databaseTag)
    {
        if (_databaseKeys.Count <= 0) LoadDatabaseKeys();

        if (_databaseKeys.ContainsKey(databaseTag))
            return _databaseKeys[databaseTag];

        Debug.LogWarning($"[WARN]({this}): Could not find database with tag: {databaseTag}");
        return null;
    }

    private DialogueTracker CurrentDialogue {
        get {
            if (_activeDialogueStack.Count <= 0) return null;
            return _activeDialogueStack[_activeDialogueStack.Count - 1];
        }
    }
    #endregion
}
