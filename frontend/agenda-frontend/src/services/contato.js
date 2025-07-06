import api from './api'

export default {
  async getAll() {
    const response = await api.get('/ContatoCQRS/ListarTodos')
    return response.data
  },

  async getById(id) {
    const response = await api.get(`/ContatoCQRS/ListarTodos/${id}`)
    return response.data
  },

  async create(contato) {
    const response = await api.post('/ContatoCQRS/CriarContato', contato)
    return response.data
  },

  async update(id, contato) {
    const response = await api.put(`/ContatoCQRS/Atualizar/${id}`, contato)
    return response.data
  },

  async delete(id) {
    await api.delete(`/ContatoCQRS/Excluir/${id}`)
  },
}
