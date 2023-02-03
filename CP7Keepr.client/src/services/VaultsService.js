import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class VaultsService {
  async getVaults() {
    const res = await api.get('/api/vaults')
    logger.log(res.data)
    AppState.vaults = res.data
  }

  async getActiveVault(vaultId) {
    const res = await api.get('api/vaults/' + vaultId)
    AppState.activeKeep = res.data
  }

  async makeVault(vaultData) {
    const res = await api.post('api/vaults', vaultData)
    logger.log(res.data)
    AppState.vaults.unshift(res.data)
    return res.data
  }

  async getMyVaults() {
    const res = await api.get('account/vaults')
    logger.log('[get my vaults]', res.data)
    AppState.myVaults = res.data
  }

  async getVaultById(id) {
    const res = await api.get(`api/vaults/${id}`)
    logger.log('[Get Vault By Id]', res.data)
    AppState.activeVault = res.data
  }

  async deleteVault(id) {
    const res = await api.delete('api/vaults/' + id)
    logger.log('[Removing Vault]', res.data)
  }
}

export const vaultsService = new VaultsService