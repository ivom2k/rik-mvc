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
        <h4 type="button" v-on:click="router.push(`/`)" class="h4"><i class="bi bi-arrow-left"></i></h4>
    <ul class="list-unstyled">
    <li><h1 class="display-6">{{ event!.name }}</h1>
    <ul>
        <li>{{ fixStartTimeFormat(event?.startTime!) }}</li>
        <li>{{ event?.location }}</li>
        <li v-if="event?.notes !== null">{{ event?.notes }}</li>
    </ul>
    </li>    
    </ul>

    </div>
</template>