<script setup lang="ts">
import { type Ref, ref } from 'vue';


const name: Ref<string> = ref("");
const code: Ref<number> = ref(0);
const notes: Ref<string | undefined> = ref("");

const props = defineProps({
    eventId: String
});

async function createCompanyParticipation(): Promise<void>{
    console.log(`createCompanyParticipation`);
}

</script>

<template>
<div>
            <div class="mb-3">
            <label class="form-label">Nimi</label>
            <input v-model="name" type="text" class="form-control">
        </div>
        <div class="mb-3">
            <label class="form-label">Registrikood</label>
            <input v-model="code" type="text" class="form-control">
        </div>
        <div class="mb-3">
            <label class="form-label">Märkmed</label>
            <input v-model="notes" type="text" class="form-control">
        </div>

        <button v-on:click="createCompanyParticipation()" type="button" class="btn btn-outline-primary">Salvesta</button>
</div>
</template>