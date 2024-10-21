using UnityEngine;

[CreateAssetMenu(fileName = "SaveFile", menuName = "Save file")]
public class SaveFile : ScriptableObject
{
        public static readonly int s_MaxLife = 3;
        public int Level;
        public int Crystal;
        public int Score;
        public int Life;

        public void Reset()
        {
                Level = 1;
                Life = s_MaxLife;

#if UNITY_EDITOR
                Crystal = 1000; // for testing
#else
        Crystal = 0;
#endif
                Score = 0;
        }
}
