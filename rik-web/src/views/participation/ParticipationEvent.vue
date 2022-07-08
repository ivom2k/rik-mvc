<script setup lang="ts">
import { useEventStore } from '@/stores/eventStore';
import { useParticipationStore } from '@/stores/participationStore';
import fixStartTimeFormat from '@/helpers/helpers';
import router from '@/router/router';

const participationStore = useParticipationStore();
await participationStore.fillParticipations();

const eventStore = useEventStore();

const event = eventStore.$state.events.find((e) => e.id === props.id);


const props = defineProps({
    id: String
});



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

    </div>
</template>