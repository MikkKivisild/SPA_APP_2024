<template>
    <div class="about">
        <h1>Peoplelist</h1>
        <div class="peoplelist">
            <DataTable :value="peoples" v-if="peoples.length > 0">
                <Column field="id" header="Inimese id" style="color: black;" />
                <Column field="name" header="Inimese nimi" style="color: black;" />
                <Column field="A" header="AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" style="color: black;" />
            </DataTable>
            <div v-else>Inimesed puuduvad</div>
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

    defineProps<{ name : String }>();
    const peoplesStore = usePeoplesStore();
    const { peoples } = storeToRefs(peoplesStore);

    onMounted(async () => {
        peoplesStore.load();
    });
</script>
<style>

</style>