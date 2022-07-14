<script setup lang="ts">
import { useEventStore } from '@/stores/eventStore';
import { useParticipationStore } from '@/stores/participationStore';
import { fixStartTimeFormat, isICompany, isIPerson } from '@/helpers/helpers';
import router from '@/router/router';
import { usePersonStore } from '@/stores/personStore';
import { useCompanyStore } from '@/stores/companyStore';
import type IPerson from '@/domain/IPerson';
import type ICompany from '@/domain/ICompany';
import type IParticipation from '@/domain/IParticipation';
import { ref, type Ref } from 'vue';

const participationStore = useParticipationStore();
await participationStore.fillParticipations();

const eventStore = useEventStore();
const event = eventStore.$state.events.find((e) => e.id === props.id);

const personStore = usePersonStore();
await personStore.fillPersons();

const companyStore = useCompanyStore();
await companyStore.fillCompanies();

const props = defineProps({
    id: String
});

const persons: Ref<IPerson[]> = ref([]);
const companies: Ref<ICompany[]> = ref([]);
const participations: IParticipation[] = participationStore.$state.participations.filter((p) => p.eventId === props.id);

participations.forEach(element => {
    if (element.personId !== null) {
        const person = personStore.$state.persons.find((p) => p.id === element.personId);
        if (isIPerson(person)) {
            persons.value.push(person);
        }
    } else if (element.companyId !== null) {
        const company = companyStore.$state.companies.find((c) => c.id === element.companyId);
        if (isICompany(company)) {
            companies.value.push(company);
        }
    }
});

async function deleteItem(item: IPerson | ICompany) {
    let participationId: string | undefined;

    if (isIPerson(item)) {
        participationId = participations.find((p) => p.personId === item.id && p.eventId === event?.id)?.id;

        if (participationId !== undefined) {
            await participationStore.deleteParticipation(participationId);
        }

        persons.value = persons.value.filter((p) => p.id !== item.id);

        if (item.id !== undefined) {
            await personStore.deletePerson(item.id);
        }

    } else if (isICompany(item)) {
        participationId = participations.find((p) => p.companyId === item.id && p.eventId === event?.id)?.id;

        if (participationId !== undefined) {
            await participationStore.deleteParticipation(participationId);
        }

        companies.value = companies.value.filter((c) => c.id !== item.id);

        if (item.id !== undefined) {
            await companyStore.deleteCompany(item.id);
        }
    }
}

</script>

<template>
    <div class="container">
        <div class="d-flex flex-row">
            <div class="bg-primary bg-gradient text-white flex-grow-1 border border-white rounded-1">
                <h4 class="display-6">Osavõtjad</h4>
            </div>
            <img src="../../img/libled-2.jpg" class="img-fluid border border-white rounded-1">
        </div>
        <h4 type="button" v-on:click="router.push(`/`)" class="h4"><i class="bi bi-arrow-left"></i></h4>
        <ul class="list-unstyled">
            <li>
                <h1 class="display-6">{{ event!.name }}</h1>
                <ul>
                    <li>{{ fixStartTimeFormat(event?.startTime!) }}</li>
                    <li>{{ event?.location }}</li>
                    <li v-if="event?.notes !== null">{{ event?.notes }}</li>
                </ul>
            </li>
        </ul>

        <table class="table table-sm">
            <thead>
                <th class="colspan-3">
                    Osavõtjad
                </th>
            </thead>
            <tbody>
                <tr v-for="person in persons" v-bind:key="person.id">
                    <RouterLink to="{ name: /personedit, props: person.id}"><td>{{ person.fullName }}</td></RouterLink>
                    <td>{{ person.personalIdentificationCode }}</td>
                    <td><img type="button" v-on:click="deleteItem(person)" src="../../img/remove.svg" height="20"></td>
                </tr>
                <tr v-for="company in companies" v-bind:key="company.id">
                    <RouterLink to="{ name: /companyedit, props: company.id }"><td>{{ company.name }}</td></RouterLink>
                    <td>{{ company.code }}</td>
                    <td><img type="button" v-on:click="deleteItem(company)" src="../../img/remove.svg" height="20"></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>