let AWS = require('aws-sdk');

let CognitoFacade = {
    AuthenticateUserWithCognito: () => {

        function getConfig() {
            return {
                aws_region: 'eu-west-1',
                provider_url: 'dev-zqmqcd7w.eu.auth0.com',
                pool_id: 'eu-west-1:36610218-b0b9-4906-ab23-00f6f1144103'
            }
        }

        function authenticateCognitoUser(tokens) {
            let config = getConfig();
            let logins = {};
            logins[config.provider_url] = tokens.idToken;

            let params = {
                IdentityPoolId: config.pool_id,
                Logins: logins
            };

            AWS.config.region = config.aws_region;

            AWS.config.credentials = new AWS.CognitoIdentityCredentials(params);

            return new Promise((resolve, reject) => {
                AWS.config.credentials.get(function (err) {
                    if (err) {
                        reject(err);
                        return;
                    }
                    resolve('success');
                });
            });
        }

        return new Promise((resolve, reject) => {
            let req = new XMLHttpRequest();
            req.open('GET', '/account/tokens', true);
            req.onreadystatechange = function () {
                if (req.readyState === 4) {
                    let tokens = JSON.parse(req.response);
                    authenticateCognitoUser(tokens)
                        .then(data => resolve(data))
                        .catch(err => reject(err));
                }
            };
            req.send();
        });
    }
};

export {CognitoFacade};