using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskGoalType { ReachAmount, UseSpecificItem }

[CreateAssetMenu(fileName = "Task", menuName = "Create_Task", order = 1)]
public class TasksScriptable : ScriptableObject
{
    [Header("-- Task Details --")]
    public TaskCategories taskCategory;
    [TextArea(10, 100)]
    public string description; //  e.g., "feed the experiment 5 apples."

    [Header("-- Task Goal --")]
    public TaskGoalType goalType;

    // -- use ONE of the following sections based on the Goal Type --

    [Header("For 'Reach Amount' Goals")]
    public float amountToReach; // e.g. "get Happiness to 100"

    [Header("For 'Use Specific Item' Goals")]
    public ItemData requiredItem; // the specific item required
    public int requiredItemCount; // how many times to use it
}
