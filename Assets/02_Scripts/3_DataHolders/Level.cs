using System.Collections.Generic;
using UnityEngine;


namespace Twenty.Data
{
    [CreateAssetMenu(fileName = "Level", menuName = "LevelDesign/CreateLevel", order = 1)]
    public class Level : ScriptableObject
    {
        public List<LevelData> levelDatas;
    }
}

