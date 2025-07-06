<template>
  <div class="p-4 container w-screen min-h-screen overflow-x-hidden">
    <Toast />
    <ConfirmDialog />

    <div class="flex justify-between mb-4">
      <h2>Lista de Contatos</h2>
      <Button label="Novo Contato" icon="pi pi-plus" @click="showDialog()" />
    </div>

    <DataTable
      :value="contatos"
      :paginator="true"
      :rows="5"
      :rowsPerPageOptions="[5, 10, 20]"
      :filters="filters"
      filter-display="menu"
      responsiveLayout="scroll"
      :loading="loading"
      :globalFilterFields="['nome', 'email', 'telefone']"
      @filter="onFilter"
      class="w-full mb-4"
    >
      <template #header>
        <div class="flex justify-end">
          <span class="p-input-icon-left">
            <i class="pi pi-search" />
            <InputText v-model="globalFilter" placeholder="Pesquisar..." />
          </span>
        </div>
      </template>

      <Column field="nome" header="Nome" sortable />
      <Column field="email" header="Email" sortable />
      <Column field="telefone" header="Telefone" sortable />

      <Column header="Ações" style="width: 140px">
        <template #body="slotProps">
          <Button
            icon="pi pi-pencil"
            class="p-button-rounded p-button-text"
            @click="showDialog(slotProps.data)"
          />
          <Button
            icon="pi pi-trash"
            class="p-button-rounded p-button-text p-button-danger"
            @click="deleteContato(slotProps.data)"
          />
        </template>
      </Column>
    </DataTable>

    <Dialog
      header="Contato"
      :visible="dialogVisible"
      :modal="true"
      :closable="false"
      @hide="hideDialog"
      :style="{ width: '400px' }"
    >
      <form @submit.prevent="saveContato" class="space-y-4">
        <div>
          <label for="nome" class="block text-sm font-medium text-gray-700 mb-1">Nome</label>
          <InputText id="nome" v-model="form.nome" class="w-full mb-1" />
        </div>
        <div>
          <label for="email" class="block text-sm font-medium text-gray-700 mb-1">Email</label>
          <InputText id="email" v-model="form.email" type="email" class="w-full mb-1" />
        </div>
        <div>
          <label for="telefone" class="block text-sm font-medium text-gray-700 mb-1"
            >Telefone</label
          >
          <InputText id="telefone" v-model="form.telefone" class="w-full mb-1" />
        </div>

        <div class="flex justify-end gap-2 pt-2">
          <Button label="Cancelar" class="p-button-text" @click="hideDialog" />
          <Button label="Salvar" type="submit" />
        </div>
      </form>
    </Dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { useToast } from 'primevue/usetoast'
import { useConfirm } from 'primevue/useconfirm'

import Toast from 'primevue/toast'
import ConfirmDialog from 'primevue/confirmdialog'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'

import contatoService from '../services/contato'

const contatos = ref([])
const loading = ref(false)
const toast = useToast()
const confirm = useConfirm()

const dialogVisible = ref(false)
const form = reactive({
  id: null,
  nome: '',
  email: '',
  telefone: '',
})

const globalFilter = ref(null)
const filters = reactive({
  global: { value: null, matchMode: 'contains' },
})

function phoneMask(el) {
  function format(value) {
    if (!value) return ''

    value = value.replace(/\D/g, '')

    if (value.length <= 10) {
      // Formato (00) 0000-0000
      value = value.replace(/^(\d{2})(\d{4})(\d{0,4}).*/, '($1) $2-$3')
    } else {
      // Formato (00) 00000-0000
      value = value.replace(/^(\d{2})(\d{5})(\d{0,4}).*/, '($1) $2-$3')
    }
    return value.trim().replace(/[-\s]+$/, '')
  }

  function onInput(e) {
    const el = e.target
    const position = el.selectionStart
    const oldValue = el.value

    el.value = format(el.value)

    if (position < el.value.length) {
      el.selectionStart = el.selectionEnd = position
    }

    el.dispatchEvent(new Event('input'))
  }

  el.addEventListener('input', onInput)

  onBeforeUnmount(() => {
    el.removeEventListener('input', onInput)
  })
}

function extractErrorMessage(error, fallback) {
  if (!error.response || !error.response.data) return fallback

  const data = error.response.data

  if (Array.isArray(data)) return `${fallback}: ${data.join('; ')}`
  if (typeof data === 'string') return `${fallback}: ${data}`
  if (data.message) return `${fallback}: ${data.message}`

  return fallback
}

function onFilter(e) {
  filters.global.value = e.filters['global']?.value ?? null
}

async function loadContatos() {
  loading.value = true
  try {
    contatos.value = await contatoService.getAll()
  } catch (error) {
    const detail = extractErrorMessage(error, 'Erro ao carregar contatos')
    toast.add({ severity: 'error', summary: 'Erro', detail })
  } finally {
    loading.value = false
  }
}

function showDialog(contato) {
  if (contato) {
    form.id = contato.id
    form.nome = contato.nome
    form.email = contato.email
    form.telefone = contato.telefone
  } else {
    form.id = null
    form.nome = ''
    form.email = ''
    form.telefone = ''
  }
  dialogVisible.value = true
}

function hideDialog() {
  dialogVisible.value = false
}

async function saveContato() {
  try {
    if (form.id) {
      await contatoService.update(form.id, {
        nome: form.nome,
        email: form.email,
        telefone: form.telefone,
      })
      toast.add({ severity: 'success', summary: 'Sucesso', detail: 'Contato atualizado' })
    } else {
      await contatoService.create({
        nome: form.nome,
        email: form.email,
        telefone: form.telefone,
      })
      toast.add({ severity: 'success', summary: 'Sucesso', detail: 'Contato criado' })
    }

    dialogVisible.value = false
    loadContatos()
  } catch (error) {
    const detail = extractErrorMessage(error, 'Erro ao salvar contato')
    toast.add({ severity: 'error', summary: 'Erro', detail })
  }
}

function deleteContato(contato) {
  confirm.require({
    message: `Deseja deletar o contato ${contato.nome}?`,
    header: 'Confirmação',
    icon: 'pi pi-exclamation-triangle',
    acceptLabel: 'Sim',
    rejectLabel: 'Não',
    accept: async () => {
      try {
        await contatoService.delete(contato.id)
        toast.add({ severity: 'success', summary: 'Sucesso', detail: 'Contato deletado' })
        loadContatos()
      } catch (error) {
        const detail = extractErrorMessage(error, 'Erro ao deletar contato')
        toast.add({ severity: 'error', summary: 'Erro', detail })
      }
    },
  })
}

onMounted(loadContatos)
</script>

<style scoped>
.actions {
  text-align: center;
}

.contato-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

.btn-novo {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 0.6rem 1rem;
  border-radius: 5px;
  font-weight: bold;
  cursor: pointer;
}

.btn-novo:hover {
  background-color: #0056b3;
}
</style>
