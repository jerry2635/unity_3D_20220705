using UnityEngine;
using UnityEditor;

namespace jerry
{
    [CreateAssetMenu(menuName ="jerry/Date Health",fileName ="Data Health")]
    public class DataHealth : ScriptableObject
    {
        [Header("��q"), Range(0, 10000)]
        public float hp;
        [HideInInspector]
        public float hpMax => hp;
        [Header("�O�_�����_��")]
        public bool isDropProp;
        [Header("�_���w�s��")]
        public GameObject goProp;
        [Header("�_���������v"),Range(0f,1f)]
        public float propProbability;
    }

    /// <summary>
    ///�ۭq�s�边_����_���O 
    /// </summary>
    [CustomEditor(typeof(DataHealth))]
    public class DatahealthEditor : Editor
    {
        //�ǦC���ݩ�_�ۭq�W��
        SerializedProperty spIsDropProp;
        SerializedProperty spGoProp;
        SerializedProperty spPropProbability;

        private void OnEnable()
        {
            //�ǦC�ƪ���.�M���ݩ�_�W��_���O.��ƦW��
            spIsDropProp = serializedObject.FindProperty(nameof(DataHealth.isDropProp));
            spGoProp = serializedObject.FindProperty(nameof(DataHealth.goProp));
            spPropProbability = serializedObject.FindProperty(nameof(DataHealth.propProbability));
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();

            if (spIsDropProp.boolValue)
            {
                EditorGUILayout.PropertyField(spGoProp);
                EditorGUILayout.PropertyField(spPropProbability);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

