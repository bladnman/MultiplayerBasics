using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour {

  [SerializeField] TMP_Text displayNameLabel;
  [SerializeField] Renderer displayColorRenderer;

  [SyncVar(hook = nameof(HandleNameUpdated))]
  [SerializeField]
  private string displayName = "Missing Name";

  [SyncVar(hook = nameof(HandleColorUpdated))]
  [SerializeField]
  private Color color = Color.white;

  # region Server
  [Server] // prevents clients from calling (not abs, needed)
  public void SetDisplayName(string displayName) {
    this.displayName = displayName;
  }

  [Server]
  public void SetColor(Color color) {
    this.color = color;
  }

  [Command]
  void CmdSetDisplayName(string displayName) {
    SetDisplayName(displayName);
    RpcLogDisplayName();
  }
  # endregion

  #region Client

  void HandleNameUpdated(string oldName, string newName) {
    displayNameLabel.text = newName;
  }
  void HandleColorUpdated(Color oldColor, Color newColor) {
    displayColorRenderer.material.SetColor("_BaseColor", newColor);
  }
  [ContextMenu("Set My Name")]
  void SetMyName() {
    CmdSetDisplayName("My New Name");
  }
  [ClientRpc]
  void RpcLogDisplayName() {
    Debug.Log($"M@ [{GetType()}] {displayName} : displayName");   // M@: 
  }
  #endregion

}
