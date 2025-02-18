using UnityEngine;

[CreateAssetMenu(fileName = "Player_data", menuName = "Data/Player_data")]
public class Player_data : ScriptableObject
{
    [Range(0, 20)] public float walk = 3.0f;
    [Range(0, 20)] public float sprint = 3.0f;
    [Range(0, 50)] public float acceleration = 3.0f;
    [Range(1, 20)] public float turnRate = 1;

    [Range(0, 10)] public float jumpHeight = 2.0f;
    [Range(0, -30.1f)] public float gravity = -9.8f;


    [Range(1, 20)] public float pushForce = 1;
}
