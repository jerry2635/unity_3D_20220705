using UnityEngine;

namespace jerry
{
    ///
    ///npc資料名稱對話音效
    ///scriptableobject 腳本化物件
    ///將程式內容存為物件放在project內
    ///
    [CreateAssetMenu(menuName = "jerry/Data NPC", fileName = "Data NPC",order =2)]
    public class NPCdata : ScriptableObject
    {
        [Header("NPC名稱")]
        public string nameNPC;
        [Header("所有對話"), NonReorderable]
        public DataDialogue[] dataDialogue;
    }

    [System.Serializable]
    public class DataDialogue
    {
        [Header("對話內容")]
        public string content;
        [Header("對話音效")]
        public AudioClip sound;
    }
}


