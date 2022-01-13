using UnityEngine;

namespace Twenty.Data
{
    public abstract class LevelData : ScriptableObject
    {
        protected IDataUser myDataUser;

        public abstract void Load();
    }
}

