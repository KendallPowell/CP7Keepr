import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class KeepsService {

  async getKeeps() {
    const res = await api.get('/api/keeps')
    logger.log(res.data)
    AppState.keeps = res.data
  }

  async getActiveKeep(keepId) {
    const res = await api.get('api/keeps/' + keepId)
    // logger.log("[getting the active keep]", res.data)
    AppState.activeKeep = res.data
    if (AppState.activeKeep.creatorId != AppState.account.id) {
      AppState.activeKeep.views++
    }
  }

  async getVaultKeeps(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    logger.log(res.data)
    AppState.activeVaultKeeps = res.data
  }

  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData)
    logger.log(res.data)
    AppState.keeps.unshift(res.data)
    return res.data
  }

  async deleteKeep(id) {
    const res = await api.delete('api/keeps/' + id)
    logger.log('[remove keep]', res.data)
  }

  async removeKeep(vaultKeepId) {
    const res = await api.delete('api/keeps/' + vaultKeepId)
    logger.log('[Removing Keep]', res.data)
    AppState.activeVaultKeeps = AppState.activeVaultKeeps.filter(k => k.vaultKeepId != vaultKeepId)
  }

}

export const keepsService = new KeepsService()