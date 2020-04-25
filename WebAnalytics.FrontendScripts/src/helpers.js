export function serializeDate(date) {
    let timezoneOffsetHours = -(date.getTimezoneOffset() / 60);
    let sign = timezoneOffsetHours >= 0 ? "+" : "-";
    let leadingZero = (Math.abs(timezoneOffsetHours) < 10) ? "0" : "";

    let correctedDate = new Date(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds(), date.getMilliseconds());
    correctedDate.setHours(date.getHours() + timezoneOffsetHours);
    let iso = correctedDate.toISOString().replace("Z", "");

    return iso + sign + leadingZero + Math.abs(timezoneOffsetHours).toString() + ":00";
}

export function debounce(func, wait, immediate) {
    let timeout;
    return function() {
        let context = this, args = arguments;
        let later = function() {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };
        let callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        if (callNow) func.apply(context, args);
    };
};