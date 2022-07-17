<script setup lang="ts">
import { usePersonStore } from '../stores/personStore';
import { type Ref, ref } from 'vue';
import type IPerson from '../domain/IPerson';

const personStore = usePersonStore();

const firstName: Ref<string> = ref("");
const lastName: Ref<string> = ref("");
const personalIdentificationCode: Ref<number> = ref(0);
const notes: Ref<string | undefined> = ref("");

const props = defineProps({
    eventId: String
});

async function createPersonParticipation(): Promise<void> {
    const personResponse: IPerson = await personStore.addPerson(
        {
            firstName: firstName.value,
            lastName: lastName.value,
            personalIdentificationCode: personalIdentificationCode.value,
            notes: notes.value
        });

    console.log(personResponse.id);
}

</script>

<template>
<div>
        <div class="mb-3">
            <label class="form-label">Eesnimi</label>
            <input v-model="firstName" type="text" class="form-control">
        </div>
        <div class="mb-3">
            <label class="form-label">Perekonnanimi</label>
            <input v-model="lastName" type="text" class="form-control">
        </div>
        <div class="mb-3">
            <label class="form-label">Isikukood</label>
            <input v-model="personalIdentificationCode" type="text" class="form-control">
        </div>
        <div class="mb-3">
            <label class="form-label">Märkmed</label>
            <input v-model="notes" type="text" class="form-control">
        </div>

        <button v-on:click="createPersonParticipation()" type="button" class="btn btn-outline-primary">Salvesta</button>
    
    
</div>
</template>