<script setup lang="ts">
import router from '@/router/router';
import { useEventStore } from '@/stores/eventStore';
import { type Ref, ref } from 'vue';


const eventStore = useEventStore();

const name: Ref<string> = ref("");
const startTime: Ref<string> = ref("");
const location: Ref<string> = ref("");
const notes: Ref<string> = ref("");

async function createEvent(): Promise<void> {    
    await eventStore.createEvent(
        {
            name: name.value,
            startTime: startTime.value,
            location: location.value,
            notes: notes.value
        }
    );

    router.push("/");
}

</script>

<template>
<div class="container">
    <h4 type="button" v-on:click="router.push(`/`)" class="h4"><i class="bi bi-arrow-left"></i></h4>
<h4 class="display-6">Uus üritus</h4>

<div class="mb-3">
    <label class="form-label">Ürituse nimi</label>
    <input v-model="name" type="text" class="form-control">
</div>
<div class="mb-3">
    <label class="form-label">Toimumisaeg</label>
    <input v-model="startTime" type="text" class="form-control">
</div>
<div class="mb-3">
    <label class="form-label">Asukoht</label>
    <input v-model="location" type="text" class="form-control">
</div>
<div class="mb-3">
    <label class="form-label">Lisainfo</label>
    <input v-model="notes" type="text" class="form-control">
    <div v-if="notes.length > 1000" class="small text-danger">Maksimaalselt 1000 tähemärki!</div>
</div>

<button v-if="notes.length > 1000" v-on:click="createEvent()" type="button" class="btn btn-outline-primary disabled">Loo</button>
<button v-else v-on:click="createEvent()" type="button" class="btn btn-outline-primary">Loo</button>

</div>
</template>