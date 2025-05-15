using Unity.VisualScripting;
using UnityEngine;
using ShipGame.Events;

public class UIShowTroubles : MonoBehaviour
{
    //ѕодписываетс€ на событи€ (если активные, то выводит, если инактив, то закрывает)
    #region ShipEvent
    [SerializeField] private BrokenWheelEvent _brokenWheelEvent;
    [SerializeField] private BrokenMastEvent _brokenMastEvent;
    [SerializeField] private IllnesEvent _illnesEvent;
    [SerializeField] private EnemyEvent _enemyEvent;
    #endregion

    #region Panels
    [SerializeField] GameObject _wheelPanel;
    [SerializeField] GameObject _mastPanel;
    [SerializeField] GameObject _illPanel;
    [SerializeField] GameObject _enemyPanel;
    #endregion

    private void OnEnable()
    {
        EventBus<WheelBrokenEvent>.Subscribe(ShowWheelAllert);
        EventBus<WheelFixedEvent>.Subscribe(HideWheelAllert);

        EventBus<MastBrokenEvent>.Subscribe(ShowMastAllert);
        EventBus<MastFixedEvent>.Subscribe(HideMastlAllert);

        EventBus<IllnesStartEvent>.Subscribe(ShowIllAllert);
        EventBus<IllnesEndEvent>.Subscribe(HideIllAllert);

        EventBus<EnemyAttackEvent>.Subscribe(ShowEnemyAllert);
        EventBus<EnemyEndAttackEvent>.Subscribe(HideEnemylAllert);
    }

    private void OnDisable()
    {
        EventBus<WheelBrokenEvent>.Unsubscribe(ShowWheelAllert);
        EventBus<WheelFixedEvent>.Unsubscribe(HideWheelAllert);

        EventBus<MastBrokenEvent>.Unsubscribe(ShowMastAllert);
        EventBus<MastFixedEvent>.Unsubscribe(HideMastlAllert);

        EventBus<IllnesStartEvent>.Unsubscribe(ShowIllAllert);
        EventBus<IllnesEndEvent>.Unsubscribe(HideIllAllert);

        EventBus<EnemyAttackEvent>.Unsubscribe(ShowEnemyAllert);
        EventBus<EnemyEndAttackEvent>.Unsubscribe(HideEnemylAllert);
    }

    private void ShowAllert(GameObject panel)
    {
        panel.SetActive(true);
    }

    private void HideAllert(GameObject panel)
    {
        panel.SetActive(false);
    }

    #region Wheel
    private void ShowWheelAllert(WheelBrokenEvent evt)
    {
        ShowAllert(_wheelPanel);
    }

    private void HideWheelAllert(WheelFixedEvent evt)
    {
        HideAllert(_wheelPanel);
    }
    #endregion

    #region Mast
    private void ShowMastAllert(MastBrokenEvent evt)
    {
        ShowAllert(_mastPanel);
    }

    private void HideMastlAllert(MastFixedEvent evt)
    {
        HideAllert(_mastPanel);
    }
    #endregion

    #region Ill
    private void ShowIllAllert(IllnesStartEvent evt)
    {
        ShowAllert(_illPanel);
    }

    private void HideIllAllert(IllnesEndEvent end)
    {
        HideAllert(_illPanel);
    }
    #endregion

    #region Enemy
    private void ShowEnemyAllert(EnemyAttackEvent evt)
    {
        ShowAllert(_enemyPanel);
    }

    private void HideEnemylAllert(EnemyEndAttackEvent evt)
    {
        HideAllert(_enemyPanel);
    }
    #endregion
}
