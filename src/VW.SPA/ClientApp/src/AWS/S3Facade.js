let AWS = require('aws-sdk');

let S3Facade = {
    uploadFile: (folder, fileName, content) => {
        let file = content;
        let folderKey = encodeURIComponent(folder) + '/';

        let key = folderKey + fileName;

        return new Promise((resolve, reject) => {

            let client = new AWS.S3();
            client.upload({
                Key: key,
                Body: content,
                Bucket: 'webplatform-documents'
            }, function (err, data) {
                if (err) {
                    reject('There was an error uploading your file: ', err.message);
                    return;
                }
                resolve('success');
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
                    Bucket: 'webplatform-documents'
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
            Bucket: 'webplatform-documents',
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