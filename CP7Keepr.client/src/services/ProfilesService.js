import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ProfilesService {
  async getProfile(id) {
    const res = await api.get(`api/profiles/${id}`)
    AppState.activeProfile = res.data
    logger.log('Active Profile', AppState.activeProfile)
  }
  async getProfileKeeps(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.profileKeeps = res.data
    logger.log('getting profile keeps', AppState.profileKeeps)
  }
  async getProfileVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.profileVaults = res.data
    logger.log('getting profile vaults', AppState.profileVaults)
  }

}

export const profilesService = new ProfilesService()