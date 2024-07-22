using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour, IRange
{
    private List<Character> targetInRangeList = new List<Character>();
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            targetInRangeList.Add(character);
            character.AddRange(this);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            targetInRangeList.Remove(character);
            character.RemoveRange(this);
        }
    }
    public void RemoveTarget(Character character)
    {
        targetInRangeList.Remove(character);
    }
    public List<Character> GetTargetInRangeList()
    {
        return targetInRangeList;
    }
    public int GetNumberOfTarget()
    {
        return targetInRangeList.Count;
    }
}
