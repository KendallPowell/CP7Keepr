import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ProfilesService {
  async getProfile(id) {
    const res = await api.get(`api/profiles/${id}`)
    logger.log('Active Profile', AppState.activeProfile)
    AppState.activeProfile = res.data
  }
  async getProfileKeeps(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    logger.log('getting profile keeps', AppState.profileKeeps)
    AppState.profileKeeps = res.data
  }
  async getProfileVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    logger.log('getting profile vaults', AppState.profileVaults)
    AppState.profileVaults = res.data
  }

}

export const profilesService = new ProfilesService()