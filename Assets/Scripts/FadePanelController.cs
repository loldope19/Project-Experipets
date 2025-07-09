using UnityEngine;

public class FadePanelController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        animator.SetTrigger("FadeOut");
    }
}