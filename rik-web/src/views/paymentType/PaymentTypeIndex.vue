<script setup lang="ts">
import { usePaymentTypeStore } from '@/stores/paymentTypeStore';


const paymentTypeStore = usePaymentTypeStore();
await paymentTypeStore.fillPaymentTypes();

</script>

<template>
<div class="d-flex justify-content-center">
<h4 class="display-6">Index</h4>



</div>
</template>