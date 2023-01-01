using System.Collections.Generic;
using UnityEngine;

public class CardTargetManager : Singleton<CardTargetManager>
{ 
    [SerializeField] private List<RectTransform> initalizeCardTarget;
    public List<RectTransform> InitalizeCardTarget { get { return  initalizeCardTarget; }  }

    [SerializeField] private RectTransform mainCardTarget;
    public RectTransform MainCardTarget { get { return mainCardTarget; } }

    [SerializeField] private RectTransform buttomCard;
    public RectTransform ButtomCard { get { return buttomCard;}  }

    [SerializeField] private RectTransform topCard;
    public RectTransform TopCard { get{ return topCard;}  }
}
