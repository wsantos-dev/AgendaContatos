import { createRouter, createWebHistory } from 'vue-router'
import Login from '../pages/Login.vue'
import Contatos from '../pages/Contatos.vue'
import authService from '../services/auth'

const routes = [
  {
    path: '/',
    redirect: '/login',
  },
  {
    path: '/login',
    component: Login,
  },
  {
    path: '/contatos',
    component: Contatos,
    meta: { requiresAuth: true },
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

// Guarda de rota
router.beforeEach((to, from, next) => {
  if (to.meta.requiresAuth && !authService.isAuthenticated()) {
    next('/login')
  } else {
    next()
  }
})

export default router
