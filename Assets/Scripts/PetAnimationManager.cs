using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetAnimationManager : MonoBehaviour
{
    public static PetAnimationManager Instance { get; private set; }

    public RuntimeAnimatorController Stage_1;
    public RuntimeAnimatorController Stage_2;
    public RuntimeAnimatorController Stage_3;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public int Stage = 1;
    public float animLength = 2f;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }
        else { Instance = this; }
    }

    public void StageChange()
    {
        switch(Stage)
        {
            case 1:
                animator.runtimeAnimatorController = Stage_1;
                break;
            case 2:
                animator.runtimeAnimatorController = Stage_2;
                break;
            case 3:
                animator.runtimeAnimatorController = Stage_3;
                break;
        }
    }

    public void Idle()
    {
        animator.Play("Idle");
    }

    public void Eat()
    {
        animator.Play("Eat");
        Invoke(nameof(Idle), animLength);
    }

    public void Rest()
    {
        animator.Play("Rest");
        Invoke(nameof(Idle), animLength);
    }

    public void Play()
    {
        animator.Play("Play");
        Invoke(nameof(Idle), animLength);
    }

    public Sprite GetSprite()
    {
        if (spriteRenderer != null)
        return spriteRenderer.sprite;

        return null;
    }

    public void AnimationOn()
    {
        if (animator != null) animator.enabled = true;
        if (spriteRenderer != null) spriteRenderer.enabled = true;
    }

    public void AnimationOff()
    {
        if (animator != null) animator.enabled = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
    }
}
