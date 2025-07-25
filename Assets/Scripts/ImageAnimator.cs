using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour
{
    public Image m_Image;
    PetAnimationManager animsManager;

    // Start is called before the first frame update
    public void OnEnable()
    {
        animsManager = PetAnimationManager.Instance;
        if (animsManager != null ) animsManager.AnimationOn();
    }

    public void OnDisable()
    {
        if (animsManager != null) animsManager.AnimationOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (animsManager != null) m_Image.sprite = animsManager.GetSprite();
    }
}
