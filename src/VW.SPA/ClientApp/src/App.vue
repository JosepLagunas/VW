<template>
    <v-app>

        <v-navigation-drawer v-if="showMenu" persistent :mini-variant="miniVariant"
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

        <v-toolbar v-if="showMenu" app :clipped-left="clipped">
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

        <v-content>
            <router-view ref="routerView"/>
        </v-content>

        <v-footer v-if="showMenu" app>
            <span>&nbsp;VW Platform&nbsp;&copy;&nbsp;2019</span>
        </v-footer>

    </v-app>
</template>

<script language="JavaScript1.5">
    import router from "@/router";
    import {EventsSubscriber} from "@/components/shared/bus/EventsSubscriber";
    import {EventsPublisher} from "@/components/shared/bus/EventsPublisher";

    export default {
        name: "App",
        components: {
            EventsSubscriber,
            EventsPublisher
        },
        data: function () {
            return {
                showMenu: false,
                clipped: false,
                drawer: true,
                miniVariant: true,
                right: true,
                title: 'VW platform',
                items: [
                    {title: 'Home', icon: 'home', link: '/'},
                    {title: 'Counter', icon: 'touch_app', link: '/counter'},
                    {title: 'Fetch data', icon: 'get_app', link: '/fetch-data'},
                    {title: 'QR scanner', icon: 'photo_camera', link: '/qr-scanner'}
                ]
            };
        },
        methods: {
            onCodeDecoded: function (e) {
                this.showMenu = true;
                this.$router.push({name: 'Home'});
            }
        },
        created: function () {
            if (!this.showMenu) {
                this.$router.push({name: 'QR scanner'});
            }
            EventsSubscriber.subscribeTo('codeDecode', this.onCodeDecoded);
        }
    }
</script>
