using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class SceneMng : MonoBehaviour
{
    [SerializeField] Text _ToolTipText;
    List<string> _ToolTipString = new List<string>();

    

    void ToolTipInit()
    {
        _Camera.transform.localPosition = new Vector3(0, 0, -10);
        _ToolTipString.Add("트럼프 카드는 영미권에서는 '플레잉 카드(Playing Card)' 라고 불립니다.");
        _ToolTipString.Add("화투는 일본에서 라틴식 트럼프 카드를 토대로 만들어진 카드입니다.");
        _ToolTipString.Add("스페이드 모양은 삽, 검을 의미하며, 기사 또는 귀족을 상징합니다. 또한 권위와 죽음을 뜻합니다.");
        _ToolTipString.Add("하트는 심장이 아닌 성배를 본따 만들어 졌습니다. 성직자를 상징하며 마음을 뜻합니다.");
        _ToolTipString.Add("다이아몬드는 보석, 화폐를 의미하며, 상인을 상징합니다. 또한 재물을 뜻합니다.");
        _ToolTipString.Add("클로버는 곤봉, 몽둥이를 의미하며 농민을 상징합니다. 또한 지혜를 뜻합니다.");
        _ToolTipString.Add("카드의 숫자가 같을 경우 ♠>♥>♦>♣ 순으로 높게 쳐줍니다.");
        _ToolTipString.Add("스페이드 A의 무늬가 유독 화려한 이유는, 과거에 세금을 징수하기 위해\n스페이드 A에 위조 방지 문양을 새긴 것이 전통이 되었기 때문입니다.");
        _ToolTipString.Add("각 문양의 J, Q, K에 그려진 그림은 모두 다른 인물을 모티브로 하며\n실존 인물, 성경속 인물, 신화속 인물들 입니다.");
    }

    void SetToolTip()
    {
        int r = Random.Range(0, _ToolTipString.Count);
        _ToolTipText.text = _ToolTipString[r];
    }
}
