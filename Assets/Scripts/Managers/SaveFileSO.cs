using UnityEngine;

[CreateAssetMenu(fileName = "SaveFile", menuName = "Save file")]
public class SaveFile : ScriptableObject
{
    public int Level;
    public int Crystal;
    public bool HapticOn;
    public bool SoundOn;

    public void Reset()
    {
        Level = 1;
        Crystal = 1000; // for testing
        HapticOn = true;
        SoundOn = true;
    }
}
