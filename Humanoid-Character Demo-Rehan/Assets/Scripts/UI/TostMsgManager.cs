using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
public class TostMsgManager : MonoBehaviour
{
    public static TostMsgManager instance;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TMP_Text tostMsgText;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }
    public void ShowToatMsg(string tostMsg)
    {
        canvasGroup.alpha = 0;
        transform.localScale = Vector3.zero;
        canvasGroup.DOFade(1, .1f);
        transform.DOScale(Vector3.one, .4f);
        tostMsgText.text = tostMsg;
        StartCoroutine(CleanToatMsg());
    }
    IEnumerator CleanToatMsg()
    {
        yield return new WaitForSeconds(1.5f);
        Sequence seq = DOTween.Sequence();
        seq.Append(canvasGroup.DOFade(0, 0.1f));
        seq.Join(transform.DOScale(Vector3.zero, 0.1f));
    }
}
