using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollTavern : MonoBehaviour, ITappable {
    public void OnTap(TapEventArgs args) {
        if(CoinManager.Instance.CurrentCoins >= TavernManager.Instance.RerollCost) {
            AudioManager.Instance.Play(EAudioId.REROLL_TAVERN);
            this.StartCoroutine(CoinManager.Instance.DeductCoins(TavernManager.Instance.RerollCost));
            TavernManager.Instance.Reroll();
        }
        else
            AudioManager.Instance.Play(EAudioId.NOT_ENOUGH_COINS);
    }
}
