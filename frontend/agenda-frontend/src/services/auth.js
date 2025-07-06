import api from './api'

export default {
  async login(email, senha) {
    const response = await api.post('/auth/login', { email, senha })
    const token = response.data.token
    localStorage.setItem('token', token)
  },

  logout() {
    localStorage.removeItem('token')
  },

  isAuthenticated() {
    return !!localStorage.getItem('token')
  },
}
