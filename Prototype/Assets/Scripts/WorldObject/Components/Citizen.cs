using UnityEngine;
using System.Collections;

public class Citizen : MonoBehaviour
{
    public bool IsFree
    {
        get
        {
            if (GetComponent<Unit>().ActionQueue.Count > 0)
            {
                return !(GetComponent<Unit>().ActionQueue.Peek() is RecruiteeInteraction);
            }
            else
                return true;
        }
    }

	public void StartClapping()
    {
        Debug.Log(gameObject.name + " started clapping");
    }

    public void StopClapping()
    {
        Debug.Log(gameObject.name + " stopped clapping");
    }
}
