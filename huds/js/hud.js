const HudEvents = {
    SHOW_SPEEDOMETER: "show_speedometer",
    HIDE_SPEEDOMETER: "hide_speedometer",
}

class Hud {
    constructor() {
        this.pixiApp = null;
        this.speedometer = null;


        this.initEventListeners();
        this.initPixi();
    }

    initEventListeners() {
        window.addEventListener("message", (event) => this.onNuiMessage(event))
    }

    initPixi() {
        const {
            clientHeight,
            clientWidth,
        } = document.documentElement;

        this.pixiApp = new PIXI.Application({width: clientWidth, height: clientHeight, transparent: true});
        document.querySelector("#hud").appendChild(this.pixiApp.view);

        this.speedometer = new Speedometer(this.pixiApp.stage, this.pixiApp.renderer);

        let rpm = 0;
        let incremet = 20;
        this.pixiApp.ticker.add((delta) => {
            rpm += incremet * delta;
            this.speedometer.setRpm(rpm);
            if (rpm >= 9000 || rpm <= 0) {
                incremet *= -1;
            }
        });
    }


    onNuiMessage(event) {
        const { data } = event;
        
        switch(data.type) {
            case HudEvents.SHOW_SPEEDOMETER:
                this.speedometer.visible(true);
                break;
            case HudEvents.HIDE_SPEEDOMETER:
                this.speedometer.visible(false);
                break;
        }
    }

}

var hud = new Hud();
