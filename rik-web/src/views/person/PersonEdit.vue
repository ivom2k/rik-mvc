<script setup lang="ts">
import type IPerson from '@/domain/IPerson';
import router from '@/router/router';
import { usePersonStore } from '@/stores/personStore';
import { ref, type Ref } from 'vue';


const personStore = usePersonStore();

let person: IPerson | undefined;
let id: string;
let firstName: Ref<string> = ref("");
let lastName: Ref<string> = ref("");
let personalIdentificationCode: Ref<number> = ref(0);
let notes: Ref<string | undefined> = ref("");

const props = defineProps({
    id: String
});

if (props.id !== undefined) {
    person = personStore.$state.persons.find((p) => p.id === props.id);

    if (person !== undefined) {

        if (person.id !== undefined) {
            id = person.id;
        }

        firstName = ref(person.firstName);
        lastName = ref(person.lastName);
        personalIdentificationCode = ref(person.personalIdentificationCode);
        notes = ref(person.notes);
    }
}

async function updatePerson() {
    await personStore.updatePerson(id,
        {
            id: id,
            firstName: firstName.value,
            lastName: lastName.value,
            personalIdentificationCode: personalIdentificationCode.value,
            notes: notes.value
        });

    router.go(-1);
}

</script>

<template>
    <div class="container">
                <div class="d-flex flex-row">
            <div class="bg-primary bg-gradient text-white flex-grow-1 border border-white rounded-1">
                <h4 class="display-6">Osavõtja info</h4>
            </div>
            <img src="../../img/libled-2.jpg" class="img-fluid border border-white rounded-1">
        </div>

        <h4 type="button" v-on:click="router.go(-1)" class="h4"><i class="bi bi-arrow-left"></i></h4>

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

        <button v-on:click="updatePerson()" type="button" class="btn btn-outline-primary">Uuenda</button>
    </div>
</template>