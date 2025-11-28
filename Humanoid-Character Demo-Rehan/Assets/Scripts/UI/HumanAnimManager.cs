using UnityEngine;
using UnityEngine.UI;

public class HumanAnimManager : MonoBehaviour
{
    [SerializeField] HumanoidChar humanoidChar;
    [SerializeField] Button smileBtn;
    [SerializeField] Button sayHiBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        smileBtn.onClick.AddListener(OnClickSmileAnim);
         sayHiBtn.onClick.AddListener(OnClickSayHiAnim);
    }

    void OnDisable()
    {
          smileBtn.onClick.RemoveListener(OnClickSmileAnim);
         sayHiBtn.onClick.RemoveListener(OnClickSayHiAnim);
    }

    void OnClickSmileAnim()
    {
        humanoidChar.GetAnimator().Play("Smile");
    }
     void OnClickSayHiAnim()
    {
         humanoidChar.GetAnimator().Play("Hello");
         SoundManager.Instance.PlaySFX(SoundManager.Instance.hiAudio);
    }
}
