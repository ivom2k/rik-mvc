<script setup lang="ts">
import { useEventStore } from '@/stores/eventStore';

const eventStore = useEventStore();
eventStore.fillEvents();

const pastEvents = (await eventStore.getEvents()).filter((e) => new Date(e.startTime) < new Date());
const upcomingEvents = (await eventStore.getEvents()).filter((e) => new Date(e.startTime) > new Date());

console.log(upcomingEvents);
console.log(pastEvents);

</script>

<template>
    <div class="container">
        <h4 class="display-6">Avaleht</h4>

        <div class="d-flex flex-row">
            <table class="table table-sm">
                <thead>
                    <tr>
                        <th colspan="6">Tulevased üritused</th>
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
                    <tr v-for="upcomingEvent in upcomingEvents" :key="upcomingEvent.id">
                    <td>{{ upcomingEvent.name }}</td>
                    <td>{{ upcomingEvent.startTime }}</td>
                    <td>{{ upcomingEvent.location }}</td>
                    <td>{{ upcomingEvent.totalParticipants }}</td>
                    <td>Lisa</td>
                    <td><img src="../../img/remove.svg"></td>
                    </tr>
                </tbody>
            </table>

            <table class="table table-sm">
                <thead>
                    <tr>
                        <th colspan="6">Toimunud üritused</th>
                    </tr>
                    <tr>
                        <th>Nimi</th>
                        <th>Aeg</th>
                        <th>Koht</th>
                        <th>Osavõtjate arv</th>
                        <th>Kustuta</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="pastEvent in pastEvents" :key="pastEvent.id">
                    <td>{{ pastEvent.name }}</td>
                    <td>{{ pastEvent.startTime }}</td>
                    <td>{{ pastEvent.location }}</td>
                    <td>{{ pastEvent.totalParticipants }}</td>
                    <td><img src="../../img/remove.svg"></td>
                    </tr>
                </tbody>
                </table>
        </div>

    </div>
</template>