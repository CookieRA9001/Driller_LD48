using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem{
    public static void saveHighScore(float hs){
        BinaryFormatter BF = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highscore.save";
        FileStream fs = new FileStream(path, FileMode.Create);
        SaveClass sc = new SaveClass(hs);
        BF.Serialize(fs,sc);
        fs.Close();
    }
    public static float GetOldHighScore() {
        string path = Application.persistentDataPath + "/highscore.save";
        if (File.Exists(path)) {
            BinaryFormatter BF = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            SaveClass sc = BF.Deserialize(fs) as SaveClass;
            fs.Close();
            return sc.highscore;
        }
        else {
            return 0;
        }
    }
    public static SaveClass LoadSave()
    {
        string path = Application.persistentDataPath + "/highscore.save";
        if (File.Exists(path)) {
            BinaryFormatter BF = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            SaveClass sc = BF.Deserialize(fs) as SaveClass;
            fs.Close();
            return sc;
        }
        else{
            return null;
        }
    }
}
