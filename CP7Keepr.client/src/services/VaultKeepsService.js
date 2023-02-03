import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async addKeepToVault(body) {
    const res = await api.post('api/vaultKeeps', body)
    return res.data
  }

  async removeKeepFromVault(vaultKeepId) {
    const res = await api.delete('api/vaultKeeps/' + vaultKeepId)
    return res.data
  }
}
export const vaultKeepsService = new VaultKeepsService()