using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ChapterTitle
{
    public int chapterNumber;
    public Sprite titleCardSprite;
    public float displayDuration = 3f;
}

public class ChapterTitleManager : MonoBehaviour
{
    public static ChapterTitleManager Instance { get; private set; }

    [SerializeField] private CanvasGroup titleViewCanvasGroup;
    [SerializeField] private Image titleCardImage;
    [SerializeField] private List<ChapterTitle> chapterTitles;

    private Dictionary<int, ChapterTitle> titleMap;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        titleMap = new Dictionary<int, ChapterTitle>();
        foreach (var title in chapterTitles)
        {
            titleMap[title.chapterNumber] = title;
        }

        titleViewCanvasGroup.alpha = 0;
        titleViewCanvasGroup.blocksRaycasts = false;
    }

    public IEnumerator ShowTitleForChapter(int chapterNumber)
    {
        if (titleMap.ContainsKey(chapterNumber))
        {
            ChapterTitle title = titleMap[chapterNumber];
            titleCardImage.sprite = title.titleCardSprite;

            // Fade in
            titleViewCanvasGroup.alpha = 1;
            titleViewCanvasGroup.blocksRaycasts = true;

            yield return new WaitForSeconds(title.displayDuration);

            // Fade out
            titleViewCanvasGroup.alpha = 0;
            titleViewCanvasGroup.blocksRaycasts = false;
        }
    }
}