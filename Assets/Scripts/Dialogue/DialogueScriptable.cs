using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogues/Dialogue", order = 0)]
public class DialogueScriptable : ScriptableObject
{
    public List<DialogueStep> DialogueSteps;
}

[Serializable]
public class DialogueStep
{
    [TextArea(1, 5)] public string Text;
    public List<DialogueOption> DialogueOptions;
}

[Serializable]
public class DialogueOption
{
    public string OptionText;
    /// <summary>
    /// The dialogue this option branches to, once finished, dialogue returns to this branch's next step (or ends if this dialogue step is the last). Leave null to not branch and simply continue to the next step.
    /// </summary>
    public DialogueScriptable DialogueBranch;
    /// <summary>
    /// The index to start the dialogue branch at, 0 is the first step
    /// </summary>
    public int BranchStartStepIndex = 0;
}
