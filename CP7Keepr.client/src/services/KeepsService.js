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

  async createKeep(keepData) {
    const res = await api.post('/api/keeps', keepData)
    logger.log(res.data)
    AppState.keeps.push(res.data)
  }

}

export const keepsService = new KeepsService()