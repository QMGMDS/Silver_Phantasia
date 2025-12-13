using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1PlotController : MonoBehaviour
{
    private NPCMovement NPCMovement;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        NPCMovement = GetComponent<NPCMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void OnEnable()
    {
        EventHandler.DragonAppear += OnDragonAppear;
    }

    private void OnDisable()
    {
        EventHandler.DragonAppear -= OnDragonAppear;
    }

    private void OnDragonAppear()
    {
        spriteRenderer.enabled = true;
        StartCoroutine(DragonAppear());
    }

    private IEnumerator DragonAppear()
    {
        NPCMovement.startGridPosition = new Vector3Int(-17,-31);
        NPCMovement.targetGridPosition = new Vector3Int(-17,-24);
        NPCMovement.InitNPC();
        StartCoroutine(NPCMovement.Movement());
        yield return new WaitUntil(() => NPCMovement.moveToTarget);
        GamePlotManager.Instance.dragonAppear = true;
    }
}
