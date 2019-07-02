<template>
    <v-app>

        <v-navigation-drawer v-if="!isFullScreenRequired" persistent :mini-variant="miniVariant"
                             :clipped="clipped"
                             v-model="drawer" enable-resize-watcher fixed app>
            <v-list>
                <v-list-tile value="true" v-for="(item, i) in items" :key="i" :to="item.link">
                    <v-list-tile-action>
                        <v-icon v-html="item.icon"></v-icon>
                    </v-list-tile-action>
                    <v-list-tile-content>
                        <v-list-tile-title v-text="item.title"></v-list-tile-title>
                    </v-list-tile-content>
                </v-list-tile>
            </v-list>
        </v-navigation-drawer>

        <v-toolbar v-if="!isFullScreenRequired" app :clipped-left="clipped">
            <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
            <v-btn icon @click.stop="miniVariant = !miniVariant">
                <v-icon v-html="miniVariant ? 'chevron_right' : 'chevron_left'"></v-icon>
            </v-btn>
            <v-btn icon @click.stop="clipped = !clipped">
                <v-icon>web</v-icon>
            </v-btn>
            <v-toolbar-title v-text="title"></v-toolbar-title>
            <v-spacer></v-spacer>
        </v-toolbar>


        <router-view ref="routerView" class="views-container"/>
        
        <v-footer v-if="!isFullScreenRequired" app>
            <span>&nbsp;VW Platform&nbsp;&copy;&nbsp;2019</span>
        </v-footer>

    </v-app>
</template>

<script>

    export default {
        name: "home",
        components: {},
        data: function () {
            return {
                clipped: false,
                drawer: true,
                miniVariant: true,
                right: true,
                title: 'VW platform',
                items: [
                    {title: 'Counter', icon: 'touch_app', link: '/home/counter'},
                    {title: 'Fetch data', icon: 'get_app', link: '/home/fetch-data'},
                    {title: 'QR scanner', icon: 'fas fa-qrcode', link: '/home/qr-scanner'},
                    {title: 'Sign out', icon: 'power_settings_new', link: '/home/log-out'}
                ]
            };
        },
        computed: {
            isFullScreenRequired(){
                console.log(this.$route.path);
                return this.$route.path === '/home/qr-scanner';
            }
        },
        methods: {}
    }
</script>

<style>
    .views-container {
        background: radial-gradient(#40404b, #111118) rgba(34,34,40,0.94);
    }
</style>