using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Room")]
public class RoomData : ScriptableObject
{
    [field: SerializeField] public string RoomName { get; private set; }
    [field: SerializeField] public int RoomUnlockCost { get; private set; }
    [field: SerializeField] public int RoomRent { get; private set; }

}

public class RoomUpgrades
{

}
