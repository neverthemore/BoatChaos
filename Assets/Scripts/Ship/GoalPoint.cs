using UnityEngine;

public class GoalPoint : MonoBehaviour
{    
    [SerializeField] private int _pointIndex;

    public int GetPointIndex()
    {
        return _pointIndex;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        string name = obj.GetComponent<BaseCharacter>()?.name;
        if (name == WhoCanEnteract.Technar.ToString()
            || name == WhoCanEnteract.Pushkar.ToString()
            || name == WhoCanEnteract.Doctor.ToString())
        {
            obj.GetComponent<AI>().ChangePointState(true);
        }        
    }
}
