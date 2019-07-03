import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import HandleNotFoundView from './views/HandleNotFoundView.vue';

Vue.use(Router);

export default new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: [
        {   path: '/',
            name: 'root'
        },
        {
            path: '/home',
            name: 'home',
            component: Home,
            children: [
                {
                    path: 'counter',
                    name: 'counter',
                    // route level code-splitting
                    // this generates a separate chunk (about.[hash].js) for this route
                    // which is lazy-loaded when the route is visited.
                    component: () => import(/* webpackChunkName: "counter" */ './views/Counter.vue'),
                },
                {
                    path: 'fetch-data',
                    name: 'fetch-data',
                    component: () => import(/* webpackChunkName: "fetch-data" */ './views/FetchData.vue'),
                },
                {
                    path: 'content-creator',
                    name: 'content-creator',
                    component: () => import('./views/ContentCreator.vue')
                },
                {
                    path: 'qr-scanner',
                    name: 'qr-scanner',
                    component: () => import('./views/QRScanner.vue'),
                    meta: {requiresAuth: false}
                },
                {
                    path: 'documentation-search',
                    name: 'documentation-search',
                    component: () => import('./views/DocumentationSearch.vue')                    
                },
                {
                    path: 'log-out',
                    name: 'log-out',
                    component: () => import('./views/LogOut.vue')
                }]
        },
        {
            path: '*',
            name: '404',
            component: () => import('./views/HandleNotFoundView.vue')
        }
    ]
});
    