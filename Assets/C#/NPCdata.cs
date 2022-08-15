using UnityEngine;

namespace jerry
{
    ///
    ///npc��ƦW�ٹ�ܭ���
    ///scriptableobject �}���ƪ���
    ///�N�{�����e�s�������bproject��
    ///
    [CreateAssetMenu(menuName = "jerry/Data NPC", fileName = "Data NPC",order =2)]
    public class NPCdata : ScriptableObject
    {
        [Header("NPC�W��")]
        public string nameNPC;
        [Header("�Ҧ����"), NonReorderable]
        public DataDialogue[] dataDialogue;
    }

    [System.Serializable]
    public class DataDialogue
    {
        [Header("��ܤ��e")]
        public string content;
        [Header("��ܭ���")]
        public AudioClip sound;
    }
}


