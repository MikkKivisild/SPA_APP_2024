<template>
    <div class="about">
        <h1>Peoplelist</h1>
        <form @submit.prevent="addPeople">
            <input v-model="newPerson.name" placeholder="Name" required />
            <input v-model="newPerson.age" placeholder="Age" required />
            <button type="submit">Add</button>
        </form>

        <div class="peoplelist">
            <DataTable :value="peoples">
                <Column field="id" header="ID" style="color: black;" />
                <Column field="name" header="Name" style="color: black;" />
                <Column field="age" header="Age" style="color: black;" />
                <Column header="Actions">
                    <template #body="{ data }">
                        <button @click="deletePeople(data.id)">Delete</button>
                        <button @click="editPeople(data)">Edit</button>
                    </template>
                </Column>
            </DataTable>

        <div v-if="editPerson" class="edit-form">
            <h2>Edit people</h2>
            <form @submit.prevent="updatePerson">
                <input v-model="editPerson.name" placeholder="Name" required />
                <input v-model="editPerson.age" placeholder="Age" required />
                <button type="submit">Save</button>
                <button type="button" @click="editPerson = null">Cancel</button>
            </form>
        </div>
    </div>
    </div>
</template>

<script setup lang="ts">
    import { type People } from '@/models/people';
    import { usePeoplesStore } from '@/stores/peoplesStore'
    import { storeToRefs } from "pinia";
    import { defineProps, onMounted, watch, ref } from "vue";
    import { useRoute } from "vue-router";

        const route = useRoute();

    watch(route, (to, from) => {
        if (to.path !== from.path || to.query) {
            peoplesStore.load();
        }
    }, { deep: true });

    defineProps<{ title : String }>();
    const peoplesStore = usePeoplesStore();
    const { peoples } = storeToRefs(peoplesStore);

    onMounted(async () => {
        peoplesStore.load();
    });

</script>
<style>

</style>