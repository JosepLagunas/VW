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
        <input type="file" id="file-uploader" style="height: 0; width:0;"/>
    </v-app>
</template>

<script>

    import {CognitoFacade} from "../AWS/CognitoFacade";

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
                    {title: 'Content creator', icon: 'far fa-edit', link: '/home/content-creator'},
                    {title: 'Fetch data', icon: 'get_app', link: '/home/fetch-data'},
                    {title: 'QR scanner', icon: 'fas fa-qrcode', link: '/home/qr-scanner'},
                    {title: 'Sign out', icon: 'power_settings_new', link: '/home/log-out'}
                ]
            };
        },
        computed: {
            isFullScreenRequired() {
                console.log(this.$route.path);
                return this.$route.path === '/home/qr-scanner';
            }
        },
        methods: {},
        created() {
            CognitoFacade.AuthenticateUserWithCognito()
                .then(data => console.log(`cognito:${data}`))
                .catch(err => console.log(`cognito-error:${err}`));
        }
    }
</script>

<style>
    .views-container {
        
    }
</style>