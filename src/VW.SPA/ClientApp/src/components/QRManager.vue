<template>
    <div class="qr-decoder-container">
        <div class="qr-decoder-wrapper">
            <qrcode-stream @decode="onDecode"></qrcode-stream>
        </div>
        <div class="qr-reader-target"></div>
    </div>
</template>

<script>
    import {QrcodeStream} from 'vue-qrcode-reader'
    import {EventsPublisher} from "./shared/bus/EventsPublisher";

    export default {
        name: "QRManager",
        components: {QrcodeStream, EventsPublisher},
        methods: {
            onDecode: function (decodedContent) {
                console.log("decoded");
                let eventArgs = {source: this, data: {code: decodedContent}};
                EventsPublisher.broadcast('codeDecode', eventArgs);
            }
        }
    }
</script>

<style lang="css" scoped>

    .qr-decoder-wrapper {
        position: absolute;
        width: 100%;
        height: 100%;
    }

    .qr-decoder-wrapper >>> .wrapper {
        display: flex;
        flex-flow: row nowrap;
        justify-content: space-around;
        align-items: center;
        height: 100%;
        width: 100%;
    }

    .qr-decoder-wrapper >>> .inside {
        position: relative;
        max-width: 100%;
        max-height: 100%;
        height: 100%;
        width: 100%;
        z-index: 0
    }

    .overlay, .tracking-layer {
        position: absolute;
        width: 100%;
        height: 100%;
        top: 0;
        left: 0
    }

    .qr-decoder-wrapper >>> .camera, .pause-frame {
        display: block;
        object-fit: cover;
        max-width: 100%;
        max-height: 100%;
        height: 100%;
        width: 100%;
    }

    .qr-decoder-container {
        position: absolute;
        height: 100%;
        width: 100%;
    }

    .qr-reader-target {
        position: absolute;
        height: 300px;
        width: 300px;
        top: 0;
        bottom: 0;
        left: 0;
        right: 0;
        margin: auto;
        border: dashed 4px yellow;
        border-radius: 20px;
    }

</style>