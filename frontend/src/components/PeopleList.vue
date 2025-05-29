<template>
  <div class="about">
    <h1>People List</h1>
    <form @submit.prevent="addPerson">
      <input v-model="newPerson.name" placeholder="Name" required />
      <input v-model="newPerson.age" placeholder="Age" required />
      <button type="submit">Add</button>
    </form>

    <DataTable :value="persons">
      <Column field="id" header="ID" />
      <Column field="name" header="Name" />
      <Column field="age" header="Age" />
      <Column header="Actions">
        <template #body="{ data }">
          <button @click="deletePerson(data.id)">Delete</button>
          <button @click="editPerson(data)">Edit</button>
        </template>
      </Column>
    </DataTable>

    <div v-if="editingPerson" class="edit-form">
      <h2>Edit Person</h2>
      <form @submit.prevent="updatePerson">
        <input v-model="editingPerson.name" placeholder="Name" required />
        <input v-model="editingPerson.age" placeholder="Age" required />
        <button type="submit">Save</button>
        <button type="button" @click="editingPerson = null">Cancel</button>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
    import { type People } from '@/models/people';
    import { usePeoplesStore } from '@/stores/peoplesStore'
    import { storeToRefs } from "pinia";
    import { defineProps, onMounted, watch, ref } from "vue";
    import { useRoute } from "vue-router";

    defineProps<{ title: String }>();
    const peoplesStore = usePeoplesStore();
    const { peoples } = storeToRefs(peoplesStore);

    interface People {
        id: number;
        name: string;
        age: string;
    }

    const persons = ref<People[]>([
        { id: 1, name: "Nimi", age: "21"}
    ]);

    const newPerson = ref<Omit<People, 'id'>>({
        name: "",
        age: ""
    });

    const editingPerson = ref<People | null>(null);

    const addPerson = () => {
        persons.value.push({
            id: persons.value.length + 1,
            ...newPerson.value
        });
        newPerson.value = { name: "", age: ""};
        peoplesStore.addPeople(newPerson);
    };

    const deletePerson = (id: number) => {
        persons.value = persons.value.filter(p => p.id !== id);
        peoplesStore.deletePeople(newPerson);
    };

    const editPerson = (people: People) => {
        editingPerson.value = { ...people };
        peoplesStore.updatePeople(newPerson);
    };

    const updatePerson = () => {
        if (!editingPerson.value) return;
        const index = persons.value.findIndex(p => p.id === editingPerson.value?.id);
        if (index !== -1) {
            persons.value[index] = { ...editingPerson.value };
        }
        editingPerson.value = null;
    };

</script>
