using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerBasics : NetworkManager {
  int playerCount = 0;

  public override void OnServerAddPlayer(NetworkConnection conn) {
    base.OnServerAddPlayer(conn);
    playerCount++;

    var player = conn.identity.GetComponent<MyNetworkPlayer>();
    player.SetDisplayName($"Player {playerCount}");
    player.SetColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
  }
  public override void OnServerDisconnect(NetworkConnection conn) {
    base.OnServerDisconnect(conn);
    playerCount--;
  }
}
