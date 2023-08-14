using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class UITextController : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float duration;
    public string text;

    public bool isToggled;
    public float lifeTime;
    public float timer;
    void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().SetText(text);
    }

    void Update()
    {
        if(duration > 0)
        {
            duration -= Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, 750 * Time.deltaTime);
        }
        if(duration <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void SetNotification(float duration, string text)
    {
        this.duration = duration;
        this.text = text;
    }

}
