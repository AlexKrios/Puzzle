using System.IO;
using System.Linq;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public GameManager manager;

    public void Save()
    {
        var saveData = new SaveLoadData();
        var blocksList = manager.blocksList;

        saveData.fieldSize = manager.fieldSize;
        for (int i = 1; i <= blocksList.Count; i++)
        {
            var block = blocksList.First(x => x.place == i);
            saveData.blockNumbers.Add(block.number);
        }

        string saveString = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/save.json", saveString);

        PlayerPrefs.SetInt("isLoad", 1);
    }

    public SaveLoadData Load()
    {
        var path = Application.persistentDataPath + "/save.json";
        var saveData = JsonUtility.FromJson<SaveLoadData>(File.ReadAllText(path));

        return saveData;
    }
}
