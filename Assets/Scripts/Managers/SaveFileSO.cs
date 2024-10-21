using UnityEngine;

[CreateAssetMenu(fileName = "SaveFile", menuName = "Save file")]
public class SaveFile : ScriptableObject
{
        public int Level;
        public int Crystal;
        public int Score;
        public bool HapticOn;
        public bool SoundOn;

        public void Reset()
        {
                Level = 1;
#if UNITY_EDITOR
                Crystal = 1000; // for testing
#else
        Crystal = 0;
#endif
                Score = 0;
                HapticOn = true;
                SoundOn = true;
        }
}
