using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


[System.Serializable]
public class Resource
{
    public int startAmount;

    public int currentAmount { get; set; }

    public int maxAmount;

    public Image bar;

    public Text resText;
    //public Text livingspaceText;

    public bool warning = false;
    public bool dark;

    public Resource()
    {
        
    }

    public void UpdateUI()
    {
        if(bar){
            bar.fillAmount = (float)currentAmount / (float)maxAmount;
        }
        resText.text = currentAmount + "/" + maxAmount;

        
        //livingspaceText.text = "5/" + currentAmount;

        if(warning){
            resText.color = new Color(0.70196f, 0.22745f, 0.22745f, 1.0f);
            resText.text = resText.text + " !";
        }else{
            resText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            resText.text = resText.text;
        }

        if(dark){
            resText.color = new Color(0.11765f, 0.22745f, 0.32157f, 1.0f);
            resText.text = currentAmount + "/" + maxAmount;
        }

    }
}
