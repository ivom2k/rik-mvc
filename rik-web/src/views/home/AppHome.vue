<script setup lang="ts">
import { useEventStore } from '@/stores/eventStore';

const eventStore = useEventStore();
eventStore.fillEvents();

async function deleteEvent(id: string | undefined): Promise<void> {
    if (id === undefined) {
        return;
    }

    await eventStore.deleteEvent(id);
}

function fixStartTimeFormat(startTime: string) {
    const [date, time] = startTime.split("T");
    const [year, month, day] = date.split("-");
    const [hours, minutes] = time.split(":");
    
    return `${day}.${month}.${year} ${hours}:${minutes}`;
}

</script>

<template>
    <div class="container">
        <h4 class="display-6">Avaleht</h4>

        <div class="d-flex flex-column">
            <table class="table table-sm align-middle">
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
                    <tr v-for="upcomingEvent in eventStore.$state.events.filter((e) => new Date(e.startTime) > new Date())" :key="upcomingEvent.id">
                        <td>{{ upcomingEvent.name }}</td>
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
                        <th colspan="6">Toimunud üritused</th>
                    </tr>
                    <tr>
                        <th>Nimi</th>
                        <th>Aeg</th>
                        <th>Koht</th>
                        <th>Osavõtjate arv</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="pastEvent in eventStore.$state.events.filter((e) => new Date(e.startTime) < new Date())" :key="pastEvent.id">
                        <td>{{ pastEvent.name }}</td>
                        <td>{{ fixStartTimeFormat(pastEvent.startTime) }}</td>
                        <td>{{ pastEvent.location }}</td>
                        <td>{{ pastEvent.totalParticipants }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>