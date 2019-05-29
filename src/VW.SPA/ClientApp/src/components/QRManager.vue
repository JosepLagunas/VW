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

    export default {
        name: "QRManager",
        components: {QrcodeStream},
        methods: {
            onDecode: function (decodedContent) {
                console.log(decodedContent);
            }
        }
    }
</script>

<style scoped>

    .qr-decoder-wrapper {
        position: absolute;
        width: 100%;
        height: 100%;
    }

    .qr-decoder-container {
        position: absolute;
        height: 667px;
        width: 375px;
    }

    .qr-decoder-container .inside {
        height: 100% !important;
        width: 100% !important;
    }

    .qr-decoder-container video {
        height: 100% !important;
        width: 100% !important;
        object-fit: cover !important;
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
    }

</style>