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
        animsManager.AnimationOn();
    }

    public void OnDisable()
    {
        animsManager.AnimationOff();
    }

    // Update is called once per frame
    void Update()
    {
        m_Image.sprite = animsManager.GetSprite();
    }
}
