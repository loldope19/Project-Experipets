using UnityEngine;
using System.Collections.Generic;

public class PanelSwitcher : MonoBehaviour
{
    public List<CanvasGroup> panelCanvasGroups;

    private int currentIndex = 0;

    void Start()
    {
        UpdatePanelVisibility();
    }

    public void SwitchPanel(int direction)
    {
        if (panelCanvasGroups.Count == 0) return;

        currentIndex += direction;

        if (currentIndex >= panelCanvasGroups.Count)
        {
            currentIndex = 0;
        }
        else if (currentIndex < 0)
        {
            currentIndex = panelCanvasGroups.Count - 1;
        }

        UpdatePanelVisibility();
    }

    private void UpdatePanelVisibility()
    {
        for (int i = 0; i < panelCanvasGroups.Count; i++)
        {
            CanvasGroup cg = panelCanvasGroups[i];

            if (i == currentIndex)
            {
                cg.alpha = 1;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
            else
            {
                cg.alpha = 0;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
        }
    }
}