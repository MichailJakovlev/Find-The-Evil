using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics.Geometry;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComplexityDirector : MonoBehaviour
{
    [Header("Object Links")]
    [SerializeField] private CardPool cardPool;
    [SerializeField] private RoleDirector roleDirector;
    
    [Header("Roles")]
    [SerializeField] private List<Role> villagerRoles;
    [SerializeField] private List<Role> outcastRoles;
    [SerializeField] private List<Role> evilRoles;
    [SerializeField] private List<Role> substituteRoles; 
    
    [Header("Complexity Settings")]
    public int streakThreshold;
    public int setComplexityLevel;

    private int[][] easyComlexityTemplates;
    private int[][] hardComlexityTemplates;
    private int streak = 0;

    static int complexityLevel;
    public static int ComplexityLevel
    {
        get => complexityLevel;
        set => complexityLevel = Mathf.Clamp(value, 1, 5);
    }
    public void Start()
    {
        ComplexityLevel = setComplexityLevel;
        easyComlexityTemplates = new int[5][];
        for (int i = 0; i < easyComlexityTemplates.Length; i++)
        {
            easyComlexityTemplates[i] = new int[3];
            switch (i)
            {
                case 1: // 6 cards
                    easyComlexityTemplates[i][0] = 5;
                    easyComlexityTemplates[i][1] = 0;
                    easyComlexityTemplates[i][2] = 1;
                    break;
                case 2: // 7 cards
                    easyComlexityTemplates[i][0] = 4;
                    easyComlexityTemplates[i][1] = 2;
                    easyComlexityTemplates[i][2] = 1;
                    break; 
                case 3: // 8 cards
                    easyComlexityTemplates[i][0] = 4;
                    easyComlexityTemplates[i][1] = 2;
                    easyComlexityTemplates[i][2] = 2;
                    break;
                case 4: // 9 cards
                    easyComlexityTemplates[i][0] = 4;
                    easyComlexityTemplates[i][1] = 3;
                    easyComlexityTemplates[i][2] = 2;
                    break;
                case 5: // 10 cards
                    easyComlexityTemplates[i][0] = 4;
                    easyComlexityTemplates[i][1] = 3;
                    easyComlexityTemplates[i][2] = 3;
                    break;
            }
        }
        
        hardComlexityTemplates = new int[5][];
        for (int i = 0; i < hardComlexityTemplates.Length; i++)
        {
            hardComlexityTemplates[i] = new int[3];
            switch (i)
            {
                case 1: // 6 cards
                    hardComlexityTemplates[i][0] = 4;
                    hardComlexityTemplates[i][1] = 1;
                    hardComlexityTemplates[i][2] = 1;
                    break;
                case 2: // 7 cards
                    hardComlexityTemplates[i][0] = 4;
                    hardComlexityTemplates[i][1] = 1;
                    hardComlexityTemplates[i][2] = 2;
                    break;
                case 3: // 8 cards
                    hardComlexityTemplates[i][0] = 4;
                    hardComlexityTemplates[i][1] = 1;
                    hardComlexityTemplates[i][2] = 3;
                    break;
                case 4: // 9 cards
                    hardComlexityTemplates[i][0] = 4;
                    hardComlexityTemplates[i][1] = 2;
                    hardComlexityTemplates[i][2] = 3;
                    break;
                case 5: // 10 cards
                    hardComlexityTemplates[i][0] = 4;
                    hardComlexityTemplates[i][1] = 2;
                    hardComlexityTemplates[i][2] = 4;
                    break;
            }
        }
        
        SetRoundComplexity();
    }

    public void UpdateCalculatedRoles()
    {
        substituteRoles.Clear();
        for (int i = 0; i < villagerRoles.Count; i++)
        {
            substituteRoles.Add(villagerRoles[i]);
        }
        for (int i = 0; i < outcastRoles.Count; i++)
        {
            substituteRoles.Add(outcastRoles[i]);
        }
        substituteRoles.Shuffle();
    }
    
    public void CalculateComplexity(bool isWin)
    {   
        if (isWin)
        {
            if (streak == streakThreshold)
            {
                ComplexityLevel--;
                streak = 0;
            }
            else
            {
                ComplexityLevel++;
                streak++;
            }
        }
        else
        {
            if (streak == streakThreshold)
            {
                streak = 0;
            }
            else
            {
                ComplexityLevel--;
                streak = 0;
            }
        }
    }
    
    public int CalculateDeckWeight(int rolesAmount) 
    {
        int rand = Random.Range(1, 3);
        switch (rand)
        {
            case 1:
                Debug.Log(rolesAmount * 2);
                return Mathf.Clamp(rolesAmount * 2, 0,100);
            case 2:
                Debug.Log(rolesAmount * 2 + 1);
                return Mathf.Clamp(rolesAmount * 2 + 1, 0,100);
            case 3:
                Debug.Log(rolesAmount * 2 - 1);
               return Mathf.Clamp(rolesAmount * 2 - 1, 0,100);
            default:
                return Mathf.Clamp(rolesAmount * 2, 0,100);
        }
    }
    
    public List<List<int>> GetWeight(int targetWeight, int termsAmount, int minRoleWeight)
    {
        List<List<int>> results = new List<List<int>>();
        List<int> currentCombination = new List<int>(new int[termsAmount]);

        Decompose(targetWeight, termsAmount,  minRoleWeight, 0, currentCombination, results);

        return results;
    }

    private void Decompose(int remainingSum, int termsLeft, int minRoleWeight, int termIndex, List<int> currentCombination, List<List<int>> results)
    {
        if (termsLeft == 0)
        {
            if (remainingSum == 0)
            {
                var newCombination = new List<int>(currentCombination);
                newCombination.Sort();
                if (!results.Any(r => r.SequenceEqual(newCombination)))
                {
                    results.Add(newCombination);
                }
            }
            return;
        }

        for (int i = minRoleWeight; i <= 3; i++)
        {
            if (i <= remainingSum && i <= 3)
            {
                currentCombination[termIndex] = i;
                Decompose(remainingSum - i, termsLeft - 1, minRoleWeight, termIndex + 1, currentCombination, results);
            }
        }
    }
    
    public List<Role> CalculateRoles(int deckWeight, int termsAmount, int minRoleWeight, List<Role> roles)
    {
        List<int> currentCombination = GetWeight(deckWeight, termsAmount, minRoleWeight)[Random.Range(0, GetWeight(deckWeight, termsAmount, minRoleWeight).Count)];
        List<Role> chosenRoles = new List<Role>();
        roles.Shuffle();
        
        for (int i = 0; i < roles.Count; i++)
        {
            if (chosenRoles.Count < termsAmount)
            {
                for (int j = 0; j < currentCombination.Count; j++)
                {
                    if (roles[i]._cardWeight == currentCombination[j])
                    {
                        chosenRoles.Add(roles[i]);
                        currentCombination.RemoveAt(j);
                        for (int z = 0; z < substituteRoles.Count; z++)
                        {
                            if (roles[i]._cardName == substituteRoles[z]._cardName)
                            {
                                substituteRoles.RemoveAt(z);
                            }
                        }
                        break;
                    }
                }
            }
        } 
       return chosenRoles;
    }
    
    public void SetRoundComplexity()
    {
        UpdateCalculatedRoles();
        int[] currentComplexityTemplate;
        if (Random.value > 0.5)
        {
            //currentComplexityTemplate = easyComlexityTemplates[ComplexityLevel];
            currentComplexityTemplate = hardComlexityTemplates[ComplexityLevel];
        }
        else
        {
            currentComplexityTemplate = hardComlexityTemplates[ComplexityLevel];
        }
        
        roleDirector.createdVillagersRoles = CalculateRoles(CalculateDeckWeight(currentComplexityTemplate[0]), currentComplexityTemplate[0], 0, villagerRoles);
        roleDirector.createdOutcastsRoles = CalculateRoles(2, currentComplexityTemplate[1], 1, outcastRoles);
        roleDirector.createdEvilsRoles = CalculateRoles(CalculateDeckWeight(currentComplexityTemplate[2]), currentComplexityTemplate[2], 1, evilRoles);
        roleDirector.createdSubstituteRoles = CalculateRoles(CalculateDeckWeight(currentComplexityTemplate[2]), currentComplexityTemplate[2], 0, substituteRoles);
        
        cardPool._cardAmount = 5 + ComplexityLevel;
        cardPool.Get(5 + ComplexityLevel);
    }
}
