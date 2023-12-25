using UnityEngine;

[CreateAssetMenu(fileName = "PickUpContainerSO", menuName = "Level/PickUp Container")]
public class PickUpContainerSO : LevelObjectsSO
{
    public PoolableData[] pickUpObjects;
}
