const Utils = {
    SHA256: (message, callback) => {

        const msgBuffer = Buffer.from(message, 'utf-8');

        crypto.subtle.digest('SHA-256', msgBuffer).then(function (hashBuffer) {

            const hashArray = Array.from(new Uint8Array(hashBuffer));
            const hashHex = hashArray.map(b => ('00' + b.toString(16)).slice(-2)).join('');
            callback(hashHex);
        });
    },
    createGUID: _ => {

        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        }

        return `${s4()}${s4()}-${s4()}-${s4()}-${s4()}-${s4()}${s4()}${s4()}`;
    },
    getMonth: (monthNumber, locale) => {

        let objDate = new Date();
        objDate.setMonth(monthNumber - 1);

        return objDate.toLocaleString(locale.trim(), {month: "long"});
    },
    isValidDecimalInput: (event) => {
        // TODO: detect key pressed without using key codes (they're being deprecated).
        if (event.ctrlKey)
            return true;

        let invalidInputs = [".", "-."];

        let candidate = event.target.value.trim() + event.key;

        if (candidate.indexOf('-') > 0) return false;

        if (!!invalidInputs.find(c => c === candidate)) return false;

        let validKeys = [8, 9, 16, 18, 37, 38, 39, 40, 46];
        if (validKeys.includes(event.keyCode)) return true;

        if (!event.key.match(RegExp("[\\d|\.|-]", 'g'))) return false;

        let dotsCount = event.target.value.match(RegExp("\\.", 'g'));
        if (dotsCount && dotsCount.length <= 1 && event.key === ".") return false;

        let minusCount = event.target.value.match(RegExp("\-", 'g'));
        return !(minusCount && minusCount.length <= 1 && event.key === "-");
    },
    isValidIntegerInput: (event) => {
        // TODO: detect key pressed without using key codes (they're being deprecated).
        let validKeys = [8, 9, 16, 18, 37, 38, 39, 40, 46];
        if (validKeys.includes(event.keyCode)) return true;

        return event.key.match(RegExp("[\\d]", 'g'));
    },
    blockPasteActionIfNotDecimalValue(event) {
        let decimalPattern = /^-?\d+(\.?\d+)?$/;

        let pastedText =
            (event.clipboardData || window.clipboardData)
                .getData('text');

        if (!pastedText) return;

        if (!decimalPattern.test(pastedText)) {
            event.preventDefault();
        } else if (decimalPattern.test(pastedText)) {
            event.target.value = '';
        }
    },
    disablePasteEvent(event) {
        if (event.type === "paste")
            event.preventDefault();
    },
    existsPathToPropertyInObject(object, propertiesPathArray) {

        if (!propertiesPathArray)
            return false;

        if ((object === null || object === undefined) && propertiesPathArray.length > 0) {
            return false;
        }

        if (propertiesPathArray.length === 0)
            return true;

        let currentPropertyToCheck = propertiesPathArray[0];

        let propertyFoundInObject =
            Object.keys(object).includes(currentPropertyToCheck);

        if (!propertyFoundInObject)
            return false;

        propertiesPathArray.shift();

        return this.existsPathToPropertyInObject(
            object[currentPropertyToCheck],
            propertiesPathArray);
    },
    removeAllLocalKeysByType(keyType, documentId) {
        let keysToDelete = [];
        for (var key in window.localStorage) {

            let keyObject = {};
            try {
                keyObject = JSON.parse(key);

            } catch (e) {
                continue;
            }
            if (keyObject.hasOwnProperty("type") && keyObject.type === keyType) {
                let recognizedInvoice = JSON.parse(localStorage.getItem(key));
                if (documentId === recognizedInvoice.id) {
                    keysToDelete.push(key);
                }
            }
        }

        keysToDelete.forEach((key) => localStorage.removeItem(key));
    }
};

export {Utils};