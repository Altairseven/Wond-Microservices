import { createRouter, createWebHashHistory, createWebHistory } from "vue-router";

import MainLayout from "./layout/MainLayout.vue";
import Products from "./pages/Products/Products.vue";
import Details from "./pages/Details/Details.vue";

const routes = [
    {
        path: '/', component: MainLayout, children: [
            { path: '/', component: Products },
            { path: '/details', component: Details }
        ]
    },
    // { path: '/products', component: Products },
]



const router = createRouter({

    history: createWebHistory(),
    routes, // short for `routes: routes`
})


export default router;