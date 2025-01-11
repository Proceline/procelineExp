using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "RPG/Attributes")]
public class VovCharacterAttributes : ScriptableObject {
    public int speed;
    public int attack;
    public int defense;
    public int health;
}