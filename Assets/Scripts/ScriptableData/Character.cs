using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character /Data")]
public class Character : ScriptableObject
{
    public string PlayerName;
    public int PlayerAtk;
    public int PlayerDef;
    public int PlayerSpeed;
    public int PlayerCapacity;
    [Header("Move Stats")]
    public int PlayerJumpCount;
    public int PlayerJumpHeight;
}
