import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class KeepsService {

  async getKeeps() {
    const res = await api.get('/api/keeps')
    logger.log(res.data)
    // AppState.keeps = res.data
    // .map(k => new Keep(k))
  }

}

export const keepsService = new KeepsService()