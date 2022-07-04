import { createRouter, createWebHistory } from "vue-router";

import AppHome from "../views/home/AppHome.vue";

import PaymentTypeIndex from "../views/paymentType/PaymentTypeIndex.vue"
import PaymentTypeCreate from "../views/paymentType/PaymentTypeCreate.vue"
import PaymentTypeEdit from "../views/paymentType/PaymentTypeEdit.vue"

import EventCreate from "../views/event/EventCreate.vue"


const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        { path: "/", name: "home", component: AppHome },
        
        { path: "/paymenttypes", name: "paymenttypes", component: PaymentTypeIndex },
        { path: "/createpaymenttype", name: "createpaymenttype", component: PaymentTypeCreate },
        { path: "/editpaymenttype/:id", name: "editpaymenttype", component: PaymentTypeEdit, props: true },
        { path: "/createevent", name: "createevent", component: EventCreate }
    ]
})

export default router;