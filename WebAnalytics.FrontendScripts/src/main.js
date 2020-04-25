import Cookies from "js-cookie";
import {VISITOR_COOKIE_NAME} from "./constants";
import uuid from "uuid-random";
import {trackAction} from "./client";
import {trackerData} from "./data";
import {debounce} from "./helpers";

function initializeTracking(options) {
    trackerData.visitorId = Cookies.get(VISITOR_COOKIE_NAME);
    trackerData.siteId = options.siteId;
    if (!trackerData.visitorId) {
        trackerData.visitorId = uuid();
        Cookies.set(VISITOR_COOKIE_NAME, visitorId, {expires: 1000});
    }

    document.addEventListener("click", (e) => {
            console.log(e.target.);
            trackAction({
                type: "ui/click",
                data: {
                    x: e.pageX,
                    y: e.pageY,
                    button: e.button
                }
            });
        }
    );

    document.addEventListener("scroll", debounce(() => trackAction({
            type: "ui/scroll",
            data: {
                scrollX: window.scrollX,
                scrollY: window.scrollY
            }
        }),
        300
        )
    )
}

function trackView() {
    // let params = window.location.search;
    // let utm;
    // if (params)
    //     utm = (new Utm(params)).get();

    let action = {
        type: "view",
    };
    trackAction(action);
}

initializeTracking({
    siteId: "E788C371-03A0-49A0-8B79-97BC48B84A88"
});

trackView();