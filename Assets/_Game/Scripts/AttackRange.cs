using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private List<Character> targetInRangeList = new List<Character>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            targetInRangeList.Add(character);
            character.AddAttackRange(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            targetInRangeList.Remove(character);
            character.RemoveAttackRange(this);
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
