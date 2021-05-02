using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour {

  [SerializeField] NavMeshAgent agent;

  Camera mainCamera;

  #region Server
  [Command]
  void CmdMove(Vector3 position) {
    // make sure this is a valid position
    if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) return;

    agent.SetDestination(hit.position);
  }
  #endregion


  #region Client

  // START for owner of this instance
  public override void OnStartAuthority() {
    mainCamera = Camera.main;
  }
  [ClientCallback]
  private void Update() {
    // only Update on your own instance
    if (!hasAuthority) return;

    if (!Input.GetMouseButtonDown(1)) return;

    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

    // make sure we hit somewhere in the scene
    if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;

    CmdMove(hit.point);
  }
  #endregion



}
