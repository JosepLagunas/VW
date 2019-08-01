<template>

</template>

<script>
    export default {
        name: "handleNotFoundView",
        created: function () {
            this.$router.push({name: 'home'});
        }
    }
</script>

<style scoped>
</style>