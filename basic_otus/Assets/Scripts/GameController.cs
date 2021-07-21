using System;
using Assets.Scripts.CustomYieldInstructions;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;
using UnityEditor;

public class GameController : MonoBehaviour
{
    [SerializeField] private CharacterComponent[] playerCharacters;
    [SerializeField] private CharacterComponent[] enemyCharacters;
    public ButtonAttack buttonAttack;

    private Coroutine gameLoop;
    private int countEnemy = 0;
    public event Action PlayerDied; 
    public event Action EnemyDied;

    private void Start()
    {
        gameLoop = StartCoroutine(GameLoop());
    }
    private IEnumerator GameLoop()
    {
        Coroutine turn = StartCoroutine(Turn(playerCharacters, enemyCharacters));

        yield return new WaitUntil(() =>
        playerCharacters.FirstOrDefault(c => !c.HealthComponent.IsDead) == null ||
        enemyCharacters.FirstOrDefault(c => !c.HealthComponent.IsDead) == null);

        StopCoroutine(turn);
        GameOver();
    }

    private CharacterComponent GetTarget(CharacterComponent[] characterComponents)
    {
        return characterComponents.FirstOrDefault(c => !c.HealthComponent.IsDead);
    }

    private CharacterComponent GetTargetEnemy()
    {
        enemyCharacters[countEnemy].GetComponentInChildren<Outline>().enabled = true;
        return enemyCharacters[countEnemy];
    }

    private void GameOver()
    {
        bool isPlayerCharacherAlive = false;
        bool isEnemyCharacherAlive = false;

        bool isVictory;

        for (int i = 0; i < playerCharacters.Length; i++)
        {
            if (!playerCharacters[i].HealthComponent.IsDead)
            {
                isPlayerCharacherAlive = true;
                PlayerDied();
            }
        }

        for (int i = 0; i < enemyCharacters.Length; i++)
        {
            if (!enemyCharacters[i].HealthComponent.IsDead)
            {
                isEnemyCharacherAlive = true;
                EnemyDied();
            }
        }

        isVictory = isPlayerCharacherAlive && !isEnemyCharacherAlive;
        
    }

    private IEnumerator Turn(CharacterComponent[] playerCharacters, CharacterComponent[] enemyCharacters)
    {
        int turnCounter = 0;
        while (true)
        {
            for (int i = 0; i < playerCharacters.Length; i++)
            {
                if(playerCharacters[i].HealthComponent.IsDead)
                {
                    continue;
                }
                yield return null;
                
                //TODO: hotfix
                yield return new WaitUntilCharacterAttack(buttonAttack);
                playerCharacters[i].SetTarget(GetTargetEnemy().HealthComponent);// ugly fix need to investigate
                playerCharacters[i].StartTurn();
                yield return new WaitUntilCharacterTurn(playerCharacters[i]);
            }

            yield return new WaitForSeconds(.5f);

            for (int i = 0; i < enemyCharacters.Length; i++)
            {
                if (enemyCharacters[i].HealthComponent.IsDead)
                {
                    continue;
                }
                enemyCharacters[i].SetTarget(GetTarget(playerCharacters).HealthComponent);
                enemyCharacters[i].StartTurn();
                yield return new WaitUntilCharacterTurn(enemyCharacters[i]);
            }

            yield return new WaitForSeconds(.5f);

            turnCounter++;
        }
    }

    public void ToggleOutline()
    {
        countEnemy++;
        if (countEnemy == enemyCharacters.Length) countEnemy = 0;
        Illumination();
    }

    private void Illumination()
    {
        if (countEnemy > 0)
        {
            enemyCharacters[countEnemy-1].GetComponentInChildren<Outline>().enabled = false;
        }
        else if (countEnemy == 0)
        {
            enemyCharacters[enemyCharacters.Length-1].GetComponentInChildren<Outline>().enabled = false;
        }
        enemyCharacters[countEnemy].GetComponentInChildren<Outline>().enabled = true;
    }

    private void Update()
    {
        if (enemyCharacters[countEnemy].HealthComponent.IsDead) buttonAttack.IsAttack = false;
        else buttonAttack.IsAttack = true;
    }
}
