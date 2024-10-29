using UnityEngine;

[CreateAssetMenu(fileName = "SaveFile", menuName = "Save file")]
public class SaveFile : ScriptableObject
{
        public static readonly int s_MaxLife = 10;
        public int Level;
        public int Crystal;
        public int Score;
        public int Life;

        public void Reset()
        {
                Level = 1;
                Life = s_MaxLife;

                Crystal = 1000; // for testing
                Score = 0;
        }
}
