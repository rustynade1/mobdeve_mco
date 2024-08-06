using UnityEngine;
using UnityEngine.UI;

//Health-related functions
public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public int hp;
    public int maxhp = 30;

    void Start()
    {
        hp = maxhp;
        healthBar.maxValue = maxhp;
        healthBar.value = hp;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        healthBar.value = hp;
    }

    public void Heal(int healAmount)
    {
        hp += healAmount;
        healthBar.value = hp;
    }

    public void FullRestore()
    {
        hp = maxhp;
        healthBar.value = hp;
    }

}
