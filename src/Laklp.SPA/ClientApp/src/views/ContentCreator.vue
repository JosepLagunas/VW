<template>
    <v-container fluid>
        <v-slide-y-transition mode="out-in">
            <div>
                <v-text-field>
                    v-model="name"
                    label="Name"
                    required>
                </v-text-field>

                <v-textarea
                        v-model="description"
                        label="Description"
                        required
                >
                </v-textarea>
                <v-btn :loading="uploadingFiles" fab color="red" @click="handleCreateContent"
                       class="create-content-btn">
                    <v-icon color="white">cloud_upload</v-icon>
                </v-btn>
                <div id="video-button-uploads-container" ref="uploadButtonsContainer">

                </div>
                <v-layout row justify-center>
                    <v-dialog v-model="dialog" persistent max-width="290">
                        <v-card>
                            <v-card-title class="headline">Document classification
                            </v-card-title>
                            <v-divider></v-divider>
                            <v-card-text>
                                <v-switch v-for="(category, index) in this.documentCategories"
                                          v-model="dataForCurrentSelectedFile.categories"
                                          :label="category" :value="category"></v-switch>
                            </v-card-text>
                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="green darken-1" flat @click="addCategoriesToFile">
                                    Classify
                                </v-btn>
                            </v-card-actions>
                        </v-card>
                    </v-dialog>
                </v-layout>
            </div>
        </v-slide-y-transition>
    </v-container>
</template>

<script>

    import FileSelector from '../components/FileSelector';
    import Vue from "vue";
    import VTextField from "vuetify/lib/components/VTextField/VTextField";
    import {S3Facade} from "../AWS/S3Facade";

    export default {
        components: {FileSelector, VTextField},
        data: () => {
            return {
                name: null,
                description: null,
                files: [],
                documentCategories: ['Images', 'Videos', 'Documents', 'Urls'],
                currentFileSelector: null,
                dataForCurrentSelectedFile: {
                    file: null, categories: [], uploader: null
                },
                uploadingFiles: false,
                dialog: false
            };
        },
        watch: {},
        computed: {},
        mounted() {
            this.buildDocumentUploader();
        },
        methods: {
            handleCreateContent(e) {
                this.uploadFiles();
            },
            uploadFiles() {
                this.uploadingFiles = true;
                this.files.forEach(f => {
                    let file = f.file;
                    let name = file.fileName;
                    let content = file.fileContent;
                    f.uploader.uploadReady = true;
                    S3Facade.uploadFile('documents', name, content, function (uploader) {
                        return function (progressData) {
                            f.uploader.uploadProgress = progressData;
                        }
                    }(f.uploader));
                });
                this.uploadingFiles = false;
            },
            handleFileSelected(e) {
                this.dataForCurrentSelectedFile.file = e.data;
                this.dataForCurrentSelectedFile.categories = [];
                this.dataForCurrentSelectedFile.uploader = this.currentFileSelector;
                this.dialog = true;
            },
            addCategoriesToFile() {
                this.dialog = false;
                this.currentFileSelector.categories = this.dataForCurrentSelectedFile.categories;
                this.files.push(this.dataForCurrentSelectedFile);
                this.dataForCurrentSelectedFile = {
                    file: null,
                    categories: []
                };
                this.buildDocumentUploader();
            },
            buildDocumentUploader() {
                let componentClass = Vue.extend(FileSelector);
                let instance = new componentClass({
                    propsData: {
                        title: '',
                        icon: 'attachment',
                        categories: this.dataForCurrentSelectedFile.categories,
                        uploadProgress: 0
                    }
                });
                instance.$mount();
                this.currentFileSelector = instance;
                instance.$on('removed', this.handleRemovedUploader);
                instance.$on('file-selected', this.handleFileSelected);
                this.$refs.uploadButtonsContainer.appendChild(instance.$el);
            },
            handleRemovedUploader(e) {
                let id = e.data.id;
                this.files = this.files.filter(f => f.file.id !== id);

                if (this.uploadersCount() === 1) {
                    this.buildDocumentUploader();
                }
            },
            uploadersCount() {
                if (!this.$refs.uploadButtonsContainer) return 0;
                return this.$refs.uploadButtonsContainer.childElementCount;
            }

        }
    }

</script>

<style scoped>
    .v-chip__content {
        padding: 0 5px 0 12px !important
    }

    .fields-container {
        position: relative;
        width: 80%;
        height: 90%;
        border: solid 1px;
        margin: auto;
    }
    
    .create-content-btn{
        position: fixed;
        bottom: 10%;
        right: 5%;
    }
</style>
  


