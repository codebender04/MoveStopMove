using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSight : MonoBehaviour
{
    private List<Character> targetInSightList = new List<Character>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            targetInSightList.Add(character);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            targetInSightList.Remove(character);
        }
    }
    public void RemoveTarget(Character character)
    {
        targetInSightList.Remove(character);
    }
    public List<Character> GetTargetInSightList()
    {
        return targetInSightList;
    }
    public int GetNumberOfTarget()
    {
        return targetInSightList.Count;
    }
}
