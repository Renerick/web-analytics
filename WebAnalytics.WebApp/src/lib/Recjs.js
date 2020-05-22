import Player from './Player'

class Recjs {

    constructor({events, fps, frame}) {
        const availableEvents = ['scroll', 'mousemove', 'keypress', 'click', 'contextmenu']
        this.events = events || availableEvents
        this.events.forEach(event => {
            if (!availableEvents.includes(event)) console.warn(`Unknown event '${event}'`)
        })
        this.frame = frame
        this.fps = fps || 30

        this.player = new Player(this.frame)
    }
}

export default Recjs
