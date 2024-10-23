using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviourSingleton<RoomManager>
{
    [SerializeField] HotelRoom[] rooms;
    public bool IsAllRoomNonUseable() => rooms.All((room) => !room.IsUsable());
    public HotelRoom GetUseableRoom()
    {
        foreach (var room in rooms)
        {
            if (room.IsUsable() ) return room;
        }
        return null;
    }

}
