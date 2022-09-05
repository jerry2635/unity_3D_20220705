using UnityEngine;

/// <summary>
/// �������
/// </summary>

namespace jerry
{
    [CreateAssetMenu(menuName ="jerry/Data Attack",fileName ="Data Attack")]//�b��椤�سy�W��
    public class DataAttack : ScriptableObject
    {
        [Header("�����O"), Range(0, 1000)]
        public float attack;
        [Header("�����ϰ�]�w")]
        public Color attackAreaColor = new Color(1, 0, 0, 0.5f);
        public Vector3 attackAreaSize = Vector3.one;
        public Vector3 attackAreaOffset;
        [Header("�����ؼйϼh")]
        public LayerMask layerTarget;
        [Header("��������ɶ�"), Range(0, 3)]
        public float delayAttack = 1.5f;
        [Header("�����ʵe��")]
        public AnimationClip animationAttack;

        public float waitAttackEnd => animationAttack.length - delayAttack;
    }

}

