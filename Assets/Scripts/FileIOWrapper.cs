using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FileIOWrapper
{
    private const string SAVE_FILE_NAME = "Phazed.bytes";

    private static string AppVersionFilePath { get { return $"{Application.persistentDataPath}/{SAVE_FILE_NAME}"; } }

    public static SaveData LoadGameFromLocalStore()
    {
        if (!File.Exists(AppVersionFilePath)) return new SaveData(){Difficulty =  Difficulty.Normal, IsSoundOn = true, LastLevelUnlocked = 1};

        FileStream fs = null;
        try
        {
            fs = new FileStream(AppVersionFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (TextReader tr = new StreamReader(fs))
            {
                var bf = new BinaryFormatter();
                return (SaveData)bf.Deserialize(fs);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            fs?.Dispose();
        }        
    }

    public static void SaveGameToLocalStore(SaveData saveData)
    {
        FileStream fs = null;
        try
        {
            fs = new FileStream(AppVersionFilePath, FileMode.Create, FileAccess.Write, FileShare.Write);
            using (TextWriter tr = new StreamWriter(fs))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, saveData);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            fs?.Dispose();
        }
    }
}
