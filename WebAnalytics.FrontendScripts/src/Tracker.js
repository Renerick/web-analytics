import Recorder from './Recorder';
import Cookies from 'js-cookie';
import {v4 as uuidv4} from 'uuid';

/*
* Main class
*/
class Tracker {

    constructor({events, fps, document, endpoint, site}) {
        const availableEvents = ['scroll', 'mousemove', 'keypress', 'click', 'contextmenu'];
        this.events = events || availableEvents;
        this.events.forEach(event => {
            if (!availableEvents.includes(event)) console.warn(`Unknown event '${event}'`)
        });
        this.document = document || window.document;
        this.fps = fps || 30;

        this.endpoint = endpoint;
        this.site = site;
        this.recorder = new Recorder(this.document, this.events, this.fps);

        this._init();
    }

    getUserCookie(createIfNotExists) {
        const userCookieName = "_sau";

        var result = Cookies.get(userCookieName);
        if (!result && createIfNotExists) {
            result = uuidv4();
            Cookies.set(userCookieName, result);
        }
        return result;
    }

    urlEncode(object, prefix) {
        let request = [];

        for (let prop in object) {
            if (!object.hasOwnProperty(prop)) continue;

            const key = prefix ? prefix + "[" + encodeURIComponent(prop) + "]" : encodeURIComponent(prop);
            let value;
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

    sendRecording() {
        const userId = this.getUserCookie(false);

        if (!userId) return;

        let request = JSON.stringify(this.recorder.getData(), (key, value) => {
            if (value !== null) return value
        });
        let params = {
            v: userId,
            s: this.site,
            g: window.location.href
        }
        navigator.sendBeacon(this.endpoint + "/track/rec?" + this.urlEncode(params), request);
    }

    ping() {
        let data = {
            t: 'view',
            s: this.site,
            v: this.getUserCookie(),
            g: window.location.href,

            u: navigator.userAgent
        }
        navigator.sendBeacon(this.endpoint + "/track/event?" + this.urlEncode(data), "");
    }

    _init() {
        if (window._inPlayer) {
            console.log("In a player, disabling tracker")
            return;
        }

        this.getUserCookie(true);

        if (!this.getUserCookie()) return;

        window.addEventListener("beforeunload", () => {
            this.recorder.stop();
            this.sendRecording()
        });

        this.ping()

        this.recorder.record({});
    }
}

export default Tracker
