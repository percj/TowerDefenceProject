using UnityEngine;

public class PlayerHealth : Health
{
    public Animator animator;
    public LifeManager LifeManager;
    public override void decreaseHealth(float damage)
    {
        base.decreaseHealth(damage);
        if (CurrentHealth <= 0 && !isDying)
        {
            isDying = true;
            animator.SetBool("Dead", true);
            if (!LifeManager.decreaseLife()) GameSingleton.Instance.levelManager.failed();
            
        }
    }

}
