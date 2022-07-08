<script setup lang="ts">
import { useEventStore } from '@/stores/eventStore';
import fixStartTimeFormat from '@/helpers/helpers';

const eventStore = useEventStore();
eventStore.fillEvents();

async function deleteEvent(id: string | undefined): Promise<void> {
    if (id === undefined) {
        return;
    }

    await eventStore.deleteEvent(id);
}

</script>

<template>
    <div class="container">
        <div class="d-flex flex-row">
            <div class="bg-primary bg-gradient text-white flex-grow-1 border border-white rounded-1">
                <h4 class="display-6">Nullam</h4>
            </div>
            <img src="../../img/pilt.jpg" class="img-fluid border border-white rounded-1">
        </div>
        <div class="d-flex flex-column">
            <table class="table table-sm align-middle">
                <thead>
                    <tr>
                        <th class="fs-4" colspan="6">Tulevased üritused</th>
                    </tr>
                    <tr>
                        <th>Nimi</th>
                        <th>Aeg</th>
                        <th>Koht</th>
                        <th>Osavõtjate arv</th>
                        <th>Lisa</th>
                        <th>Kustuta</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="upcomingEvent in eventStore.$state.events.filter((e) => new Date(e.startTime) > new Date())"
                        :key="upcomingEvent.id">
                        <td><RouterLink v-bind:to="{ name: 'eventparticipation', params: { id: upcomingEvent.id }}">{{ upcomingEvent.name }}</RouterLink></td>
                        <td>{{ fixStartTimeFormat(upcomingEvent.startTime) }}</td>
                        <td>{{ upcomingEvent.location }}</td>
                        <td>{{ upcomingEvent.totalParticipants }}</td>
                        <td>Lisa</td>
                        <td><img type="button" v-on:click="deleteEvent(upcomingEvent.id)" src="../../img/remove.svg"
                                height="20"></td>
                    </tr>
                </tbody>
            </table>

            <table class="table table-sm align-middle">
                <thead>
                    <tr>
                        <th class="fs-4" colspan="6">Toimunud üritused</th>
                    </tr>
                    <tr>
                        <th>Nimi</th>
                        <th>Aeg</th>
                        <th>Koht</th>
                        <th>Osavõtjate arv</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="pastEvent in eventStore.$state.events.filter((e) => new Date(e.startTime) < new Date())"
                        :key="pastEvent.id">
                        <td>{{ pastEvent.name }}</td>
                        <td>{{ fixStartTimeFormat(pastEvent.startTime) }}</td>
                        <td>{{ pastEvent.location }}</td>
                        <td>{{ pastEvent.totalParticipants }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <RouterLink to="/createevent"><button type="button" class="btn btn-outline-primary">Lisa üritus</button>
        </RouterLink>
    </div>
</template>