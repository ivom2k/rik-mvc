<script setup lang="ts">
import AppHeader from "./components/AppHeader.vue";


</script>

<template>
<AppHeader />

<Suspense>
<RouterView />
</Suspense>

</template>
