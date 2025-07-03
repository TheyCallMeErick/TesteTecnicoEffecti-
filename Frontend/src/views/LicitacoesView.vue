<template>
  <div class="licitacoes-view">
    <h1 class="text-title">Licitacoes</h1>
    <div class="filtro-container">
      <div class="input-group">
        <label for="uasg" class="input-label">Pesquisar por UASG</label>
        <input id="uasg" type="text" v-model="query.uasg" placeholder="Digite o código UASG" class="input-field"
          @input="debounceFetchLicitacoes" />
      </div>

      <div class="input-group">
        <label for="pregao" class="input-label">Pesquisar por Número de Pregão</label>
        <input id="pregao" type="text" v-model="query.pregao" placeholder="Digite o número do pregão"
          class="input-field" @input="debounceFetchLicitacoes" />
      </div>

      <div class="button-container">
        <button @click="fetchLicitacoes" class="search-button">Buscar Licitações</button>
      </div>
    </div>

    <div v-if="loading" class="text-center">Carregando...</div>
    <div v-if="error" class="error-message">{{ error }}</div>
    <div v-if="error" class="error-message">
      <button @click="migrateDatabase" class="search-button">Migrar o banco e tentar novamente</button>
    </div>

    <div v-if="!loading && !error" class="licitacoes-list">
      <LicitacaoComponent v-if="licitacoes.length > 0" :licitacao="licitacao" v-for="licitacao in licitacoes" :key="licitacao.id" />
      <div v-if="licitacoes.length === 0" >Nenhuma licitação encontrada.</div>
      <button  v-if="licitacoes.length === 0" @click="syncLicitacoes" class="search-button">Buscar licitações</button>
    </div>
    <div v-if="totalItems > 0" class="pagination-container">
      <button @click="previousPage" :disabled="page <= 1" class="pagination-button">
        ← Anterior
      </button>

      <span class="pagination-info">
        Página {{ page }} de {{ totalPages }}
      </span>

      <button @click="nextPage" :disabled="page >= totalPages" class="pagination-button">
        Próximo →
      </button>
    </div>
  </div>
</template>
<script lang="ts">
import type { Licitacao } from '@/types/licitacao';
import { defineComponent } from 'vue';
import LicitacaoComponent from '@/components/LicitacaoComponent.vue';
import { configs } from '@/utils/configs';
export default defineComponent({
  name: 'LicitacoesView',
  components: {
    LicitacaoComponent,
  },
  data() {
    return {
      licitacoes: [] as Licitacao[],
      loading: true,
      error: null as string | null,
      page: 1,
      pageSize: 10,
      totalItems: 0,
      query: {
        uasg: '',
        pregao: '',
        objeto: ''
      },
      debounceTimeout: null as ReturnType<typeof setTimeout> | null,
    };
  },
  computed: {
    totalPages(): number {
      return Math.ceil(this.totalItems / this.pageSize);
    }
  },
  mounted() {
    this.fetchLicitacoes();
  },
  methods: {
    async debounceFetchLicitacoes() {
      if (this.debounceTimeout !== null) {
        clearTimeout(this.debounceTimeout);
      }
      this.debounceTimeout = setTimeout(() => {
        this.fetchLicitacoes();
      }, 500);
    },
    async fetchLicitacoes() {
      this.loading = true;
      this.error = null;
      try {
        const response = await fetch(`http://${configs.apiUrl}:${configs.apiPort}/api/Licitacao?page=${this.page}&pageSize=${this.pageSize}&uasg=${this.query.uasg}&pregao=${this.query.pregao}`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });
        if (!response.ok) {
          throw new Error('Erro ao buscar licitações: ' + response.statusText);
        }
        const data = await response.json();
        this.licitacoes = data.data;
        this.totalItems = data.totalItems;
      } catch (error) {
        if (error instanceof Error) {
          this.error = error.message;
        } else {
          this.error = 'Um erro inesperado ocorreu.';
        }
      } finally {
        this.loading = false;
      }
    },
    async migrateDatabase() {
      try {
        const response = await fetch(`http://${configs.apiUrl}:${configs.apiPort}/api/Licitacao/apply-migrations`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });
        if (!response.ok) {
          throw new Error('Erro ao migrar o banco de dados: ' + response.statusText);
        }
        this.fetchLicitacoes();
      } catch (error) {
        if (error instanceof Error) {
          this.error = error.message;
        } else {
          this.error = 'Um erro inesperado ocorreu.';
        }
      }
    },
    async syncLicitacoes() {
      try {
       this.loading = true;

        const response = await fetch(`http://${configs.apiUrl}:${configs.apiPort}/api/Licitacao/sync`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });
        if (!response.ok) {
          throw new Error('Erro ao sincronizar licitações: ' + response.statusText);
        }
        this.fetchLicitacoes();
      } catch (error) {
        if (error instanceof Error) {
          this.error = error.message;
        } else {
          this.error = 'Um erro inesperado ocorreu.';
        }
      }finally {
        this.loading = false;
      }

    },
    previousPage() {
      if (this.page > 1) {
        this.page--;
        this.fetchLicitacoes();
      }
    },
    nextPage() {
      if (this.page < this.totalPages) {
        this.page++;
        this.fetchLicitacoes();
      }
    }
  },
})
</script>
<style scoped>
.error-message {
  color: red;
  font-weight: bold;
  text-align: center;
}

.text-title {
  font-size: 2rem;
  font-weight: bold;
  margin-bottom: 1rem;
  text-align: center;
}

.licitacoes-list {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.filtro-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  align-items: flex-end;
  gap: 1rem;
  margin-bottom: 1.5rem;
  width: 90%;
  padding: 8px;
}

.input-group {
  display: flex;
  flex-direction: column;
  width: 100%;
  max-width: 300px;
}

.input-label {
  font-weight: 600;
  color: #1f2937;
  margin-bottom: 0.25rem;
}

.input-field {
  padding: 0.5rem 0.75rem;
  border: 1px solid #cbd5e1;
  border-radius: 0.5rem;
  background-color: #ffffff;
  font-size: 1rem;
  transition: border-color 0.2s ease;
}

.input-field:focus {
  outline: none;
  border-color: #3b82f6;
  box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.2);
}

.button-container {
  margin-left: auto;
  margin-right: 2%;
}

.search-button {
  background-color: #3b82f6;
  color: white;
  padding: 0.6rem 1.25rem;
  font-size: 1rem;
  border: none;
  border-radius: 0.5rem;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.search-button:hover {
  background-color: #2563eb;
}

.pagination-container {
  margin-top: 1.5rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 60%;
  align-self: center;
  padding: 1rem;
  background-color: #ffffff;
  border-radius: 8px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
}

.pagination-button {
  padding: 0.5rem 1rem;
  background-color: #2563eb;
  color: #ffffff;
  border: none;
  border-radius: 6px;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.2s ease;
}

.pagination-button:hover:not(:disabled) {
  background-color: #1d4ed8;
}

.pagination-button:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.pagination-info {
  font-weight: 500;
  color: #374151;
}
.licitacoes-view {
  padding: 2rem;
  background-color: #f9fafb;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>
