using UnityEngine;

namespace TickBeGone
{
    internal class BetterBillboard : MonoBehaviour
    {
        private void LateUpdate() => transform.forward = Camera.main.transform.forward;
    }
}
