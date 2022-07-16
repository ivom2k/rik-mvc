<script setup lang="ts">
import type ICompany from '@/domain/ICompany';
import { useCompanyStore } from '@/stores/companyStore';
import router from '@/router/router';
import { ref, type Ref } from 'vue';


const companystore = useCompanyStore();

let company: ICompany | undefined;
let id: string;
let name: Ref<string> = ref("");
let code: Ref<number> = ref(0);
let notes: Ref<string | undefined> = ref("");

const props = defineProps({
    id: String
});

if (props.id !== undefined) {
    company = companystore.$state.companies.find((c) => c.id === props.id);

    if (company !== undefined) {
        if (company.id !== undefined) {
            id = company.id;
        }
        name = ref(company.name);
        code = ref(company.code);
        notes = ref(company.notes);
    }
}

async function updateCompany() {
    await companystore.updateCompany(id,
    {
        id: id,
        name: name.value,
        code: code.value,
        notes: notes.value
    });

    router.go(-1);
}

</script>

<template>
    <div class="container">

        <h4 type="button" v-on:click="router.go(-1)" class="h4"><i class="bi bi-arrow-left"></i></h4>

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

        <button v-on:click="updateCompany()" type="button" class="btn btn-outline-primary">Uuenda</button>
    </div>
</template>