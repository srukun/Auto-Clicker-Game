using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransitionManager : MonoBehaviour
{
    public Image transitionImage;
    public float transitionTime = 0.15f; 

    private Vector2 startPos, endPos;

    void Start()
    {
        float screenHeight = transitionImage.rectTransform.rect.height;
        startPos = new Vector2(0, screenHeight);
        endPos = new Vector2(0, 0);

        transitionImage.rectTransform.anchoredPosition = startPos;
        transitionImage.gameObject.SetActive(false);
    }

    public IEnumerator SlideTransition(System.Action afterTransition)
    {
        transitionImage.gameObject.SetActive(true);

        yield return StartCoroutine(MovePanel(startPos, endPos, transitionTime));

        afterTransition?.Invoke();

        yield return StartCoroutine(MovePanel(endPos, startPos, transitionTime));

        transitionImage.gameObject.SetActive(false);
    }

    private IEnumerator MovePanel(Vector2 from, Vector2 to, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transitionImage.rectTransform.anchoredPosition = Vector2.Lerp(from, to, elapsedTime / duration);
            yield return null;
        }
        transitionImage.rectTransform.anchoredPosition = to;
    }
}
