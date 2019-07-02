<template>
    <v-app class="main">
        <div v-if="isRootView" class="main-action-icons-container">
            <div class="action-buttons-wrapper">
                <div class="action-button-item">
                    <v-btn fab dark large color="red" class="action-button"
                           @click="handleScanningClick">
                        <v-icon>fas fa-qrcode</v-icon>
                    </v-btn>
                    <span class="action-label">scan QR</span>
                </div>
                <div class="action-button-item">
                    <v-btn fab dark large color="red" class="action-button"
                           @click="handleHomeClick">
                        <v-icon dark>fas fa-rocket</v-icon>
                    </v-btn>
                    <span class="action-label">start</span>
                </div>
            </div>
        </div>
        <v-content v-if="!isRootView">
            <router-view>

            </router-view>
        </v-content>
    </v-app>
</template>

<script>

    import { EventsSubscriber } from "./components/shared/bus/EventsSubscriber";

    export default {
        name: "App",
        components: { EventsSubscriber },
        data: function () {
            return {
                isRootView: true
            }
        },
        watch: {
            isRootView(value) {
                console.log("watch: " + value);
            }
        },
        methods: {
            handleScanningClick(e) {
                this.$router.push({ name: 'qr-scanner' });
            },
            handleHomeClick(e) {
                this.$router.push({ name: 'home' });
            },
            onQRCodeDecoded(e) {
                console.log("code decoded:" + e.data.code);
                this.$router.push({ name: 'home' });
            }
        },
        created: function () {
            this.isRootView = window.location.pathname === '/';
            this.$router.beforeEach((to, from, next) => {
                
                if (to.meta.requiresAuth === undefined || to.meta.requiresAuth) {
                    this.axios.get('/account/logged')
                        .then(r => {
                            if (!r.data) {
                                window.location.assign('/account/login');
                            } else {
                                this.isRootView = to.path === '/';
                                next();
                            }
                        })
                        .catch(_ => next({ path: 'account/login' }));
                    return;
                }
                this.isRootView = to.path === '/';
                next();
            });

            EventsSubscriber.subscribeTo('codeDecode', this.onQRCodeDecoded);

        },
        mounted: function () {
            let actionButtons = document.getElementsByClassName("action-button");
            for (let button of actionButtons) {
                button.classList.add("action-button-ready");
                button.addEventListener("transitionend", e => {
                    let actionLabels = document.getElementsByClassName("action-label");
                    for (let label of actionLabels) {
                        label.classList.add("show-action-label");
                    }
                });
            }
        }
    }
</script>

<style>
    @import url('https://use.fontawesome.com/releases/v5.0.6/css/all.css');
</style>

<style>
    .main {
        background: radial-gradient(#40404b, #111118) rgba(34,34,40,0.94);
    }

    .action-label{
        opacity: 0;
    }

    .show-action-label{
        opacity: 1;
        transition: opacity 0.3s ease-in 250ms;
    }

    .main-action-icons-container {
        position: relative;
        margin: auto;
        text-align: center;
        width: 100%;
        height: 100%;
        background: radial-gradient(#40404b, #111118) rgba(34,34,40,0.94);
        opacity: 1;
        transition: opacity 0.3s ease-in 2s;
    }

    .action-buttons-wrapper {
        width: 215px;
        margin: auto;
        position: relative;
        top: 45%;
    }

    .action-button-item {
        top: 40%;
        position: relative;
        margin-right: 2.5%;
        width: 100px;
        float: left;
    }

        .action-button-item > span {
            color: #fff;
        }

    .action-button {
        animation-name: action-button-anim;
        animation-duration: 1s;
        animation-iteration-count: 1;
        animation-fill-mode: forwards;
        animation-timing-function: ease-out;
        animation-delay: 0ms;
    }

    @keyframes action-button-anim {
        0% {
            transform: scale(0.5);
        }

        50% {
            transform: scale(1.5) rotate(360deg);
        }

        100% {
            transform: scale(1) rotate(720deg);
        }
    }
</style>
