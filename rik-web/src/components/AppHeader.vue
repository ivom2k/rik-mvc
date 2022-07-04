<script setup lang="ts">
import { RouterLink } from 'vue-router';


</script>

<template>
    <div class="d-flex flex-row justify-content-center">
        <nav class="navbar navbar-expand-lg bg-light">
            <div class="container-fluid">
                <RouterLink to="/" class="navbar-brand">Avaleht</RouterLink>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav"
                    aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <RouterLink to="/createevent" class="nav-link">Uus üritus</RouterLink>
                        </li>
                        <li class="nav-item">
                            <RouterLink to="/paymenttypes" class="nav-link">Maksetüübid</RouterLink>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </div>
</template>