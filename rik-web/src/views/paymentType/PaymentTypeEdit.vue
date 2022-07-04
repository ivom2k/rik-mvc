<script setup lang="ts">
import type IPaymentType from '@/domain/IPaymentType';
import router from '@/router/router';
import { usePaymentTypeStore } from '@/stores/paymentTypeStore';
import { ref, type Ref } from 'vue';

const paymentTypeStore = usePaymentTypeStore();

let paymentType: IPaymentType | undefined;
let id: string;
let type: Ref<string>;

const props = defineProps({
    id: String,
})

if (props.id !== undefined) {
    paymentType = paymentTypeStore.$state.paymentTypes.find((p) => p.id === props.id);

    if (paymentType?.id !== undefined) {
        id = paymentType.id;
    }

    if (paymentType?.type !== undefined) {
        type = ref(paymentType.type);
    }
}

async function updatePaymentType(): Promise<void> {
    await paymentTypeStore.updatePaymentType(id,
    {
        id: id,
        type: type.value
    });

    router.push("/paymenttypes");
}

</script>

<template>
<div class="container">
    <button v-on:click="router.push(`/paymenttypes`)" type="button" class="btn btn-outline-primary btn-sm">Tagasi</button>
<h4 class="display-6">Muuda</h4>

<div class="mb-3">
    <label class="form-label">Tüüp</label>
    <input v-model="type" type="text" class="form-control">
</div>

<button v-on:click="updatePaymentType()" type="button" class="btn btn-outline-primary">Uuenda</button>

</div>
</template>