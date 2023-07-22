using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerObject instance = PlayerObject.getInstance();

    public Collider box;
    public Slider healthSlider;
    private int attackCounter = 0;

    public Animator charAnimator;
    private void Update()
    {
        if (!EnemyController.isAttack || !BossController.isAttack)
        {
            attackCounter = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (EnemyController.isAttack || BossController.isAttack)
        {
            attackCounter++;
            if (other.gameObject.tag == "Player" && attackCounter == 1)
            {

                StartCoroutine(damaged());
                instance.setHealth(instance.getHealth() - 10);
                healthSlider.value = instance.getHealth();
            }
        }
    }

    private IEnumerator damaged()
    {
        charAnimator.SetBool("isDamaged", true);
        yield return new WaitForSeconds(0.1f);
        charAnimator.SetBool("isGettingUp", true);
        yield return new WaitForSeconds(0.1f);
        charAnimator.SetBool("isGettingUp", false);
        charAnimator.SetBool("isDamaged", false);
    }
}
