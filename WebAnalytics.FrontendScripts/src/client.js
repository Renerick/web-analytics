import {trackerData} from "./data";
import {serializeDate} from "./helpers";

function urlEncode(object, prefix) {
    let request = [];

    for (var prop in object) {
        if (!object.hasOwnProperty(prop)) continue;

        var key = prefix ? prefix + "[" + encodeURIComponent(prop) + "]" : encodeURIComponent(prop);
        var value;
        if (typeof object[prop] === "object")
            request.push(urlEncode(object[prop], key));
        else {
            value = encodeURIComponent(object[prop]);
            request.push(key + "=" + value);
        }
    }

    request = request.join("&");
    return request;
}

function send(endpoint, object) {
    var request = urlEncode(object);

    fetch(
        'https://localhost:5001/' + endpoint,
        {
            mode: "no-cors",
            method: 'POST',
            body: request,
            headers: {
                'Content-Type': "application/x-www-form-urlencoded;charset=UTF-8"
            }
        }
    )
}

export function trackAction(action) {

    action = {...action,
        visitorId: trackerData.visitorId,
        siteId: trackerData.siteId,
        name: action.name ? action.name : document.title,
        time: serializeDate(new Date()),
        url: action.url ? action.url : window.location.pathname,
        referrer: document.referrer,
    };

    send("track/action", action)
}