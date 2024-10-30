using UnityEngine;

[CreateAssetMenu(fileName = "SaveFile", menuName = "Save file")]
public class SaveFile : ScriptableObject
{
        public static readonly int s_MaxLife = 10;
        public int Level;
        public int Crystal;
        public int Score;
        public int Life;
        public bool GameOver = false;

        public void Reset()
        {
                Level = 1;
                Life = s_MaxLife;

                Crystal = 0;
                Score = 0;
                GameOver = false;
        }
}
