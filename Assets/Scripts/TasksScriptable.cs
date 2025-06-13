using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Create_Task", order = 1)]


public class TasksScriptable : ScriptableObject
{
    public TaskCategories taskCat;

    public float amount;

    [TextArea(10, 100)]
    public string message;
}
