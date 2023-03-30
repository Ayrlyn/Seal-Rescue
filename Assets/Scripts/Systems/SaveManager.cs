using System;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using CompressionLevel = System.IO.Compression.CompressionLevel;

public class SaveManager : SingletonDontDestroy<SaveManager>
{
    public bool Load()
    {
        try
        {
            var gameSave = LoadFile<GameSave>(Path("save/game"));
            if (gameSave == null)
            {
                Debug.Log("SaveSystem::Load() - No game save found");
                return false;
            }
            Game.Instance.Load(gameSave);
            return true;
        }
        catch(Exception exception)
        {
            Debug.LogError(exception);
            return false;
        }
    }

    public static T LoadFile<T>(string path) where T : class
    {
        // Debug.Log($"SaveSystem::LoadFile({path}) - Loading...");
        var jsonTime = DateTime.Now;
        T save = null;
        if (!File.Exists(path)) return null;

        using (var jsonFile = File.Open(path, FileMode.Open))
#if UNITY_EDITOR
        using (var reader = new StreamReader(jsonFile))
#else
      using (var gzip = new GZipStream(jsonFile, CompressionMode.Decompress))
      using (var reader = new StreamReader(gzip))
#endif
        {
            var jsonString = reader.ReadToEnd();
            save = JsonUtility.FromJson<T>(jsonString);
        }
        var jsonTimeMillis = (DateTime.Now - jsonTime).TotalMilliseconds;
        Debug.Log($"SaveSystem::LoadFile({path}) - {jsonTimeMillis}ms");
        return save;
    }

    public static string Path(string name)
    {
#if UNITY_EDITOR
        string JSON_PATH = @".";
        string JSON_EXT = @".json";
#else
      string JSON_PATH = Application.persistentDataPath;
      string JSON_EXT = @".dat";
#endif

        return $"{JSON_PATH}/{name}{JSON_EXT}";
    }

    public void Save()
    {
        try
        {
            // Store game state
            Game game = GetComponent<Game>();
            GameSave gameSave = game.Save();
            SaveFile(gameSave, Path("save/game"));
        }
        catch (Exception exception)
        {
            Debug.LogError(exception);
        }
    }

    public static void SaveFile(object save, string path)
    {
        // Ensure directory exists
        var dirName = System.IO.Path.GetDirectoryName(path);
        Directory.CreateDirectory(dirName);

        var json = JsonUtility.ToJson(save, true);
        using (var jsonFile = File.Open(path, FileMode.Create))
#if UNITY_EDITOR
        using (var jsonWriter = new StreamWriter(jsonFile))
#else
    using (var gzip = new GZipStream(jsonFile, CompressionLevel.Optimal, leaveOpen: false))
    using (var jsonWriter = new StreamWriter(gzip))
#endif
        {
            jsonWriter.Write(json);
        }
        var jsonSize = new FileInfo(path).Length;
        Debug.Log($"SaveSystem::SaveFile({save}, {path}) - {jsonSize} bytes");
    }
}
