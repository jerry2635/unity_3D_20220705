using UnityEngine;

namespace jerry
{
    /// <summary>
    /// 敵人資料
    /// order 表單排序
    /// </summary>
    [CreateAssetMenu(menuName = "jerry/Data Enemy", fileName = "Data Enemy", order = 1)]
    public class DataEnemy : ScriptableObject
    {
        [Header("血量"), Range(0, 2000)]
        public float hp;
        [Header("攻擊力"), Range(0, 200)]
        public float attack;
        [Header("追蹤距離"), Range(0, 200)]
        public float rangeTrack;
        [Header("攻擊距離"), Range(0, 10)]
        public float rangeAttack;
        [Header("走路速度"), Range(0, 100)]
        public float speedWalk;
        [Header("掉落道具機率"), Range(0, 1)]
        public float propbilityProp;
        [Header("掉落道具")]
        public GameObject goProp;
        [Header("等待時間")]
        public Vector2 timeIdleRange;
        [Header("追蹤目標圖層")]
        public LayerMask layerTarget;
        [Header("攻擊間隔"), Range(0, 5)]
        public float intervalAttack;
    }

}

