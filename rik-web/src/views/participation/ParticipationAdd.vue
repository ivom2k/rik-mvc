<script setup lang="ts">

const props = defineProps({
    id: String
});

</script>

<template>
    <div class="container">

        {{ props.id }}

    </div>
</template>