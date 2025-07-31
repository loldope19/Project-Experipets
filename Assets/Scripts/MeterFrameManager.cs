using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterFrameManager : MonoBehaviour
{
    public Image meterImage;
    public List<Sprite> meters;

    // Update is called once per frame
    void Update()
    {
        if (!PlayerData.Instance.isPrologueComplete) return;

        switch (DayManager.Instance.GetCurrentChapter())
        {
            case 1:
                meterImage.sprite = meters[0];
                break;
            case 2:
                meterImage.sprite = meters[1];
                break;
            case 3:
                meterImage.sprite = meters[2];
                break;
            case 4:
                meterImage.sprite = meters[3];
                break;
        }
    }
}
