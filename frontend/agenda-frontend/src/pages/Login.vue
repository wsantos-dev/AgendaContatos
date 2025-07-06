<template>
  <div class="flex justify-center items-center bg-gray-100">
    <div class="p-6 w-full max-w-sm bg-white rounded-xl shadow-md">
      <h2 class="text-xl font-semibold mb-4 text-center">Login</h2>
      <form @submit.prevent="login">
        <div class="mb-3">
          <label class="block mb-1">Email</label>
          <InputText v-model="email" class="w-full" />
        </div>
        <div class="mb-4">
          <label class="block mb-1">Senha</label>
          <Password v-model="senha" toggleMask class="w-full" />
        </div>
        <Button label="Entrar" type="submit" class="w-full" />
      </form>
    </div>
    <Toast />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useToast } from 'primevue/usetoast'
import InputText from 'primevue/inputtext'
import Password from 'primevue/password'
import Button from 'primevue/button'
import Toast from 'primevue/toast'
import authService from '../services/auth'

const email = ref('')
const senha = ref('')
const router = useRouter()
const toast = useToast()

const login = async () => {
  try {
    await authService.login(email.value, senha.value)
    router.push('/contatos')
  } catch (error) {
    toast.add({ severity: 'error', summary: 'Erro', detail: 'Credenciais inválidas', life: 3000 })
  }
}
</script>
