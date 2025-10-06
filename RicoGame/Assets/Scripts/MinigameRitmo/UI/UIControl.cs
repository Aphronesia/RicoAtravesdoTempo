using TMPro;
using UnityEngine;

namespace MinigameRitmo.UI
{
    public class UIControl : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pontostxt;

        [SerializeField] private TextMeshProUGUI combotxt;
        public void RefreshTextPontos(int value)
        {
            pontostxt.text = $"pontos: {value}";
        }

        public void RefreshTextCombos(int value)
        {
            combotxt.text = $"Combo: {value}";
        }
    }
}
