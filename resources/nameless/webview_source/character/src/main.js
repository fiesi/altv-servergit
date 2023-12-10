import { createApp } from 'vue'
import App from './App.vue'

import '@/index.css'

import CreateView from '@/views/create.vue'
import SelectView from '@/views/select.vue'
const routes = [
    {
        path: '/',
        component: {
            template: '',
        },
    },
    {
        path: '/create',
        component: CreateView,
    },
    {
        path: '/select',
        component: SelectView,
    },
]

import { createRouter, createWebHashHistory } from 'vue-router'
const router = createRouter({
    history: createWebHashHistory(),
    routes,
})

createApp(App).use(router).mount('#app')
