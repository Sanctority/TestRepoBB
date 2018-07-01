using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour {

    private GameManager _gm;
    private int _coins;
    private GameObject[] _shopItems;

    [SerializeField]
    private GameObject _currentSelectedObject;

    [SerializeField]
    private TextMeshProUGUI _txtCoins;


    private void Start()
    {
        _gm = GameManager._instance;

        _shopItems = GameObject.FindGameObjectsWithTag("ShopItem");

        UpdateCoins();
    }

    public void BuyItem(int ItemID)
    {
        _currentSelectedObject = EventSystem.current.currentSelectedGameObject;

        if(PlayerPrefs.GetInt(ItemID.ToString()) != 1)
        {
            UnlockItem(ItemID);
            UpdateCoins();
            Debug.Log("buying item");
        }
        else
        {
            PlayerPrefs.SetInt("Equipped", ItemID);
            UpdateAllShopItemText();
        }
        
    }

    private void UnlockItem(int num)        // this function will be used to unlock the hat if the player has the right amount of coins
    {
        int _holder = num;
        
                if(PlayerPrefs.GetInt(_holder.ToString()) == 0)
                {
                    if (_gm.BuyItem(_currentSelectedObject.GetComponent<ItemChecker>()._coinsCost) == true)
                    {
                        PlayerPrefs.SetInt(_holder.ToString(), 1);
                        _currentSelectedObject.GetComponent<ItemChecker>().UpdateText();
                        UpdateCoins();
                    }
                } 
    }

    private void UpdateCoins()
    {
        _coins = _gm.ReturnCoins();
        _txtCoins.text = "Coins: " + _coins.ToString();
    }

    private void UpdateAllShopItemText()
    {
        foreach (GameObject _shopText in _shopItems)
        {
            _shopText.GetComponent<ItemChecker>().UpdateText();
        }
    }
}
