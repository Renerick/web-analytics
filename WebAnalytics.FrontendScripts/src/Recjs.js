import Recorder from './Recorder';
import Cookies from 'js-cookie';
import {v4 as uuidv4} from 'uuid';

/*
* Main class
*/
class Recjs {

    constructor({events, fps, document, endpoint, site}) {
        const availableEvents = ['scroll', 'mousemove', 'keypress', 'click', 'contextmenu'];
        const userCookieName = "_sau";
        this.events = events || availableEvents;
        this.events.forEach(event => {
            if (!availableEvents.includes(event)) console.warn(`Unknown event '${event}'`)
        });
        this.document = document || window.document;
        this.fps = fps || 30;

        this.endpoint = endpoint;
        this.site = site;
        this.userId = this.getUserCookie(userCookieName);

        this.recorder = new Recorder(this.document, this.events, this.fps);
    }

    getUserCookie(cookieName) {
        var result = Cookies.get(cookieName);
        if (!result) {
            result = uuidv4();
            Cookies.set(cookieName, result);
        }
        return result;
    }

    urlEncode(object, prefix) {
        let request = [];

        for (var prop in object) {
            if (!object.hasOwnProperty(prop)) continue;

            var key = prefix ? prefix + "[" + encodeURIComponent(prop) + "]" : encodeURIComponent(prop);
            var value;
            if (typeof object[prop] === "object")
                request.push(this.urlEncode(object[prop], key));
            else {
                value = encodeURIComponent(object[prop]);
                request.push(key + "=" + value);
            }
        }

        request = request.join("&");
        return request;
    }

    send() {
        let request = JSON.stringify(this.recorder.getData(), (key, value) => {
            if (value !== null) return value
        });
        navigator.sendBeacon(this.endpoint + "/track/rec" + "?v=" + this.userId + "&s=" + this.site + "&g=" + window.location.href, request);
    }
}

export default Recjs
