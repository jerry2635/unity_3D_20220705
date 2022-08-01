using TMPro;
using UnityEngine;
using System.Collections;

namespace jerry
{
    /// <summary>
    /// ��ܨt��.�H�J.��sNPC��ƦW��.���e.����.�H�X
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class dialoguesystem : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("�e����ܨt��")]
        private CanvasGroup groupdialogue;
        [SerializeField, Header("���ܪ̦W��")]
        private TextMeshProUGUI textName;
        [SerializeField, Header("��ܤ��e")]
        private TextMeshProUGUI textContent;

        private AudioSource aud;
        #endregion

        [SerializeField, Header("��ܼХ�")]
        private GameObject gotriangle;

        public NPCdata dataNpc;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

            StartCoroutine(FadeIn());

            textName.text = dataNpc.nameNPC;
            textContent.text = "";
        }
        #region ��P�о�
        ///��P�{�ǻݭn
        ///�ޥ�system.collection
        ///�w�q��k.�Ǧ^ieunumerator
        ///�Ұ�startcoroutine
        private IEnumerator test()
        {
            print("�Ĥ@���r");
            yield return new WaitForSeconds(2);
            print("�ĤG���r");
            yield return new WaitForSeconds(5);
            print("�ĤT���r");
        }
        #endregion

        private IEnumerator FadeIn()
        {
            for(int i=0;i<10;i++)
            {
                groupdialogue.alpha += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            StartCoroutine(TypeEffect());
        }

        private IEnumerator TypeEffect()
        {
            aud.PlayOneShot(dataNpc.dataDialogue[0].sound);

            string content = dataNpc.dataDialogue[0].content;

            for (int i = 0; i < content.Length; i++)
            {
                textContent.text += content[i];
                yield return new WaitForSeconds(0.05f);
            }

            gotriangle.SetActive(true);
        }
    }
}

