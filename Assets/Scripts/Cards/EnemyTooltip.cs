using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Enemy enemy;
    bool hovering;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (!hovering)
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.T))
        {
            return;
        }

        TooltipUI.instance.Show(
            "Enemy",
            $"HP: {enemy.Health}\nAttack: {enemy.AttackDamage}"
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        TooltipUI.instance.Hide();
    }
}
