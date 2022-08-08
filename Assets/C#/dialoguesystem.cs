using TMPro;
using UnityEngine;
using System.Collections;
using System;

namespace jerry
{
    //委派簽名.無傳回.無參數
    public delegate void DelegateFinishDialogue();

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

        [SerializeField, Header("淡入間隔")]
        private float intervalFadeIn = 0.1f;
        [SerializeField, Header("打字間隔")]
        private float intervalType = 0.05f;

        [SerializeField, Header("對話標示")]
        private GameObject gotriangle;

        private NPCdata dataNpc;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

            //StartCoroutine(StartDialogue());
        }

        #region 公開資料與方法
        /// <summary>
        /// 對話執行與否
        /// </summary>
        public bool isDialogue;

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

        public IEnumerator StartDialogue(NPCdata _dataNPC,DelegateFinishDialogue callback)
        {
            isDialogue = true;

            dataNpc = _dataNPC;

            textName.text = dataNpc.nameNPC;
            textContent.text = "";

            yield return StartCoroutine(Fade());

            for (int i=0;i< dataNpc.dataDialogue.Length; i++)
            {
                yield return StartCoroutine(TypeEffect(i));

                while (!Input.GetKeyDown(KeyCode.E))
                {
                    yield return null;
                }
            }
            StartCoroutine(Fade(false));
            isDialogue = false;
            callback(); //執行呼叫程式
        }
        #endregion

        private IEnumerator Fade( bool fadeIn=true)
        {
            float increase = fadeIn ? 0.1f : -0.1f;

            for (int i = 0; i < 10; i++)
            {
                groupdialogue.alpha += increase;
                yield return new WaitForSeconds(0.3f);
            }
        }

        private IEnumerator TypeEffect(int indexDialogue)
        {
            textContent.text = "";
            aud.PlayOneShot(dataNpc.dataDialogue[indexDialogue].sound);

            string content = dataNpc.dataDialogue[indexDialogue].content;

            for (int i = 0; i < content.Length; i++)
            {
                textContent.text += content[i];
                yield return new WaitForSeconds(0.05f);
            }

            gotriangle.SetActive(true);
        }
    }
}

