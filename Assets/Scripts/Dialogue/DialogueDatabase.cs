using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "ScriptableObjects/Dialogues/DialogueDatabase", order = 0)]
public class DialogueDatabase : ScriptableObject
{
    public string DatabaseTag;
    [SerializeReference] public List<DialogueScriptable> Dialogues = new();
    private Dictionary<string, DialogueScriptable> _dialogueDictionary = new();
    public void Initialize()
    {
        _dialogueDictionary.Clear();
        foreach (var dialogue in Dialogues)
        {
            string tag = dialogue.name;
            if (!_dialogueDictionary.ContainsKey(tag))
                _dialogueDictionary.Add(tag, dialogue);
        }
    }
    public DialogueScriptable GetDialogue(string dialogueTag)
    {
        if (_dialogueDictionary.Count <= 0) Initialize();

        if (_dialogueDictionary.ContainsKey(dialogueTag))
            return _dialogueDictionary[dialogueTag];

        Debug.LogWarning($"[WARN]({this}):Could not find dialogue with tag: {dialogueTag}");
        return null;
    }
}
