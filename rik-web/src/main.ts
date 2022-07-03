import { createApp } from 'vue'
import App from './App.vue'

import 'jquery';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import { createPinia } from 'pinia';
import router from "../src/router/router"

const app = createApp(App);
const pinia = createPinia();

app.use(pinia);

app.use(router);

app.mount("#app");