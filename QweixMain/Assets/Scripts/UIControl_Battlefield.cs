using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DataClasses;

public class UIControl_Battlefield : MonoBehaviour
{
    #region Constants

    [SerializeField]
    private DummyData dummyData;

    [SerializeField]
    private UIDocument UIControlBattlefield;

    private Label timeLabel;

    private Label playerNameLabel;
    private Label playerClanLabel;
    private Label playerXPLabel;

    private Label opponentNameLabel;
    private Label opponentClanLabel;
    private Label opponentXPLabel;

    private Label cardQwiexCostNext;
    private VisualElement cardFrameNext;
    private Label cardQwiexCost1;
    private VisualElement cardFrame1;
    private Label cardQwiexCost2;
    private VisualElement cardFrame2;
    private Label cardQwiexCost3;
    private VisualElement cardFrame3;
    private Label cardQwiexCost4;
    private VisualElement cardFrame4;

    // Cards in hand display
    private CardDisplay cardDisplayNext;
    private CardDisplay cardDisplay1;
    private CardDisplay cardDisplay2;
    private CardDisplay cardDisplay3;
    private CardDisplay cardDisplay4;

    private Label qwiexLabel;
    private List<VisualElement> qwiexBar;

    private Button cardButton1;
    private Button cardButton2;
    private Button cardButton3;
    private Button cardButton4;

    #endregion Constants

    private void Awake()
    {
        var UIRoot = UIControlBattlefield.rootVisualElement;

        timeLabel = UIRoot.Q<Label>("lbl_TimeLeftDisplay");

        playerNameLabel = UIRoot.Q<Label>("lbl_PlayerName");
        playerClanLabel = UIRoot.Q<Label>("lbl_PlayerClan");
        playerXPLabel = UIRoot.Q<Label>("lbl_PlayerExperience");

        opponentNameLabel = UIRoot.Q<Label>("lbl_OpponentName");
        opponentClanLabel = UIRoot.Q<Label>("lbl_OpponentClan");
        opponentXPLabel = UIRoot.Q<Label>("lbl_OpponentExperience");

        cardQwiexCostNext = UIRoot.Q<Label>("lbl_NextCardCost");
        cardFrameNext = UIRoot.Q<VisualElement>("CardFrameNext");
        cardQwiexCost1 = UIRoot.Q<Label>("lbl_Card1Cost");
        cardFrame1 = UIRoot.Q<VisualElement>("CardFrame1");
        cardQwiexCost2 = UIRoot.Q<Label>("lbl_Card2Cost");
        cardFrame2 = UIRoot.Q<VisualElement>("CardFrame2");
        cardQwiexCost3 = UIRoot.Q<Label>("lbl_Card3Cost");
        cardFrame3 = UIRoot.Q<VisualElement>("CardFrame3");
        cardQwiexCost4 = UIRoot.Q<Label>("lbl_Card4Cost");
        cardFrame4 = UIRoot.Q<VisualElement>("CardFrame4");

        qwiexBar = new List<VisualElement>();

        qwiexLabel = UIRoot.Q<Label>("lbl_QwiexTotal");
        qwiexBar.Add(UIRoot.Q<VisualElement>("QwiexBar1"));
        qwiexBar.Add(UIRoot.Q<VisualElement>("QwiexBar2"));
        qwiexBar.Add(UIRoot.Q<VisualElement>("QwiexBar3"));
        qwiexBar.Add(UIRoot.Q<VisualElement>("QwiexBar4"));
        qwiexBar.Add(UIRoot.Q<VisualElement>("QwiexBar5"));
        qwiexBar.Add(UIRoot.Q<VisualElement>("QwiexBar6"));
        qwiexBar.Add(UIRoot.Q<VisualElement>("QwiexBar7"));
        qwiexBar.Add(UIRoot.Q<VisualElement>("QwiexBar8"));
        qwiexBar.Add(UIRoot.Q<VisualElement>("QwiexBar9"));
        qwiexBar.Add(UIRoot.Q<VisualElement>("QwiexBar10"));

        cardButton1 = UIRoot.Q<Button>("CardButton1");
        cardButton2 = UIRoot.Q<Button>("CardButton2");
        cardButton3 = UIRoot.Q<Button>("CardButton3");
        cardButton4 = UIRoot.Q<Button>("CardButton4");

        cardButton1.RegisterCallback<ClickEvent>(CardClick);
        cardButton2.RegisterCallback<ClickEvent>(CardClick);
        cardButton3.RegisterCallback<ClickEvent>(CardClick);
        cardButton4.RegisterCallback<ClickEvent>(CardClick);
    }

    // Should be updated every frame
    public void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timeLabel.text = "" + minutes + ":" + seconds;
    }

    // Should be updated every frame
    public void UpdateQwiexDisplay(float currentQwiex)
    {
        // Iterate through the VisualElements in the Qwiex Bar
        foreach(VisualElement element in qwiexBar)
        {
            // If the current Qwiex level exceeds the index of the current display segment
            if (currentQwiex >= qwiexBar.IndexOf(element) + 1)
            {
                // Set the width of the display segment to full
                element.style.width = Length.Percent(100);
            }
            // If the current Qwiex level is between the index of the last and current
            else if(Mathf.Floor(currentQwiex) == qwiexBar.IndexOf(element))
            {
                // Set the width of the display segment to the proper percentage
                element.style.width = Length.Percent((currentQwiex - Mathf.Floor(currentQwiex)) * 100);
            }
            else
            {
                // Otherwise set the width of the display segment to zero
                element.style.width = Length.Percent(0);
            }
        }

        // Update the Qwiex label
        qwiexLabel.text = Math.Floor(currentQwiex).ToString();
    }

    public void SetPlayerInfo(PlayerInfo user, PlayerInfo opponent)
    {
        playerNameLabel.text = user.playerName;
        playerClanLabel.text = user.playerClan;
        playerXPLabel.text = user.playerXP.ToString();

        opponentNameLabel.text = opponent.playerName;
        opponentClanLabel.text = opponent.playerClan;
        opponentXPLabel.text = opponent.playerXP.ToString();
    }

    // Set card info into card slot
    //     0 = Next
    //     1 = Left
    //     2 = Left-Center
    //     3 = Right-Center
    //     4 = Right
    public void SetCardInfo(CardInfo info, int slot)
    {
        switch(slot)
        {
            case 0:
                cardDisplayNext.displayQwiexCost = info.cardQwiexCost;
                cardDisplayNext.displayTexture = info.cardTexture;
                cardDisplayNext.info = info;
                cardFrameNext.style.backgroundImage = new StyleBackground(Sprite.Create(info.cardTexture, new Rect(0.0f, 0.0f, info.cardTexture.width, info.cardTexture.height), new Vector2(0.5f, 0.5f), 100.0f));
                cardQwiexCostNext.text = info.cardQwiexCost.ToString();
                
                break;

            case 1:
                cardDisplay1.displayQwiexCost = info.cardQwiexCost;
                cardDisplay1.displayTexture = info.cardTexture;
                cardDisplay1.info = info;
                cardFrame1.style.backgroundImage = new StyleBackground(Sprite.Create(info.cardTexture, new Rect(0.0f, 0.0f, info.cardTexture.width, info.cardTexture.height), new Vector2(0.5f, 0.5f), 100.0f));
                cardQwiexCost1.text = info.cardQwiexCost.ToString();

                break;

            case 2:
                cardDisplay2.displayQwiexCost = info.cardQwiexCost;
                cardDisplay2.displayTexture = info.cardTexture;
                cardDisplay2.info = info;
                cardFrame2.style.backgroundImage = new StyleBackground(Sprite.Create(info.cardTexture, new Rect(0.0f, 0.0f, info.cardTexture.width, info.cardTexture.height), new Vector2(0.5f, 0.5f), 100.0f));
                cardQwiexCost2.text = info.cardQwiexCost.ToString();

                break;

            case 3:
                cardDisplay3.displayQwiexCost = info.cardQwiexCost;
                cardDisplay3.displayTexture = info.cardTexture;
                cardDisplay3.info = info;
                cardFrame3.style.backgroundImage = new StyleBackground(Sprite.Create(info.cardTexture, new Rect(0.0f, 0.0f, info.cardTexture.width, info.cardTexture.height), new Vector2(0.5f, 0.5f), 100.0f));
                cardQwiexCost3.text = info.cardQwiexCost.ToString();

                break;

            case 4:
                cardDisplay4.displayQwiexCost = info.cardQwiexCost;
                cardDisplay4.displayTexture = info.cardTexture;
                cardDisplay4.info = info;
                cardFrame4.style.backgroundImage = new StyleBackground(Sprite.Create(info.cardTexture, new Rect(0.0f, 0.0f, info.cardTexture.width, info.cardTexture.height), new Vector2(0.5f, 0.5f), 100.0f));
                cardQwiexCost4.text = info.cardQwiexCost.ToString();

                break;

        }
    }

    private void CardClick(ClickEvent evt)
    {
        dummyData.UI_CardClick(evt);
    }

    public void SetSelectedCard(int selectedCard)
    {
        switch (selectedCard)
        {
            case 1:
                cardFrame1.AddToClassList("SelectedCardSlot");
                cardFrame1.RemoveFromClassList("CardSlot");
                cardFrame2.RemoveFromClassList("SelectedCardSlot");
                cardFrame2.AddToClassList("CardSlot");
                cardFrame3.RemoveFromClassList("SelectedCardSlot");
                cardFrame3.AddToClassList("CardSlot");
                cardFrame4.RemoveFromClassList("SelectedCardSlot");
                cardFrame4.AddToClassList("CardSlot");
                break;

            case 2:
                cardFrame1.RemoveFromClassList("SelectedCardSlot");
                cardFrame1.AddToClassList("CardSlot");
                cardFrame2.AddToClassList("SelectedCardSlot");
                cardFrame2.RemoveFromClassList("CardSlot");
                cardFrame3.RemoveFromClassList("SelectedCardSlot");
                cardFrame3.AddToClassList("CardSlot");
                cardFrame4.RemoveFromClassList("SelectedCardSlot");
                cardFrame4.AddToClassList("CardSlot");
                break;

            case 3:
                cardFrame1.RemoveFromClassList("SelectedCardSlot");
                cardFrame1.AddToClassList("CardSlot");
                cardFrame2.RemoveFromClassList("SelectedCardSlot");
                cardFrame2.AddToClassList("CardSlot");
                cardFrame3.AddToClassList("SelectedCardSlot");
                cardFrame3.RemoveFromClassList("CardSlot");
                cardFrame4.RemoveFromClassList("SelectedCardSlot");
                cardFrame4.AddToClassList("CardSlot");
                break;

            case 4:
                cardFrame1.RemoveFromClassList("SelectedCardSlot");
                cardFrame1.AddToClassList("CardSlot");
                cardFrame2.RemoveFromClassList("SelectedCardSlot");
                cardFrame2.AddToClassList("CardSlot");
                cardFrame3.RemoveFromClassList("SelectedCardSlot");
                cardFrame3.AddToClassList("CardSlot");
                cardFrame4.AddToClassList("SelectedCardSlot");
                cardFrame4.RemoveFromClassList("CardSlot");
                break;

            default:
                break;
        }


    }
}

// Struct that will be derived from card objects
public struct CardDisplay
{
    public CardInfo info;
    public int displayQwiexCost;
    public Texture displayTexture;
}