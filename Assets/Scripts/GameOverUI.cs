using System.Collections;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Animator gameOverAnimator;

    [Header("Master Fade Transition")]
    [SerializeField] private Animator fadePanelAnimator;

    private void OnEnable()
    {
        GameManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        gameOverAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(3f);

        fadePanelAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);

        gameOverScreen.SetActive(false);

        GameManager.Instance.ResetGame();
    }
}