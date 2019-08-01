import {Utils} from "../utils/Utils";
import {EventsBus} from "./EventsBus";

class EventsPublisher {

    static navigateToField(location) {
        if (!location) {
            EventsBus.$emit('navigateToField', '');
            return;
        }
        let key = JSON.stringify(location);
        Utils.SHA256(key, (hashedId) => EventsBus.$emit('navigateToField', hashedId));
    }

    static broadcast(eventName, eventArgument) {
        if (!eventArgument.source) {
            throw new Error("source required [eventArgument.source]");
        }
        EventsBus.$emit(eventName, eventArgument);
    }
}

export {EventsPublisher};