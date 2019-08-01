<template>
    <div class="uploader-wrapper">
        <input type="file" ref="fileInput" id="file-input" style="height:0;width:0;"/>
        <v-btn v-if="!fileLoaded" fab dark color="indigo"
               @click.prevent="handleClick" class="btn-wrapper">
            {{title}}
            <v-icon dark>{{icon}}</v-icon>
        </v-btn>
        <v-chip v-if="fileLoaded" label
                :class="{'chip-upload':!uploadCompleted, 'chip-upload-completed': uploadCompleted}"
                :close="!uploadReady && !uploadCompleted"
                @input="handleRemoveClick">
            &nbsp;
            <v-icon v-for="(c, index) in (!categories?[]:categories)" left small>
                {{categoriesIConsMapping[c]}}
            </v-icon>
            {{this.fileName}}
            {{this.remainingUploadLabel}}

            <v-progress-circular v-if="uploadReady && !uploadCompleted"
                                 style="margin-left: 10px;"
                                 :indeterminate="uploadReady && !uploading"
                                 :class="{ 'uploader-running': uploading , 
                                 'uploader-waiting': !uploading  }"
                                 :width="3" :size="23"
                                 :value="uploadProgress.percentage"></v-progress-circular>
            <v-icon v-if="uploadCompleted" style="color:green; margin-left: 4px;"
                    color="yellow">check
            </v-icon>
        </v-chip>
    </div>
</template>

<script>
    import {EventsPublisher} from "./shared/bus/EventsPublisher";
    import VProgressCircular from "vuetify/lib/components/VProgressCircular/VProgressCircular";

    export default {
        name: "FileSelector",
        props: ["title", "icon", "categories", "uploadReady", "uploadProgress"],
        components: {EventsPublisher, VProgressCircular},
        data() {
            return {
                fileName: null,
                fileSize: 0,
                fileContent: null,
                fileLoaded: false,
                uploading: false,
                uploadCompleted: false,
                categoriesIConsMapping: {
                    Videos: 'videocam',
                    Images: 'camera_alt',
                    Documents: 'attach_file',
                    Urls: 'link'
                }
            }
        },
        watch: {
            uploadProgress(newValue) {
                if (newValue.percentage > 0) {
                    this.uploadCompleted = this.uploadCompleted || newValue.percentage === 100;
                    this.uploading = !this.uploadCompleted;
                }
            },
        },
        computed: {
            fileSizeLabel: function () {
                return this.stringifyFileSize(this.fileSize);
            },
            remainingUploadLabel: function () {
                if (this.uploading) {
                    return this.stringifyFileSize(this.fileSize - this.uploadProgress.loaded);
                }
                return this.fileSizeLabel;
            }
        },
        methods: {
            simulateClick: function (elem) {
                // Create our event (with options)
                let evt = new MouseEvent('click', {
                    bubbles: true,
                    cancelable: true,
                    view: window
                });
                let canceled = !elem.dispatchEvent(evt);
            },
            handleClick: function (e) {
                let inputFile = this.$refs.fileInput;
                inputFile.removeEventListener("change", this.handleFileSelected);
                inputFile.addEventListener("change", this.handleFileSelected);
                this.simulateClick(inputFile);
                let eventArgs = {source: this, data: null};
                this.$emit('click', eventArgs);
            },
            handleRemoveClick: function (e) {
                console.log('close');
                let eventArgs = {source: this, data: {id: this._uid}};
                this.$emit('removed', eventArgs);
                this.$el.remove();
            },
            handleFileSelected(e) {
                let files = e.target.files;
                if (files.length < 1) {
                    return;
                }

                for (let file of files) {

                    this.fileName = file.name;
                    this.fileSize = file.size;
                    this.notifyFileSelected(file);
                }
            },
            notifyFileSelected(file) {
                this.fileContent = file;
                let eventArgs = {
                    source: this, data: {
                        id: this._uid,
                        fileName: this.fileName,
                        fileSize: this.fileSize,
                        fileContent: this.fileContent
                    }
                };
                this.fileLoaded = true;
                this.$emit('file-selected', eventArgs);
            },
            getCategoriesIcons() {
                if (!this.categories) return [];
                return this.categories.map(c => this.categoriesIConsMapping[c.toLowerCase()]);
            },
            stringifyFileSize: (fileSize) => {
                if (fileSize < 1024) {
                    return fileSize.toString() + " bytes";
                } else if (fileSize < 1048576) {
                    let size = (fileSize / 1024).toFixed(2);
                    return size + "Kb";
                } else if (fileSize < 1073741824) {
                    let size = (fileSize / 1048576).toFixed(2);
                    return size + "Mb";
                } else {
                    let size = (fileSize / 1073741824).toFixed(2);
                    return size + "Gb";
                }
            }
        }
    }
</script>

<style scoped>
    .uploader-wrapper {
        float: left;
        display: contents;
        width: fit-content;
    }

    .btn-wrapper {
        transform: rotate(-90deg);
        float: none;
        right: 23%;
        bottom: 10%;
        position: fixed;
    }

    .file-selected {
        position: relative;
        padding: 2px;
        background-color: #4caf50;
        border: solid 1px #1b5e20;
        width: fit-content;
        border-radius: 4px;
        color: #1b5e20;
    }

    .uploader-running {
        color: #edff32;
    }

    .uploader-waiting {
        color: #ffd019;
    }

    .chip-upload {
        color: #fff;
        color: #fff;
        background: #00b0ff;
        border: solid 1px #10b2c8;
    }

    .chip-upload-completed {
        background-color: #00c853;
        color: #fff;
    }
</style>

