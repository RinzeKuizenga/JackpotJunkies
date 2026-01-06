using UnityEngine;
using System;

[CreateAssetMenu(menuName = "UI/Intent Icon Library")]
public class IntentIconLibrary : ScriptableObject
{
    // Entrees voor de Icon Library. Zo wordt een intention getoont.
    [Serializable]
    public struct Entry {
        public IntentType type;
        public Sprite icon;
    }

    [SerializeField] private Entry[] entries;

    // Pakt de icon gebaseerd op de type van de intention (attack, defend)
    public Sprite GetIcon(IntentType type) {
        if (entries == null) return null;

        for (int i = 0; i < entries.Length; i++)
        {
           if (entries[i].type == type)
               return entries[i].icon;
        }

        return null;
    }
}
