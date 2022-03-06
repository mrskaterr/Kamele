using UnityEngine;

public class BuildingsHealthSystem : MonoBehaviour
{
    [SerializeField] 
    private int hitPoints;

    public void DealDamage() => hitPoints--;
    public int GetHP() => hitPoints;
}