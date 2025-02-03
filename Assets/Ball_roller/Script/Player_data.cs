using UnityEngine;

[CreateAssetMenu(fileName = "Player_data", menuName = "Data/Player_data")]
public class Player_data : ScriptableObject
{
    [Range(0, 20)] public float speed = 3.0f;
    [Range(0, 10)] public float jumpHeight = 2.0f;
    [Range(0, -30.1f)] public float gravity = -9.8f;
    [Range(1, 5)] public float turnRate = 1;
}
