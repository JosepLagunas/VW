let AWS = require('aws-sdk');
let bucket = 'webplatform-documents';

let S3Facade = {
    uploadFile: (folder, fileName, content, notifyProgressCallback) => {
        let file = content;
        let folderKey = encodeURIComponent(folder) + '/';

        let key = folderKey + fileName;

        return new Promise((resolve, reject) => {

            let client = new AWS.S3();
            client.upload({
                Key: key,
                Body: content,
                Bucket: bucket
            }, function (err, data) {
                if (err) {
                    reject('There was an error uploading your file: ', err.message);
                    return;
                }
                resolve('success');
            }).on('httpUploadProgress', (evt) => {
                if (!!notifyProgressCallback) {
                    let percentage = Math.floor(evt.loaded * 100 / evt.total);
                    notifyProgressCallback({
                        percentage: percentage,
                        loaded: evt.loaded,
                        total: evt.total
                    });
                }
            });
        });
    },
    deleteFile: (folder, fileName) => {
        let folderKey = encodeURIComponent(folder) + '/';
        let key = folderKey + fileName;

        return new Promise((resolve, reject) => {
            let client = new AWS.S3();
            client.deleteObject({
                    Key: key,
                    Bucket: bucket
                },
                function (err, data) {
                    if (err) {
                        reject('There was an error deleting your file: ', err.message);
                        return;
                    }
                    resolve('success');

                });
        });
    },
    listAllFiles: () => {
        let params = {
            Bucket: bucket,
            Prefix: ''  // Can be your folder name
        };

        return new Promise((resolve, reject) => {
            let client = new AWS.S3();
            client.listObjectsV2(params, function (err, data) {
                if (err) {
                    reject('There was an error listing your files', err.stack);
                    return;
                }
                resolve(data);
            });
        });
    }
};

export {S3Facade};