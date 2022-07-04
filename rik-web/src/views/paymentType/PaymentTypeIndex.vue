<script setup lang="ts">
import { usePaymentTypeStore } from '@/stores/paymentTypeStore';


const paymentTypeStore = usePaymentTypeStore();
await paymentTypeStore.fillPaymentTypes();

async function deletePaymentType(id: string | undefined) {
    if (id == undefined) {
        return;
    }

    await paymentTypeStore.deletePaymentType(id);
}

</script>

<template>
    <div class="container">
        <h4 class="display-6">Maksetüübid</h4>

        <table class="table table-borderless">
            <thead>
                <tr>
                    <th scope="col">Tüüp</th>
                    <th scope="col">Muuda</th>
                    <th scope="col">Kustuta</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in paymentTypeStore.paymentTypes" :key="item.id">
                    <td>{{ item.type }}</td>
                    <td>
                        <RouterLink :to="{ name: 'editpaymenttype', params: { id: item.id } }"><button type="button"
                                class="btn btn-outline-primary btn-sm">Muuda</button></RouterLink>
                    </td>
                    <td v-on:click="deletePaymentType(item.id)"><button type="button"
                            class="btn btn-outline-primary btn-sm">Kustuta</button></td>
                </tr>
            </tbody>
        </table>
        <RouterLink :to="{ name: 'createpaymenttype' }"><button type="button" class="btn btn-outline-primary">Loo</button>
        </RouterLink>
    </div>
</template>