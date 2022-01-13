using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Twenty.Managers;
using UnityEditor;
using UnityEngine;

namespace Twenty.Data
{
    public class LevelSaver : MonoBehaviour
    {
        private LevelManager levelManager;

#if UNITY_EDITOR
        [Button(ButtonSizes.Large)]
        public void SaveCurrentLevel(int currentLevelIndex)
        {
            AssetDatabase.CreateFolder("Assets/04_GamePrefabs/1_Essentials/3_Levels", "Level" + currentLevelIndex);

            Level currentLevel = ScriptableObject.CreateInstance<Level>();

            currentLevel.levelDatas = new List<LevelData>();

            var saverArray = FindObjectsOfType<MonoBehaviour>().OfType<IDataSaver>();

            foreach (var saver in saverArray)
            {
                currentLevel.levelDatas.Add(saver.SaveData(currentLevelIndex));
            }

            AssetDatabase.CreateAsset(currentLevel, "Assets/04_GamePrefabs/1_Essentials/3_Levels/Level" + currentLevelIndex + "/Level" + currentLevelIndex + ".asset");

            levelManager = GetComponent<LevelManager>();
            levelManager.levels.Add(currentLevel);
        }
#endif
    }
}

