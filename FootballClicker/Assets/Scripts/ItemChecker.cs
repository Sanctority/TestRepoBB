using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemChecker : MonoBehaviour {

    public int _coinsCost;
    public int _gemsCost;

    [SerializeField]
    private bool _costsCoinsToBuyItem;

    // ball id stuff
    private enum _itemEnumID { HelmetBall, SpikeBall, BombBall }; // item ids go here.

    [SerializeField]
    private _itemEnumID _chosenID;

    [SerializeField]
    private TextMeshProUGUI _txtBuyItem;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {

        if (PlayerPrefs.GetInt(_chosenID.ToString()) == 1)
        {

            if(PlayerPrefs.GetString("Equipped") == _chosenID.ToString())
            {
                _txtBuyItem.text = "Equipped";
            }
            else
            {
                _txtBuyItem.text = "Equip";
            }
            
        }
        else
        {
            if(_costsCoinsToBuyItem == true)
            {
                _txtBuyItem.text = "Coins: " + _coinsCost.ToString();
            }
            else if(_costsCoinsToBuyItem == false)
            {
                _txtBuyItem.text = "Gems: " + _gemsCost.ToString();
            }
            
        }
    }

    public bool ReturnIfBoughtWithCoins()
    {
        return _costsCoinsToBuyItem;
    }

    public string ReturnEnumString()
    {
        return _chosenID.ToString();
    }
}
