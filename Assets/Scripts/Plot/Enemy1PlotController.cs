using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1PlotController : MonoBehaviour
{
    private NPCMovement NPCMovement;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;


    private void Awake()
    {
        NPCMovement = GetComponent<NPCMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
    }

    private void OnEnable()
    {
        EventHandler.DragonAppear += OnDragonAppear;
        EventHandler.DragonRush += OnDragonRush;
    }

    private void OnDisable()
    {
        EventHandler.DragonAppear -= OnDragonAppear;
        EventHandler.DragonRush -= OnDragonRush;
    }


    private void OnDragonAppear()
    {
        spriteRenderer.enabled = true;
        StartCoroutine(DragonAppear());
    }

    private void OnDragonRush()
    {
        StartCoroutine(DragonRush());
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

    private IEnumerator DragonRush()
    {
        boxCollider2D.enabled = true;
        NPCMovement.moveTime = 0.1f;
        yield return new WaitForSeconds(0.5f);
        NPCMovement.startGridPosition = new Vector3Int(-17,-24);
        NPCMovement.targetGridPosition = new Vector3Int(-17,-20);
        StartCoroutine(NPCMovement.Movement());
        yield return new WaitUntil(() => NPCMovement.moveToTarget);
        GamePlotManager.Instance.dragonRush = true;
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
    }
}
