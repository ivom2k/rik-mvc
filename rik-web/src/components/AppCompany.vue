<script setup lang="ts">
import { usePaymentTypeStore } from '../stores/paymentTypeStore';
import { type Ref, ref, computed } from 'vue';
import type IPaymentType from '../domain/IPaymentType';
import type ICompany from '../domain/ICompany';
import { useCompanyStore } from '../stores/companyStore';
import { useParticipationStore } from '../stores/participationStore';
import router from '../router/router';

const companyStore = useCompanyStore();
const participationStore = useParticipationStore();
const paymentTypeStore = usePaymentTypeStore();
await paymentTypeStore.fillPaymentTypes();

const name: Ref<string> = ref("");
const code: Ref<number> = ref(0);
const participantCount: Ref<string> = ref("");
const notes: Ref<string | undefined> = ref("");

const props = defineProps({
    eventId: String
});

async function deletePaymentType(id: string | undefined) {
    if (id !== undefined) {
        await paymentTypeStore.deletePaymentType(id);
    }
}

const paymentTypes = computed(() => {
    return paymentTypeStore.$state.paymentTypes as IPaymentType[];
})

const paymentTypeId = ref(paymentTypes.value.at(0)?.id);

async function createCompanyParticipation(): Promise<void> {
    const companyPostResponse: ICompany = await companyStore.addCompany({
        name: name.value,
        code: code.value,
        participantsCount: participantCount.value,
        notes: notes.value  
    });

    const companyId = companyPostResponse.id;

    await participationStore.createParticipation({
        eventId: props.eventId,
        companyId: companyId,
        paymentTypeId: paymentTypeId.value
    });

    router.push("/");
}
</script>

<template>
    <div>
        <div class="mb-3">
            <label class="form-label">Nimi</label>
            <input v-model="name" type="text" class="form-control">
        </div>
        <div class="mb-3">
            <label class="form-label">Registrikood</label>
            <input v-model="code" type="text" class="form-control">
        </div>
        <div class="mb-3">
            <label class="form-label">Märkmed</label>
            <input v-model="notes" type="text" class="form-control">
        </div>

            <div class="mb-3">
            <label class="form-label">Osalejate arv</label>
            <input v-model="participantCount" type="text" class="form-control">
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
    
        <button v-on:click="createCompanyParticipation()" type="button"
            class="btn btn-outline-primary">Salvesta</button>
    </div>
</template>