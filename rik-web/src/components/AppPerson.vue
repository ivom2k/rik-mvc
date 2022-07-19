<script setup lang="ts">
import { usePersonStore } from '../stores/personStore';
import { type Ref, ref } from 'vue';
import type IPerson from '../domain/IPerson';
import { useParticipationStore } from '../stores/participationStore';
import { usePaymentTypeStore } from '../stores/paymentTypeStore';
import type IPaymentType from '../domain/IPaymentType';
import router from '../router/router';
import { computed, watch } from 'vue';
import { verifyPersonalIdentificationCode }from "../helpers/helpers"

const props = defineProps({
    eventId: String
});

const personStore = usePersonStore();
const participationStore = useParticipationStore();
const paymentTypeStore = usePaymentTypeStore();
await paymentTypeStore.fillPaymentTypes();

const firstName: Ref<string> = ref("");
const lastName: Ref<string> = ref("");
const personalIdentificationCode: Ref<number> = ref(0);
const notes: Ref<string | undefined> = ref("");
const personalIdentificationCodeLengthErrorHtml: Ref<string> = ref("");
const personalIdentificationCodeVerifyErrorHtml: Ref<string> = ref("");
const paymentTypes = computed(() => {
    return paymentTypeStore.$state.paymentTypes as IPaymentType[];
})

const paymentTypeId = ref(paymentTypes.value.at(0)?.id);

watch(personalIdentificationCode, (newPersonalIdentificationCode) => {

    if (newPersonalIdentificationCode.toString().length < 11 || newPersonalIdentificationCode.toString().length > 11) {
        personalIdentificationCodeLengthErrorHtml.value = `<div class="small text-danger">Isikukood peab olema 11 numbrit!</div>`;
    } else if (!verifyPersonalIdentificationCode(newPersonalIdentificationCode.toString())) {
        personalIdentificationCodeLengthErrorHtml.value = ``;
        personalIdentificationCodeVerifyErrorHtml.value = `<div class="small text-danger">Isikukood ei ole õige!</div>`;
    } else {
        personalIdentificationCodeLengthErrorHtml.value = ``;
        personalIdentificationCodeVerifyErrorHtml.value = ``;
    }
})

async function createPersonParticipation(): Promise<void> {
    const personPostResponse: IPerson = await personStore.addPerson(
        {
            firstName: firstName.value,
            lastName: lastName.value,
            personalIdentificationCode: personalIdentificationCode.value,
            notes: notes.value
        });

    const personId = personPostResponse.id;

    await participationStore.createParticipation({
        eventId: props.eventId,
        personId: personId,
        paymentTypeId: paymentTypeId.value
    });

    router.push("/");
}

async function deletePaymentType(id: string | undefined) {
    if (id !== undefined) {
        await paymentTypeStore.deletePaymentType(id);
    }
}

</script>

<template>
    <div>
        <div class="mb-3">
            <label class="form-label">Eesnimi</label>
            <input v-model="firstName" type="text" class="form-control">
        </div>
        <div class="mb-3">
            <label class="form-label">Perekonnanimi</label>
            <input v-model="lastName" type="text" class="form-control">
        </div>
        <div class="mb-3">
            <label class="form-label">Isikukood</label>
            <input v-model="personalIdentificationCode" type="text" class="form-control">
            <div v-html="personalIdentificationCodeLengthErrorHtml"></div>
            <div v-html="personalIdentificationCodeVerifyErrorHtml"></div>
        </div>
        <div class="mb-3">
            <label class="form-label">Märkmed</label>
            <input v-model="notes" type="text" class="form-control">
        </div>

        <div class="mb-3">
            <label v-if="paymentTypes.length > 0" class="form-label">Maksetüüp <RouterLink v-bind:to="{ name: 'createpaymenttype' }">
            <i class="bi bi-plus-lg"></i></RouterLink><RouterLink v-bind:to="{ name: 'editpaymenttype', params: { id: paymentTypeId } }">
            <i class="bi bi-pencil"></i></RouterLink><i v-on:click="deletePaymentType(paymentTypeId)" type="button" class="bi bi-x"></i>
            </label>
            <label v-else-if="paymentTypes.length === 0" class="form-label"><RouterLink v-bind:to="{ name: 'createpaymenttype' }">Lisa</RouterLink> maksetüüp</label>
            <label v-if="paymentTypeId !== undefined" class="form-label"></label>
            <select class="form-select" v-model="paymentTypeId">
                <!-- <option v-for="type in paymentTypes" v-bind:value="type.id" v-bind:key="type.id"> -->
                <option v-for="type in paymentTypes" v-bind:value="type.id" v-bind:key="type.id">
                    {{ type.type }}
                </option>
            </select>
        </div>

        <button v-on:click="createPersonParticipation()" type="button" class="btn btn-outline-primary" v-bind:class="{
            disabled:
                firstName.length === 0 ||
                lastName.length === 0 ||
                personalIdentificationCode.toString().length === 0
        }">Salvesta</button>

    </div>
</template>