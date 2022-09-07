using UnityEngine;
using UnityEditor;

namespace jerry
{
    [CreateAssetMenu(menuName ="jerry/Date Health",fileName ="Data Health")]
    public class DataHealth : ScriptableObject
    {
        [Header("血量"), Range(0, 10000)]
        public float hp;
        [HideInInspector]
        public float hpMax => hp;
        [Header("是否掉落寶物")]
        public bool isDropProp;
        [Header("寶物預製物")]
        public GameObject goProp;
        [Header("寶物掉落機率"),Range(0f,1f)]
        public float propProbability;
    }

    /// <summary>
    ///自訂編輯器_類型_類別 
    /// </summary>
    [CustomEditor(typeof(DataHealth))]
    public class DatahealthEditor : Editor
    {
        //序列化屬性_自訂名稱
        SerializedProperty spIsDropProp;
        SerializedProperty spGoProp;
        SerializedProperty spPropProbability;

        private void OnEnable()
        {
            //序列化物件.尋找屬性_名稱_類別.資料名稱
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

