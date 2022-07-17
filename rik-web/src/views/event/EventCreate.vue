<script setup lang="ts">
import router from '../../router/router';
import { useEventStore } from '../../stores/eventStore';
import { type Ref, ref, watch } from 'vue';


const eventStore = useEventStore();

const name: Ref<string> = ref("");
const startTime: Ref<string> = ref("");
const location: Ref<string> = ref("");
const notes: Ref<string> = ref("");

const nameErrorMessageHtml = ref(``);
const startTimeErrorMessageHtml = ref(``);
const locationErrorMessageHtml = ref(``);
const missingValueMessage = `<div class="small text-danger">Sisesta väärtus!</div>`;
const startTimeFormatValidation = ref(``);
const startTimePastValidation = ref(``);

watch(name, (newName) => {
    if (newName.length === 0) {
        nameErrorMessageHtml.value = missingValueMessage;
    } else {
        nameErrorMessageHtml.value = ``;
    }
})

watch(startTime, (newStartTime) => {
    const startTimeRegex = new RegExp('^\\d+.\\d+.\\d+\\s\\d+\\:\\d\\d$');

    if (newStartTime.length === 0) {
        startTimeErrorMessageHtml.value = missingValueMessage;
    } else {
        startTimeErrorMessageHtml.value = ``;
    }

    if (!startTimeRegex.test(newStartTime)) {
        startTimeFormatValidation.value = `<div class="small text-danger">Kuupäev peab järgima pp.kk.aaaa hh.mm formaati!</div>`;
    } else {
        startTimeFormatValidation.value = ``;
    }

    if (dateIsInThePast(new Date(parseStartTime(startTime.value)))) {
        startTimePastValidation.value = `<div class="small text-danger">Kuupäev ei tohi olla minevikus!</div>`
    } else {
        startTimePastValidation.value = ``
    }
})

watch(location, (newLocation) => {
    if (newLocation.length === 0) {
        locationErrorMessageHtml.value = missingValueMessage;
    } else {
        locationErrorMessageHtml.value = ``;
    }
})

function dateIsInThePast(date: Date) {
    return date < new Date();
}

function parseStartTime(startTime: string) {
    const [date, time] = startTime.split(" ");
    const [day, month, year] = date.split(".");
    return `${year}-${month}-${day}T${time}:00`;
}

async function createEvent(): Promise<void> {

    await eventStore.createEvent(
        {
            name: name.value,
            startTime: parseStartTime(startTime.value),
            location: location.value,
            notes: notes.value
        }
    );

    router.push("/");
}

</script>

<template>
    <div class="container">
        <div class="d-flex flex-row">
            <div class="bg-primary bg-gradient text-white flex-grow-1 border border-white rounded-1">
                <h4 class="display-6">Uus üritus</h4>
            </div>
            <img src="../../img/libled-2.jpg" class="img-fluid border border-white rounded-1">
        </div>
        <h4 type="button" v-on:click="router.push(`/`)" class="h4"><i class="bi bi-arrow-left"></i></h4>
        <div class="mb-3">
            <label class="form-label">Ürituse nimi</label>
            <input v-model="name" type="text" class="form-control">
            <div v-html="nameErrorMessageHtml"></div>
        </div>
        <div class="mb-3">
            <label class="form-label">Toimumisaeg</label>
            <input v-model="startTime" type="text" class="form-control" placeholder="pp.kk.aaaa hh:mm">
            <div v-html="startTimeErrorMessageHtml"></div>
            <div v-html="startTimeFormatValidation"></div>
            <div v-html="startTimePastValidation"></div>
        </div>
        <div class="mb-3">
            <label class="form-label">Asukoht</label>
            <input v-model="location" type="text" class="form-control">
            <div v-html="locationErrorMessageHtml"></div>
        </div>
        <div class="mb-3">
            <label class="form-label">Lisainfo</label>
            <input v-model="notes" type="text" class="form-control">
            <div v-if="notes.length > 1000" class="small text-danger">Maksimaalselt 1000 tähemärki!</div>
        </div>

        <button v-if="notes.length > 1000" type="button" class="btn btn-outline-primary disabled">Loo</button>
        <button v-else v-on:click="createEvent()" type="button" class="btn btn-outline-primary" v-bind:class="{
            disabled:
                name.length === 0 ||
                startTime.length === 0 ||
                location.length === 0
        }">Loo</button>

    </div>
</template>