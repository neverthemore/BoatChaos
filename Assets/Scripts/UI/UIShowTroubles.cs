using Unity.VisualScripting;
using UnityEngine;

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
        _brokenWheelEvent.OnWheelBroken.AddListener(ShowWheelAllert);
        _brokenWheelEvent.OnWheelFixed.AddListener(HideWheelAllert);

        _brokenMastEvent.OnMastBroken.AddListener(ShowMastAllert);
        _brokenMastEvent.OnMastFixed.AddListener(HideMastlAllert);

        _illnesEvent.OnIllnessStart.AddListener(ShowIllAllert);
        _illnesEvent.OnIllnesEnd.AddListener(HideIllAllert);

        _enemyEvent.OnEnemyStart.AddListener(ShowEnemyAllert);
        _enemyEvent.OnEnemyEnd.AddListener(HideEnemylAllert);
    }

    private void OnDisable()
    {
        _brokenWheelEvent.OnWheelBroken.RemoveListener(ShowWheelAllert);
        _brokenWheelEvent.OnWheelFixed.RemoveListener(HideWheelAllert);

        _brokenMastEvent.OnMastBroken.RemoveListener(ShowMastAllert);
        _brokenMastEvent.OnMastFixed.RemoveListener(HideMastlAllert);

        _illnesEvent.OnIllnessStart.RemoveListener(ShowIllAllert);
        _illnesEvent.OnIllnesEnd.RemoveListener(HideIllAllert);

        _enemyEvent.OnEnemyStart.RemoveListener(ShowEnemyAllert);
        _enemyEvent.OnEnemyEnd.RemoveListener(HideEnemylAllert);
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
    private void ShowWheelAllert()
    {
        ShowAllert(_wheelPanel);
    }

    private void HideWheelAllert()
    {
        HideAllert(_wheelPanel);
    }
    #endregion

    #region Mast
    private void ShowMastAllert()
    {
        ShowAllert(_mastPanel);
    }

    private void HideMastlAllert()
    {
        HideAllert(_mastPanel);
    }
    #endregion

    #region Ill
    private void ShowIllAllert()
    {
        ShowAllert(_illPanel);
    }

    private void HideIllAllert()
    {
        HideAllert(_illPanel);
    }
    #endregion

    #region Enemy
    private void ShowEnemyAllert()
    {
        ShowAllert(_enemyPanel);
    }

    private void HideEnemylAllert()
    {
        HideAllert(_enemyPanel);
    }
    #endregion
}
