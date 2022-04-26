import { createPinia } from 'pinia'
import { createApp } from 'vue'



import App from './App.vue'
import router from './routes';
import "./Styles/index.css";



createApp(App)

.use(createPinia())
.use(router)
.mount('#app')
