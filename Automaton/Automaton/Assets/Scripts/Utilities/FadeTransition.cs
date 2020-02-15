using UnityEngine.SceneManagement;
using UnityEngine;

//Manages the fade transition object that animates each time the player is entering and exiting a scene

public class FadeTransition : MonoBehaviour
{
    private int level;
    public Animator animator;

    void Update()
    {
        
    }

    public void fadeToLevel(int level)
    {
        this.level = level;
        animator.SetTrigger("FadeOut");
        
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(level);
    }
}
