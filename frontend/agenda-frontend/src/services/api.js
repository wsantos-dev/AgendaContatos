import axios from 'axios'

const isDocker = window.location.hostname !== 'localhost'

const api = axios.create({
  baseURL: isDocker ? 'http://api:8080/api' : 'http://localhost:5000/api',
})

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

export default api
