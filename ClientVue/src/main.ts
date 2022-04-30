

import { createPinia } from 'pinia'
import { defineConfig } from 'vite'

import { createApp } from 'vue'




console.log(import.meta.env) // remove this after you've confirmed it working




import App from './App.vue'
import router from './routes';
import "./Styles/index.css";



createApp(App)

.use(createPinia())
.use(router)
.mount('#app')
