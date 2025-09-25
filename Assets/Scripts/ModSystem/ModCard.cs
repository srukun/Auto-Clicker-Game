using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModCard 
{
    public string modCardName;
    public string cardIcon;
    public string cardContent;
    public string cardType;

    public ModCard(string modCardName, string cardIcon, string cardContent, string cardType)
    {
        this.modCardName = modCardName;
        this.cardIcon = cardIcon;
        this.cardContent = cardContent;
        this.cardType = cardType;
    }
}
