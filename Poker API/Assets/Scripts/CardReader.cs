using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class CardReader : MonoBehaviour
{
    public static CardReader Instance;

    struct DataPair
    {
        public string Suit;
        public int NumValue;

        public DataPair(string Suit, int NumValue)
        {
            this.Suit = Suit;
            this.NumValue = NumValue;
        }
    }

    private List<DataPair> CardValuePairs = new List<DataPair>();
    private Dictionary<string, int> m_SuitCount = new Dictionary<string, int>();
    private Dictionary<int, int> m_SimilarValues = new Dictionary<int, int>();

    public void CheckResult(List<Card> cards)
    {
        SimplifyData(cards);

        //Calcs
        this.CheckSameSuit();
        this.CheckSameValue();

        CompareHierarchies();
    }

    private void SimplifyData(List<Card> cards)
    {
        CardValuePairs.Clear();

        foreach (Card card in cards)
        {
            int nValue;

            switch (card.Value)
            {
                case "ACE":
                    nValue = 1;
                    break;
                case "JACK":
                    nValue = 11;
                    break;
                case "QUEEN":
                    nValue = 12;
                    break;
                case "KING":
                    nValue = 13;
                    break;
                default:
                    nValue = int.Parse(card.Value);
                    break;
            }

            CardValuePairs.Add(new DataPair(card.Suit, nValue));
        }

        //Verification
        /*foreach (DataPair data in CardValuePairs)
        {
            Debug.Log($"{data.Suit}-{data.NumValue}");
        }*/

    }

    private void CompareHierarchies()
    {
        string result;
        if (IsRoyalFlush()) result = "ROYAL FLUSH";
        else if (IsStraightFlush()) result = "STRAIGHT FLUSH";
        else if (IsFourOfKind()) result = "FOUR OF A KIND";
        else if (IsFullHouse()) result = "FULL HOUSE";
        else if (IsFlush()) result = "FLUSH";
        else if (IsStraight()) result = "STRAIGHT";
        else if (IsThreeOfKind()) result = "THREE OF A KIND";
        else if (IsTwoPair()) result = "TWO PAIR";
        else if (IsOnePair()) result = "ONE PAIR";
        else result = "HIGH CARD";

        GUIManager.Instance.HandRanking.style.color = (result != "HIGH CARD") ? Color.yellow : Color.red;
        GUIManager.Instance.HandRanking.text = result;
    }

    private bool IsRoyalFlush()
    {
        if (m_SuitCount.Values.Max() >= 5)
        {
            List<int> valuesForRoyalFlush = new List<int>();
            valuesForRoyalFlush.Add(10);
            valuesForRoyalFlush.Add(11);
            valuesForRoyalFlush.Add(12);
            valuesForRoyalFlush.Add(13);
            valuesForRoyalFlush.Add(1);

            foreach (DataPair data in CardValuePairs)
            {
                if (valuesForRoyalFlush.Contains(data.NumValue))
                    valuesForRoyalFlush.Remove(data.NumValue);
                else
                    return false;
            }

            if (valuesForRoyalFlush.Count == 0)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsStraightFlush()
    {
        return (IsStraight() && IsFlush());
    }

    private bool IsFourOfKind()
    {
        return (m_SimilarValues.Values.Max() == 4);
    }

    private bool IsFullHouse()
    {
        return m_SimilarValues.Values.Contains(2) && m_SimilarValues.Values.Contains(3);
    }

    private bool IsFlush()
    {
        return (m_SuitCount.Values.Max() == 5);
    }

    private bool IsStraight()
    {
        //GET THE SMALLEST VALUE 
        List<int> extractedNum = new List<int>();
        foreach (DataPair data in CardValuePairs)
        {
            extractedNum.Add(data.NumValue);
        }

        //VALUES TO COMPARE WITH
        List<int> scanAbove = new List<int>();
        for(int i = 0; i < 5; i++)
        {
            int val = extractedNum.Min()+ i;
            if (val > 13)
            {
                val -= 13;
            }
            scanAbove.Add(val);
        }

        foreach(int cardNum in extractedNum)
        {
            if (scanAbove.Contains(cardNum))
            {
                scanAbove.Remove(cardNum);
            }
            else
                break;
        }

        //Debug.Log("Above " +  scanAbove.Count);
        return (scanAbove.Count == 0);
    }
    private bool IsThreeOfKind()
    {
        return (m_SimilarValues.Values.Max() == 3);
    }

    private bool IsTwoPair()
    {
        int nPairsFound = 0;

        for (int i = 1; i <= 13; i++)
        {
            if (m_SimilarValues[i] >= 2)
            {
                nPairsFound++;
            }
        }

        return nPairsFound == 2;
    }
    private bool IsOnePair()
    {
        return (m_SimilarValues.Values.Max() == 2);
    }

    private void CheckSameSuit()
    {
        m_SuitCount["SPADES"] = 0;
        m_SuitCount["HEARTS"] = 0;
        m_SuitCount["DIAMONDS"] = 0;
        m_SuitCount["CLUBS"] = 0;

        foreach (DataPair data in CardValuePairs)
        {
            m_SuitCount[data.Suit] += 1;
        }

        Debug.Log($"MOST SIMILAR SUITS: {m_SuitCount.Values.Max()}");
    }

    private void CheckSameValue()
    {
        for(int i = 1; i <= 13; i++)
        {
            m_SimilarValues[i] = 0;
        }

        foreach (DataPair data in CardValuePairs)
        {
            m_SimilarValues[data.NumValue] += 1;
        }

        Debug.Log($"MOST SIMILAR Values: {m_SimilarValues.Values.Max()}");
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
