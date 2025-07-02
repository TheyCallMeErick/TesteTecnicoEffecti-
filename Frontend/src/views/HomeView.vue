<script setup lang="ts">
</script>

<template>
  <main>
    <header class="page-header">
      <h1>Teste Técnico – Desenvolvedor Pleno</h1>
      <p>Sistema de Gestão de Propostas para Licitações Públicas</p>
      <router-link class="badge button-center " :to="{ name: 'licitacoes' }">
        👉 Ver licitações buscadas
      </router-link>
    </header>

    <section>
      <h2>📦 Funcionalidades Implementadas</h2>
      <ul>
        <li>API RESTful com C# (.Net)</li>
        <li>Captura de licitações do <code>comprasnet.gov.br</code></li>
        <li>Filtros por <strong>UASG</strong> e <strong>Número do Pregão</strong></li>
        <li>Armazenamento com banco relacional</li>
        <li>Frontend com Vue.js para exibição e filtro</li>
        <li>Marcar licitações como lidas/não lidas</li>
      </ul>
    </section>

    <section>
      <h2>🚀 Como executar</h2>
      <p>Confira o <code>README.md</code> no repositório para instruções completas.</p>
    </section>

    <section>
      <h2>📈 Melhorias propostas para desempenho</h2>
      <ul>
        <li>Paginação dos dados na API para reduzir carga e latência</li>
        <li>Indexação no banco de dados pelos campos filtráveis</li>
      </ul>
    </section>

    <section>
      <h2>📁 Repositório</h2>
      <p>
        <a href="https://github.com/TheyCallMeErick/TesteTecnicoEffecti-" target="_blank" class="link">
          🔗 https://github.com/TheyCallMeErick/TesteTecnicoEffecti-
        </a>
      </p>
    </section>

    <section>
      <h2>🕐 Tempo gasto estimado</h2>
      <p>30/06/2025 - 2 horas e 30 minutos</p>
      <p>01/07/2025 - 5 horas</p>
      <p>02/07/2025 - 3 horas</p>

      <p>Aproximadamente 10 horas e 30 minutos no total, divididas entre backend, frontend, testes e documentação.</p>
    </section>
  </main>
</template>

<style scoped>
main {
  max-width: 900px;
  margin: 2rem auto;
  padding: 1.5rem;
  background-color: #fff;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  color: #2c3e50;
}

.page-header {
  border-bottom: 2px solid #e1e4e8;
  padding-bottom: 1rem;
  margin-bottom: 2rem;
  display: flex;
  flex-direction: column;
}

h1 {
  font-size: 2rem;
  font-weight: bold;
  margin-bottom: 0.5rem;
}

h2 {
  font-size: 1.4rem;
  color: #34495e;
  margin-bottom: 0.8rem;
  border-left: 4px solid #3498db;
  padding-left: 0.5rem;
}

p {
  line-height: 1.6;
  color: #444;
}

ul {
  margin-left: 1.5rem;
  list-style-type: disc;
  color: #555;
}

code {
  background-color: #f3f4f6;
  padding: 0.2rem 0.5rem;
  border-radius: 4px;
  font-family: monospace;
  font-size: 0.95rem;
  color: #d63384;
}

.button-center {
  display: inline-block;
  text-align: center;
  margin-top: 1rem;
  align-self: center;
  text-decoration: none;
}

.badge {
  display: inline-block;
  background: #3498db;
  color: white;
  padding: 12px 24px;
  border-radius: 6px;
  font-size: 0.95rem;
  margin-top: 0.5rem;
  text-decoration: none;
  transition: background-color 0.3s ease;
}

.badge:hover {
  background-color: #2a7bbd;
}

.link {
  color: #2c3e50;
  font-weight: 500;
  text-decoration: none;
  transition: color 0.2s ease;
}

.link:hover {
  color: #3498db;
  text-decoration: underline;
}
</style>
