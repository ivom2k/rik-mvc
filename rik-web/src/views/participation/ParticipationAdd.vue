<script setup lang="ts">
import AppPerson from "../../components/AppPerson.vue";
import AppCompany from '../../components/AppCompany.vue';
import { shallowRef } from "vue";
import { useEventStore } from "../../stores/eventStore"
import router from "../../router/router";

const activeComponent = shallowRef(AppPerson);
const eventStore = useEventStore();

const props = defineProps({
    id: String
});

const event = eventStore.$state.events.find((e) => e.id === props.id);


</script>

<template>
    <div class="container">
        <div class="d-flex flex-row">
            <div class="bg-primary bg-gradient text-white flex-grow-1 border border-white rounded-1">
                <h4 class="display-6">Osaleja lisamine</h4>
            </div>
            <img src="../../img/libled-2.jpg" class="img-fluid border border-white rounded-1">
        </div>

        <h4 type="button" v-on:click="router.go(-1)" class="h4"><i class="bi bi-arrow-left"></i></h4>

        <div class="form-check">
            <label class="form-check-label">
                <input class="form-check-input" type="radio" v-model="activeComponent" :value="AppPerson"> Isik
            </label>
        </div>
        <div class="form-check">
            <label class="form-check-label">
                <input class="form-check-input" type="radio" v-model="activeComponent" :value="AppCompany"> Ettevõte
            </label>
            <Transition name="fade" mode="out-in">
                <component v-bind:is="activeComponent" v-bind:eventId="props.id"></component>
            </Transition>
        </div>

    </div>
</template>