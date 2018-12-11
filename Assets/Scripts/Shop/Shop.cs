using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public GameObject shopPanel;
    [SerializeField] int[] itemCosts;
    private int currentSelectedItem;

    Player player;

    private void Update()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();

            if (player != null)
            {
                UIManager.Instance.OpenShop(player.diamonds);
            }

            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        currentSelectedItem = item;

        switch(item)
        {
            case 0: // Flame Sword
                UIManager.Instance.UpdateShopSelection(122);
                break;
            case 1: // Boots of Flight
                UIManager.Instance.UpdateShopSelection(19);
                break;
            case 2: // Key to Castle
                UIManager.Instance.UpdateShopSelection(-90);
                break;
        }
    }

    public void BuyItem()
    {
        int cost = itemCosts[currentSelectedItem];

        if (cost <= player.diamonds)
        {
            //Award item
            if (currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            player.diamonds -= cost;
            shopPanel.SetActive(false);
        }
        else
        {
            shopPanel.SetActive(false);
        }
    }
}
