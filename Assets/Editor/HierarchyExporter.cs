using UnityEngine;
using UnityEditor;
using System.Text;
using UnityEngine.SceneManagement;

public class HierarchyExporter
{
    [MenuItem("GameObject/Export Selected Hierarchy", false, 40)]
    private static void ExportSelectedHierarchy()
    {
        GameObject selectedObject = Selection.activeGameObject;
        if (selectedObject == null)
        {
            Debug.LogWarning("No GameObject selected. Please select an object in the Hierarchy.");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("--- SELECTED HIERARCHY EXPORT ---");
        PrintObjectHierarchy(selectedObject.transform, sb, "");
        Debug.Log(sb.ToString());
    }

    [MenuItem("GameObject/Export Entire Scene Hierarchy", false, 41)]
    private static void ExportEntireScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (!activeScene.isLoaded)
        {
            Debug.LogWarning("No active scene loaded.");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"--- SCENE HIERARCHY EXPORT FOR: {activeScene.name} ---");

        // Get all root GameObjects in the scene
        GameObject[] rootObjects = activeScene.GetRootGameObjects();

        foreach (GameObject rootObject in rootObjects)
        {
            PrintObjectHierarchy(rootObject.transform, sb, "");
        }

        Debug.Log(sb.ToString());
    }

    private static void PrintObjectHierarchy(Transform t, StringBuilder sb, string indent)
    {
        sb.AppendLine($"{indent}- {t.gameObject.name} (GameObject)");
        Component[] components = t.gameObject.GetComponents<Component>();
        foreach (Component component in components)
        {
            if (component is Transform) continue;
            System.Type componentType = component.GetType();
            string componentName = componentType.Name;
            string namespaceName = componentType.Namespace;

            bool isUserScript = (namespaceName == null) ||
                            (!namespaceName.StartsWith("UnityEngine") && !namespaceName.StartsWith("TMPro"));

            if (isUserScript)
            {
                sb.AppendLine($"{indent}  L-- {componentName} (Script)");
            }
            else
            {
                sb.AppendLine($"{indent}  L-- {componentName}");
            }
        }

        foreach (Transform child in t)
        {
            PrintObjectHierarchy(child, sb, indent + "  ");
        }
    }
}