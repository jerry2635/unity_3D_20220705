using UnityEngine;

/// <summary>
/// 攻擊資料
/// </summary>

namespace jerry
{
    [CreateAssetMenu(menuName ="jerry/Data Attack",fileName ="Data Attack")]//在選單中建造名稱
    public class DataAttack : ScriptableObject
    {
        [Header("攻擊力"), Range(0, 1000)]
        public float attack;
        [Header("攻擊區域設定")]
        public Color attackAreaColor = new Color(1, 0, 0, 0.5f);
        public Vector3 attackAreaSize = Vector3.one;
        public Vector3 attackAreaOffset;
        [Header("攻擊目標圖層")]
        public LayerMask layerTarget;
        [Header("攻擊延遲時間"), Range(0, 3)]
        public float delayAttack = 1.5f;
        [Header("攻擊動畫檔")]
        public AnimationClip animationAttack;

        public float waitAttackEnd => animationAttack.length - delayAttack;
    }

}

