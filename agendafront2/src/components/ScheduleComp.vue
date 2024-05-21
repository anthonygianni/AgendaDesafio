<template>
  <div class="schedule">
    <h1>{{ msg }}</h1>
    <div class="container">
      <div class="input-group">
        <div class="input-wrapper">
          <label for="username">Nome </label>
          <InputText id="username" v-model="newItem.nome" aria-describedby="username-help" />
          <!-- <small id="username-help">Enter your username to reset your password.</small> -->
        </div>
        <div class="input-wrapper">
          <label for="username">Email </label>
          <InputText id="username" v-model="newItem.email" aria-describedby="username-help" />
          <!-- <small id="username-help">Enter your username to reset your password.</small> -->
        </div>
        <div class="input-wrapper">
          <label for="username">Telefone </label>
          <InputMask id="basic" v-model="newItem.telefone" mask="99-99999-9999" placeholder="99-99999-9999" />
        </div>
        <div class="input-button">
          <Button @click="addItem" label="Salvar" />
        </div>
      </div>
    </div>
    <div>
      <DataTable :value="items" v-model:selection="selectedProducts" editMode="row" dataKey="id" :paginator="true"
        :rows="10" :filters="filters"
        paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
        :rowsPerPageOptions="[5, 10, 25]"
        currentPageReportTemplate="Showing {first} to {last} of {totalRecords} products">
        <template #header>

        </template>
        <Column field="id" header="ID"></Column>
        <Column field="nome" header="Nome"></Column>
        <Column field="email" header="Email"></Column>
        <Column field="telefone" mask="99-99999-9999" header="Telefone"></Column>
        <Column :exportable="false" style="min-width:8rem">
          <template #body="slotProps">
            <Button icon="pi pi-pencil" outlined rounded class="mr-2" @click="showModalEditItem(slotProps.data)" />
            <Button icon="pi pi-trash" outlined rounded severity="danger" @click="deleteItem(slotProps.data)" />
          </template>
        </Column>
      </DataTable>

      <Dialog v-model:visible="visible" modal header="Edit Profile" :style="{ width: '25rem' }">
        <span class="p-text-secondary block mb-5">Update your information.</span>
        <div class="flex align-items-center gap-3 mb-3">
          <label for="username" class="font-semibold w-6rem">Username</label>
          <InputText id="username" v-model="products.nome" class="flex-auto" autocomplete="off" />
        </div>
        <div class="flex align-items-center gap-3 mb-5">
          <label for="email" class="font-semibold w-6rem">Email</label>
          <InputText id="email" v-model="products.email" class="flex-auto" autocomplete="off" />
        </div>
        <div class="flex align-items-center gap-3 mb-5">
          <label for="telefone" class="font-semibold w-6rem">Telefone</label>
          <InputText id="telefone" v-model="products.telefone" class="flex-auto" autocomplete="off" />
        </div>
        <div class="flex justify-content-end gap-2">
          <Button type="button" label="Cancel" severity="secondary" @click="showModalEditItem()"></Button>
          <Button type="button" label="Save" @click="editItem()"></Button>
        </div>
      </Dialog>
    </div>
  </div>

</template>

<script>
import 'primevue/resources/themes/aura-light-green/theme.css'
import 'primevue/resources/primevue.min.css';
import 'primeicons/primeicons.css'
import Button from 'primevue/button';
import InputMask from 'primevue/inputmask';
import InputText from 'primevue/inputtext';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import axios from 'axios';
import Dialog from 'primevue/dialog';


export default {
  data() {
    return {
      items: [],
      visible: false,
      products: {
        nome: '',
        email: '',
        telefone: '',
      },
      newItem: {
        nome: '',
        email: '',
        telefone: '',
      },
    };
  },
  mounted() {
    this.getAllItems();
  },
  methods: {
    async getAllItems() {
      try {
        const response = await axios.get(`http://api-app:5000/api/Agenda`);
        this.items = response.data.value;
        console.log(response.data.value);
      } catch (error) {
        console.error('Erro ao buscar os itens:', error);
      }

    },
    async showModalEditItem(data) {

      this.visible = true;
      this.products = { ...data };
      this.visible = true;

    },
    async addItem() {

      const item = {
        nome: this.newItem.nome,
        email: this.newItem.email,
        telefone: this.newItem.telefone.replace(/\D/g, ''),
      };

      try {

        const response = await axios.post(`http://api-app:5000/api/Agenda/`, item);
        this.items.push(response.data);
        this.newItem = {};

        console.log('Item adicionado!');
        this.getAllItems();

      } catch (error) {
        console.error('Erro ao adicionar o item:', error);
      }
    },
    async editItem() {

      const item = {
        id: this.products.id,
        nome: this.products.nome,
        email: this.products.email,
        telefone: this.products.telefone.replace(/\D/g, ''),
      };

      try {
        const response = await axios.put(`http://api-app:5000/api/Agenda/${this.products.id}`, item);
        console.log('Item atualizado:', response.data);
        this.getAllItems();
      } catch (error) {
        console.error('Erro ao atualizar o item:', error);
      }
      this.visible = false;
    },
    async deleteItem(item) {

      try {
        const response = await axios.delete(`http://api-app:5000/api/Agenda/${item.id}`);
        this.items = this.items.filter(i => i.id !== item.id);
        console.log("Item deletado com sucesso:", response.json());

      } catch (error) {
        console.error('Erro ao deletar o item:', error);

      }
    }

  },
  name: 'ScheduleComp',
  components: {
    InputMask,
    InputText, Column,
    DataTable,
    Button,
    Dialog
  },
  props: {
    msg: String
  }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}

.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
  background-color: #f9f9f9;
  border: 1px solid #ddd;
  border-radius: 4px;
}

.input-group {
  display: flex;
  justify-content: space-between;
}

.input-wrapper {
  flex: 1;
  margin-right: 20px;
}

.input-wrapper:last-child {
  margin-right: 0;
}

.input-wrapper label {
  display: block;
  margin-bottom: 8px;
}

.input-wrapper input {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
}
.input-button {
  width: 100%;
  flex: 1;
  padding: 8px;
  box-sizing: border-box;
  margin-right: 20px;
  margin-top: auto;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #42b983;
}
</style>
