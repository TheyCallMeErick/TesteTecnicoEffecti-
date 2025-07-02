<template>
  <div @click="toggle" class="licitacao-card border-slate-200 border">
    <div class="licitacao-card__info">
      <div class="licitacao-summary">
        <p><span class="label">UASG:</span> {{ licitacao.uasg }}</p>
        <p><span class="label">Pregão:</span> {{ licitacao.pregao }}</p>
        <p><span class="label">Objeto:</span> {{ licitacao.objeto }}</p>
        <p><span class="label">Órgão:</span> {{ licitacao.orgao }}</p>
        <p><span class="label">Instituição:</span> {{ licitacao.instituicao }}</p>
        <p><span class="label">Data edital:</span> {{ formatDate(licitacao.dataDisponibilizacaoEdital) }} {{ licitacao.horaInicioEdital }} - {{ licitacao.horaFimEdital }}</p>
      </div>

      <div v-if="open" class="licitacao-card__details">
        <h2 class="licitacao-card__title">Itens da Licitação</h2>
        <hr class="licitacao-card__divider" />
        <ul class="licitacao-card__items">
          <li v-for="item in licitacao.itens" :key="item.id" class="licitacao-card__item">
            <p><strong>Nome:</strong> {{ item.nome }}</p>
            <p><strong>Descrição:</strong> {{ item.descricao }}</p>
            <p><strong>Qtd:</strong> {{ item.quantidade }} {{ item.unidadeFornecimento }}</p>
            <p><strong>Tratamento diferenciado:</strong> {{ item.tratamentoDiferenciado }}</p>
            <p><strong>Aplicabilidade 7174:</strong> {{ item.aplicabilidade7174 }}</p>
            <p><strong>Margem de preferência:</strong> {{ item.aplicabilidadeMargemPreferencia }}</p>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  props: {
    licitacao: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      open: false
    };
  },
  methods: {
    toggle() {
      this.open = !this.open;
    },
    formatDate(dateStr:string) {
      if (!dateStr) return 'N/A';
      const date = new Date(dateStr);
      return date.toLocaleDateString('pt-BR');
    }
  }
});
</script>

<style scoped>
.licitacao-card {
  display: flex;
  flex-direction: column;
  width: 100%;
  cursor: pointer;
  background-color: #ffffff;
  padding: 1.75rem;
  border-radius: 0.75rem;
  border: 1px solid #e2e8f0;
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.06);
  transition: all 0.3s ease;
}

.licitacao-card:hover {
  background-color: #f8fafc;
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.08);
}

.licitacao-summary {
  margin-bottom: 1.25rem;
  color: #1e293b;
  line-height: 1.6;
}

.label {
  font-weight: 700;
  color: #0f172a;
}

strong {
  font-weight: 700;
  color: #0f172a;
}

.licitacao-card__details {
  margin-top: 1.25rem;
  padding-top: 1rem;
  border-top: 1px solid #e2e8f0;
}

strong{
  color: #334155;
  font-weight: 500;
  display: inline-block;
  margin-bottom: 6px;
  margin-right: 0.5rem;
}

.licitacao-card__title {
  font-size: 1.25rem;
  font-weight: 600;
  color: #1e3a8a;
  margin-bottom: 0.75rem;
}

.licitacao-card__divider {
  border: none;
  border-top: 1px solid #cbd5e1;
  margin-bottom: 1rem;
}

.licitacao-card__items {
  display: flex;
  flex-direction: column;
  gap: 1.25rem;
}

.licitacao-card__item {
  background-color: #f1f5f9;
  padding: 1rem;
  border-radius: 0.5rem;
  border: 1px solid #e2e8f0;
  color: #334155;
  transition: background-color 0.3s ease;
}

.licitacao-card__item:hover {
  background-color: #e2e8f0;
}
</style>
