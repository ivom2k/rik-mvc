<script setup lang="ts">
import router from '../../router/router';
import { usePaymentTypeStore } from '../../stores/paymentTypeStore';
import { type Ref, ref } from 'vue';

const paymentTypeStore = usePaymentTypeStore();

const type: Ref<string> = ref("");

async function createPaymentType(): Promise<void> {
    await paymentTypeStore.createPaymentType(
        {
            type: type.value
        });

        router.go(-1);
}

</script>

<template>
<div class="container">

    <h4 type="button" v-on:click="router.go(-1)" class="h4"><i class="bi bi-arrow-left"></i></h4>
<h4 class="display-6">Loo</h4>

<div class="mb-3">
    <label class="form-label">Tüüp</label>
    <input v-model="type" type="text" class="form-control">
</div>

<button v-on:click="createPaymentType()" type="button" class="btn btn-outline-primary">Loo</button>

</div>
</template>