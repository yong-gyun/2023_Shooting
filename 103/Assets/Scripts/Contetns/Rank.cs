
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RankData
{
    public string name;
    public int score;
}

public class Rank : MonoBehaviour
{
    public int[] scores = new int[5];
    public string[] names = new string[5];

    public void SortRank(string name)
    {
        int score = GameManager.Instance.Score;
        for (int i = 0; i < scores.Length; i++)
        {
            for (int j = 0; j < scores.Length; j++)
            {
                if (scores[i] > scores[j])
                {
                    int i_temp = scores[i];
                    string s_temp = names[i];

                    scores[i] = scores[j];
                    names[i] = names[j];
                    scores[j] = i_temp;
                    names[j] = s_temp;
                }
            }
        }

        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] < score)
            {
                int i_temp = scores[i];
                string s_temp = names[i];

                scores[i] = score;
                names[i] = name;

                score = i_temp;
                name = s_temp;
            }
        }
    }
}
