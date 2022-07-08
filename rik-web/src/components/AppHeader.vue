<script setup lang="ts">
import { RouterLink } from 'vue-router';


</script>

<template>
    <div class="d-flex flex-row justify-content-center">
        <nav class="navbar navbar-expand-lg">
            <div class="container-fluid">
                <!-- <RouterLink to="/" class="navbar-brand">Avaleht</RouterLink> -->
                <RouterLink to="/" class="navbar-brand"><img src="../img/logo.svg" class="img-fluid"></RouterLink>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav"
                    aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav">
                    </ul>
                </div>
            </div>
        </nav>
    </div>
</template>