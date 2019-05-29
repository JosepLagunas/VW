import {EventsBus} from "./EventsBus";

class EventsSubscriber {
    static subscribeTo(eventName, callback) {
        EventsSubscriber.tryValidateCallback(callback);
        EventsSubscriber.tryValidateEventName(eventName);
        EventsBus.$on(eventName, callback);
    }

    static unsubscribeFrom(eventName, callback) {
        EventsSubscriber.tryValidateCallback(callback);
        EventsSubscriber.tryValidateEventName(eventName);
        EventsBus.$off(eventName, callback);
    }

    static unsubscribeAllFrom(eventName) {
        EventsSubscriber.tryValidateEventName(eventName);
        EventsBus.$off(eventName);
    }

    static tryValidateCallback(callback) {
        if (!callback) {
            throw new Error("callback instance to be detached not optional," +
                " use unsubscribeAllOnEvent for unsubscribe all listeners");
        }
    }

    static tryValidateEventName(eventName) {
        if (!eventName || eventName.trim() === '') {
            throw new Error("illegal event name, must be a string.");
        }
    }
}

export {EventsSubscriber};