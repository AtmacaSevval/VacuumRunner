using Sirenix.OdinInspector;
using System.Collections.Generic;
using Twenty.Data;
using UnityEngine;

namespace Twenty.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public bool testMode;

        [InlineEditor(InlineEditorModes.GUIAndPreview)]
        public List<Level> levels;

        [Button]
        public void LoadLevel(int index)
        {
            if (!testMode)
            {
                for (int i = 0; i < levels[index].levelDatas.Count; i++)
                {
                    levels[index].levelDatas[i].Load();
                }
            }
            else
            {
                for (int i = 0; i < levels[0].levelDatas.Count; i++)
                {
                    levels[0].levelDatas[i].Load();
                }
            }
        }
    }
}

