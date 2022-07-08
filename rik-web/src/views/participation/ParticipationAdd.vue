<script setup lang="ts">
import type IEvent from '@/domain/IEvent';
import { useEventStore } from '@/stores/eventStore';
import { useParticipationStore } from '@/stores/participationStore';
import fixStartTimeFormat from '@/helpers/helpers';

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
    <ul class="list-unstyled">
    <li><h1 class="display-6">{{ event!.name }}</h1>
    <ul>
        <li>{{ fixStartTimeFormat(event?.startTime!) }}</li>
        <li>{{ event?.location }}</li>
    </ul>
    </li>    
    </ul>

    </div>
</template>