using UnityEngine;

namespace jerry
{
    public class PlayerGetProp : MonoBehaviour
    {
        private TurtleObjectPool turtleObjectChips;
        private string proChips = "¬v¨¡¤ù¥]";

        private void Awake()
        {
            turtleObjectChips = FindObjectOfType<TurtleObjectPool>();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.name.Contains(proChips))
            {
                turtleObjectChips.ReleasePoolObject(hit.gameObject);
            }
        }
    }
}

