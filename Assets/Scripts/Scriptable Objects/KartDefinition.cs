using UnityEngine;

[CreateAssetMenu(fileName = "New Car Definition", menuName = "Scriptable Object/Car Definiton")]
public class CarDefinition : ScriptableObject
{
	public const int MAX_STAT = 5;

	public CarEntity prefab;
	[SerializeField, Range(1, MAX_STAT)] private int speedStat;
	[SerializeField, Range(1, MAX_STAT)] private int accelStat;
	[SerializeField, Range(1, MAX_STAT)] private int turnStat;

	public float SpeedStat => (float)speedStat / MAX_STAT;
	public float AccelStat => (float)accelStat / MAX_STAT;
	public float TurnStat => (float)turnStat / MAX_STAT;
}
