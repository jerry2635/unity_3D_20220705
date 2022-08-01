using TMPro;
using UnityEngine;
using System.Collections;

namespace jerry
{
    /// <summary>
    /// 對話系統.淡入.更新NPC資料名稱.內容.音效.淡出
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class dialoguesystem : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("畫布對話系統")]
        private CanvasGroup groupdialogue;
        [SerializeField, Header("說話者名稱")]
        private TextMeshProUGUI textName;
        [SerializeField, Header("對話內容")]
        private TextMeshProUGUI textContent;

        private AudioSource aud;
        #endregion

        [SerializeField, Header("對話標示")]
        private GameObject gotriangle;

        public NPCdata dataNpc;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

            StartCoroutine(FadeIn());

            textName.text = dataNpc.nameNPC;
            textContent.text = "";
        }
        #region 協同教學
        ///協同程序需要
        ///引用system.collection
        ///定義方法.傳回ieunumerator
        ///啟動startcoroutine
        private IEnumerator test()
        {
            print("第一行文字");
            yield return new WaitForSeconds(2);
            print("第二行文字");
            yield return new WaitForSeconds(5);
            print("第三行文字");
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

