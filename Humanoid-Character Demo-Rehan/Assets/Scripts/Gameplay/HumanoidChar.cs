using UnityEngine;

public class HumanoidChar : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 

 public Animator GetAnimator()
    {
        return animator;
    }
}
