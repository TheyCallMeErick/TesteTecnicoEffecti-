<template>
  <div class="main-layout">
    <header class="header">
      <h1>Teste Técnico - Effecti</h1>
      <nav>
        <ul>
          <li><router-link to="/">Home</router-link></li>
          <li><router-link to="/licitacoes">Licitacoes</router-link></li>
        </ul>
      </nav>
    </header>
    <div class="main-container" >
      <router-view class="main-content"/>
    </div>
  </div>
</template>
<script lang="ts">
export default {
  name: 'MainLayout',
  components: {},
  data() {
    return {};
  },
  mounted() {
  },
};
</script>
<style lang="css" scoped>
.header {
  background-color: #4a90e2;
  color: white;
  padding: 1rem;
  text-align: center;
  display: flex;
  flex-direction: row;
  height: 60px;
  align-items: center;
}
.header h1 {
  margin-right: 2rem;
  font-size: 1.2rem;
}
.header nav ul{
  display: flex;
  flex-direction:row ;
  list-style-type: none;
  gap: 1rem;
}
.header a{
  color: white;
  text-decoration: none;
  font-weight: bold;
  background-color: #357ABD;
  padding: 0.8rem;
  border-radius: 0.3rem;
}
.header a:hover {
  text-decoration: underline;

}
.header nav {
  margin-left: auto;
}
.header h1 {
  margin: 0;
  font-size: 1.5rem;

}
.main-layout {
  width: 100vw;
  height: 100vh;
  overflow-y: hidden;
  overflow-x: hidden;
  display: flex;
  flex-direction: column;
}
.main-container {
  width: 100%;
  height: calc(100vh - 60px);
  overflow-x: hidden;
  overflow-y: auto;

}
.main-content {
  width: 100%;
  padding: 16px;
  overflow-y: auto;
  overflow-x: hidden;

}
</style>
