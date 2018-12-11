using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UIManager is Null");
            }

            return instance;
        }
    }

    public Text playerGemCountText;
    public Image selectionImg;
    public Text gemCountText;
    public Image[] healthBars;

    private void Awake()
    {
        instance = this;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "" + gemCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector3(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int gemCount)
    {
        gemCountText.text = "" + gemCount;
    }

    public void UpdateLives(int livesRemaining)
    {
        healthBars[livesRemaining].enabled = false;
    }
}
