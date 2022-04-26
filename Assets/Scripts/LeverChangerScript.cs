using UnityEngine;

public class LeverChangerScript : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(int levelIndex)
    {
        FadeOUT();
    }

    public void FadeOUT()
    {
        animator.SetTrigger("FadeOut");
    }

    public void FadeIN()
    {
        animator.SetTrigger("FadeIn");
    }

}
